using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace SpecflowProject.Extensions;

public static class ISearchContextExtension
{
    /// <summary>
    /// Busca elementos usando o padrão  CSS Selector. '#' para ID, '.' para Classe, '/' para XPath ou '' para busca por Tags
    /// </summary>
    /// <param name="by">elemento com padrão CSS Selector.
    /// #[IDSearch]
    /// .[ClassSearch]
    /// [TagSearch]
    /// Se iniciar com '/' irá fazer a busca por XPath
    public static IWebElement FindElement(this ISearchContext obj, string by)
    {
        if (by.StartsWith("."))
            return obj.FindElement(By.ClassName(by.Remove(0, 1)));
        else if (by.StartsWith("#"))
            return obj.FindElement(By.Id(by.Remove(0, 1)));
        else if (by.StartsWith("/"))
            return obj.FindElement(By.XPath(by));
        else
            return obj.FindElement(By.TagName(by.Remove(0, 1)));
    }

    /// <summary>
    /// Busca elementos usando o padrão  CSS Selector. '#' para ID, '.' para Classe, '/' para XPath ou '' para busca por Tags
    /// </summary>
    /// <param name="by">elemento com padrão CSS Selector.
    /// #[IDSearch]
    /// .[ClassSearch]
    /// [TagSearch]
    /// Se iniciar com '/' irá fazer a busca por XPath
    public static ReadOnlyCollection<IWebElement> FindElements(this ISearchContext obj, string by)
    {
        if (by.StartsWith("."))
            return obj.FindElements(By.ClassName(by.Remove(0, 1)));
        else if (by.StartsWith("#"))
            return obj.FindElements(By.Id(by.Remove(0, 1)));
        else if (by.StartsWith("/"))
            return obj.FindElements(By.XPath(by));
        else
            return obj.FindElements(By.TagName(by.Remove(0, 1)));
    }
}
