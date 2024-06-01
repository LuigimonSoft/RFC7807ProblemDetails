using System.Resources;

namespace RFC7807ProblemDetails.Core.Exceptions
{
    public class ServiceException : BaseException
    {
        public ServiceException(int errorCode, Exception innerException, ResourceManager resourceManager, string language) : base(errorCode, innerException, resourceManager, language)
        {
        }
    }
}
