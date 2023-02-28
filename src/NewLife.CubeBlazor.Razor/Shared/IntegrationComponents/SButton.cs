using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace NewLife.CubeBlazor.Razor.Shared
{
  public class SButton : SAutoLoadingButton
  {
    [Parameter]
    public bool Medium { get; set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
      Color = "primary";
      await base.SetParametersAsync(parameters);
    }

    protected override void SetComponentClass()
    {
      base.SetComponentClass();

      CssProvider.Merge(delegate (CssBuilder cssBuilder)
      {
        cssBuilder.Add("btn");
        cssBuilder.AddFirstIf(("large-button", () => Large), ("medium-button", () => Medium), ("small-button", () => Small));
      });
    }
  }
}
