using System.Resources;

namespace RFC7807ProblemDetails.Core.Exceptions
{
    public class RepositoryException : BaseException
    {
        public RepositoryException(int errorCode, Exception innerException, ResourceManager resourceManager, string language) : base(errorCode, innerException, resourceManager, language)
        {
        }
    }
}
