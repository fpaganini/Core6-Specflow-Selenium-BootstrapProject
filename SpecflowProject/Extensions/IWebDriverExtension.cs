using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowProject.Extensions;

public static class IWebDriverExtension
{
    public static WebDriverWait Wait(this IWebDriver original, double totalSeconds)
        => new WebDriverWait(original, TimeSpan.FromSeconds(totalSeconds));
}
