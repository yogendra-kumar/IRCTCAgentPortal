using System;
using Mpower.Rail.Model.Rail;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Mpower.Rail.Model.ViewModel;


namespace Mpower.Rail.Processor.User
{
    public interface IUserProcessor : IDisposable
    {        
        /// <summary>
        //  This Method will return the list of Merchant's agent's list. 
        /// </summary>
        /// <param name="MerchantId">MerchantId of logged-In merchant, who want to access their agents list.</param>
        /// <returns>this will return the UserRegistration list object</returns>
        List<UserRegistration> GetMerchantAgent(string MerchantId);
     

        /// <summary>
        //  This Method will return the list of Merchant's agent's list. 
        /// </summary>
        /// <param name="userManager">Object of Identity usermanager class injected on ConfigureServices.</param>
        /// <param name="userDetail">Object of UserViewModel class to register the agent detail.</param>
        /// <returns>this will return the UserRegistration object with user detail.</returns>
        UserRegistration RegisterAgent(UserManager<UserViewModel> userManager, Mpower.Rail.Model.Request.UserRegistration userDetail);
        

        /// <summary>
        /// This Api will Update existing User
        /// </summary>
        /// <param name="userUpdate">req is an object type of userUpdate class</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        Boolean UpdateUser(Mpower.Rail.Model.Request.UserUpdate userUpdate);
        
        /// <summary>
        /// This API will Activate new created User
        /// </summary>
        /// <param name="merchantId">This Api Accept MerchantId As a query string</param>
        /// <param name="userId">This Api Accept Userid As a query string</param>
        /// <param name="status">This actionmethod accepts boolean value to Activate/De-Activate user in query string</param>
        /// <returns>This Api will rerurn boolean.</returns>
        Boolean ChangeUserStatus(string merchantId, string userId, bool status);
        
        /// <summary>
        //  This Method will return the agent's detail by AccountNumber. 
        /// </summary>
        /// <param name="AccountNumber">AccountNumber of logged-In agent.</param>
        /// <returns>this will return the UserRegistration object</returns>
        UserRegistration GetAgentByAccountNumber(string AccountNumber);      

         /// <summary>
        //  This Method will return true or false based on agent's email. 
        /// </summary>
        /// <param name="Email">email of registering agent.</param>
        /// <returns>this will return true or false</returns>
        Boolean UserExists(string email);

         /// <summary>
        //  This Method will check if same IRCTC credentials for this merchant exists with another agent's. 
        /// </summary>
        /// <param name="subUserId">login id provided by irctc to registering agent.</param>
        /// <param name="subUserPassword">password provided by irctc to registering agent.</param>
        /// <param name="merchantId">merchant id provided by irctc to registering agent.</param>
        /// <param name="merchantAccount">merchant id provided by oxigent to registering agent.</param>
        /// <returns>this will return true or false</returns>
        Boolean UserCredentialExists(string subUserId, string subUserPassword, string merchantId,string merchantAccount);
        
    }
}