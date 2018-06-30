
using Mpower.Rail.Model.Rail;
using Mpower.Rail.Model.Application;
using Mpower.Rail.Model.Ticket;
using Mpower.Rail.Model.Rail.Master;

namespace Mpower.Rail.Data.IRepository
{
    public interface IMerchantRepository : IEntityBaseRepository<Rail_Merchant>{}
    public interface IApplicationErrorRepository : IEntityBaseRepository<Application_Errors> { }
    public interface IApplicationSettingsRepository : IEntityBaseRepository<Application_Settings>{}
    public interface IConfigurationRepository : IEntityBaseRepository<Configuration>{}
    public interface IUserRegistrationRepository : IEntityBaseRepository<UserRegistration>{}
    public interface ITravelListRepository : IEntityBaseRepository<TravelList>{}
    public interface ITravelPassengerListRepository : IEntityBaseRepository<TravelPassengerLists>{}
    public interface IPassengerRepository : IEntityBaseRepository<Passengers>{}

    public interface ITicketOrdersRepository : IEntityBaseRepository<TicketOrders>{
        TicketOrders AddTicketOrder(TicketOrders objTicketOrdr);
    }

    public interface IBookedTicketsRepository : IEntityBaseRepository<BookedTickets>{}

    public interface ITicketPassengersRepository : IEntityBaseRepository<TicketPassengers>{
        TicketPassengers AddPassenger(TicketPassengers objPassenger);
    }

    public interface IInsuranceRepository : IEntityBaseRepository<InsuranceDetails>{}

    public interface ITicketCancellationRepository : IEntityBaseRepository<TicketCancellations>{
        TicketCancellations AddCancelTicket(TicketCancellations objTicketCancel);
    }

    public interface IBerthTypeRepository : IEntityBaseRepository<BerthType>{}
    public interface IIDCardTypeRepository : IEntityBaseRepository<IDCardType>{}

    public interface IQuotaRepository : IEntityBaseRepository<Quota>{}
    
    public interface ITicketStatusRepository : IEntityBaseRepository<TicketStatus>{}

    public interface ICancelledTicketPassengersRepository : IEntityBaseRepository<CancelledTicketPassengers>{}

    public interface IMerchantTypeRepository : IEntityBaseRepository<MerchantType>{}
     public interface ILogsRepository : IEntityBaseRepository<Logs>{
         bool Insert(Logs log);
     }
    public interface ITdrsRepository : IEntityBaseRepository<TdrDetails>{}

    public interface ITdrReasonsRepository : IEntityBaseRepository<TdrReasons>{}

    public interface IStationCacheRepository : IEntityBaseRepository<StationsCache>{}
}