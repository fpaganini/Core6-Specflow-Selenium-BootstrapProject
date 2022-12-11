namespace SpecflowProject.Extensions;

public class DadoAttribute : GivenAttribute
{
    public DadoAttribute()
        : base() { }


    public DadoAttribute(string regex)
        : base(regex) { }

    public DadoAttribute(string regex, string culture)
        : base(regex, culture) { }
}
