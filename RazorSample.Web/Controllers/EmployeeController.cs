using Mapster;
using Microsoft.AspNetCore.Mvc;
using RazorSample.Data.Entities;
using RazorSample.Hr;
using RazorSample.Random;
using RazorSample.Vm;
using RazorSample.Web.Commands;
using RazorSample.Web.Extensions;
using RazorSample.Web.Queries;
using RazorSample.Web.Services;
using System;
using System.Threading.Tasks;

namespace RazorSample.Web.Controllers
{
  public sealed class EmployeeController : Controller
  {
    private readonly IResourceBuilder _builder;
    private readonly IEmployeeService _employeeService;
    private readonly IRandomGenerator _randomGenerator;

    public EmployeeController(IResourceBuilder builder, IEmployeeService employeeService, IRandomGenerator randomGenerator)
    {
      _builder = builder ?? throw new ArgumentNullException(nameof(builder));
      _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
      _randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator));
    }

    [HttpGet]
    public async Task<IActionResult> Index(SearchEmployeeQuery query)
    {
      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Action, "+ new employee", nameof(Add), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Self, "Employees", nameof(Index), nameof(EmployeeController)));

      var employees = await _employeeService.HandleAsync(query);

      if (employees.HasError == false)
      {
        if (employees.Result.PageNo > employees.Result.FirstPageNo)
        {
          _builder.Link(Url.AppLink(RelTypes.First, "First", nameof(Index), nameof(EmployeeController), new SearchEmployeeQuery(query.EmployeeNo, employees.Result.FirstPageNo)))
                  .Link(Url.AppLink(RelTypes.Prev, "Previous", nameof(Index), nameof(EmployeeController), new SearchEmployeeQuery(query.EmployeeNo, employees.Result.PageNo - 1)));
        }

        if (employees.Result.PageNo < employees.Result.LastPageNo)
        {
          _builder.Link(Url.AppLink(RelTypes.Next, "Next", nameof(Index), nameof(EmployeeController), new SearchEmployeeQuery(query.EmployeeNo, employees.Result.PageNo + 1)))
                  .Link(Url.AppLink(RelTypes.Last, "Last", nameof(Index), nameof(EmployeeController), new SearchEmployeeQuery(query.EmployeeNo, employees.Result.LastPageNo)));
        }

        foreach (var employee in employees.Result)
        {
          _builder.Embedded(RelTypes.Row)
                  .Property("name", "Name", employee.FullName)
                  .Property(nameof(employee.EmployeeNo), "Employee No", employee.EmployeeNo)
                  .Property(nameof(employee.Created), "Created", employee.Created)
                  .Link(Url.AppLink(RelTypes.Self, "Name", nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(employee.EmployeeId)))
                  .Link(Url.AppLink(RelTypes.Action, "+ new client", nameof(ClientController.Add), nameof(ClientController), new CreateClientQuery(employee.EmployeeId)));
        }
      }

      var vm = _builder.Build()
                       .ToGridVm();

      return View("GridView", vm);
    }

    [HttpGet]
    public IActionResult Add()
    {
      var command = _randomGenerator.RandomEmployee();
      var vm = BuildAddFormVm(command);

      return View("FormView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateEmployeeCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = BuildAddFormVm(command);

        return View("FormView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Index), new SearchEmployeeQuery(command.EmployeeNo));
    }

    [HttpGet]
    public async Task<IActionResult> GeneralInfo(UpdateEmployeeGeneralInfoQuery query)
    {
      var employeeEntity = await _employeeService.HandleAsync(query);
      var vm = BuildGeneralInfoForm(employeeEntity.Result);

      return View("FormView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> GeneralInfo(UpdateEmployeeGeneralInfoQuery query, UpdateEmployeeGeneralInfoCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var employeeEntity = command.Adapt<EmployeeEntity>();
        var vm = BuildGeneralInfoForm(employeeEntity);

        return View("FormView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Index), new SearchEmployeeQuery(command.EmployeeNo));
    }

    [HttpGet]
    public async Task<IActionResult> Addresses(UpdateEmployeeAddressesQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);

      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Breadcrumb, queryExecutionResult.Result.FullName, nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Addresses", nameof(Addresses), nameof(EmployeeController), query))
              .Link(Url.AppLink(RelTypes.Tab, "General Info", nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Addresses", nameof(Addresses), nameof(EmployeeController), new UpdateEmployeeAddressesQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Emails", nameof(Emails), nameof(EmployeeController), new UpdateEmployeeEmailsQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Phones", nameof(Phones), nameof(EmployeeController), new UpdateEmployeePhonesQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "IM", nameof(Ims), nameof(EmployeeController), new UpdateEmployeeImsQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Self, "Addresses", nameof(Addresses), nameof(EmployeeController), query))
              .Link(Url.AppLink(RelTypes.Action, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)));

      if (queryExecutionResult.Result.Emails != null)
      {
        foreach (var address in queryExecutionResult.Result.Addresses)
        {
          _builder.Property("Addresses", "Addresses", address.Address);
        }
      }

      var vm = _builder.Build()
                       .ToFormVm();

      return View("FormView", vm);
    }

    [HttpGet]
    public async Task<IActionResult> Emails(UpdateEmployeeEmailsQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);

      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Breadcrumb, queryExecutionResult.Result.FullName, nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Emails", nameof(Emails), nameof(EmployeeController), query))
              .Link(Url.AppLink(RelTypes.Tab, "General Info", nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Addresses", nameof(Addresses), nameof(EmployeeController), new UpdateEmployeeAddressesQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Emails", nameof(Emails), nameof(EmployeeController), new UpdateEmployeeEmailsQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Phones", nameof(Phones), nameof(EmployeeController), new UpdateEmployeePhonesQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "IM", nameof(Ims), nameof(EmployeeController), new UpdateEmployeeImsQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Self, "Emails", nameof(Emails), nameof(EmployeeController), query))
              .Link(Url.AppLink(RelTypes.Action, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)));

      if (queryExecutionResult.Result.Emails != null)
      {
        foreach (var email in queryExecutionResult.Result.Emails)
        {
          _builder.Property("Emails", "Emails", email.Email);
        }
      }

      var vm = _builder.Build()
                       .ToFormVm();

      return View("FormView", vm);
    }

    [HttpGet]
    public async Task<IActionResult> Phones(UpdateEmployeePhonesQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);

      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Breadcrumb, queryExecutionResult.Result.FullName, nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Phones", nameof(Phones), nameof(EmployeeController), query))
              .Link(Url.AppLink(RelTypes.Tab, "General Info", nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Addresses", nameof(Addresses), nameof(EmployeeController), new UpdateEmployeeAddressesQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Emails", nameof(Emails), nameof(EmployeeController), new UpdateEmployeeEmailsQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Phones", nameof(Phones), nameof(EmployeeController), new UpdateEmployeePhonesQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "IM", nameof(Ims), nameof(EmployeeController), new UpdateEmployeeImsQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Self, "Phones", nameof(Phones), nameof(EmployeeController), query))
              .Link(Url.AppLink(RelTypes.Action, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)));

      if (queryExecutionResult.Result.Emails != null)
      {
        foreach (var phone in queryExecutionResult.Result.Phones)
        {
          _builder.Property("Phones", "Phones", phone.Phone);
        }
      }

      var vm = _builder.Build()
                       .ToFormVm();

      return View("FormView", vm);
    }

    [HttpGet]
    public async Task<IActionResult> Ims(UpdateEmployeeImsQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);

      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Breadcrumb, queryExecutionResult.Result.FullName, nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "IM", nameof(Ims), nameof(EmployeeController), query))
              .Link(Url.AppLink(RelTypes.Tab, "General Info", nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Addresses", nameof(Addresses), nameof(EmployeeController), new UpdateEmployeeAddressesQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Emails", nameof(Emails), nameof(EmployeeController), new UpdateEmployeeEmailsQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "Phones", nameof(Phones), nameof(EmployeeController), new UpdateEmployeePhonesQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Tab, "IM", nameof(Ims), nameof(EmployeeController), new UpdateEmployeeImsQuery(query.EmployeeId)))
              .Link(Url.AppLink(RelTypes.Self, "IM", nameof(Ims), nameof(EmployeeController), query))
              .Link(Url.AppLink(RelTypes.Action, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)));

      if (queryExecutionResult.Result.Emails != null)
      {
        foreach (var im in queryExecutionResult.Result.Ims)
        {
          _builder.Property("Ims", "IM", im.Im);
        }
      }

      var vm = _builder.Build()
                       .ToFormVm();

      return View("FormView", vm);
    }

    private IFormVm BuildAddFormVm(CreateEmployeeCommand command) =>
      BuildFormBase(command).Property(nameof(command.Email), "Email", command.Email)
                            .Link(Url.AppLink(RelTypes.Self, "Employees", nameof(Index), nameof(EmployeeController)))
                            .Link(Url.AppLink(RelTypes.Breadcrumb, "New Employee", nameof(Add), nameof(EmployeeController)))
                            .Build()
                            .ToFormVm();

    private IResourceBuilder BuildFormBase(EmployeeFormCommandBase command) =>
      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)))
              .Property(nameof(command.FirstName), "First Name", command.FirstName)
              .Property(nameof(command.LastName), "Last Name", command.LastName)
              .Property(nameof(command.EmployeeNo), "Employee No", command.EmployeeNo);

    private IResourceBuilder BuildPageBase() =>
      _builder.Link(Url.AppLink(RelTypes.Nav, "Employees", nameof(Index), nameof(EmployeeController)))
              .Link(Url.AppLink(RelTypes.Nav, "Client", nameof(ClientController.Index), nameof(ClientController)))
              .Link(Url.AppLink(RelTypes.Breadcrumb, "Employees", nameof(Index), nameof(EmployeeController)));

    private IResourceBuilder BuildEditBase(EmployeeEntity employeeEntity) =>
      BuildPageBase().Link(Url.AppLink(RelTypes.Breadcrumb, employeeEntity.FullName, nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(employeeEntity.EmployeeId)))
                     .Link(Url.AppLink(RelTypes.Action, "+ new employee", nameof(EmployeeController.Add), nameof(EmployeeController)))
                     .Link(Url.AppLink(RelTypes.Tab, "General Info", nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(employeeEntity.EmployeeId)))
                     .Link(Url.AppLink(RelTypes.Tab, "Addresses", nameof(Addresses), nameof(EmployeeController), new UpdateEmployeeAddressesQuery(employeeEntity.EmployeeId)))
                     .Link(Url.AppLink(RelTypes.Tab, "Emails", nameof(Emails), nameof(EmployeeController), new UpdateEmployeeEmailsQuery(employeeEntity.EmployeeId)))
                     .Link(Url.AppLink(RelTypes.Tab, "Phones", nameof(Phones), nameof(EmployeeController), new UpdateEmployeePhonesQuery(employeeEntity.EmployeeId)))
                     .Link(Url.AppLink(RelTypes.Tab, "IM", nameof(Ims), nameof(EmployeeController), new UpdateEmployeeImsQuery(employeeEntity.EmployeeId)));

    private IFormVm BuildGeneralInfoForm(EmployeeEntity employeeEntity) =>
      BuildEditBase(employeeEntity).Link(Url.AppLink(RelTypes.Self, "Employees", nameof(GeneralInfo), nameof(EmployeeController), new UpdateEmployeeGeneralInfoQuery(employeeEntity.EmployeeId)))
                                   .Property(nameof(UpdateEmployeeGeneralInfoCommand.FirstName), "First Name", employeeEntity.FirstName)
                                   .Property(nameof(UpdateEmployeeGeneralInfoCommand.LastName), "Last Name", employeeEntity.LastName)
                                   .Property(nameof(UpdateEmployeeGeneralInfoCommand.EmployeeNo), "Employee No", employeeEntity.EmployeeNo)
                                   .Build()
                                   .ToFormVm();
  }
}
