using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpower.Data.Models
{
    public class Application
    {
                    
[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [StringLength(450)]
        [RequiredAttribute]
        public string guid { get; set; }

        [StringLength(150)]
        [RequiredAttribute]
        public string applicationName { get; set; }

        [StringLength(150)]
        public string domainUrl { get; set; }

        [StringLength(45)]
        public string domainIP { get; set; }

        [RequiredAttribute]
        public Int64 layoutID { get; set; }

        public Int64 feviconFileId { get; set; }

        [RequiredAttribute]
        public Int64 logoFileId { get; set; }

        [StringLength(25)]
        public string supportEmail { get; set; }

        public Int64 footerId{get;set;}

    }
}