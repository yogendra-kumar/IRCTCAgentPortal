using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpower.Data.Models
{
    public class Pages_Metadata
    {
         [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public Int64 id {get;set;}


         [StringLengthAttribute(150)]
         public string keyword {get;set;}


         [StringLengthAttribute(250)]
         public string description {get;set;}


         public DateTime createdDate {get;set;}
         

         public Nullable<DateTime> modifiedDate {get;set;}
    }
}