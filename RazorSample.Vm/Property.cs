namespace RazorSample.Vm
{
  public sealed class Property
  {
    public Property(string name, string displayName, object value)
    {
      Name = name;
      DisplayName = displayName;
      Value = value;
    }

    public string Name { get; internal set; }
    public string DisplayName { get; internal set; }

    public object Value { get; internal set; }
  }
}
