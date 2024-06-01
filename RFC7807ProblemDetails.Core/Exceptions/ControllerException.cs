using System.Resources;

namespace RFC7807ProblemDetails.Core.Exceptions
{
    public class ControllerException : BaseException
    {
        public ControllerException(int errorCode, Exception innerException, ResourceManager resourceManager, string language) : base(errorCode, innerException, resourceManager, language)
        {
        }
    }
}
