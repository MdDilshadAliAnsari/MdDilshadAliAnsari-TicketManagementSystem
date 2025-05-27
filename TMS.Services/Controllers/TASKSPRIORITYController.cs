using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.Services.Model;

namespace TMS.Services.Controllers
{
    [ApiController]
    [Route("api/v{1:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class TASKSPRIORITYController : ControllerBase
    {
        #region  Variable Constructor and Dependency injection in Constructor level
        private readonly TMSDbContext _context;
        public TASKSPRIORITYController(TMSDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Operation on Tasks
        [HttpGet("AllTASKSPRIORITY")]
        public IActionResult GetTASKSPRIORITY()
        { 
            var result = _context.TASKSPRIORITIES.ToList();
            if (result == null)
            {
                return NotFound(new { message = "TASKS PRIORITY not found." });
            }
            var response = new { result };
            return Ok(new { response });
        }

        [HttpGet("TASKSPRIORITY")]
        public IActionResult GetTASKSPRIORITY(int TASKSPRIORITYId)
        {
            if (TASKSPRIORITYId == 0)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }
            var result = _context.TASKSPRIORITIES.Where(m => m.TASKSPRIORITYID == TASKSPRIORITYId).SingleOrDefault();
            if (result == null)
            {
                return NotFound(new { message = "TASKS PRIORITY not found." });
            }
            var response = new { result };
            return Ok(new { response });
        }

        [HttpPost("NewTASKSPRIORITY")]
        public IActionResult NewTASKSPRIORITY([FromBody] TASKSPRIORITY tsk)
        {
            if (tsk is null)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }
            tsk.TASKSPRIORITYID = null;
            tsk.ISDELETED = 0;
            _context.TASKSPRIORITIES.Add(tsk);
            _context.SaveChanges();
            return Ok(new { message = "TASK TASKSPRIORITY Successfully Added!!! " });
        }

        [HttpPut("UpdTASKSPRIORITY")]
        public IActionResult UpdTASKSPRIORITY([FromBody] TASKSPRIORITY tsk)
        {
            if (tsk is null)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }

            // Step 1: Retrieve the Project from the database
            var _Task = _context.TASKSPRIORITIES.Find(tsk.TASKSPRIORITYID);

            if (_Task != null)
            {
                _context.Entry(_Task).State = EntityState.Detached;
                // Step 2: Modify the Task property
                tsk.NAME          = tsk.NAME;
                tsk.DESCRIPTION   = tsk.DESCRIPTION;
                tsk.ISDELETED     = 0;
                tsk.CREATEDBY     = tsk.CREATEDBY;
                tsk.CREATEDON     = tsk.CREATEDON;
                tsk.UPDATEDBY     = tsk.UPDATEDBY;
                tsk.UPDATEDON     = tsk.UPDATEDON;

                // Step 3: Save the changes to the database
                _context.TASKSPRIORITIES.Update(tsk);
                _context.SaveChangesAsync();

                return Ok(new { message = "TASKS PRIORITY  Successfully Modified!!!" + tsk.TASKSPRIORITYID });
            }
            else
            {
                // Handle case when the Project is not found
                return Ok(new { message = "TASKS PRIORITY not found!!!" });
            }
        }

        [HttpPut("DelTASKSPRIORITY")]
        public IActionResult DelTASKSPRIORITY([FromBody] TASKSPRIORITY tsk)
        {
            if (tsk is null)
            {
                return BadRequest(new { message = "Invalid user request!!!" });
            }

            // Step 1: Retrieve the Project from the database
            var _Task = _context.TASKSPRIORITIES.Find(tsk.TASKSPRIORITYID);

            if (_Task != null)
            {
                _context.Entry(_Task).State = EntityState.Detached;
                // Step 2: Modify the Project property
                tsk.ISDELETED = 1;
                tsk.CREATEDBY = tsk.CREATEDBY;
                tsk.CREATEDON = tsk.CREATEDON;
                tsk.UPDATEDBY = tsk.UPDATEDBY;
                tsk.UPDATEDON = tsk.UPDATEDON;

                // Step 3: Save the changes to the database
                _context.TASKSPRIORITIES.Update(tsk);
                _context.SaveChangesAsync();

                return Ok(new { message = "TASKS PRIORITY  Successfully Deleted!!! " + tsk.TASKSPRIORITYID });
            }
            else
            {
                // Handle case when the Project is not found
                return Ok(new { message = "TASKS PRIORITY not found!!!" });
            }
        }
        #endregion
    }
}
