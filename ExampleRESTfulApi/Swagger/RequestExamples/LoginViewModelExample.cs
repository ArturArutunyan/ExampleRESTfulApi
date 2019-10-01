using JwtAuthentication.ViewModels;
using Swashbuckle.AspNetCore.Filters;

namespace ExampleRESTfulApi.Swagger.RequestExamples
{
    public class LoginViewModelExample : IExamplesProvider<LoginViewModel>
    {
        public LoginViewModel GetExamples()
        {
            return new LoginViewModel()
            {
                Username = "admin",
                Password = "111111"
            };
        }
    }
}
