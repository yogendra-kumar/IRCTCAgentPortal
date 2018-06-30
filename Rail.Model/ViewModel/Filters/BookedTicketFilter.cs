using System;

namespace Mpower.Rail.Model.ViewModel.Filters
{
public class BookedTicketFilter
{
   public string agentAccountNo {get;set;}
   public string pnr {get;set;}
   public string ticketNumber {get;set;}
   public string fromStation {get;set;}
   public string toStation {get;set;}
   public DateTime dateOfJourney {get;set;}
   public int ticketStatus {get;set;}
   public int pageIndex {get;set;} = 1;
   public int pages {get;set;}
}
}