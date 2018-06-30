
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Mpower.Rail.Model.Application;
using Mpower.Rail.Model.Rail;
using Mpower.Rail.Model.ViewModel;
using Mpower.Rail.Model.Ticket;
using Mpower.Rail.Model.Rail.Master;

namespace Mpower.Rail.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserViewModel>
    {
        public DbSet<Rail_Merchant> Merchants { get; set; }
        public DbSet<Configuration> Configuration { get; set; }
        //public DbSet<Application> ObjApplication { get; set; }
        public DbSet<Application_Settings> ObjApplication_Settings { get; set; }
        public DbSet<Application_Errors> application_Errors { get; set; }
        //public DbSet<ApplicationResponseCodes> ObjApplicationResponseCodes { get; set; }
        public DbSet<TicketOrders> TicketOrders {get;set;}

        public DbSet<Logs> logs {get; set;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // foreach (var entity in builder.Model.GetEntityTypes())
            // {
            //     entity.Relational().TableName = entity.DisplayName();
            // }
            
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            builder.Entity<Rail_Merchant>().ToTable("Rail_Merchant");
            builder.Entity<Rail_Merchant>().Property(s => s.MerchantID);
            builder.Entity<Rail_Merchant>().Property(s => s.UserName).IsRequired();
            builder.Entity<Rail_Merchant>().Property(s => s.Password).IsRequired();
            builder.Entity<Rail_Merchant>().Property(s => s.DateCreated);
            builder.Entity<Rail_Merchant>().Property(s => s.DateUpdated);
            builder.Entity<Rail_Merchant>().Property(s => s.IsActive).HasDefaultValue(false);
            builder.Entity<Rail_Merchant>().Property(s => s.Creater).IsRequired();

            builder.Entity<Application_Errors>().ToTable("Application_Errors");
            builder.Entity<Application_Errors>().Property(s => s.Id).HasColumnName("id");
            builder.Entity<Application_Errors>().Property(s => s.applicationID).IsRequired();
            builder.Entity<Application_Errors>().Property(s => s.errorDescription).IsRequired();
            builder.Entity<Application_Errors>().Property(s => s.errorType).IsRequired();
            builder.Entity<Application_Errors>().Property(s => s.logDate);
            builder.Entity<Application_Errors>().Property(s => s.pageID).IsRequired();
            builder.Entity<Application_Errors>().Property(s => s.Source).HasColumnName("source");

            builder.Entity<Application_Settings>().ToTable("Application_Settings");
            builder.Entity<Application_Settings>().Property(s=>s.Id).HasColumnName("id");
            builder.Entity<Application_Settings>().Property(s=>s.applicationID).IsRequired();
            builder.Entity<Application_Settings>().Property(s=>s.key).IsRequired();
            builder.Entity<Application_Settings>().Property(s=>s.value).IsRequired();

            builder.Entity<Configuration>().ToTable("Rail_Configuration");
            builder.Entity<Configuration>().Property(s => s.Id);
            builder.Entity<Configuration>().Property(s => s.description).IsRequired();
            builder.Entity<Configuration>().Property(s => s.key);
            builder.Entity<Configuration>().Property(s => s.value);
            builder.Entity<Configuration>().Property(s => s.applicationID);
            builder.Entity<Configuration>().Property(s => s.active).HasDefaultValue(false);
            builder.Entity<Configuration>().Property(s => s.date);

            builder.Entity<Rail_AccountType>().ToTable("Rail_AccountType");
            builder.Entity<Rail_AccountType>().Property(s => s.Id);
            builder.Entity<Rail_AccountType>().Property(s => s.name).IsRequired();
            builder.Entity<Rail_AccountType>().Property(s => s.margin).IsRequired();
            builder.Entity<Rail_AccountType>().Property(s => s.isDeleted).HasDefaultValue(false);

            builder.Entity<Logs>().ToTable("Rail_Logs");
            builder.Entity<Logs>().Property(s => s.Id);
            builder.Entity<Logs>().Property(s => s.type);
            builder.Entity<Logs>().Property(s => s.source);
            builder.Entity<Logs>().Property(s => s.host);
            builder.Entity<Logs>().Property(s => s.description).IsRequired();
            builder.Entity<Logs>().Property(s => s.session);
            builder.Entity<Logs>().Property(s => s.logDate);

            builder.Entity<Rail_UserAgent>().ToTable("Rail_UserAgent");
            builder.Entity<Rail_UserAgent>().Property(s => s.Id);
            builder.Entity<Rail_UserAgent>().Property(s => s.merchantAccount).IsRequired();
            builder.Entity<Rail_UserAgent>().Property(s => s.transactions);
            builder.Entity<Rail_UserAgent>().Property(s => s.email).IsRequired();
            builder.Entity<Rail_UserAgent>().Property(s => s.isActive).HasDefaultValue(false);
            builder.Entity<Rail_UserAgent>().Property(s => s.merchantId).IsRequired();
            builder.Entity<Rail_UserAgent>().Property(s => s.merchanrType);
            builder.Entity<Rail_UserAgent>().Property(s => s.subUserId).IsRequired();
            builder.Entity<Rail_UserAgent>().Property(s => s.subUserPassword).IsRequired();
            builder.Entity<Rail_UserAgent>().Property(s => s.accountNumber).IsRequired();
            builder.Entity<Rail_UserAgent>().Property(s => s.panCard).IsRequired();
            builder.Entity<Rail_UserAgent>().Property(s => s.digitalCertificate).IsRequired();
            builder.Entity<Rail_UserAgent>().Property(s => s.deviceId).IsRequired();
            builder.Entity<Rail_UserAgent>().Property(s => s.mobileNumber).IsRequired();
            builder.Entity<Rail_UserAgent>().Property(s => s.isIrctcCardActiveted).HasDefaultValue(false);
            builder.Entity<Rail_UserAgent>().Property(s => s.IsB2B).HasDefaultValue(false);
            builder.Entity<Rail_UserAgent>().Property(s => s.IsOxiSmart).HasDefaultValue(false);
            builder.Entity<Rail_UserAgent>().Property(s => s.macId).IsRequired();

            // MerchantDeclarations Table
            builder.Entity<MerchantDeclarations>().ToTable("Rail_MerchantDeclarations");
            builder.Entity<MerchantDeclarations>().Property(s => s.Id);
            builder.Entity<MerchantDeclarations>().Property(s => s.Merchant);
            builder.Entity<MerchantDeclarations>().Property(s => s.DeclarationDate);
            builder.Entity<MerchantDeclarations>().Property(s => s.Agreed).HasDefaultValue(false);

            // MerchantType Table
            builder.Entity<MerchantType>().ToTable("Rail_MerchantType");
            builder.Entity<MerchantType>().Property(s => s.Id);
            builder.Entity<MerchantType>().Property(s => s.merchantId);
            builder.Entity<MerchantType>().Property(s => s.nonACCharge);
            builder.Entity<MerchantType>().Property(s => s.aCCharge);
            builder.Entity<MerchantType>().Property(s => s.packageId);
            builder.Entity<MerchantType>().Property(s => s.pgName);
            builder.Entity<MerchantType>().Property(s => s.isOxiCashLogin).HasDefaultValue(false);
            builder.Entity<MerchantType>().Property(s => s.loginUrl);
            builder.Entity<MerchantType>().Property(s => s.partnerID);
            builder.Entity<MerchantType>().Property(s => s.pgChargeUnderCondition);
            builder.Entity<MerchantType>().Property(s=>s.pgChargeBeyondCondition);
            builder.Entity<MerchantType>().Property(s=>s.conditionalAmount);
            builder.Entity<MerchantType>().Property(s => s.acPackageID);
            builder.Entity<MerchantType>().Property(s=>s.oxigenServiceCharge);
            builder.Entity<MerchantType>().Property(s=>s.merchantCommission);

            // DisputedCases Table
            builder.Entity<DisputedCases>().ToTable("Rail_DisputedCases");
            builder.Entity<DisputedCases>().Property(s => s.Id);
            builder.Entity<DisputedCases>().Property(s => s.disputedCaseTypes);
            builder.Entity<DisputedCases>().Property(s => s.reference);
            builder.Entity<DisputedCases>().Property(s => s.status);
            builder.Entity<DisputedCases>().Property(s => s.retryCount);
            builder.Entity<DisputedCases>().Property(s => s.lastRetryDate);
            builder.Entity<DisputedCases>().Property(s => s.resolutionType);
            builder.Entity<DisputedCases>().Property(s => s.sessions);
            builder.Entity<DisputedCases>().Property(s => s.date);
            builder.Entity<DisputedCases>().Property(s => s.advOrder);
            builder.Entity<DisputedCases>().Property(s => s.eTSbRequest);
            builder.Entity<DisputedCases>().Property(s => s.iSB2B).HasDefaultValue(false);

            // DisputedCaseTypes Table
            builder.Entity<DisputedCaseTypes>().ToTable("Rail_DisputedCaseTypes");
            builder.Entity<DisputedCaseTypes>().Property(s => s.Id);
            builder.Entity<DisputedCaseTypes>().Property(s => s.identifier);
            builder.Entity<DisputedCaseTypes>().Property(s => s.Description);
            builder.Entity<DisputedCaseTypes>().Property(s => s.ReferenceTable);

            // Ngetbresponse Table      
            builder.Entity<Ngetbresponse>().ToTable("Rail_Ngetbresponse");
            builder.Entity<Ngetbresponse>().Property(s => s.Id);
            builder.Entity<Ngetbresponse>().Property(s => s.tktOrderID).IsRequired();
            builder.Entity<Ngetbresponse>().Property(s => s.response).IsRequired();
            builder.Entity<Ngetbresponse>().Property(s => s.date).IsRequired();
            builder.Entity<Ngetbresponse>().Property(s => s.sessions);

            // OxiRailReversal Table
            builder.Entity<OxiRailReversal>().ToTable("Rail_OxiRailReversal");
            builder.Entity<OxiRailReversal>().Property(s => s.Id);
            builder.Entity<OxiRailReversal>().Property(s => s.gatewayTxnNumber).IsRequired();
            builder.Entity<OxiRailReversal>().Property(s => s.txnDate).IsRequired();
            builder.Entity<OxiRailReversal>().Property(s => s.isReverse).HasDefaultValue(false);
            builder.Entity<OxiRailReversal>().Property(s => s.reversalDate);

            // Passengers Table
            builder.Entity<Passengers>().ToTable("Rail_Passengers");
            builder.Entity<Passengers>().Property(s => s.Id);
            builder.Entity<Passengers>().Property(s => s.loginAccount);
            builder.Entity<Passengers>().Property(s => s.name);
            builder.Entity<Passengers>().Property(s => s.bDay);
            builder.Entity<Passengers>().Property(s => s.bMonth);
            builder.Entity<Passengers>().Property(s => s.bYear);
            builder.Entity<Passengers>().Property(s => s.sex);
            builder.Entity<Passengers>().Property(s => s.birthPf);
            builder.Entity<Passengers>().Property(s => s.sex);
            builder.Entity<Passengers>().Property(s => s.idCardTypeId);
            builder.Entity<Passengers>().Property(s => s.idCardNumber);
            builder.Entity<Passengers>().Property(s => s.senior).HasDefaultValue(false);

            // PaymentCancellationRequests Table
            builder.Entity<PaymentCancellationRequests>().ToTable("Rail_PaymentCancellationRequests");
            builder.Entity<PaymentCancellationRequests>().Property(s => s.Id);
            builder.Entity<PaymentCancellationRequests>().Property(s => s.ticketCancellation);
            builder.Entity<PaymentCancellationRequests>().Property(s => s.bookedTickets);
            builder.Entity<PaymentCancellationRequests>().Property(s => s.transactions);
            builder.Entity<PaymentCancellationRequests>().Property(s => s.TransactionNumber);
            builder.Entity<PaymentCancellationRequests>().Property(s => s.amount);
            builder.Entity<PaymentCancellationRequests>().Property(s => s.paymentGateways);
            builder.Entity<PaymentCancellationRequests>().Property(s => s.sessions);
            builder.Entity<PaymentCancellationRequests>().Property(s => s.date);
            builder.Entity<PaymentCancellationRequests>().Property(s => s.vId);
            builder.Entity<PaymentCancellationRequests>().Property(s => s.password);

            // PaymentRequests Table
            builder.Entity<PaymentRequests>().ToTable("Rail_PaymentRequests");
            builder.Entity<PaymentRequests>().Property(s => s.Id);
            builder.Entity<PaymentRequests>().Property(s => s.ticketOrders);
            builder.Entity<PaymentRequests>().Property(s => s.merchantAccount);
            builder.Entity<PaymentRequests>().Property(s => s.transactionNumber);
            builder.Entity<PaymentRequests>().Property(s => s.amount);
            builder.Entity<PaymentRequests>().Property(s => s.paymentGateways);
            builder.Entity<PaymentRequests>().Property(s => s.sessions);
            builder.Entity<PaymentRequests>().Property(s => s.date);
            builder.Entity<PaymentRequests>().Property(s => s.vId);
            builder.Entity<PaymentRequests>().Property(s => s.vPassword);

            // PaymentReversalRequests Table
            builder.Entity<PaymentReversalRequests>().ToTable("Rail_PaymentReversalRequests");
            builder.Entity<PaymentReversalRequests>().Property(s => s.Id);
            builder.Entity<PaymentReversalRequests>().Property(s => s.ticketOrders);
            builder.Entity<PaymentReversalRequests>().Property(s => s.transactions);
            builder.Entity<PaymentReversalRequests>().Property(s => s.transactionNumber);
            builder.Entity<PaymentReversalRequests>().Property(s => s.amount);
            builder.Entity<PaymentReversalRequests>().Property(s => s.paymentGateways);
            builder.Entity<PaymentReversalRequests>().Property(s => s.sessions);
            builder.Entity<PaymentReversalRequests>().Property(s => s.date);
            builder.Entity<PaymentReversalRequests>().Property(s => s.vId);
            builder.Entity<PaymentReversalRequests>().Property(s => s.password);
            builder.Entity<PaymentReversalRequests>().Property(s => s.gatwayTxNumber);

            // RoIpAddress Table
            builder.Entity<RoIpAddress>().ToTable("Rail_RoIpAddress");
            builder.Entity<RoIpAddress>().Property(s => s.Id);
            builder.Entity<RoIpAddress>().Property(s => s.roMDN);
            builder.Entity<RoIpAddress>().Property(s => s.date);
            builder.Entity<RoIpAddress>().Property(s => s.ipAddress);

            // sessions Table
            builder.Entity<Sessions>().ToTable("Rail_Sessions");
            builder.Entity<Sessions>().Property(s => s.Id);
            builder.Entity<Sessions>().Property(s => s.application);
            builder.Entity<Sessions>().Property(s => s.date);

            // StationsCache Table
            builder.Entity<StationsCache>().ToTable("Rail_StationsCache");
            builder.Entity<StationsCache>().Property(s => s.Id);
            builder.Entity<StationsCache>().Property(s => s.stnCode);
            builder.Entity<StationsCache>().Property(s => s.stnName);

            // TdrDetails Table
            builder.Entity<TdrDetails>().ToTable("Rail_TdrDetails");
            builder.Entity<TdrDetails>().Property(s => s.Id);        
            builder.Entity<TdrDetails>().Property(s => s.pnr).IsRequired();
            builder.Entity<TdrDetails>().Property(s => s.reasonId);
            builder.Entity<TdrDetails>().Property(s => s.passengerId).IsRequired();
            builder.Entity<TdrDetails>().Property(s => s.date).IsRequired();
            builder.Entity<TdrDetails>().Property(s => s.status);
            builder.Entity<TdrDetails>().Property(s => s.amountRefunded).IsRequired();
            builder.Entity<TdrDetails>().Property(s => s.refundedDate);
            builder.Entity<TdrDetails>().Property(s => s.email);
            builder.Entity<TdrDetails>().Property(s => s.isMailSent).IsRequired();
            builder.Entity<TdrDetails>().Property(s => s.tdrSentDate);
            builder.Entity<TdrDetails>().Property(s => s.confirmed);

            // TicketCancellationRequests Table
            builder.Entity<TicketCancellationRequests>().ToTable("Rail_TicketCancellationRequests");
            builder.Entity<TicketCancellationRequests>().Property(s => s.Id);
            builder.Entity<TicketCancellationRequests>().Property(s => s.bookedTicketId);
            builder.Entity<TicketCancellationRequests>().Property(s => s.ticketNumber);
            builder.Entity<TicketCancellationRequests>().Property(s => s.passenger1);
            builder.Entity<TicketCancellationRequests>().Property(s => s.passenger2);
            builder.Entity<TicketCancellationRequests>().Property(s => s.passenger3);
            builder.Entity<TicketCancellationRequests>().Property(s => s.passenger4);
            builder.Entity<TicketCancellationRequests>().Property(s => s.passenger5);
            builder.Entity<TicketCancellationRequests>().Property(s => s.passenger6);
            builder.Entity<TicketCancellationRequests>().Property(s => s.date);
            builder.Entity<TicketCancellationRequests>().Property(s => s.sessions);

            // TicketCancellations Table
            builder.Entity<TicketCancellations>().ToTable("Rail_TicketCancellations");
            builder.Entity<TicketCancellations>().Property(s => s.Id);
            builder.Entity<TicketCancellations>().Property(s => s.bookedTicketId).IsRequired();
            builder.Entity<TicketCancellations>().Property(s=>s.noOfPassenger);
            builder.Entity<TicketCancellations>().Property(s => s.ticketNumber);
            builder.Entity<TicketCancellations>().Property(s => s.status);
            builder.Entity<TicketCancellations>().Property(s => s.amountCollected);
            builder.Entity<TicketCancellations>().Property(s => s.refundedAmount);
            builder.Entity<TicketCancellations>().Property(s => s.cashDeducted);
            builder.Entity<TicketCancellations>().Property(s => s.cashCollected);
            builder.Entity<TicketCancellations>().Property(s => s.amount);
            builder.Entity<TicketCancellations>().Property(s => s.paymentGateways);
            builder.Entity<TicketCancellations>().Property(s => s.sessions);
            builder.Entity<TicketCancellations>().Property(s => s.date);
            builder.Entity<TicketCancellations>().Property(s => s.cancelledDate);

            // TrainsOnRoute Table
            builder.Entity<TrainsOnRoute>().ToTable("Rail_TrainsOnRoute");
            builder.Entity<TrainsOnRoute>().Property(s => s.Id);
            builder.Entity<TrainsOnRoute>().Property(s => s.fromStn);
            builder.Entity<TrainsOnRoute>().Property(s => s.toStn);
            builder.Entity<TrainsOnRoute>().Property(s => s.day);
            builder.Entity<TrainsOnRoute>().Property(s => s.irctcXML);
            builder.Entity<TrainsOnRoute>().Property(s => s.date);

            // Transactions Table
            builder.Entity<Transactions>().ToTable("Rail_Transactions");
            builder.Entity<Transactions>().Property(s => s.Id);
            builder.Entity<Transactions>().Property(s => s.transactionTypes);
            builder.Entity<Transactions>().Property(s => s.transactionNumber);
            builder.Entity<Transactions>().Property(s => s.merchantAccount);
            builder.Entity<Transactions>().Property(s => s.ticketOrders);
            builder.Entity<Transactions>().Property(s => s.ticketCancellations);
            builder.Entity<Transactions>().Property(s => s.paymentGateways);
            builder.Entity<Transactions>().Property(s => s.amount);
            builder.Entity<Transactions>().Property(s => s.remark);
            builder.Entity<Transactions>().Property(s => s.sessions);
            builder.Entity<Transactions>().Property(s => s.date);
            builder.Entity<Transactions>().Property(s => s.pgTransactionNumber);

            // TransactionTypes Table
            builder.Entity<TransactionTypes>().ToTable("Rail_TransactionTypes");
            builder.Entity<TransactionTypes>().Property(s => s.Id);
            builder.Entity<TransactionTypes>().Property(s => s.trasactionType);

            // TravelList Table
            builder.Entity<TravelList>().ToTable("Rail_TravelList");
            builder.Entity<TravelList>().Property(s => s.Id);
            builder.Entity<TravelList>().Property(s => s.listName);
            builder.Entity<TravelList>().Property(s => s.description);
            builder.Entity<TravelList>().Property(s => s.loginAccount);

            // TravelPassengerLists Table
            builder.Entity<TravelPassengerLists>().ToTable("Rail_TravelPassengerLists");
            builder.Entity<TravelPassengerLists>().Property(s => s.Id);
            builder.Entity<TravelPassengerLists>().Property(s => s.passenger);
            builder.Entity<TravelPassengerLists>().Property(s => s.travelList);

            // UserRegistration Table
            builder.Entity<UserRegistration>().ToTable("Rail_UserRegistration");
            builder.Entity<UserRegistration>().Property(s => s.Id);
            builder.Entity<UserRegistration>().Property(s => s.merchantAccount);
            //builder.Entity<UserRegistration>().Property(s => s.transactions);
            builder.Entity<UserRegistration>().Property(s => s.email);
            builder.Entity<UserRegistration>().Property(s => s.isActive);
            builder.Entity<UserRegistration>().Property(s => s.merchantId);
            //builder.Entity<UserRegistration>().Property(s => s.date);
            //builder.Entity<UserRegistration>().Property(s => s.merchantType);
            builder.Entity<UserRegistration>().Property(s => s.subUserId);
            builder.Entity<UserRegistration>().Property(s => s.subUserPassword);
            //builder.Entity<UserRegistration>().Property(s => s.accountNumber);
            //builder.Entity<UserRegistration>().Property(s => s.accountPassword);
            //builder.Entity<UserRegistration>().Property(s => s.correct);
            //builder.Entity<UserRegistration>().Property(s => s.isB2B);
            //builder.Entity<UserRegistration>().Property(s => s.userPassword);
            //builder.Entity<UserRegistration>().Property(s => s.corporateName);
            //builder.Entity<UserRegistration>().Property(s => s.firstName);
            //builder.Entity<UserRegistration>().Property(s => s.middleName);
            //builder.Entity<UserRegistration>().Property(s => s.lastName);
            //builder.Entity<UserRegistration>().Property(s => s.gender);
            //builder.Entity<UserRegistration>().Property(s => s.maritalStatus);
            // builder.Entity<UserRegistration>().Property(s => s.dob_day);
            // builder.Entity<UserRegistration>().Property(s => s.dob_month);
            // builder.Entity<UserRegistration>().Property(s => s.dob_year);
            // builder.Entity<UserRegistration>().Property(s => s.occupation);
            // builder.Entity<UserRegistration>().Property(s => s.shippingAddressPref);
            // builder.Entity<UserRegistration>().Property(s => s.residAddress);
            // builder.Entity<UserRegistration>().Property(s => s.residCity);
            // builder.Entity<UserRegistration>().Property(s => s.residState);
            // builder.Entity<UserRegistration>().Property(s => s.residPin);
            // builder.Entity<UserRegistration>().Property(s => s.residPhone);
            // builder.Entity<UserRegistration>().Property(s => s.officeAddress);
            // builder.Entity<UserRegistration>().Property(s => s.officeCity);
            // builder.Entity<UserRegistration>().Property(s => s.officeState);
            // builder.Entity<UserRegistration>().Property(s => s.officeCountry);
            // builder.Entity<UserRegistration>().Property(s => s.officePin);
            // builder.Entity<UserRegistration>().Property(s => s.officePhone);
            // builder.Entity<UserRegistration>().Property(s => s.pnrEmailAlerts);
            // builder.Entity<UserRegistration>().Property(s => s.forgotPswdQn);
            // builder.Entity<UserRegistration>().Property(s => s.forgotPswdAns);
            // builder.Entity<UserRegistration>().Property(s => s.idType);
            // builder.Entity<UserRegistration>().Property(s => s.idNumber);
            // builder.Entity<UserRegistration>().Property(s => s.isOxiSmart);
            builder.Entity<UserRegistration>().Property(s => s.digitalCertificate);
            //builder.Entity<UserRegistration>().Property(s => s.macId);
            builder.Entity<UserRegistration>().Property(s => s.mobNo);
            builder.Entity<UserRegistration>().Property(s => s.isIRCTCActivated);
            builder.Entity<UserRegistration>().Property(s => s.pancard);
            builder.Entity<UserRegistration>().Property(s => s.deviceId);
            builder.Entity<UserRegistration>().Property(s => s.CreatedDate).HasDefaultValue(System.DateTime.Now);
            builder.Entity<UserRegistration>().Property(s => s.UpdatedDate);
            builder.Entity<UserRegistration>().Property(s=>s.UserId).IsRequired();

            // TicketOrders Table
            builder.Entity<TicketOrders>().ToTable("Rail_TicketOrders");
            builder.Entity<TicketOrders>().Property(s => s.Id);
            builder.Entity<TicketOrders>().Property(s => s.sourceStation);
            builder.Entity<TicketOrders>().Property(s => s.destStation);
            builder.Entity<TicketOrders>().Property(s => s.trainNumber);
            builder.Entity<TicketOrders>().Property(s => s.trainName);
            builder.Entity<TicketOrders>().Property(s => s.Class);
            builder.Entity<TicketOrders>().Property(s => s.reserveUpto);
            builder.Entity<TicketOrders>().Property(s => s.bordingPoint);
            builder.Entity<TicketOrders>().Property(s => s.journeyDay);
            builder.Entity<TicketOrders>().Property(s => s.journeyMonth);
            builder.Entity<TicketOrders>().Property(s => s.journeyYear);
            builder.Entity<TicketOrders>().Property(s => s.quota);
            builder.Entity<TicketOrders>().Property(s => s.pmtGatewayName);
            builder.Entity<TicketOrders>().Property(s => s.pmtCardType);
            builder.Entity<TicketOrders>().Property(s => s.accountNumber);
            builder.Entity<TicketOrders>().Property(s => s.irctcServiceCharge);
            builder.Entity<TicketOrders>().Property(s => s.operatorCode);
            builder.Entity<TicketOrders>().Property(s => s.mobileNo);
            builder.Entity<TicketOrders>().Property(s => s.idCardNumber);
            builder.Entity<TicketOrders>().Property(s => s.idCardType);
            builder.Entity<TicketOrders>().Property(s => s.ticketStatus);
            builder.Entity<TicketOrders>().Property(s => s.paymentGateway);
            builder.Entity<TicketOrders>().Property(s => s.session);
            builder.Entity<TicketOrders>().Property(s => s.bookingDate);
            builder.Entity<TicketOrders>().Property(s => s.ticketAmt);
            builder.Entity<TicketOrders>().Property(s => s.totalAmt);
            builder.Entity<TicketOrders>().Property(s => s.oxigenServiceCharge);
            builder.Entity<TicketOrders>().Property(s => s.email);
            builder.Entity<TicketOrders>().Property(s => s.loginAccountNo);
            builder.Entity<TicketOrders>().Property(s => s.bookingSource);
            builder.Entity<TicketOrders>().Property(s => s.reservationChoice);
            builder.Entity<TicketOrders>().Property(s => s.isTatkal);
            builder.Entity<TicketOrders>().Property(s => s.ticketVerificationKey);
            builder.Entity<TicketOrders>().Property(s => s.psgAddress);
            builder.Entity<TicketOrders>().Property(s => s.irctcTxnNumber);
           
            // Berth type
            builder.Entity<BerthType>().ToTable("Rail_BerthType");
            builder.Entity<BerthType>().Property(s => s.Id);
            builder.Entity<BerthType>().Property(s => s.shortName).IsRequired();
            builder.Entity<BerthType>().Property(s => s.fullName).IsRequired();

            // IDCard Type
            builder.Entity<IDCardType>().ToTable("Rail_IDCardType");
            builder.Entity<BerthType>().Property(s => s.Id);
            builder.Entity<IDCardType>().Property(s => s.name).IsRequired();
            builder.Entity<IDCardType>().Property(s => s.description).HasDefaultValue(string.Empty);

            // quota
            builder.Entity<Quota>().ToTable("Rail_Quota");
            builder.Entity<Quota>().Property(s=>s.Id);
            builder.Entity<Quota>().Property(s=>s.description);
            builder.Entity<Quota>().Property(s=>s.fullName);
            builder.Entity<Quota>().Property(s=>s.isActive);
            builder.Entity<Quota>().Property(s=>s.shortName);

            // ticket status 
            builder.Entity<TicketStatus>().ToTable("Rail_TicketStatus");
            builder.Entity<TicketStatus>().Property(s=>s.Id);
            builder.Entity<TicketStatus>().Property(s=>s.description);
            builder.Entity<TicketStatus>().Property(s=>s.fullName);
            builder.Entity<TicketStatus>().Property(s=>s.isActive);
            builder.Entity<TicketStatus>().Property(s=>s.shortName);

            // Booked Tickets     
            builder.Entity<BookedTickets>().ToTable("Rail_BookedTickets");      
            builder.Entity<BookedTickets>().Property(s => s.Id);
            builder.Entity<BookedTickets>().Property(s => s.ticketOrderId);
            builder.Entity<BookedTickets>().Property(s => s.transactionId);
            builder.Entity<BookedTickets>().Property(s => s.ticketNumber);
            builder.Entity<BookedTickets>().Property(s => s.pnrNumber);
            builder.Entity<BookedTickets>().Property(s => s.bordingPoint);
            builder.Entity<BookedTickets>().Property(s => s.departure);
            builder.Entity<BookedTickets>().Property(s => s.bordingDay);
            builder.Entity<BookedTickets>().Property(s => s.bordingMonth);
            builder.Entity<BookedTickets>().Property(s => s.bordingYear);
            builder.Entity<BookedTickets>().Property(s => s.distance);
            builder.Entity<BookedTickets>().Property(s => s.ticketFare);
            builder.Entity<BookedTickets>().Property(s => s.totalFare);
            builder.Entity<BookedTickets>().Property(s => s.irctcServiceCharge);
            builder.Entity<BookedTickets>().Property(s => s.oxigenBalance);
            builder.Entity<BookedTickets>().Property(s => s.bookedOn);
            builder.Entity<BookedTickets>().Property(s => s.slipRouteMessage);
            builder.Entity<BookedTickets>().Property(s => s.serverIP);
            builder.Entity<BookedTickets>().Property(s => s.serverDateTime);
            builder.Entity<BookedTickets>().Property(s => s.pgCharges);
            builder.Entity<BookedTickets>().Property(s => s.roCommissionOnPGCharge);
            builder.Entity<BookedTickets>().Property(s => s.idIssueAuth);

            //Ticket Passengers
            builder.Entity<TicketPassengers>().ToTable("Rail_TicketPassengers");
            builder.Entity<TicketPassengers>().Property(s => s.Id);
            builder.Entity<TicketPassengers>().Property(s => s.ticketOrderId);
            builder.Entity<TicketPassengers>().Property(s => s.name);
            builder.Entity<TicketPassengers>().Property(s => s.sex);
            builder.Entity<TicketPassengers>().Property(s => s.age);
            builder.Entity<TicketPassengers>().Property(s => s.berthPreference);
            builder.Entity<TicketPassengers>().Property(s => s.foodPreference);
            builder.Entity<TicketPassengers>().Property(s => s.concessionCode);
            builder.Entity<TicketPassengers>().Property(s => s.idCardType);
            builder.Entity<TicketPassengers>().Property(s => s.idCardNumber);
            builder.Entity<TicketPassengers>().Property(s => s.firstChildName);
            builder.Entity<TicketPassengers>().Property(s => s.firstChildAge);
            builder.Entity<TicketPassengers>().Property(s => s.firstChildSex);
            builder.Entity<TicketPassengers>().Property(s => s.secondChildName);
            builder.Entity<TicketPassengers>().Property(s => s.secondChildAge);
            builder.Entity<TicketPassengers>().Property(s => s.secondChildSex);
            builder.Entity<TicketPassengers>().Property(s => s.bedRoll);
            builder.Entity<TicketPassengers>().Property(s => s.bookingStatus);
            builder.Entity<TicketPassengers>().Property(s => s.coach);
            builder.Entity<TicketPassengers>().Property(s => s.seat);
            builder.Entity<TicketPassengers>().Property(s => s.berth);
            builder.Entity<TicketPassengers>().Property(s => s.currentStatus);

            //Insurance Details
            builder.Entity<InsuranceDetails>().ToTable("Rail_InsuranceDetails");
            builder.Entity<InsuranceDetails>().Property(s=>s.Id);
            builder.Entity<InsuranceDetails>().Property(s=>s.pnrNumber);
            builder.Entity<InsuranceDetails>().Property(s=>s.insuranceIssued);
            builder.Entity<InsuranceDetails>().Property(s=>s.insuranceCharge);
            builder.Entity<InsuranceDetails>().Property(s=>s.insurancePassangerCount);
            builder.Entity<InsuranceDetails>().Property(s=>s.insuranceCompany);
            builder.Entity<InsuranceDetails>().Property(s=>s.policyIssueDate);
            builder.Entity<InsuranceDetails>().Property(s=>s.policyStatus);

            //Cancelled ticket passenger id
            builder.Entity<CancelledTicketPassengers>().ToTable("Rail_CancelledTicketPassengers");
            builder.Entity<CancelledTicketPassengers>().Property(s=>s.Id);
            builder.Entity<CancelledTicketPassengers>().Property(s=>s.passengerId);
            builder.Entity<CancelledTicketPassengers>().Property(s=>s.ticketId);

            //TdrReasons
            builder.Entity<TdrReasons>().ToTable("Rail_TdrReasons");
            builder.Entity<TdrReasons>().Property(s=>s.Id);
            builder.Entity<TdrReasons>().Property(s=>s.Reasons);
            builder.Entity<TdrReasons>().Property(s=>s.status).HasDefaultValue(true);

            base.OnModelCreating(builder);
        }
    }
}
