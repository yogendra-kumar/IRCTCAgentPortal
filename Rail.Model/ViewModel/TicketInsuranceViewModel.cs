using System;
using System.ComponentModel.DataAnnotations;
using Mpower.Rail.Model.EntityBase;


namespace Mpower.Rail.Model.ViewModel
{
    public class TicketInsuranceViewModel : IEntityBase,IDisposable
    {
        void IDisposable.Dispose()
       {
           GC.SuppressFinalize(this);
       }

         [RequiredAttribute]
        public Int64 Id { get; set; }

        public Int64 ticketOrderId {get;set;}

        public bool insuranceOpted {get;set;}

        public string policyStatus {get;set;}

        public DateTime policyIssueDate {get;set;}

        public string insuranceCompany {get;set;}

        public string insuranceCompanyUrl {get;set;}
    }
}