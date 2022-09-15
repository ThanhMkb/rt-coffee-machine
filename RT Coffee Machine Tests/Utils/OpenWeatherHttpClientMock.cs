using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using RT_Coffee_Machine.Client;
using System;
using System.Collections.Generic;
using System.Net;

namespace RT.Coffe.Api.Test.Utils
{
    public class OpenWeatherHttpClientMock
    {
        public static OpenWeatherHttpClient FakeOWHttpClient(string temperature)
        {
            var response = new
            {
                main = new
                {
                    temp = temperature
                }
            };
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, 
                    Content = new StringContent(JsonConvert.SerializeObject(response)) });

            var client = new HttpClient(mockHttpMessageHandler.Object);

            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var config = configBuilder.Build();
            return new OpenWeatherHttpClient(client, config);
        }
    }
}
