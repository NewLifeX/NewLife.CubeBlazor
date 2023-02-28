using System.Runtime.Serialization;

namespace NewLife.CubeBlazor.Razor.Exceptions
{
  [Serializable]
  public class UserStatusException : UserFriendlyException
  {
    public UserStatusException(string message)
        : base(message)
    {
    }

    public UserStatusException(string errorCode, params object[] parameters)
        : base(errorCode, null, parameters)
    {
    }

    protected UserStatusException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }
  }
}
