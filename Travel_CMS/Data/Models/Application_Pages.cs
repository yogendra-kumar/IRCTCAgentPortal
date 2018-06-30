using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpower.Data.Models
{
    public class Application_Pages
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }

         [RequiredAttribute]
        public Int32 index{get;set;}

        [StringLengthAttribute(250)]
        [RequiredAttribute]
        public string guid { get; set; }
        [RequiredAttribute]
        public Int64 parentID { get; set; }
        [RequiredAttribute]
        public Int64 applicationID { get; set; }

        [StringLengthAttribute(50)]
        [RequiredAttribute]
        public string title { get; set; }

        [StringLengthAttribute(25)]
        [RequiredAttribute]
        public string viewName { get; set; }

        [StringLengthAttribute(25)]
        public string externalUrl { get; set; }

        [RequiredAttribute]
        public Int64 layoutID { get; set; }

        [RequiredAttribute]
        public bool IsActive { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public Int64 scriptFileId{ get; set; }

        [StringLengthAttribute(10000)]
        [RequiredAttribute]
        public string pageHtml { get; set; }

        public Int64 leftPanId {get;set;}

        public Int64 rightPanId {get;set;}

        public Int64 metaId {get;set;}

    }
}