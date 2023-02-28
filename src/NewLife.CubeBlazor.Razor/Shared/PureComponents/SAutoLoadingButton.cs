using BlazorComponent;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace NewLife.CubeBlazor.Razor.Shared
{
  public class SAutoLoadingButton : MButton
  {
    [Parameter]
    public string BorderRadiusClass { get; set; } = "rounded-pill";

    [Parameter]
    public bool DisableLoading { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
      Color = "primary";
      await base.SetParametersAsync(parameters);
    }

    protected override void OnParametersSet()
    {
      var originalOnClick = OnClick;

      if (OnClick.HasDelegate)
      {
        OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, async (args) =>
        {
          Loading = DisableLoading is false;
          Disabled = true;

          try
          {
            await originalOnClick.InvokeAsync(args);
          }
          finally
          {
            Loading = false;
            Disabled = false;
            StateHasChanged();
          }
        });
      }
    }

    protected override void SetComponentClass()
    {
      base.SetComponentClass();

      CssProvider.Merge(delegate (CssBuilder cssBuilder)
      {
        cssBuilder.AddIf(BorderRadiusClass, () =>
        {
          return !(Class?.Split(' ').Contains(BorderRadiusClass) ?? false);
        });
      });
    }
  }

}
