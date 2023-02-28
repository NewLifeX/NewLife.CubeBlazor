using BlazorComponent;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;

namespace NewLife.CubeBlazor.Razor.Shared
{
  public class SListItem : MListItem
  {
    [Parameter]
    public bool Medium { get; set; }

    protected override void SetComponentClass()
    {
      base.SetComponentClass();

      CssProvider.Merge(css => { css.AddIf("m-list-item--medium", () => Medium); });
    }
  }
}
