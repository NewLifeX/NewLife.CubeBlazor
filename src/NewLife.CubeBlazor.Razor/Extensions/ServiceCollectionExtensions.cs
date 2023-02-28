using BlazorComponent.I18n;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewLife.CubeBlazor.Razor.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddNewLifeComponentsForBlazor(this WebAssemblyHostBuilder builder,
       string? i18nDirectoryPath = "wwwroot/i18n")
    {
      builder.Services.AddScoped<JsInitVariables>();
      builder.Services.AddAutoInject();
      builder.Services.AddBlazoredLocalStorage();

      var masaBuilder = builder.Services.AddMasaBlazor(builder =>
    {
      builder.ConfigureTheme(theme =>
      {
        theme.Themes.Light.Primary = "#4318FF";
        theme.Themes.Light.Accent = "#4318FF";
        theme.Themes.Light.Error = "#FF5252";
        theme.Themes.Light.Success = "#00B42A";
        theme.Themes.Light.Warning = "#FF7D00";
        theme.Themes.Light.Info = "#37A7FF";
      });
    })
    .AddI18n(GetLocales().ToArray());

      if (i18nDirectoryPath is not null)
      {
        masaBuilder.AddI18nForWasmAsync(i18nDirectoryPath);
      }

      return builder.Services;
    }

    private static IEnumerable<(string cultureName, Dictionary<string, string> map)> GetLocales()
    {
      var output = new List<(string cultureName, Dictionary<string, string> map)>();
      var assembly = typeof(ServiceCollectionExtensions).Assembly;
      var availableResources = assembly.GetManifestResourceNames()
                                       .Select(s => Regex.Match(s, @"^.*Locales\.(.+)\.json"))
                                       .Where(s => s.Success && s.Groups[1].Value != "supportedCultures")
                                       .ToDictionary(s => s.Groups[1].Value, s => s.Value);
      foreach (var (cultureName, fileName) in availableResources)
      {
        using var fileStream = assembly.GetManifestResourceStream(fileName);
        if (fileStream is not null)
        {
          using var streamReader = new StreamReader(fileStream);
          var content = streamReader.ReadToEnd();
          var locale = I18nReader.Read(content);
          output.Add((cultureName, locale));
        }
      }
      return output;
    }
  }
}
