using Mapster;
using RazorSample.Data;
using RazorSample.Data.Entities;
using RazorSample.Data.Specifications;
using RazorSample.Web.Commands;
using RazorSample.Web.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorSample.Web.Services
{
  public interface INotificationService :
    IQueryHandler<GetOpenNotificationsQuery, IEnumerable<NotificationEntity>>,
    ICommandHandler<CreateNotificationCommand>,
    ICommandHandler<CloseNotificationCommand>
  { }

  public sealed class NotificationService : INotificationService
  {
    private readonly IRepository _repository;

    public NotificationService(IRepository repository)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<QueryExecutionResult<IEnumerable<NotificationEntity>>> HandleAsync(
      GetOpenNotificationsQuery query)
    {
      var notifications = await _repository.PageAsync(
        new OpenNotificationsOfSubjectSpecification(query.SubjectId), 10, 0);
      var queryExecutionResult = new QueryExecutionResult<IEnumerable<NotificationEntity>>(notifications);

      return queryExecutionResult;
    }

    public async Task<CommandExecutionResult> HandleAsync(CreateNotificationCommand command)
    {
      var notificationEntity = command.Adapt<NotificationEntity>();

      await _repository.InsertAsync(notificationEntity);

      return CommandExecutionResult.Success;
    }

    public async Task<CommandExecutionResult> HandleAsync(CloseNotificationCommand command)
    {
      var changeEntry = new ChangeEntry<NotificationEntity>().Key(notification => notification.NotificationId, command.NotificationId)
                                                             .Property(notification => notification.Closed, DateTime.UtcNow);

      await _repository.UpdateAsync(changeEntry.Entity, changeEntry.Properties);

      return CommandExecutionResult.Success;
    }
  }
}
