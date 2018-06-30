using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpower.Data.Models
{
    public class Application_PagePans
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }


        [RequiredAttribute]
        public Int64 applicationID { get; set; }
        
        [StringLengthAttribute(25)]
        [RequiredAttribute]
        public string title { get; set; }

        public Int64 scriptFileId { get; set; }


        [StringLengthAttribute(10000)]
        [RequiredAttribute]
        public string blockHtml { get; set; }

    }
}