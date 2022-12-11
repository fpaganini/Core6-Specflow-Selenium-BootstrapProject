namespace SpecflowProject.Extensions;

public class EntaoAttribute : ThenAttribute
{
    public EntaoAttribute() : base() { }

    public EntaoAttribute(string regex)
        : base(regex) { }

    public EntaoAttribute(string regex, string culture)
        : base(regex, culture) { }
}
