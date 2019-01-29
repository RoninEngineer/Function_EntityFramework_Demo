
using Function_EntityFramework_Demo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;




namespace Function_EntityFramework.Tests
{
    public class GetAllClientsTests
    {
        private readonly ILogger logger = NullLoggerFactory.Instance.CreateLogger("GetAllClients");

        [Fact]
        public async void GetAllClients_Success()
        {
            var dbContextMock = new Mock<ClientsContext>();
            

            var request = GenerateHttpRequest();
            var response = await GetAllClients.Run(request, logger);

            Assert.Equal(200, ((ObjectResult)response).StatusCode);
        }

        private DefaultHttpRequest GenerateHttpRequest()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());
            return request;
        }
    }
}
