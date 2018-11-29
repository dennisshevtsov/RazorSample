using System;
using System.Linq;
using RazorSample.Hr;

namespace RazorSample.Vm
{
  internal sealed class NotificationVm : INotificationVm
  {
    private readonly IResource _resource;

    internal NotificationVm(IResource resource)
    {
      _resource = resource ?? throw new ArgumentNullException(nameof(resource));
    }

    private Property _property;
    private Property Property => _property ?? (_property = _resource.Properties.Single());

    public string Title => Property.Value.ToString();

    public bool IsInfo => true;

    public bool IsError => IsInfo == false;

    private Link _close;
    public Link Close => _close ?? (_close =_resource.Links.Single(link => link.Rel == RelTypes.Action));
  }
}
