
using System;
using System.Collections.Generic;
using Mpower.Rail.Data;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;
using Mpower.Rail.Model.Rail;
using Mpower.Rail.Model.ViewModel;
using System.Linq;

namespace Mpower.Rail.Processor.Application
{
    public class TdrProcessor : ITdrProcessor
    {
        private readonly ApplicationDbContext _dbContext = null;

        // private ILogsRepository _logsRepository;
        private ITdrsRepository _tdrRepository;
        private ITicketOrdersRepository _ticketOrdersRepository;
        private IBookedTicketsRepository _bookedTicketsRepository;

        private ITdrReasonsRepository _tdrReasonsRepository;

        public TdrProcessor(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _ticketOrdersRepository = new TicketOrdersRepository(_dbContext);
            _bookedTicketsRepository = new BookedTicketsRepository(_dbContext);
            _tdrRepository = new TdrsRepository(_dbContext);
            _tdrReasonsRepository=new TdrReasonsRepository(_dbContext);
        }

        ~TdrProcessor()
        {
            _dbContext.Dispose();
            _tdrRepository = null;
            _ticketOrdersRepository = null;
            _bookedTicketsRepository = null;
            _tdrReasonsRepository =null;
        }

        public List<TdrViewModel> GetTdrDetailList(string Roid)
        {
            if (string.IsNullOrWhiteSpace(Roid))
            {
                return null;
            }
            // var list = _tdrsRepository.FindBy(x => x.Roid == Roid).AsQueryable();
           // var list = _tdrsRepository.GetAll();
           //var xyz=_ticketOrdersRepository.GetAll()
            var ticketOrders = _ticketOrdersRepository.GetAll();
            var bookedTickets = _bookedTicketsRepository.GetAll();
            List<TdrViewModel> TdrModel = new List<TdrViewModel>();
            // var entities = (from s in _ticketOrdersRepository.GetAll()
            //                 join c in _bookedTicketsRepository.GetAll() on s.Id equals c.ticketOrderId
            //                 where s.loginAccountNo == Roid
            //                 select c.pnrNumber);

            
            var entities=(from s in ticketOrders.ToList() 
                          join c in bookedTickets.ToList() on s.Id equals c.ticketOrderId
                          where s.loginAccountNo == Roid
                          select c.pnrNumber);
            

            foreach (var e in entities)
            {
                // var list = _tdrsRepository.FindBy(x => x.Roid == Roid).AsQueryable();
                var tdrlist = _tdrRepository.GetSingle(x => x.pnr == e);
                if(tdrlist!=null)
                {
                TdrModel.Add(
                    new TdrViewModel
                    {
                        Id = tdrlist.Id,
                        pnr = tdrlist.pnr,
                        reasonId = tdrlist.reasonId,
                        passengerId = tdrlist.passengerId,
                        date = tdrlist.date,
                        status = tdrlist.status,
                        amountRefunded = tdrlist.amountRefunded,
                        refundedDate = tdrlist.refundedDate,
                        email = tdrlist.email,
                        isMailSent = tdrlist.isMailSent,
                        tdrSentDate = tdrlist.tdrSentDate,
                        confirmed = tdrlist.confirmed
                    });
                }
            }

                if (!TdrModel.Any())
                {
                    return null;
                }
                return TdrModel;
        }

        public List<TdrReasons> GetTdrReasonsList()
        {
            return _tdrReasonsRepository.GetAll().Where(x=>x.status==true).ToList();
        }
 
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
    
}