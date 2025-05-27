using  Microsoft.AspNetCore.Mvc;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Net.Http; 
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMS.DATA.Model;
using TMS.Domain;

namespace TMS.DATA.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    { 
        [HttpPost("ImportMails")] 
        public async Task<IActionResult> ImportMails()
        {
            string[] scopes = new[] { "Mail.Read" }; 
            // Use PublicClientApplicationBuilder for interactive authentication (no client secret)
            var app = PublicClientApplicationBuilder.Create(Tsecret.ClientIdProd)
                .WithAuthority(AzureCloudInstance.AzurePublic, Tsecret.TenantIdProd)
                .WithDefaultRedirectUri()  // Use default redirect URI (http://localhost)
                .Build();

            try
            {
                // Trigger interactive authentication
                var result = await app.AcquireTokenInteractive(scopes)
                                      .ExecuteAsync();

                // Access token received after successful login
                string accessToken = result.AccessToken;
                // Now you can use the token to call APIs (e.g., Microsoft Graph)
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
               var response = await httpClient.GetAsync(Tsecret.UrlProd);


                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error retrieving mails: {response.StatusCode} - {error}");
                }

                var json = await response.Content.ReadAsStringAsync();
                RootJson emailData = JsonConvert.DeserializeObject<RootJson>(json);

                return Ok(new { message = json });

            }
            catch (MsalUiRequiredException msalEx)
            {
                // This exception is thrown when the user cancels the authentication or closes the browser
                Console.WriteLine($"Authentication UI required but was cancelled: {msalEx.Message}");
                return BadRequest(new { Message = "Authentication was cancelled or the user closed the browser." });
            }
            catch (MsalServiceException msalServiceEx)
            {
                // This exception is thrown if there's an issue with the MSAL service
                Console.WriteLine($"Authentication failed due to service error: {msalServiceEx.Message}");
                return BadRequest(new { Message = "Authentication failed due to service error." });
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine($"General error: {ex.Message}");
                return BadRequest(new { Message = $"Authentication failed: {ex.Message}" });
            }
        }
 

    }

}
       
 

 
