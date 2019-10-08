using JwtAuthentication.ViewModels;
using Swashbuckle.AspNetCore.Filters;

namespace ExampleRESTfulApi.Swagger.RequestExamples
{
    public class LoginModelExample : IExamplesProvider<LoginModel>
    {
        public LoginModel GetExamples()
        {
            return new LoginModel()
            {
                Username = "admin",
                Password = "111111"
            };
        }
    }
}
