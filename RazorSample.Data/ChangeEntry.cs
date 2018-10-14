using System.Collections.Generic;

namespace RazorSample.Data
{
  public sealed class ChangeEntry<TEntity> where TEntity : class, new()
  {
    private readonly ISet<string> _properties;

    public ChangeEntry()
    {
      Entity = new TEntity();
      _properties = new HashSet<string>();
    }

    public TEntity Entity { get; }
    public IEnumerable<string> Properties => _properties;

    public void Property(string propertyName) => _properties.Add(propertyName);
  }
}
