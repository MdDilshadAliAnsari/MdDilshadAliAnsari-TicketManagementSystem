using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.Services.Model;

namespace TMS.Services.Controllers
{
    [ApiController]
    [Route("api/v{1:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class DocumentController : ControllerBase
    {
        #region  Variable Constructor and Dependency injection in Constructor level
        private readonly TMSDbContext _context;
        public DocumentController(TMSDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Operation on Tasks
        [HttpGet("GetAllDocument")]
        public IActionResult GetDocument(int TaskId)
        {
            if (TaskId == 0)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }
            var result = _context.Documents.Where(m => m.TASKID == TaskId).ToList();
            if (result == null)
            {
                // Handle case when the Document is not found
                return Ok(new { message = "Document not found!!!" });
            }
            var response = new { result };
            return Ok(new { response });
        }
        
        [HttpGet("GetDocument")]
        public IActionResult GetDocument(int TaskId, int DocumentId)
        {
            if (TaskId == 0)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }
            var result = _context.Documents.Where(m => m.TASKID == TaskId && m.DOCUMENTID == DocumentId).ToList();
            if (result == null)
            {
                return Ok(new { message = "Document not found!!!" });
            }
            var response = new { result };
            return Ok(new { response });
        }

        [HttpPost("NewDocument")]
        public IActionResult NewDocument([FromBody] Document tsk)
        {
            if (tsk is null)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }
            tsk.DOCUMENTID = null;
            tsk.ISDELETED = 0;
            _context.Documents.Add(tsk);
            _context.SaveChanges();
            return Ok(new { message = "Document Successfully Added!!! " });
        }

        [HttpPut("UpdDocument")]
        public IActionResult UpdDocument([FromBody] Document tsk)
        {
            if (tsk is null)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }

            // Step 1: Retrieve the Project from the database
            var _Task = _context.Documents.Find(tsk.DOCUMENTID);

            if (_Task != null)
            {
                _context.Entry(_Task).State = EntityState.Detached;
                // Step 2: Modify the Task property
                _Task.PROJECTID     = tsk.PROJECTID;
                _Task.TASKID        = tsk.TASKID;
                _Task.TASKSSTATUSID = tsk.TASKSSTATUSID;
                _Task.DOCUMENTURL   = tsk.DOCUMENTURL;
                _Task.ISDELETED     = 0;
                _Task.CREATEDBY     = tsk.CREATEDBY;
                _Task.CREATEDON     = tsk.CREATEDON;
                _Task.UPDATEDBY     = tsk.UPDATEDBY;
                _Task.UPDATEDON     = tsk.UPDATEDON;

                // Step 3: Save the changes to the database
                _context.Documents.Update(_Task);
                _context.SaveChangesAsync();

                return Ok(new { message = "Document Successfully Modified!!!" + tsk.DOCUMENTID });
            }
            else
            {
                // Handle case when the Document is not found
                return Ok(new { message = "Document not found!!!" });
            }
        }

        [HttpPut("DelDocument")]
        public IActionResult DelDocument([FromBody] Document tsk)
        {
            if (tsk is null)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }

            // Step 1: Retrieve the Project from the database
            var _Task = _context.Documents.Find(tsk.DOCUMENTID);

            if (_Task != null)
            {
                _context.Entry(_Task).State = EntityState.Detached;
                // Step 2: Modify the Project property
                _Task.ISDELETED = 1;
                _Task.CREATEDBY = tsk.CREATEDBY;
                _Task.CREATEDON = tsk.CREATEDON;
                _Task.UPDATEDBY = tsk.UPDATEDBY;
                _Task.UPDATEDON = tsk.UPDATEDON;

                // Step 3: Save the changes to the database
                _context.Documents.Update(_Task);
                _context.SaveChangesAsync();

                return Ok(new { message = "Documents Successfully Deleted!!! " + tsk.DOCUMENTID });
            }
            else
            {
                // Handle case when the Document is not found
                return Ok(new { message = "Document not found!!!" });
            }
        }
        #endregion
    }
}
