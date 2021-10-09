using Microsoft.IdentityModel.Tokens;
namespace ASPNetCoreMastersTodoList.Api
{
    public class JwtOptions
    {
        public SecurityKey SecurityKey { get; set; }
    }
}
