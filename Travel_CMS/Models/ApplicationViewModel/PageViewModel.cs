using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Mpower.Models.ApplicationViewModel
{
    public class PageViewModel
    {
        public Int64 id { get; set; }

        [StringLengthAttribute(250)]
        [RequiredAttribute]
        public string guid { get; set; }

         [RequiredAttribute]
        public Int64 parentID { get; set; }
        public Int64 applicationID { get; set; }

        [RequiredAttribute]
        public Int32 index {get;set;}

        [StringLengthAttribute(50)]
        [RequiredAttribute]
        public string title { get; set; }

         [StringLengthAttribute(25)]
        public string externalUrl { get; set; }

        [RequiredAttribute]
        public Int64 layoutID { get; set; }

        [StringLength(25)]
        [RequiredAttribute]
        public string layoutName {get;set;}

        public bool isActive {get;set;}

        public DateTime createdDate {get;set;}

        public DateTime modifiedDate {get;set;}

        [StringLengthAttribute(25)]
        [RequiredAttribute]
        public string viewName {get;set;}

        public Int64 scriptFileId{ get; set; }

        [StringLengthAttribute(10000)]
        [RequiredAttribute]
        public string pageHtml { get; set; }

        public Int64 leftPanId {get;set;}

        public Int64 rightPanId {get;set;}

         public IEnumerable<SelectList> pageList {get;set;}

         public Int64 metaId {get;set;}

         public string keyword {get;set;}

         public string description {get;set;}
         
    }
}   