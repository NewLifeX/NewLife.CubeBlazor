using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using NewLife.CubeBlazor.Razor.Configs;

namespace NewLife.CubeBlazor.Razor.Components
{
  public class CubeComponentBase : ComponentBase
  {
    [CascadingParameter]
    public BlazorComponent.I18n.I18n LanguageProvider
    {
      get => _languageProvider ?? throw new Exception("please inject I18n!");
      set => _languageProvider = value;
    }

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public IPopupService PopupService { get; set; } = default!;

    [Inject]
    public GlobalConfig GlobalConfig { get; set; } = null!;


    private BlazorComponent.I18n.I18n? _languageProvider;

    protected string T(string key)
    {
      return LanguageProvider.T(key);
    }


    protected string GetIsDisplayStyle(bool show)
    {
      return show ? "" : "display:none !important;";
    }
  }
}
