using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TMS.Authentication.Model;
using TMS.Domain;
using Newtonsoft.Json;
 

namespace TMS.Authentication.Authenticate
{
    public class ProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public ProxyController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();

        }

        #region This Section For Loger Services
        [HttpPost("Loggers")]
        [Authorize]
        public async Task<IActionResult> Logger([FromBody] ExceptionLogger log)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:Loggers"];
            string jsonData = JsonConvert.SerializeObject(log); 
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PostToExternalService(productServiceUrl, content);
            return Content(result, "application/json");

        }
        #endregion
        #region This Section For Master data which is used for Dropdown binding
        [HttpGet("MSTPRIORITY")]
        public async Task<IActionResult> MSTPRIORITY( )
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:TASKSPRIORITYlIST"];            
            var result = await GetToExternalService(productServiceUrl);
            return Content(result, "application/json");

        }
        [HttpGet("TASKSTATUS")]
        public async Task<IActionResult> TASKSTATUS()
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:TASKSTATUS"];
            var result = await GetToExternalService(productServiceUrl);
            return Content(result, "application/json");

        }
        [HttpGet("TASKCATEGORY")]
        public async Task<IActionResult> TASKCATEGORY()
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:TASKCATEGORY"];
            var result = await GetToExternalService(productServiceUrl);
            return Content(result, "application/json");

        }

        #endregion
        #region This section is used for Project Module

        [HttpGet("GetAllProj")]
        public async Task<IActionResult> GetAllProj()
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetAllProject"];
            var result = await GetToExternalService(productServiceUrl);
            return Content(result, "application/json");

        }
        
        [HttpGet("GetProjByID")]
        public async Task<IActionResult> GetProjByID(int Id)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetProject"];  
            var result = await GetToExternalService(productServiceUrl+Id.ToString());
            return Content(result, "application/json");

        }
        [HttpPost("NewProject")]
        public async Task<IActionResult> NewProject([FromBody] Project Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:NewProject"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PostToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }

        [HttpPut("UpdProject")]
        public async Task<IActionResult> UpdProject([FromBody] Project Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:UpdProject"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        [HttpPut("DelProject")]
        public async Task<IActionResult> DelProject([FromBody] Project Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:DelProject"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        #endregion
        #region This section is used for  Task

        [HttpGet("GetAllTask")]
        public async Task<IActionResult> GetAllTask()
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetAllTask"];
            var result = await GetToExternalService(productServiceUrl);
            return Content(result, "application/json");

        }

        [HttpGet("GetTaskByID")]
        public async Task<IActionResult> GetTaskByID(int Id)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetTask"];
            var result = await GetToExternalService(productServiceUrl + Id.ToString());
            return Content(result, "application/json");

        }
        [HttpPost("NewTask")]
        public async Task<IActionResult> NewTask([FromBody] Tassk Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:NewTask"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PostToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }

        [HttpPut("UpdTask")]
        public async Task<IActionResult> UpdTask([FromBody] Tassk Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:UpdTask"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        [HttpPut("DelTask")]
        public async Task<IActionResult> DelTask([FromBody] Tassk Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:DelTask"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }

        #endregion
        #region This section is used for Task Comment

        [HttpGet("GetAllComment")]
        public async Task<IActionResult> GetAllComment(int TaskId)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetAllComment"];
            var result = await GetToExternalService(productServiceUrl + TaskId.ToString());
            return Content(result, "application/json");

        }

        [HttpGet("GetComment")]
        public async Task<IActionResult> GetComment(int TaskId, int CommentId)
        {
            var  productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetComment"]; 
            // Replace placeholders with actual values
            var formattedUrl = productServiceUrl.Replace("{TaskId}", TaskId.ToString())
                               .Replace("{CommentId}",CommentId.ToString());  

            var result = await GetToExternalService(formattedUrl);
            return Content(result, "application/json");

        }
        [HttpPost("NewComment")]
        public async Task<IActionResult> NewComment([FromBody] Comment Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:NewComment"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PostToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }

        [HttpPut("UpdComment")]
        public async Task<IActionResult> UpdComment([FromBody] Comment tsk)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:UpdComment"];
            string jsonData = JsonConvert.SerializeObject(tsk);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        [HttpPut("DelComments")]
        public async Task<IActionResult> DelComments([FromBody] Comment tsk)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:DelComment"];
            string jsonData = JsonConvert.SerializeObject(tsk);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        #endregion
        #region This section is used for Project Document

        [HttpGet("GetAllDocument")]
        public async Task<IActionResult> GetAllDocument(int TaskId)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetAllDocument"];
            var result = await GetToExternalService(productServiceUrl+TaskId.ToString());
            return Content(result, "application/json");

        }

        [HttpGet("GetDocument")]
        public async Task<IActionResult> GetDocument(int TaskId, int DocumentId)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetDocument"]; 
            // Replace placeholders with actual values
            var formattedUrl = productServiceUrl.Replace("{TaskId}", TaskId.ToString())
                               .Replace("{DocumentId}", DocumentId.ToString());
            var result = await GetToExternalService(formattedUrl);
            return Content(result, "application/json");

        }
        [HttpPost("NewDocument")]
        public async Task<IActionResult> NewDocument([FromBody] Document Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:NewDocument"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PostToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }

        [HttpPut("UpdDocument")]
        public async Task<IActionResult> UpdDocument([FromBody] Document Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:UpdDocument"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        [HttpPut("DelDocument")]
        public async Task<IActionResult> DelDocument([FromBody] Document Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:DelDocument"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        #endregion
        #region This section is used for Task Status Statement  

        [HttpGet("GetAllTaskStatus")]
        public async Task<IActionResult> GetAllTaskStatus()
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetAllTaskStatus"];
            var result = await GetToExternalService(productServiceUrl);
            return Content(result, "application/json");

        }

        [HttpGet("GetTaskStatus")]
        public async Task<IActionResult> GetTaskStatus(int Id)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetTaskStatus"];
            var result = await GetToExternalService(productServiceUrl + Id.ToString());
            return Content(result, "application/json");

        }
        [HttpPost("NewTaskStatus")]
        public async Task<IActionResult> NewTaskStatus([FromBody] TasskStatus tsk)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:NewTaskStatus"];
            string jsonData = JsonConvert.SerializeObject(tsk);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PostToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }

        [HttpPut("UpdTaskStatus")]
        public async Task<IActionResult> UpdTaskStatus([FromBody] TasskStatus tsk)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:UpdTaskStatus"];
            string jsonData = JsonConvert.SerializeObject(tsk);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        [HttpPut("DelTaskStatus")]
        public async Task<IActionResult> DelTaskStatus([FromBody] TasskStatus tsk)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:DelTaskStatus"];
            string jsonData = JsonConvert.SerializeObject(tsk);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        #endregion
        #region This section is used for General Status    

        [HttpGet("GetAllStatus")]
        public async Task<IActionResult> GetAllStatus()
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetAllStatus"];
            var result = await GetToExternalService(productServiceUrl);
            return Content(result, "application/json");

        }

        [HttpGet("GetStatus")]
        public async Task<IActionResult> GetStatus(int Id)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetStatus"];
            var result = await GetToExternalService(productServiceUrl + Id.ToString());
            return Content(result, "application/json");

        }
       
        [HttpPost("Newstatus")]
        public async Task<IActionResult> Newstatus([FromBody] STATUS Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:Newstatus"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PostToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }

        [HttpPut("Updstatus")]
        public async Task<IActionResult> Updstatus([FromBody] STATUS Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:Updstatus"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        
        [HttpPut("Delstatus")]
        public async Task<IActionResult> Delstatus([FromBody] STATUS Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:Delstatus"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        #endregion
        #region This section is used for Task Category Statement  

        [HttpGet("GetAllTASKCATEGORY")]
        public async Task<IActionResult> GetAllTASKCATEGORY()
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetAllTASKCATEGORY"];
            var result = await GetToExternalService(productServiceUrl);
            return Content(result, "application/json");

        }

        [HttpGet("GetTASKCATEGORY")]
        public async Task<IActionResult> GetTASKCATEGORY(int Id)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:GetTASKCATEGORY"];
            var result = await GetToExternalService(productServiceUrl + Id.ToString());
            return Content(result, "application/json");

        }
       
        [HttpPost("NewTASKCATEGORY")]
        public async Task<IActionResult> NewTASKCATEGORY([FromBody] TASKCATEGORY Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:NewTASKCATEGORY"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PostToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }

        [HttpPut("UpdTASKCATEGORY")]
        public async Task<IActionResult> UpdTASKCATEGORY([FromBody] TASKCATEGORY Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:UpdTASKCATEGORY"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        
        [HttpPut("DelTASKCATEGORY")]
        public async Task<IActionResult> DelTASKCATEGORY([FromBody] TASKCATEGORY Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:DelTASKCATEGORY"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        #endregion
        #region This section is used for Task Status Statement  

        [HttpGet("AllTASKSPRIORITY")]
        public async Task<IActionResult> AllTASKSPRIORITY()
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:AllTASKSPRIORITY"];
            var result = await GetToExternalService(productServiceUrl);
            return Content(result, "application/json");

        }

        [HttpGet("TASKSPRIORITY")]
        public async Task<IActionResult> TASKSPRIORITY(int Id)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:TASKSPRIORITY"];
            var result = await GetToExternalService(productServiceUrl + Id.ToString());
            return Content(result, "application/json");

        }
      
        [HttpPost("NewTASKSPRIORITY")]
        public async Task<IActionResult> NewTASKSPRIORITY([FromBody] TASKSPRIORITY Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:NewTASKSPRIORITY"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PostToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }

        [HttpPut("UpdTASKSPRIORITY")]
        public async Task<IActionResult> UpdTASKSPRIORITY([FromBody] TASKSPRIORITY Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:UpdTASKSPRIORITY"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
      
        [HttpPut("DelTASKSPRIORITY")]
        public async Task<IActionResult> DelTASKSPRIORITY([FromBody] TASKSPRIORITY Proj)
        {
            var productServiceUrl = TMS.Authentication.Authenticate.ConfigurationManager.AppSetting["URL:DelTASKSPRIORITY"];
            string jsonData = JsonConvert.SerializeObject(Proj);
            // Create HttpContent
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var result = await PUTToExternalService(productServiceUrl, content);
            return Content(result, "application/json");
        }
        #endregion
        #region This section is common for User APi

        private async Task<string> PostToExternalService(string url ,dynamic obj)
        { 
            var client = _httpClient;
            var response = await client.PostAsync(url, obj);
            response.EnsureSuccessStatusCode(); // Throw on error code. 
            return await response.Content.ReadAsStringAsync();
        } 
        private async Task<string> GetToExternalService(string url)
        {
            var client = _httpClient;
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throw on error code. 
            return await response.Content.ReadAsStringAsync();
        }
        private async Task<string> PUTToExternalService(string url, dynamic obj)
        {
            var client = _httpClient;
            var response = await client.PutAsync(url, obj);
            response.EnsureSuccessStatusCode(); // Throw on error code. 
            return await response.Content.ReadAsStringAsync();
        }
        #endregion
    }
}
