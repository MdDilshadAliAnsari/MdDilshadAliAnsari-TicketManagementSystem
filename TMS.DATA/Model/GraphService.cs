 
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

namespace TMS.DATA.Model
{
    //public class GraphService
    //{
    //    private readonly GraphServiceClient _graphClient;

    //    public GraphService(IConfiguration config)
    //    {
    //        var clientId = config["AzureAd:ClientId"];
    //        var tenantId = config["AzureAd:TenantId"];
    //        var clientSecret = config["AzureAd:ClientSecret"];

    //        var confidentialClient = ConfidentialClientApplicationBuilder
    //            .Create(clientId)
    //            .WithTenantId(tenantId)
    //            .WithClientSecret(clientSecret)
    //            .Build();

    //        //var authProvider = new DelegateAuthenticationProvider(async (requestMessage) =>
    //        //{
    //        //    var result = await confidentialClient
    //        //        .AcquireTokenForClient(new[] { "https://graph.microsoft.com/.default" })
    //        //        .ExecuteAsync();

    //        //    requestMessage.Headers.Authorization =
    //        //        new AuthenticationHeaderValue("Bearer", result.AccessToken);
    //        //});
    //        var authProvider = "";
    //        _graphClient = new GraphServiceClient(authProvider);
    //    }

    //    public async Task<IEnumerable<Message>> GetMailsAsync(string userEmail)
    //    {
    //        var messages = await _graphClient.Users[userEmail].MailFolders.Inbox.Messages
    //            .Request()
    //            .Top(20)
    //            .Select(m => new { m.Subject, m.Body, m.Sender, m.ReceivedDateTime })
    //            .GetAsync();

    //        return messages.CurrentPage;
    //    }


    //}
}
