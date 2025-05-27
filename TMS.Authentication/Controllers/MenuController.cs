using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;
using TMS.Authentication.Model;
using TMS.Authentication.Menu;

namespace TMS.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController :  ControllerBase
    {
        #region Admin Menu
        [HttpPost("AdminMenu")]
        [Authorize]
        public IActionResult AdminMenu()
        {
            // Specify the type you expect to deserialize the XML into
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Menus)); // Replace 'MenuType' with the actual class you're deserializing to

            // Construct the correct file path
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Menu", "AdminMenu.xml");
     
            //// Use 'using' to ensure the FileStream is disposed of
            using (FileStream xmlStream = new FileStream(filePath, FileMode.Open))
            {
                // Deserialize the XML stream to the object
                var result = xmlSerializer.Deserialize(xmlStream);

                // Return the result (you can change 'MenuType' as per your requirements)
                return Ok(new { result });
            }
        }

        #endregion
        #region Customer  Menu
        [HttpPost("CustomerMenu")]
        [Authorize]
        public IActionResult CustomerMenu()
        {
            // Specify the type you expect to deserialize the XML into
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Menus)); // Replace 'MenuType' with the actual class you're deserializing to

            // Construct the correct file path
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Menu", "CustomerMenu.xml");

            //// Use 'using' to ensure the FileStream is disposed of
            using (FileStream xmlStream = new FileStream(filePath, FileMode.Open))
            {
                // Deserialize the XML stream to the object
                var result = xmlSerializer.Deserialize(xmlStream);

                // Return the result (you can change 'MenuType' as per your requirements)
                return Ok(new { result });
            }
        }

        #endregion
        #region Agent or Developer Menu
        [HttpPost("AgentMenu")]
        [Authorize]
        public IActionResult AgentMenu()
        {
            // Specify the type you expect to deserialize the XML into
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Menus)); // Replace 'MenuType' with the actual class you're deserializing to

            // Construct the correct file path
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Menu", "AgentMenu.xml");

            //// Use 'using' to ensure the FileStream is disposed of
            using (FileStream xmlStream = new FileStream(filePath, FileMode.Open))
            {
                // Deserialize the XML stream to the object
                var result = xmlSerializer.Deserialize(xmlStream);

                // Return the result (you can change 'MenuType' as per your requirements)
                return Ok(new { result });
            }
        }

        #endregion
    }
}
