[![.NET](https://github.com/fpaganini/Core6-Specflow-Selenium-BootstrapProject/actions/workflows/dotnet.yml/badge.svg)](https://github.com/fpaganini/Core6-Specflow-Selenium-BootstrapProject/actions/workflows/dotnet.yml)

# Page Object Model
![picture 4](images/387a1a69d9528f2dc5029623612b5b7cfda03beabb778e818eee9a1e2550f2a9.png)  

O modelo de Page Object é utilizado para separar os elementos que representam uma página a ser testada, aprimorando a organização e manutenção dos testes.

Para exemplo de uma página de login onde temos os três elementos:

```html
    <html>
        <input id="txtUsuario" name="userName" type="text" value="">
        <input id="txtSenha" name="userPassword" type="password" value="">
        <button id="btnLogar" type="submit">Logar</button>
    </html>
```

teremos o seguinte PageObject:

```c#
namespace project.PageObjects;

public class LoginPageObject
{
    private readonly IWebDriver _webDriver;

    public LoginPageObject(IWebDriver webDriver)
        => _webDirver = webDriver;

    public IWebElement TxtUsuario => _webDriver?.FindElement(By.Id("txtUsuario"));
    public IWebElement TxtSenha => _webDriver?.FindElement(By.Id("txtSenha"));
    public IWebElement BtnLogar => _webDriver?.FindElement(By.Id("btnLogar"));

}
```

## Cache na captura dos elementos

```c#

namespace project.PageObjects;

public class LoginPageObject
{
    private readonly IWebDriver _webDriver;
    private readonly Lazy<IWebElement> _txtUsuario;
    private readonly Lazy<IWebElement> _txtSenha;
    private readonly Lazy<IWebElement> _btnLogar;

    public LoginPageObject(IWebDriver webDriver)
    {
        _webDriver = webDriver;
        _txtUsuario = new Lazy<IWebElement>(() => _webDriver.FindElement(By.Id("txtUsuario")));
        _txtSenha = new Lazy<IWebElement>(() => _webDriver.FindElement(By.Id("txtSenha")));
        _btnLogar = new Lazy<IWebElement>(() => _webDriver.FindElement(By.Id("btnLogar")));
    }

    public IWebElement TxtUsuario => _txtUsuario.Value;
    public IWebElement TxtSenha => _txtSenha.Value;
    public IWebElement BtnLogar => _btnLogar.Value;
}

```

## Hierarquia de objetos

```html
    <html>
        <div class="grupoUsuario">
            <div class="usuario"></div>
        </div>
        <div class="usuario"></div>
    </html>
```

```c#

namespace project.PageObjects;

public class GrupoUsuarioPageObject
{
    private readonly IWebDriver _webDriver;

    public GrupoUsuarioPageObject(IWebDriver webDriver)
        => _webDirver = webDriver;

    public IWebElement DivGrupoUsuario => _webDriver.FindElement(By.ClassName("grupoUsuario"));
    public UsuarioPageObject UsuarioPageObject => new UsuarioPageObject(DivGrupoUsuario);
}

public class UsuarioPageObject
{
    private readonly IWebElement _webElement;

    public GrupoUsuarioPageObject(IWebElement webElement)
        => _webElement = webElement;

    public IWebElement DivUsuario => _webElement.FindElement(By.ClassName("usuario"));
}
```

## Benefícios do Page Object

- A responsabilidade do parser da página fica separada da lógica dos testes e da definição dos steps
- Quando ocorrer alguma mudança nas páginas, sem mudanças de processo, fica muito mais fácil a manutenção do teste por estas alterações estarem centralizadas nesta camada
- Os Bindings se tornam muito menos dependentes da estrutura do HTML


Mais informaçõpes:

[PageObjectModel - Specflow Official Docs](https://docs.specflow.org/projects/specflow/en/latest/Guides/PageObjectModel.html)



# Driver Pattern
![picture 1](images/6f95d61adf1ddab02ad878f1860e97ebf537c2af50362788e0c463a815788e56.png)  

O Driver Pattern é a criação de uma camada adicional entre as definições dos Steps e o código automatizado. Esta camada é uma boa prática para melhor organizar os Bindings e o Código de automatização.

## Benefícios do Driver Pattern

- Mais fácil de manter pois divide as responsabilidades
- Metodos ficam mais fáceis de reutilizar em diferentes definições e etapas, ou até mesmo combinar várias etapas em uma única.
- As definições das Steps ficam mais fáceis de ler, tornando muito mais acessível a leitura para pessoas que não são tecnicas, agiliza também a leitura para pessoas técnicas pois obtem de imediato o resumo dos passos realizados sem conter detalhes do desenvolvimento.
- Usa a injeção de contexto para conectar várias classes

*Antes*

![picture 2](images/9ca9307e388cb4bd3dad5039f64773bb5800fdc775862e29da9d27438647e2f5.png)  

*Depois*

![picture 3](images/a86246a56cea03f1381baaf461ff3b2ed90db923057f5964efbf018387bf2f0b.png)  

Mais informações:

[Driver Pattern - Specflow Official Docs](https://docs.specflow.org/projects/specflow/en/latest/Guides/DriverPattern.html)

