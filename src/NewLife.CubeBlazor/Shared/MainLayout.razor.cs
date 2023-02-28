namespace NewLife.CubeBlazor.Shared
{
  public partial class MainLayout
  {
    List<string> _whiteUris = new();

    public MainLayout()
    {
      _whiteUris = new List<string> { "http://www.qq.com" };
    }

    private bool SignOut()
    {
      return false;
    }
  }
}
