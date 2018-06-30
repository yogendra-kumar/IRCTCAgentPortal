using System;
using System.Collections.Generic;
using Mpower.Rail.Data;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;
using Mpower.Rail.Data.Repository.Master;
using Mpower.Rail.Model.Ticket;
using System.Linq;
using Mpower.Rail.Model.ViewModel;
using Mpower.Rail.Model.ViewModel.Filters;
using Mpower.Rail.NGETSystem.Processor.EnquiryServices;

namespace Mpower.Rail.Processor.Ticket
{
    public class TicketProcessor : ITicketProcessor
    {
        private ITicketOrdersRepository _ticketOrdersRepository;
        private readonly ApplicationDbContext _dbContext=null;
        private IBookedTicketsRepository _bookedTicketsRepository;
        private ITicketPassengersRepository _ticketPassengersRepository;
        private ITicketStatusRepository _ticketStatusRepository;
        private IInsuranceRepository _insuranceRepository;
        private ITicketCancellationRepository _ticketCancellationRepository;

        private ICancelledTicketPassengersRepository _cancelledTicketPassengersRepository;
        private IQuotaRepository _quotaRepository;

       
        public TicketProcessor(ApplicationDbContext dbContext)
        {
            _dbContext=dbContext;
            _ticketOrdersRepository=new TicketOrdersRepository(_dbContext);
            _bookedTicketsRepository=new BookedTicketsRepository(_dbContext);
            _ticketPassengersRepository= new TicketPassengersRepository(_dbContext);
            _ticketStatusRepository = new TicketStatusRepository(_dbContext);
            _insuranceRepository=new InsuranceRepository(_dbContext);
            _ticketCancellationRepository=new TicketCancellationRepository(_dbContext);
            _cancelledTicketPassengersRepository=new CancelledTicketPassengersRepository(_dbContext);
            _quotaRepository=new QuotaRepository(_dbContext);
        }

        ~TicketProcessor()
        {
            _dbContext.Dispose();
            _ticketOrdersRepository = null;
            _bookedTicketsRepository=null;
            _ticketPassengersRepository=null;
            _ticketStatusRepository=null;
            _insuranceRepository =null;
            _ticketCancellationRepository=null;
            _quotaRepository=null;
            _cancelledTicketPassengersRepository=null;
        }

        /// <summary>
        //  This Method will return the details of ticket. 
        /// </summary>
        /// <param name="ticketOrderId">Id of ticket orders.</param>
        /// <returns>this will return the ticket details object</returns>

        public TicketOrderViewModel GetTicketDetail(Int64 ticketOrderId)
        {
            if(ticketOrderId <=0)
            {
                return null;
            }
            var ticketOrders = _ticketOrdersRepository.FindBy(x => x.Id == ticketOrderId);
            var bookedTickets = _bookedTicketsRepository.FindBy(x => x.ticketOrderId == ticketOrderId);
            if (!ticketOrders.Any() || !bookedTickets.Any())
            {
                return null;
            }
            var ticketStatus = _ticketStatusRepository.FindBy(x => x.Id == ticketOrders.FirstOrDefault().ticketStatus);
            var insurances = _insuranceRepository.FindBy(x => x.pnrNumber == bookedTickets.FirstOrDefault().pnrNumber);
            var quotas = _quotaRepository.FindBy(x => x.Id == ticketOrders.FirstOrDefault().quota);
            var refunds = _ticketCancellationRepository.FindBy(x => x.ticketNumber == bookedTickets.FirstOrDefault().ticketNumber);

            List<RefundHistoryViewModel> refunded = new List<RefundHistoryViewModel>();
            if(refunds.Any())
            {
                foreach(TicketCancellations refund in refunds.ToList())
                {
                    var cancelledPassengerDetails = _cancelledTicketPassengersRepository.GetSingle(x=>x.ticketId==refund.Id);
                    refunded.Add(new RefundHistoryViewModel{
                        Id=refund.Id,
                        ticketOrderId=ticketOrderId,
                        amount=refund.refundedAmount,
                        date=refund.cancelledDate,
                        cancelledPassenger=_ticketPassengersRepository.GetSingle(x=>x.Id==cancelledPassengerDetails.passengerId).name
                    });
                }
            }

            var ticketOrder = (
                from orders in ticketOrders.ToList()
                join tickets in bookedTickets.ToList() on orders.Id equals tickets.ticketOrderId into ticket
                from t in ticket.DefaultIfEmpty()
                join quota in quotas.ToList() on orders.quota equals quota.Id into quota
                from q in quota.DefaultIfEmpty()
                select new TicketOrderViewModel
                {
                    Id = ticketOrderId,
                    pnrNumber = t.pnrNumber,
                    transactionId = orders.irctcTxnNumber,
                    sourceStation = orders.sourceStation,
                    destStation = orders.destStation,
                    trainNumber = orders.trainNumber,
                    trainName = orders.trainName,
                    Class = orders.Class,
                    reserveUpto = orders.reserveUpto,
                    bordingPoint = orders.bordingPoint,
                    journeyDay = orders.journeyDay,
                    journeyMonth = orders.journeyMonth,
                    journeyYear = orders.journeyYear,
                    quota = q == null ? "" : q.shortName,
                    bordingDay = t.bordingDay,
                    bordingMonth = t.bordingMonth,
                    bordingYear = t.bordingYear,
                    ticketStatus = ticketStatus.Any() ? ticketStatus.First().shortName : "",
                    ticketInsurance = insurances.ToList().Any() == true ? insurances.First() : null,
                    paymentGatewayName = orders.pmtGatewayName,
                    ticketFare = t.ticketFare,
                    irctcServiceCharge = t.irctcServiceCharge,
                    pgCharges = t.pgCharges,
                    roCommissionOnPGCharge = t.roCommissionOnPGCharge,
                    oxigenServiceCharge = orders.oxigenServiceCharge,
                    totalFare = t.totalFare,
                    refundHistoryList = refunded.Any() ? refunded : null
                }).SingleOrDefault();
            return ticketOrder;
        }

          /// <summary>
        //  This Method will return the List of passengers. 
        /// </summary>
        /// <param name="ticketOrderId">Id of ticket orders.</param>
        /// <returns>this will return the ticket passenger list object</returns>
        public List<TicketPassengers> PassengerList(Int64 ticketOrderId)
        {
            if (ticketOrderId <= 0)
            {
                return null;
            }
            var list = _ticketPassengersRepository.FindBy(x => x.ticketOrderId == ticketOrderId).AsQueryable();
            if (!list.Any())
            {
                return null;
            }
           return list.ToList();
        }

          /// <summary>
        //  This Method will return the Booked ticket list. 
        /// </summary>
        /// <param name="BookedTicketFilter">Class for ticket filtering.</param>
        /// <returns>this will return the booked ticket view model object</returns>
        public List<BookedTicketViewModel> BookedTicketList(BookedTicketFilter filter)
        {

            var ticketOrdersList = _ticketOrdersRepository.FindBy(x => x.loginAccountNo == filter.agentAccountNo && x.ticketStatus == filter.ticketStatus);
            var bookedTicketList = _bookedTicketsRepository.GetAll();
            var ticketStatus = _ticketStatusRepository.GetAll();

            DateTime defaultDate = Convert.ToDateTime("01/01/1900");
            if (!ticketOrdersList.Any() || !bookedTicketList.Any() || !ticketStatus.Any())
            {
                return null;
            }

            var ticketList = (from t in ticketOrdersList.ToList()
                             join b in bookedTicketList.ToList() on t.Id equals b.ticketOrderId
                             join s in ticketStatus.ToList() on t.ticketStatus equals s.Id
                             select new BookedTicketViewModel
                             {
                                 Id = b.Id,
                                 ticketOrderId = t.Id,
                                 irctcTxnNumber=t.irctcTxnNumber,
                                 ticketNumber = b.ticketNumber,
                                 pnrNumber = b.pnrNumber,
                                 sourceStation = t.sourceStation,
                                 destStation = t.destStation,
                                 journeyDay = t.journeyDay,
                                 journeyMonth = t.journeyMonth,
                                 journeyYear = t.journeyYear,
                                 bookedOn = b.bookedOn,
                                 status = s.shortName
                             }).AsQueryable();

            if (!string.IsNullOrEmpty(filter.pnr))
                ticketList = ticketList.Where(x => x.pnrNumber == filter.pnr);

            if (!string.IsNullOrEmpty(filter.fromStation))
                ticketList = ticketList.Where(x => x.sourceStation == filter.fromStation);

            if (!string.IsNullOrEmpty(filter.toStation))
                ticketList = ticketList.Where(x => x.destStation == filter.toStation);

            if (!string.IsNullOrEmpty(filter.ticketNumber))
                ticketList = ticketList.Where(x => x.ticketNumber == filter.ticketNumber);

            if (filter.dateOfJourney != defaultDate)
                ticketList = ticketList.Where(x => x.journeyDay == filter.dateOfJourney.Day
                      && x.journeyMonth == filter.dateOfJourney.Month && x.journeyYear == filter.dateOfJourney.Year);

            ticketList.Skip((filter.pageIndex - 1) * filter.pages)
                         .Take(filter.pages);

            return ticketList.ToList();

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}