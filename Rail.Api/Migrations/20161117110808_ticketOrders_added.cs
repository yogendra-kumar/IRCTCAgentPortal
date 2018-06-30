using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mpower.Rail.Api.Migrations
{
    public partial class ticketOrders_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Application_Errors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Source = table.Column<string>(maxLength: 50, nullable: true),
                    applicationID = table.Column<long>(nullable: false),
                    errorDescription = table.Column<string>(maxLength: 1000, nullable: false),
                    errorType = table.Column<string>(maxLength: 50, nullable: false),
                    logDate = table.Column<DateTime>(nullable: false),
                    pageID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application_Errors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Application_Settings",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    applicationID = table.Column<double>(nullable: false),
                    key = table.Column<string>(maxLength: 25, nullable: false),
                    value = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application_Settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_BerthType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    fullName = table.Column<string>(maxLength: 50, nullable: false),
                    shortName = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_BerthType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_BirthType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    type = table.Column<int>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_BirthType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_Configuration",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    active = table.Column<bool>(nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    applicationID = table.Column<string>(maxLength: 450, nullable: true),
                    date = table.Column<DateTime>(nullable: true),
                    description = table.Column<string>(maxLength: 100, nullable: false),
                    key = table.Column<string>(maxLength: 50, nullable: true),
                    value = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_Configuration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_DisputedCases",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    advOrder = table.Column<long>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    disputedCaseTypes = table.Column<int>(nullable: false),
                    eTSbRequest = table.Column<long>(nullable: false),
                    iSB2B = table.Column<bool>(maxLength: 5, nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    lastRetryDate = table.Column<DateTime>(nullable: false),
                    reference = table.Column<long>(nullable: false),
                    resolutionType = table.Column<long>(nullable: false),
                    retryCount = table.Column<long>(nullable: false),
                    sessions = table.Column<long>(nullable: false),
                    status = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_DisputedCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_DisputedCaseTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    ReferenceTable = table.Column<string>(maxLength: 50, nullable: true),
                    identifier = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_DisputedCaseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_IDCardType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    description = table.Column<string>(maxLength: 100, nullable: false, defaultValue: "")
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_IDCardType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_Logs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    description = table.Column<string>(maxLength: 50, nullable: false),
                    host = table.Column<string>(maxLength: 50, nullable: true),
                    logDate = table.Column<DateTime>(nullable: true),
                    session = table.Column<string>(maxLength: 50, nullable: true),
                    source = table.Column<string>(maxLength: 50, nullable: true),
                    type = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_MerchantDeclarations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Agreed = table.Column<bool>(nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    DeclarationDate = table.Column<string>(maxLength: 50, nullable: true),
                    Merchant = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_MerchantDeclarations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_MerchantType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    aCCharge = table.Column<decimal>(nullable: false),
                    acPackageID = table.Column<int>(nullable: false),
                    isOxiCashLogin = table.Column<bool>(nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    loginUrl = table.Column<string>(maxLength: 1000, nullable: true),
                    nonACCharge = table.Column<decimal>(nullable: false),
                    packageId = table.Column<int>(nullable: false),
                    partnerID = table.Column<string>(maxLength: 50, nullable: true),
                    pgCharge = table.Column<string>(maxLength: 5, nullable: true),
                    pgName = table.Column<string>(maxLength: 50, nullable: true),
                    type = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_MerchantType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_Ngetbresponse",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    date = table.Column<DateTime>(nullable: false),
                    response = table.Column<string>(maxLength: 5000, nullable: false),
                    sessions = table.Column<long>(nullable: false),
                    tktOrderID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_Ngetbresponse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_OxiRailReversal",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    gatewayTxnNumber = table.Column<string>(maxLength: 50, nullable: false),
                    isReverse = table.Column<bool>(nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    reversalDate = table.Column<DateTime>(nullable: false),
                    txnDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_OxiRailReversal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_Passengers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    bDay = table.Column<int>(nullable: false),
                    bMonth = table.Column<int>(nullable: false),
                    bYear = table.Column<int>(nullable: false),
                    birthPf = table.Column<string>(maxLength: 20, nullable: true),
                    foodPf = table.Column<string>(maxLength: 16, nullable: true),
                    idCardNumber = table.Column<string>(maxLength: 25, nullable: true),
                    idCardTypeId = table.Column<long>(nullable: false),
                    loginAccount = table.Column<string>(maxLength: 20, nullable: true),
                    name = table.Column<string>(maxLength: 16, nullable: true),
                    senior = table.Column<bool>(nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    sex = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_Passengers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_PaymentCancellationRequests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    TransactionNumber = table.Column<string>(maxLength: 50, nullable: true),
                    amount = table.Column<decimal>(nullable: false),
                    bookedTickets = table.Column<long>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    password = table.Column<string>(maxLength: 50, nullable: true),
                    paymentGateways = table.Column<long>(nullable: false),
                    sessions = table.Column<long>(nullable: false),
                    ticketCancellation = table.Column<long>(nullable: false),
                    transactions = table.Column<long>(nullable: false),
                    vId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_PaymentCancellationRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_PaymentRequests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    amount = table.Column<decimal>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    merchantAccount = table.Column<string>(maxLength: 50, nullable: true),
                    paymentGateways = table.Column<long>(nullable: false),
                    sessions = table.Column<long>(nullable: false),
                    ticketOrders = table.Column<long>(nullable: false),
                    transactionNumber = table.Column<string>(maxLength: 50, nullable: true),
                    vId = table.Column<string>(maxLength: 50, nullable: true),
                    vPassword = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_PaymentRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_PaymentReversalRequests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    amount = table.Column<decimal>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    gatwayTxNumber = table.Column<string>(maxLength: 50, nullable: true),
                    password = table.Column<string>(maxLength: 50, nullable: true),
                    paymentGateways = table.Column<long>(nullable: false),
                    sessions = table.Column<long>(nullable: false),
                    ticketOrders = table.Column<long>(nullable: false),
                    transactionNumber = table.Column<string>(maxLength: 50, nullable: true),
                    transactions = table.Column<long>(nullable: false),
                    vId = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_PaymentReversalRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_AccountType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    isDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    margin = table.Column<decimal>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_AccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_Merchant",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Creater = table.Column<long>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    MerchantID = table.Column<string>(maxLength: 500, nullable: false),
                    Password = table.Column<string>(maxLength: 25, nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_Merchant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_UserAgent",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    IsB2B = table.Column<bool>(nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    IsOxiSmart = table.Column<bool>(nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    accountNumber = table.Column<string>(maxLength: 20, nullable: false),
                    deviceId = table.Column<string>(maxLength: 20, nullable: false),
                    digitalCertificate = table.Column<string>(maxLength: 50, nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: false),
                    isActive = table.Column<bool>(maxLength: 50, nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    isIrctcCardActiveted = table.Column<bool>(nullable: false, defaultValue: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    macId = table.Column<string>(maxLength: 50, nullable: false),
                    merchanrType = table.Column<long>(nullable: false),
                    merchantAccount = table.Column<string>(maxLength: 20, nullable: false),
                    merchantId = table.Column<string>(maxLength: 50, nullable: false),
                    mobileNumber = table.Column<string>(maxLength: 20, nullable: false),
                    panCard = table.Column<string>(maxLength: 15, nullable: false),
                    subUserId = table.Column<string>(maxLength: 20, nullable: false),
                    subUserPassword = table.Column<string>(maxLength: 20, nullable: false),
                    transactions = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_UserAgent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_RoIpAddress",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    date = table.Column<DateTime>(nullable: false),
                    ipAddress = table.Column<string>(maxLength: 12, nullable: true),
                    roMDN = table.Column<int>(maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_RoIpAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_Sessions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    application = table.Column<string>(maxLength: 50, nullable: true),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_StationsCache",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    stnCode = table.Column<int>(maxLength: 10, nullable: false),
                    stnName = table.Column<int>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_StationsCache", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_TdrDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    amountRefunded = table.Column<string>(maxLength: 50, nullable: true),
                    confirmed = table.Column<string>(maxLength: 50, nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: true),
                    isMailSent = table.Column<bool>(nullable: false),
                    mid = table.Column<string>(maxLength: 50, nullable: false),
                    pnr = table.Column<string>(maxLength: 50, nullable: false),
                    psgn1 = table.Column<string>(maxLength: 1, nullable: false),
                    psgn2 = table.Column<string>(maxLength: 1, nullable: false),
                    psgn3 = table.Column<string>(maxLength: 1, nullable: false),
                    psgn4 = table.Column<string>(maxLength: 1, nullable: false),
                    psgn5 = table.Column<string>(maxLength: 1, nullable: false),
                    psgn6 = table.Column<string>(maxLength: 1, nullable: false),
                    reason = table.Column<string>(maxLength: 250, nullable: true),
                    refundedDate = table.Column<DateTime>(nullable: false),
                    status = table.Column<string>(maxLength: 1000, nullable: true),
                    tdrSentDate = table.Column<DateTime>(nullable: false),
                    ticketNumber = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_TdrDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_TicketCancellationRequests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    bookedTickets = table.Column<long>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    passenger1 = table.Column<bool>(nullable: false),
                    passenger2 = table.Column<bool>(nullable: false),
                    passenger3 = table.Column<bool>(nullable: false),
                    passenger4 = table.Column<bool>(nullable: false),
                    passenger5 = table.Column<bool>(nullable: false),
                    passenger6 = table.Column<bool>(nullable: false),
                    sessions = table.Column<long>(nullable: false),
                    ticketNumber = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_TicketCancellationRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_TicketCancellations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    amount = table.Column<decimal>(nullable: false),
                    amountCollected = table.Column<decimal>(nullable: false),
                    bookedTickets = table.Column<long>(nullable: false),
                    cancelledDate = table.Column<string>(maxLength: 50, nullable: true),
                    cashCollected = table.Column<decimal>(nullable: false),
                    cashDeducted = table.Column<decimal>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    paymentGateways = table.Column<long>(nullable: false),
                    refundedAmount = table.Column<decimal>(nullable: false),
                    sessions = table.Column<long>(nullable: false),
                    status = table.Column<bool>(nullable: false),
                    ticketNumber = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_TicketCancellations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_TrainsOnRoute",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    date = table.Column<DateTime>(nullable: false),
                    day = table.Column<int>(nullable: false),
                    fromStn = table.Column<string>(maxLength: 50, nullable: true),
                    irctcXML = table.Column<int>(nullable: false),
                    toStn = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_TrainsOnRoute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_Transactions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    amount = table.Column<decimal>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    merchantAccount = table.Column<string>(maxLength: 50, nullable: true),
                    paymentGateways = table.Column<long>(nullable: false),
                    pgTransactionNumber = table.Column<string>(maxLength: 50, nullable: true),
                    remark = table.Column<string>(maxLength: 200, nullable: true),
                    sessions = table.Column<long>(nullable: false),
                    ticketCancellations = table.Column<long>(nullable: false),
                    ticketOrders = table.Column<long>(nullable: false),
                    transactionNumber = table.Column<string>(maxLength: 50, nullable: true),
                    transactionTypes = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_TransactionTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    trasactionType = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_TransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_TravelList",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    description = table.Column<string>(maxLength: 50, nullable: true),
                    listName = table.Column<string>(maxLength: 20, nullable: true),
                    loginAccount = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_TravelList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_TravelPassengerLists",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    passenger = table.Column<long>(nullable: false),
                    travelList = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_TravelPassengerLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_UserRegistration",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2016, 11, 17, 16, 38, 8, 339, DateTimeKind.Local))
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    deviceId = table.Column<string>(maxLength: 50, nullable: true),
                    digitalCertificate = table.Column<string>(maxLength: 50, nullable: true),
                    email = table.Column<string>(maxLength: 50, nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    isIRCTCActivated = table.Column<bool>(nullable: false),
                    macId = table.Column<string>(maxLength: 50, nullable: true),
                    merchantAccount = table.Column<string>(maxLength: 20, nullable: true),
                    merchantId = table.Column<string>(maxLength: 250, nullable: false),
                    mobNo = table.Column<string>(maxLength: 12, nullable: true),
                    pancard = table.Column<string>(maxLength: 20, nullable: true),
                    subUserId = table.Column<string>(maxLength: 20, nullable: true),
                    subUserPassword = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_UserRegistration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rail_TicketOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Class = table.Column<string>(maxLength: 5, nullable: false),
                    accountNumber = table.Column<string>(maxLength: 20, nullable: false),
                    bookingDate = table.Column<DateTime>(nullable: false),
                    bookingSource = table.Column<string>(maxLength: 50, nullable: false),
                    bordingPoint = table.Column<string>(maxLength: 5, nullable: true),
                    destStation = table.Column<string>(maxLength: 5, nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: false),
                    idCardNumber = table.Column<string>(maxLength: 16, nullable: false),
                    idCardType = table.Column<int>(maxLength: 15, nullable: false),
                    irctcServiceCharge = table.Column<decimal>(nullable: false),
                    irctcTxnNumber = table.Column<string>(maxLength: 20, nullable: false),
                    isTatkal = table.Column<string>(maxLength: 1, nullable: false),
                    journeyDay = table.Column<int>(nullable: false),
                    journeyMonth = table.Column<int>(nullable: false),
                    journeyYear = table.Column<int>(nullable: false),
                    loginAccountNo = table.Column<string>(maxLength: 50, nullable: false),
                    mobileNo = table.Column<string>(maxLength: 15, nullable: false),
                    operatorCode = table.Column<string>(maxLength: 15, nullable: false),
                    oxigenServiceCharge = table.Column<decimal>(nullable: false),
                    paymentGateway = table.Column<long>(nullable: false),
                    pmtCardType = table.Column<string>(maxLength: 15, nullable: false),
                    pmtGatewayName = table.Column<string>(maxLength: 15, nullable: false),
                    psgAddress = table.Column<string>(maxLength: 100, nullable: false),
                    quota = table.Column<int>(nullable: false),
                    reservationChoice = table.Column<string>(maxLength: 1, nullable: false),
                    reserveUpto = table.Column<string>(maxLength: 5, nullable: false),
                    session = table.Column<long>(nullable: false),
                    sourceStation = table.Column<string>(maxLength: 5, nullable: false),
                    ticketAmt = table.Column<decimal>(nullable: false),
                    ticketStatus = table.Column<int>(nullable: false),
                    ticketVerificationKey = table.Column<string>(maxLength: 500, nullable: false),
                    totalAmt = table.Column<decimal>(nullable: false),
                    trainName = table.Column<string>(maxLength: 50, nullable: false),
                    trainNumber = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rail_TicketOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Application_Errors");

            migrationBuilder.DropTable(
                name: "Application_Settings");

            migrationBuilder.DropTable(
                name: "Rail_BerthType");

            migrationBuilder.DropTable(
                name: "Rail_BirthType");

            migrationBuilder.DropTable(
                name: "Rail_Configuration");

            migrationBuilder.DropTable(
                name: "Rail_DisputedCases");

            migrationBuilder.DropTable(
                name: "Rail_DisputedCaseTypes");

            migrationBuilder.DropTable(
                name: "Rail_IDCardType");

            migrationBuilder.DropTable(
                name: "Rail_Logs");

            migrationBuilder.DropTable(
                name: "Rail_MerchantDeclarations");

            migrationBuilder.DropTable(
                name: "Rail_MerchantType");

            migrationBuilder.DropTable(
                name: "Rail_Ngetbresponse");

            migrationBuilder.DropTable(
                name: "Rail_OxiRailReversal");

            migrationBuilder.DropTable(
                name: "Rail_Passengers");

            migrationBuilder.DropTable(
                name: "Rail_PaymentCancellationRequests");

            migrationBuilder.DropTable(
                name: "Rail_PaymentRequests");

            migrationBuilder.DropTable(
                name: "Rail_PaymentReversalRequests");

            migrationBuilder.DropTable(
                name: "Rail_AccountType");

            migrationBuilder.DropTable(
                name: "Rail_Merchant");

            migrationBuilder.DropTable(
                name: "Rail_UserAgent");

            migrationBuilder.DropTable(
                name: "Rail_RoIpAddress");

            migrationBuilder.DropTable(
                name: "Rail_Sessions");

            migrationBuilder.DropTable(
                name: "Rail_StationsCache");

            migrationBuilder.DropTable(
                name: "Rail_TdrDetails");

            migrationBuilder.DropTable(
                name: "Rail_TicketCancellationRequests");

            migrationBuilder.DropTable(
                name: "Rail_TicketCancellations");

            migrationBuilder.DropTable(
                name: "Rail_TrainsOnRoute");

            migrationBuilder.DropTable(
                name: "Rail_Transactions");

            migrationBuilder.DropTable(
                name: "Rail_TransactionTypes");

            migrationBuilder.DropTable(
                name: "Rail_TravelList");

            migrationBuilder.DropTable(
                name: "Rail_TravelPassengerLists");

            migrationBuilder.DropTable(
                name: "Rail_UserRegistration");

            migrationBuilder.DropTable(
                name: "Rail_TicketOrders");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
