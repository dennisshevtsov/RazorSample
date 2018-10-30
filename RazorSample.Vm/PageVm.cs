using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorSample.Vm
{
    public abstract class VmBase : IPageVm
    {
        protected readonly IResource _resource;

        protected VmBase(IResource resource)
        {
            _resource = resource ?? throw new ArgumentNullException(nameof(resource));
        }

        public string Title { get; set; }

        public IEnumerable<Link> Navs => _resource.Links.Where(link => link.Rel == RelTypes.Nav);

        public IEnumerable<Link> Breadcrumbs => _resource.Links.Where(link => link.Rel == RelTypes.Breadcrumb);
    }

    public sealed class ListVm : VmBase, IListVm
    {
        public ListVm(IResource resource) : base(resource) { }

        public IEnumerable<Column> Columns => _resource.Embedded.FirstOrDefault(resource => resource.Key == RelTypes.List)
                                                               .Value?.Properties.Select(property => new Column(property.Name,
                                                                                                                property.DisplayName));

        public IEnumerable<IVm> Rows => _resource.Embedded.Where(resource => resource.Key == RelTypes.List)
                                                           .Select(resource => new ListItemVm(resource.Value));
    }

    public sealed class ListItemVm : IVm
    {
        private readonly IResource _resource;

        public ListItemVm(IResource resource)
        {
            _resource = resource ?? throw new ArgumentNullException(nameof(resource));
        }

        public IEnumerable<Property> Properties => _resource.Properties;

        public IEnumerable<Link> Actions => _resource.Links.Where(link => link.Rel == RelTypes.Action);

        public Link Self => _resource.Links.Single(link => link.Rel == RelTypes.Self);
    }
}
