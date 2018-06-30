using System;
using System.Collections.Generic;
using Mpower.Rail.Model.Ticket;
using Mpower.Rail.Model.ViewModel;
using Mpower.Rail.Model.ViewModel.Filters;

namespace Mpower.Rail.Processor.Ticket
{
    public interface ITicketProcessor : IDisposable
    {
        /// <summary>
        //  This Method will return the details of Ticket Orders. 
        /// </summary>
        /// <param name="ticketOrderId">
        /// primary key of ticket order 
        ///</param>
        /// <returns>this will return the Ticket Orders list object</returns>
        TicketOrderViewModel GetTicketDetail(Int64 ticketOrderId);

        /// <summary>
        //  This Method will return the list of PassengerList. 
        /// </summary>
        /// <param name="Filter">filters(pnr,ticketNumber,fromStation,toStation,
        /// dateofJourney,ticketStatus,pageIndex and pages) to filter the ticket ordes list.
        ///</param>
        /// <returns>this will return the passenger list object</returns>
        List<TicketPassengers> PassengerList(Int64 ticketOrderId);

         /// <summary>
        //  This Method will return the list of Booked Tickets. 
        /// </summary>
        /// <param name="BookedTicketFilter">filters(pnr,ticketNumber,fromStation,toStation,
        /// dateofJourney,ticketStatus,pageIndex and pages) to filter the ticket ordes list.
        ///</param>
        /// <returns>this will return the Booked tickey list view model object</returns>
        List<BookedTicketViewModel> BookedTicketList(BookedTicketFilter filter);

    }
}