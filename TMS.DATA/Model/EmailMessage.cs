using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.DATA.Model
{
    [Table("EMAILDATA")]
    public class EmailMessage
    {
        [Key]
        public int?             EMAILId         { get; set; }
        public string?          SUBJECT         { get; set; }
        public string?          BODY            { get; set; }
        public string?          SENDER          { get; set; }
        public int?             ISMIGRATED      { get; set; }
        public DateTime         RECIEVEDDDATE   { get; set; }
        public int?             ISDELETED       { get; set; }
        public int?             CREATEDBY       { get; set; }
        public DateTime?        CREATEDON       { get; set; }
        public int?             UPDATEDBY       { get; set; }
        public DateTime?        UPDATEDON       { get; set; }


    }
}
