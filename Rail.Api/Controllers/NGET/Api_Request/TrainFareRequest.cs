using Mpower.Rail.NGETSystem.Models.Request;

namespace Mpower.Rail.Api.NGET.Request
{
    public class TrainFareRequest
    {
        public FareEnquiry request { get; set; }
        public string trainNo { get; set; }
        public string journyDate { get; set; }
        public string trainFrom { get; set; }
        public string trainTo { get; set; }
        public string trainClass { get; set; }
        public string quota { get; set; }
        public long userSession { get; set; }

    }
    public class TransactionalTrainFareRequest
    {
        public TransactionalFareEnquiry request { get; set; }
        public string trainNo { get; set; }
        public string journyDate { get; set; }
        public string trainFrom { get; set; }
        public string trainTo { get; set; }
        public string trainClass { get; set; }
        public string quota { get; set; }
        public string pmtCardType { get; set; }
        public string accountNumber { get; set; }
        public string operatorCode { get; set; }
        public string loginAccountNo { get; set; }
        public string isTatkal { get; set; }
        public long userSession { get; set; }
        public string psgAddress { get; set; }
        public string PassengerEmail { get; set; }
        public string payment_Flag { get; set; }
        public string firstChildName { get; set; }
        public int firstChildAge { get; set; }
        public string firstChildSex { get; set; }
        public string secondChildName { get; set; }
        public int secondChildAge { get; set; }
        public string secondChildSex { get; set; }
        public string bedRoll { get; set; }
        public string berthPreference{get;set;}
    }
}