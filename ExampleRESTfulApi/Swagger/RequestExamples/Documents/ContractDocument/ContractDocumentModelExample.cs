using DAL.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ExampleRESTfulApi.Swagger.RequestExamples
{
    public class ContractDocumentModelExample : IExamplesProvider<ContractDocument>
    {
        public ContractDocument GetExamples()
        {
            return new ContractDocument()
            {
                Title = "TEST_DOCUMENT",
                DocumentName = "TEST"
            };
        }
    }
}
