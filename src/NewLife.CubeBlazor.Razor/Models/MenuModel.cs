namespace NewLife.CubeBlazor.Razor.Models
{
  public class MenuModel
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public string Icon { get; set; }

    public string Url { get; set; }

    public List<MenuModel> Children { get; set; } = new List<MenuModel>();

  }
}
