using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.Services.Model;

namespace TMS.Services.Controllers
{
    [ApiController]
    [Route("api/v{1:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class TaskController : ControllerBase
    {
        #region  Variable Constructor and Dependency injection in Constructor level
        private readonly TMSDbContext _context; 
        public TaskController(TMSDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Operation on Tasks
        [HttpGet("GetAllTask")]
        public IActionResult GetTask()
        {

            var result = _context.Tasks.ToList();
            var response = new { result };
            return Ok(new { response });
        }

        [HttpGet("GetTask")]
        public IActionResult GetTask(int TaskId)
        {
            if (TaskId == 0)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }
            var result = _context.Tasks.Where(m => m.TASKID == TaskId).ToList();
            var response = new { result };
            return Ok(new { response });
        }

        [HttpPost("NewTask")]
        public IActionResult NewTask([FromBody] Tassk tsk)
        {
            if (tsk is null)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }
            tsk.ISDELETED = 0;
            tsk.TASKID = null;
            _context.Tasks.Add(tsk);
            _context.SaveChanges();
            return Ok(new { message = "Task Successfully Added!!! " });
        }

        [HttpPut("UpdTask")]
        public IActionResult UpdTask([FromBody] Tassk tsk)
        {
            if (tsk is null)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }

            // Step 1: Retrieve the Project from the database
            var _Task = _context.Tasks.Find(tsk.TASKID);

            if (_Task != null)
            {
                _context.Entry(_Task).State = EntityState.Detached;
                // Step 2: Modify the Task property
                _Task.PROJECTID         = tsk.PROJECTID;
                _Task.DESCRIPTION       = tsk.DESCRIPTION;
                _Task.TASKSPRIORITYID   = tsk.TASKSPRIORITYID;
                _Task.TASKCATEGORYID    = tsk.TASKCATEGORYID;
                _Task.DUEDATE           = tsk.DUEDATE;
                _Task.USERID            = tsk.USERID;
                _Task.ISDELETED         = 0;
                _Task.CREATEDBY         = tsk.CREATEDBY;
                _Task.CREATEDON         = tsk.CREATEDON;
                _Task.UPDATEDBY         = tsk.UPDATEDBY;
                _Task.UPDATEDON         = tsk.UPDATEDON;

                // Step 3: Save the changes to the database
                _context.Tasks.Update(_Task);
                _context.SaveChangesAsync();

                return Ok(new { message = "Task Successfully Modified!!!" + tsk.TASKID });
            }
            else
            {
                // Handle case when the Project is not found
                return Ok(new { message = "Task not found!!!" });
            }
        }

        [HttpPut("DelTask")]
        public IActionResult DelTask([FromBody] Tassk tsk)
        {
            if (tsk is null)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }

            // Step 1: Retrieve the Project from the database
            var _Task = _context.Tasks.Find(tsk.TASKID);

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
                _context.Tasks.Update(_Task);
                _context.SaveChangesAsync();

                return Ok(new { message = "Task Successfully Deleted!!! " + tsk.TASKID});
            }
            else
            {
                // Handle case when the Project is not found
                return Ok(new { message = "Task not found!!!" });
            }
        }
        #endregion
    }
}
