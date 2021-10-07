## Skapa en ny WPF Applikation med stöd för Microsoft.Toolkit.Mvvm 

Börja med att skapa en ny tom WPF application för NET 5. I detta dokumentet utgår vi ifrån att denna heter WpfApp, tänk på att byta ut detta till ditt eget namn. Tänk på att efterhand som du lägger till kod så kan du kontrollpunkta (Ctrl-.) vissa fel så den korrigerar de usings som saknas...

### Projektfilen

Öppna din csproj-fil så du ser innehållet i den textmässigt.

På första raden ändrar du <Project Sdk="Microsoft.NET.Sdk"> till <Project Sdk="Microsoft.NET.Sdk.Worker">

Detta är för att kompilatorn ska hantera projektet lite bättre än en vanlig standard console applikation, bland annat i hur den hanterar config-filer (appsettings.json).

Lägg sedan till följande referenser:

```xml
<ItemGroup>
    <PackageReference Include="FluentValidation" Version="10.0.3" />
    <PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="10.0.3" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="5.0.0" />
    <PackageReference Include="Microsoft.Extensions.Http" Version="5.0.0" />
    <PackageReference Include="Microsoft.Toolkit.Mvvm" Version="7.0.1" />
    <PackageReference Include="Microsoft.Xaml.Behaviors.Wpf" Version="1.1.31" />
    <PackageReference Include="ScottPlot.WPF" Version="4.1.12-beta" />
</ItemGroup>
```

FluentValidation används i detta exempel för att validera värden både i kod och i XAML.

Microsoft.Extensions ger oss stöd för apphosting, dependency injection, configuration, logging och en massa annat. Allt byggt i en samling assemblies som använder välkända design patterns.

Microsoft.Toolikit.Mvvm

Microsoft.Xaml.Behaviors.Wpf används för att hantera triggers och behaviors i XAML.

ScottPlot.Wpf används som ett exempel på hur man kan implementera grafer i WPF.

Du kan nu spara och stänga csproj-filen. Glöm inte bort att titta i Nugethanteraren för din applikation om det finns nyare versioner av de paket som används i exemplet.

### Projektinnehåll

Nu när vi har satt upp de referenser som behövs kan vi fortsätta med att tillföra lite mappar och konfigurationsfiler mm

#### Appsettings

Börja med att lägga till två json-filer i rotkatalogen för WPF applikationen, döp dem till appsettings.json och appsettings.Development.json. Lägg till följande innehåll i båda två:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

Dessa konfigurationsfiler kommer vi att använda för att hantera olika konfigurationsvärden och genom att ha en som heter Development kan vi utnyttja en standard som finns i Microsoft.Extensions som ger oss en möjlighet att identifiera i vilken miljö vi exekverar applikationen. 

För att identifiera att vi kör i en specifik miljö används en miljövariabel som heter DOTNET_ENVIRONMENT, denna bör ha värdet Development när vi kör i Visual Studio. Skapa därför en mapp i projektet som heter Properties och i denna skapar du en json-fil som heter launchSettings.json. Innehållet i denna ska vara följande:

```json
{
  "profiles": {
    "WpfApp": {
      "commandName": "Project",
      "dotnetRunMessages": "true",
      "environmentVariables": {
        "DOTNET_ENVIRONMENT": "Development"
      }
    }
  }
}
```

Nu kommer automatiskt appsettings.Development.json att överrida appsettings.json när vi kör i Visual Studio. Vi kan alltså med denna miljövariabel styra så olika appsettings.{miljövariabel}.json används i olika miljöer, Production, Test etc.

#### Mappstruktur

Skapa en mappstruktur med följande mappar/namespace i projektet:

- Configurations, lägg här till en publik statisk klass ConfigurationExtensions enligt nedan
- Converters, låt denna mappen vara tom så länge
- Models, låt denna mappen vara tom så länge
- Services, lägg här till en publik statisk klass ServiceExtensions enligt nedan
- Validators, låt denna mappen vara tom så länge
- ViewModels, lägg här till en publik statisk klass ViewModelExtensions enligt nedan
- Views, lägg här till en publik statisk klass ViewExtensions enligt nedan

I de mappar som ska innehålla klasser enligt ovan så ska dessa se ut så här:

```c#
public static class ConfigurationExtensions
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services, HostBuilderContext context)
    {
        return services;
    }
}
```
```c#
public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}
```
```c#
public static class ViewModelExtensions
{
    public static IServiceCollection AddApplicationViewModels(this IServiceCollection services)
    {
        services.AddTransient<MainViewModel>();
        return services;
    }
}
```
```c#
public static class ViewExtensions
{
    public static IServiceCollection AddApplicationViews(this IServiceCollection services)
    {
        services.AddTransient<MainWindow>();
        return services;
    }
}
```

#### MainWindow

Flytta MainWindow.xaml och MainWindow.xaml.cs till mappen Views, glöm inte att ändra:

I **MainWindow.xaml**:

```x:Class="WpfApp.MainWindow"```
Ska vara
```x:Class="WpfApp.Views.MainWindow"```

```xmlns:local="clr-namespace:WpfApp"```
Ska vara
```xmlns:local="clr-namespace:WpfApp.Views"```

I **MainWindow.xaml.cs**:

```namespace WpfApp```
Ska vara
```namespace WpfApp.Views```

Detta är bara för att vi flyttade MainWindow från roten av projektet till namespacet Views

#### App

Ta bort StartupUri från App.xaml, den ska inte finnas där!

I **App.xaml.cs**:

```c#
public partial class App : Application
{
    private readonly IHost host;

    public App()
    {
        host = Host.CreateDefaultBuilder()
                 .ConfigureAppConfiguration(builder => builder.AddJsonFile("WindowsInfo.json", optional: true))
                 .ConfigureServices((context, services) => services
                     .AddApplicationConfiguration(context)
                     .AddApplicationViewModels()
                     .AddApplicationViews()
                     .AddApplicationServices())
        .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await host.StartAsync();
        
        host.Services
            .GetRequiredService<MainWindow>()
            .Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (host)
        {
            await host.StopAsync();
        }

        base.OnExit(e);
    }
}
```

Det vi gör här är att skapa en IHost med dess innehåll, den är inkapslingen av en ServiceProvider för dependency injection, diverse olika builders, factories och andra stödklasser för att hantera loggning och konfiguration bland annat.

En IHost har dels en Run metod som startar upp exekveringen inuti hosten men den har även en StartAsync respektive StopAsync för att starta IHost i bakgrunden för att på så vis kunna dra nytta av vad den har att erbjuda.

Du ska nu ha en fullt körbar applikation, en så kallad BoilerPlate, alltså den innehåller hela plattformen för att kunna bygga en MVVM-baserad applikation, den innehåller fasta positioner/platser för att definiera allt på ett bra sätt.

### Börja bygga MVVM

Det första vi ska göra är att lägga till en ViewModel för MainWindow, vi kallar den MainViewModel

