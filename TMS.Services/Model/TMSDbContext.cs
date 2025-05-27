using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace TMS.Services.Model
{
    public class TMSDbContext : DbContext, IDisposable
    {
        public TMSDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DbConnection");
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TMS");
            modelBuilder.Entity<Project>().HasKey(am => new
            {                am.PROJECTID
            });
            modelBuilder.Entity<Tassk>().HasKey(am => new
            {
                am.TASKID
            });

            modelBuilder.Entity<TasskStatus>().HasKey(am => new
            {
                am.TASKSSTATUSID
            });

            modelBuilder.Entity<Comment>().HasKey(am => new
            {
                am.COMMENTID
            });

            modelBuilder.Entity<Document>().HasKey(am => new
            {
                am.DOCUMENTID
            });
            modelBuilder.Entity<TASKCATEGORY>().HasKey(am => new
            {
                am.TASKCATEGORYID
            });
            modelBuilder.Entity<TASKSPRIORITY>().HasKey(am => new
            {
                am.TASKSPRIORITYID
            });
            modelBuilder.Entity<STATUS>().HasKey(am => new
            {
                am.STATUSID
            });
            base.OnModelCreating(modelBuilder); 
            #region Feed Data
            modelBuilder.Entity<TASKCATEGORY>().HasData(
            new TASKCATEGORY
            {
                TASKCATEGORYID      = 1,
                NAME                = "Bug / Defect",
                DESCRIPTION         = "A flaw in the software or system that causes incorrect or unexpected behavior",
                ISDELETED           = 0,
                CREATEDBY           = 1,
                CREATEDON           = new DateTime(2025, 04, 30),
                UPDATEDBY           = 1,
                UPDATEDON           = new DateTime(2025, 04, 30)  

            }, new TASKCATEGORY
            {
                TASKCATEGORYID = 2,
                NAME = "Feature Request",
                DESCRIPTION = "A suggestion to add new functionality or improve existing features.",
                ISDELETED = 0,
                CREATEDBY = 1,
                CREATEDON = new DateTime(2025, 04, 30),
                UPDATEDBY = 1,
                UPDATEDON = new DateTime(2025, 04, 30)
            }, new TASKCATEGORY
            {
                TASKCATEGORYID = 3,
                NAME = "Technical Support",
                DESCRIPTION = "Help with resolving technical problems, including hardware, software, or network issues",
                ISDELETED = 0,
                CREATEDBY = 1,
                CREATEDON = new DateTime(2025, 04, 30),
                UPDATEDBY = 1,
                UPDATEDON = new DateTime(2025, 04, 30)
            }, new TASKCATEGORY
            {
                TASKCATEGORYID = 4,
                NAME = "Service Request",
                DESCRIPTION = "A formal request for something standard (e.g., access, install, information).",
                ISDELETED = 0,
                CREATEDBY = 1,
                CREATEDON = new DateTime(2025, 04, 30),
                UPDATEDBY = 1,
                UPDATEDON = new DateTime(2025, 04, 30)
            }, new TASKCATEGORY
            {
                TASKCATEGORYID = 5,
                NAME = "Service Request Major",
                DESCRIPTION = "A formal request for something Major (e.g., Business logic changes).",
                ISDELETED = 0,
                CREATEDBY = 1,
                CREATEDON = new DateTime(2025, 04, 30),
                UPDATEDBY = 1,
                UPDATEDON = new DateTime(2025, 04, 30)
            }
            , new TASKCATEGORY
            {
                TASKCATEGORYID = 6,
                NAME = "Incident",
                DESCRIPTION = "An unplanned interruption to a service or a reduction in service quality.",
                ISDELETED = 0,
                CREATEDBY = 1,
                CREATEDON = new DateTime(2025, 04, 30),
                UPDATEDBY = 1,
                UPDATEDON = new DateTime(2025, 04, 30)
            }
            , new TASKCATEGORY
            {
                TASKCATEGORYID = 7,
                NAME = "Access / Permission Issue",
                DESCRIPTION = "User cannot access a system, file, or application.",
                ISDELETED = 0,
                CREATEDBY = 1,
                CREATEDON = new DateTime(2025, 04, 30),
                UPDATEDBY = 1,
                UPDATEDON = new DateTime(2025, 04, 30)
            }
            , new TASKCATEGORY
            {
                TASKCATEGORYID = 8,
                NAME = "Account Issues",
                DESCRIPTION = "Login problems, password resets, locked accounts.",
                ISDELETED = 0,
                CREATEDBY = 1,
                CREATEDON = new DateTime(2025, 04, 30),
                UPDATEDBY = 1,
                UPDATEDON = new DateTime(2025, 04, 30)
            }, new TASKCATEGORY
            {
                TASKCATEGORYID = 9,
                NAME = "Performance Issue",
                DESCRIPTION = "System/application is slow or unresponsive.",
                ISDELETED = 0,
                CREATEDBY = 1,
                CREATEDON = new DateTime(2025, 04, 30),
                UPDATEDBY = 1,
                UPDATEDON = new DateTime(2025, 04, 30)
            }, new TASKCATEGORY
            {
                TASKCATEGORYID = 10,
                NAME = "UI / UX Feedback",
                DESCRIPTION = "Non-critical feedback about the interface or usability.",
                ISDELETED = 0,
                CREATEDBY = 1,
                CREATEDON = new DateTime(2025, 04, 30),
                UPDATEDBY = 1,
                UPDATEDON = new DateTime(2025, 04, 30)
            });
           
            modelBuilder.Entity<TASKSPRIORITY>().HasData(
           new TASKSPRIORITY
           {
               TASKSPRIORITYID = 1,
               NAME = "LOW",
               DESCRIPTION = "Low Priority",
               ISDELETED = 0,
               CREATEDBY = 1,
               CREATEDON = new DateTime(2025, 04, 30),
               UPDATEDBY = 1,
               UPDATEDON = new DateTime(2025, 04, 30)

           }, new TASKSPRIORITY
           {
               TASKSPRIORITYID = 2,
               NAME = "Medium",
               DESCRIPTION = "Medium Priority.",
               ISDELETED = 0,
               CREATEDBY = 1,
               CREATEDON = new DateTime(2025, 04, 30),
               UPDATEDBY = 1,
               UPDATEDON = new DateTime(2025, 04, 30)
           }, new TASKSPRIORITY
           {
               TASKSPRIORITYID = 3,
               NAME = "Hign",
               DESCRIPTION = "High Priority",
               ISDELETED = 0,
               CREATEDBY = 1,
               CREATEDON = new DateTime(2025, 04, 30),
               UPDATEDBY = 1,
               UPDATEDON = new DateTime(2025, 04, 30)
           }, new TASKSPRIORITY
           {
               TASKSPRIORITYID = 4,
               NAME = "Critical",
               DESCRIPTION = "Critical  Priority.",
               ISDELETED = 0,
               CREATEDBY = 1,
               CREATEDON = new DateTime(2025, 04, 30),
               UPDATEDBY = 1,
               UPDATEDON = new DateTime(2025, 04, 30)
           });

            modelBuilder.Entity<STATUS>().HasData(
         new STATUS
         {
             STATUSID = 1,
             NAME = "New",
             DESCRIPTION = "This task is new",
             ISDELETED = 0,
             CREATEDBY = 1,
             CREATEDON = new DateTime(2025, 04, 30),
             UPDATEDBY = 1,
             UPDATEDON = new DateTime(2025, 04, 30)

         }, new STATUS
         {
             STATUSID = 2,
             NAME = "In Progress",
             DESCRIPTION = "This task is in Progress.",
             ISDELETED = 0,
             CREATEDBY = 1,
             CREATEDON = new DateTime(2025, 04, 30),
             UPDATEDBY = 1,
             UPDATEDON = new DateTime(2025, 04, 30)
         }, new STATUS
         {
             STATUSID = 3,
             NAME = "Resolved",
             DESCRIPTION = "This task resolved in Developement Environment",
             ISDELETED = 0,
             CREATEDBY = 1,
             CREATEDON = new DateTime(2025, 04, 30),
             UPDATEDBY = 1,
             UPDATEDON = new DateTime(2025, 04, 30)
         }, new STATUS
         {
             STATUSID = 4,
             NAME = "Closed",
             DESCRIPTION = "This task already Closed.",
             ISDELETED = 0,
             CREATEDBY = 1,
             CREATEDON = new DateTime(2025, 04, 30),
             UPDATEDBY = 1,
             UPDATEDON = new DateTime(2025, 04, 30)
         });
            #endregion

        }
        public TMSDbContext(DbContextOptions<TMSDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Tassk> Tasks { get; set; }
        public virtual DbSet<TasskStatus> TaskStatuses { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Document> Documents { get; set; }

        public virtual DbSet<TASKCATEGORY> TASKCATEGORIES { get; set; }
        public virtual DbSet<TASKSPRIORITY> TASKSPRIORITIES { get; set; }
        public virtual DbSet<STATUS>        STATUS { get; set; }




    }
}
