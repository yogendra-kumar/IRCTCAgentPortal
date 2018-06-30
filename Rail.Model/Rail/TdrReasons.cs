
using System;
using Mpower.Rail.Model.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpower.Rail.Model.Rail
{
    public class TdrReasons : IEntityBase, IDisposable
    {
       
       [KeyAttribute, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity) ]
        public Int64 Id {get;set;}


        [StringLengthAttribute(250)]
        public string Reasons { get; set; }

        public bool status { get; set; }
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}