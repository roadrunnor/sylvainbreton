using IdentityServer4.Models;
using IdentityServer4.Test;

namespace api_sylvainbreton.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            // Define your clients here
            return new List<Client>
            {
                // Example client configuration
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            // Define your identity resources here
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            // Others as needed
        };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            // Define your API resources here
            return new List<ApiResource>
            {
                // Example API resource configuration
            };
        }

        public static List<TestUser> GetUsers()
        {
            // Define your test users here (for development)
            return new List<TestUser>
        {
            new TestUser
            {
                // User definition
            }
        };
        }
    }

}
