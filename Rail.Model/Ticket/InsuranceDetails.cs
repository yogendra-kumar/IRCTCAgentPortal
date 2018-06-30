using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Ticket
{
    public class InsuranceDetails : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(25)]
        public string pnrNumber { get; set; }

        [RequiredAttribute]
        public bool insuranceIssued { get; set; }

        [RequiredAttribute]
        public decimal insuranceCharge { get; set; }

        [RequiredAttribute]
        public int insurancePassangerCount { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(80)]
        public string insuranceCompany { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(150)]
        public string insuranceCompanyUrl { get; set; }

        public DateTime policyIssueDate { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(20)]
        public string policyStatus { get; set; }
    }
}