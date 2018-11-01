namespace RazorSample.Hr
{
  public sealed class Property
  {
    public Property(string name, string displayName, object value)
    {
      Name = name;
      DisplayName = displayName;
      Value = value;
    }

    public Property(string name, string displayName, object value, string displayValue)
      : this(name, displayName, value)
    {
      DisplayValue = displayValue;
    }

    public string Name { get; internal set; }
    public string DisplayName { get; internal set; }

    public object Value { get; internal set; }
    public string DisplayValue { get; internal set; }
  }
}
