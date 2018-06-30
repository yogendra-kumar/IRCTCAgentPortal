using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class DisputedCases : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        public int disputedCaseTypes { get; set; }

        public long reference { get; set; }

        public long status { get; set; }

        public long retryCount { get; set; }

        public DateTime lastRetryDate { get; set; }

        public long resolutionType { get; set; }

        public long sessions { get; set; }

        public DateTime date { get; set; }

        public long advOrder { get; set; }

        public long eTSbRequest { get; set; }

        [StringLength(5)]
        public bool iSB2B { get; set; }
    }
}