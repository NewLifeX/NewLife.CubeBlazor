using Masa.Blazor;
using Microsoft.AspNetCore.Components;

namespace NewLife.CubeBlazor.Razor.Shared
{
  public class STooltip : MTooltip
  {
    public override async Task SetParametersAsync(ParameterView parameters)
    {
      Color = "emphasis2";
      Right = true;
      ContentClass = "rounded-2";
      ContentStyle = "border: 1px solid #E2E7F4;";

      await base.SetParametersAsync(parameters);
    }
  }
}
