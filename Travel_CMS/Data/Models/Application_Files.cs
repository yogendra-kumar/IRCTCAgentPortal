using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpower.Data.Models
{
    public class Application_Files
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }

        [RequiredAttribute]
        public Int64 applicationID { get; set; }

        [RequiredAttribute]
        [StringLength(50)]
        public string actualFileName{get;set;}

        [RequiredAttribute]
        [StringLength(100)]
        public string systemFileName{get;set;}

        [RequiredAttribute]
        [StringLength(250)]
        public string attachmentUrl{get;set;}

        public DateTime createdDate{get;set;}

        public System.Nullable<DateTime> updatedDate{get;set;}
    }
    
}