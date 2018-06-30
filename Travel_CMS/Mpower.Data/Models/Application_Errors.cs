using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpower.Data.Models
{
    public class Application_Errors
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }

        [Required]
        public Int64 applicationID { get; set; }

        [Required]
        public Int64 pageID { get; set; }

        [StringLength(50)]
        [RequiredAttribute]
        public string errorType { get; set; }

        [StringLength(1000)]
        [RequiredAttribute]
        public string errorDescription { get; set; }

        [RequiredAttribute]
        public DateTime logDate { get; set; }

    }
}