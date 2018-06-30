using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpower.Data.Models
{
    public class Application_Layouts
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public Int64 id { get; set; }

        [StringLength(25)]
        [RequiredAttribute]
        public string layoutName { get; set; }
        
        [RequiredAttribute]
        public Int64 applicationID { get; set; }

    }
}