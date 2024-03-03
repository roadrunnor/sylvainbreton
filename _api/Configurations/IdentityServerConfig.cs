namespace api_sylvainbreton.Configuations
{
    using IdentityServer4.Models;
    using IdentityServer4.Test;
    using System.Security.Claims;

    public static class IdentityServerConfig
    {
        public static IEnumerable<Client> GetClients()
        {
            return
            [
                 new Client
                 {
                     ClientId = "postman",
                     AllowedGrantTypes = GrantTypes.ClientCredentials,
                     ClientSecrets = { new Secret("your_secret".Sha256()) },
                     AllowedScopes = { "api_sylvainbreton" }
                 }
            ];
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return
            [
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            ];
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return
            [
                new ApiResource("api_sylvainbreton", "Sylvain Breton API")
            ];
        }

        public static List<TestUser> GetUsers()
        {
            return
            [
                new TestUser
                {
                    SubjectId = "1",
                    Username = "testuser",
                    Password = "password",
                    Claims =
                    [
                        new Claim("name", "Test User"),
                        new Claim("email", "testuser@example.com")
                    ]
                }
            ];
        }
    }
}
