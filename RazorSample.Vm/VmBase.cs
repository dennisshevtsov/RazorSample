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
}
