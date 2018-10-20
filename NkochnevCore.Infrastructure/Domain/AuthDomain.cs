using NkochnevCore.Infrastructure.Data;

namespace NkochnevCore.Infrastructure.Domain
{
    public class AuthDomain : BaseEntity
    {
        public string PassHash { get; set; }
    }
}