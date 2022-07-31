using Newtonsoft.Json.Serialization;

namespace GoRest.Api.Client.Client.Realization
{
    public class LowerCaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return name.ToLower();
        }
    }
}
