using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;

namespace TMS.Services.Model
{
    /// <summary>
    /// Tasks to Projects           : One-to-many (A project can have multiple tasks)
    //  Tasks to Users              : Many-to-one (Multiple tasks can be assigned to one user)
    /// </summary>
    /// 
    #region Master Table
    [Table("TASKSPRIORITY")]
    public class TASKSPRIORITY
    {
        [Key]
        public int? TASKSPRIORITYID { get; set; }
        public string? NAME { get; set; }
        public string? DESCRIPTION { get; set; }
        public int? ISDELETED { get; set; }
        public int? CREATEDBY { get; set; }
        public DateTime? CREATEDON { get; set; }
        public int? UPDATEDBY { get; set; }
        public DateTime? UPDATEDON { get; set; }
    }

    [Table("TASKCATEGORY")]
    public class TASKCATEGORY
    {
        [Key]
        public int? TASKCATEGORYID { get; set; }
        public string? NAME { get; set; }
        public string? DESCRIPTION { get; set; }
        public int? ISDELETED { get; set; }
        public int? CREATEDBY { get; set; }
        public DateTime? CREATEDON { get; set; }
        public int? UPDATEDBY { get; set; }
        public DateTime? UPDATEDON { get; set; }
    }

    [Table("STATUS")]
    public class STATUS
    {
        [Key]
        public int? STATUSID { get; set; }
        public string? NAME { get; set; }
        public string? DESCRIPTION { get; set; }
        public int? ISDELETED { get; set; }
        public int? CREATEDBY { get; set; }
        public DateTime? CREATEDON { get; set; }
        public int? UPDATEDBY { get; set; }
        public DateTime? UPDATEDON { get; set; }
    }

    #endregion

    [Table("PROJECTS")]
    public class Project
    {
        [Key]
        public int?                 PROJECTID { get; set; }
        public string?              PROJECTNAME { get; set; }
        public string?              DESCRIPTION { get; set; }
        public DateTime?            STARTDATE { get; set; }
        public DateTime?            ENDDATE { get; set; } 
        public int?                 ISDELETED { get; set; }
        public int?                 CREATEDBY { get; set; }
        public DateTime?            CREATEDON { get; set; }
        public int?                 UPDATEDBY { get; set; }
        public DateTime?            UPDATEDON { get; set; }
    }



    [Table("TASKS")]
    public class Tassk
    {
        [Key]
        public int?                 TASKID              { get; set; } 

        [ForeignKey("PROJECTID")]
        public int?                 PROJECTID           { get; set; } 
        public string?              DESCRIPTION         { get; set; }

        [ForeignKey("TASKSPRIORITYID")] 
        public int?                 TASKSPRIORITYID     { get; set; }

        [ForeignKey("TASKCATEGORYID")]
        public int?                 TASKCATEGORYID      { get; set; }
        public DateTime?            DUEDATE             { get; set; }
        [ForeignKey("USERID")]
        public int?                 USERID              { get; set; }         
        public int?                 ISDELETED           { get; set; }
        public int?                 CREATEDBY           { get; set; }
        public DateTime?            CREATEDON           { get; set; }
        public int?                 UPDATEDBY           { get; set; }
        public DateTime? UPDATEDON                      { get; set; }
    }

    [Table("TASKSTATUS")]
    public class TasskStatus
    {
        [Key]
        public int? TASKSSTATUSID { get; set; }


        [ForeignKey("STATUSID")]
        public int? STATUSID { get; set; }
        /// <summary>
        /// (e.g., "Open", "In Progress", "Completed")
        /// </summary>
        public string? DESCRIPTION { get; set; }

        [ForeignKey("TASKCATEGORYID")]
        public int? TASKCATEGORYID { get; set; }

        [ForeignKey("TASKPRIORITYID")]
        public int? TASKPRIORITYID { get; set; }
        public int? ISDELETED { get; set; }
        public int? CREATEDBY { get; set; }
        public DateTime? CREATEDON { get; set; }
        public int? UPDATEDBY { get; set; }
        public DateTime? UPDATEDON { get; set; }
    }
     
    [Table("COMMENTS")]
    public class Comment
    {
        [Key]
        public int? COMMENTID { get; set; }

        [ForeignKey("TASKID")]
        public int? TASKID { get; set; }
        [ForeignKey("USERID")]
        public int? USERID { get; set; }

        public string? COMMENTTEXT { get; set; }
        public int? ISDELETED { get; set; }
        public int? CREATEDBY { get; set; }
        public DateTime? CREATEDON { get; set; }
        public int? UPDATEDBY { get; set; }
        public DateTime? UPDATEDON { get; set; }
    }

    [Table("DOCUMENTS")]
    public class Document
    {
        [Key]
        public int? DOCUMENTID { get; set; }

       /// <summary>
       /// All three are foreign Key which is used ofr tracking the document which is uploaded by enduser
       /// </summary>
        [ForeignKey("PROJECTID")] 
        public int? PROJECTID { get; set; }

        [ForeignKey("TASKID")]       
        public int? TASKID { get; set; }

        [ForeignKey("TASKSSTATUSID")]
        public int? TASKSSTATUSID { get; set; }

        public string? DOCUMENTURL { get; set; }
        public int? ISDELETED { get; set; }
        public int? CREATEDBY { get; set; }
        public DateTime? CREATEDON { get; set; }
        public int? UPDATEDBY { get; set; }
        public DateTime? UPDATEDON { get; set; }
    }
}


