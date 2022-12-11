namespace SpecflowProject.Extensions;

public class QuandoAttribute : WhenAttribute
{
    public QuandoAttribute() : base() { }

    public QuandoAttribute(string regex)
        : base(regex) { }

    public QuandoAttribute(string regex, string culture)
        : base(regex, culture) { }
}
