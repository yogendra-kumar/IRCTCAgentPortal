using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Mpower.Models.ApplicationViewModel
{
 public class Application_ViewModel
    {
         public Int64 Id { get; set; }

        [StringLength(450)]
        [RequiredAttribute]
        public string guid { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(150)]
        public string name {get;set;}


        [StringLengthAttribute(150)]
        public string domainUrl {get;set;}


        [StringLengthAttribute(45)]
        public string domainIP {get;set;}


        public IEnumerable<SelectList> layoutList {get;set;}


        [RequiredAttribute]
        public Int64 layoutId {get;set;}


        [StringLengthAttribute(150)]
        public string supportUrl {get;set;}


        [StringLengthAttribute(150)]
        public string favIconImageUrl {get;set;}


        public long favIconImageId {get;set;}


        [StringLengthAttribute(150)]
        public string logoImageUrl {get;set;}

        public long logoImageId {get;set;}

         [StringLength(25)]
        public string supportEmail { get; set; }

          [RequiredAttribute]
        public Int64 footerId {get;set;}

        public IEnumerable<SelectList> pageBlockList {get;set;}
    }
}