namespace NewLife.CubeBlazor.Razor.Models
{
  public class Nav : NavBase
  {
    private List<Nav>? _children;

    public string? Icon { get; set; }

    public string? ParentCode { get; set; }

    public string? Url { get; set; }

    public List<Nav> Children
    {
      get
      {
        if (_children is null)
        {
          _children = new();
        }
        return _children;
      }
      set => _children = value;
    }

    public List<Nav> Actions => Children.Where(item => item.IsAction).ToList();

    public bool IsAction { get; set; }

    public bool Disabled { get; set; }

    public bool Reversed { get; set; }

    public bool HasChildren => Children.Any() && !HasActions;

    public bool HasActions => Actions.Any();

    public Nav()
    {
      Code = "";
      Name = "";
    }

    /// <summary>
    /// Initializes a nav-action.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="name"></param>
    public Nav(string code, string name)
    {
      Code = code;
      Name = name;
    }

    public Nav(string code, string name, string? icon, string? url, string? parentCode = null)
    {
      Code = code;
      Name = name;
      Url = url;
      Icon = icon;
      ParentCode = parentCode;
    }

    public Nav(string code, string name, string parentCode, List<Nav> children)
    {
      Code = code;
      Name = name;
      ParentCode = parentCode;
      Children = children;
    }

    public bool IsActive(string url)
    {
      if (Url is null)
      {
        return false;
      }

      var tempUrl = Url;

      if (tempUrl.StartsWith("/"))
      {
        tempUrl = tempUrl[1..];
      }

      return string.Equals(tempUrl, url, StringComparison.OrdinalIgnoreCase);
    }

    internal bool Filter(string? search) => string.IsNullOrEmpty(search) ? true : Name.Contains(search, StringComparison.OrdinalIgnoreCase) || Children.Any(children => children.Filter(search));

    public override bool Equals(object? obj)
    {
      return obj is Nav nav && nav.Code == Code;
    }

    public override int GetHashCode()
    {
      return 1;
    }
  }
}
