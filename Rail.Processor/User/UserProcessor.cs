using System;
using Mpower.Rail.Data;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;
using Mpower.Rail.Model.Rail;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Mpower.Rail.Model.ViewModel;



namespace Mpower.Rail.Processor.User
{
    public class UserProcessor : IUserProcessor
    {
        private IUserRegistrationRepository _userRegistrationRepository;
        private readonly ApplicationDbContext _dbcontext = null;

        public UserProcessor(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _userRegistrationRepository = new UserRegistrationRepository(dbcontext);
        }

        ~UserProcessor()
        {
            _dbcontext.Dispose();
            _userRegistrationRepository = null;
        }

        /// <summary>
        //  This Method will return the list of Merchant's agent's list. 
        /// </summary>
        /// <param name="MerchantId">MerchantId of logged-In merchant, who want to access their agents list.</param>
        /// <returns>this will return the UserRegistration list object</returns>
        public List<UserRegistration> GetMerchantAgent(string MerchantId)
        {
            IEnumerable<UserRegistration> agentlist = _userRegistrationRepository.FindBy(m => m.merchantId == MerchantId);
            var _agents = (
                        from u in _dbcontext.Users
                        join a in agentlist on u.Id equals a.UserId
                        orderby a.email
                        select a
                        ).ToList();
            return _agents;
        }

        /// <summary>
        //  This Method will return the list of Merchant's agent's list. 
        /// </summary>
        /// <param name="userManager">Object of Identity usermanager class injected on ConfigureServices.</param>
        /// <param name="userDetail">Object of UserViewModel class to register the agent detail.</param>
        /// <returns>this will return the UserRegistration object with user detail.</returns>
        public UserRegistration RegisterAgent(UserManager<UserViewModel> userManager, Mpower.Rail.Model.Request.UserRegistration userDetail)
        {
            var user = new UserViewModel { UserName = userDetail.email, Email = userDetail.email };
            UserRegistration _userDetail = null;
            var result = userManager.CreateAsync(user, userDetail.password).Result;
            //Assign Role to user Here 
            if (result.Succeeded)
            {
                var a = userManager.AddToRoleAsync(user, "Agent").Result;

                _userDetail = new UserRegistration()
                {
                    UserId = user.Id,
                    merchantAccount = userDetail.merchantAccount,
                    email = userDetail.email,
                    isActive = false,
                    merchantId = userDetail.merchantId,
                    subUserId = userDetail.subUserId,
                    subUserPassword = userDetail.subUserPassword,
                    digitalCertificate = userDetail.digitalCertificate,
                    macId = userDetail.macId,
                    mobNo = userDetail.mobileNo,
                    isIRCTCActivated = true,
                    pancard = userDetail.panCard,
                    deviceId = userDetail.deviceId,
                    CreatedDate = DateTime.Now,
                };
                _userRegistrationRepository.Add(_userDetail);
                _userRegistrationRepository.Commit();

                //var requiredDetail = "{userId:" + user.Id + ",merchantId:" + this._userManager.GetUserId(User).ToString() + "}";
                //_logger.LogInformation(3, "User created a new account with password.");
                //return Ok(new Application_ResponseWrapper(){ResponseCode="100",ResponseMessage="ok",Status="successs",ResponseResult=requiredDetail});
            }
            return _userDetail;
        }

        /// <summary>
        /// This Api will Update existing User
        /// </summary>
        /// <param name="userUpdate">req is an object type of userUpdate class</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        public Boolean UpdateUser(Mpower.Rail.Model.Request.UserUpdate userUpdate)
        {
            UserRegistration user = _userRegistrationRepository.FindBy(m => m.UserId == userUpdate.userId && m.merchantId == userUpdate.merchantId).FirstOrDefault();
            if (user != null)
            {
                user.isActive = userUpdate.active;
                user.UpdatedDate = DateTime.Now;
                _userRegistrationRepository.Update(user);
                _userRegistrationRepository.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This API will Activate new created User
        /// </summary>
        /// <param name="merchantId">This Api Accept MerchantId As a query string</param>
        /// <param name="userId">This Api Accept Userid As a query string</param>
        /// <param name="status">This actionmethod accepts boolean value to Activate/De-Activate user in query string</param>
        /// <returns>This Api will rerurn boolean.</returns>
        public Boolean ChangeUserStatus(string merchantId, string userId, bool status)
        {
            UserRegistration user = _userRegistrationRepository.FindBy(m => m.UserId == userId && m.merchantId == merchantId).FirstOrDefault();
            if (user != null)
            {
                user.isActive = status;
                user.UpdatedDate = DateTime.Now;
                _userRegistrationRepository.Update(user);
                _userRegistrationRepository.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        //  This Method will return the agent's detail by AccountNumber. 
        /// </summary>
        /// <param name="AccountNumber">AccountNumber of logged-In agent.</param>
        /// <returns>this will return the UserRegistration object</returns>
        public UserRegistration GetAgentByAccountNumber(string AccountNumber)
        {
            return _userRegistrationRepository.FindBy(m => m.merchantAccount == AccountNumber).FirstOrDefault<UserRegistration>();
        }

        /// <summary>
        //  This Method will return true or false based on agent's email. 
        /// </summary>
        /// <param name="Email">email of registering agent.</param>
        /// <returns>this will return true or false</returns>
        public Boolean UserExists(string email)
        {
            bool isExists = false;
            var user = _userRegistrationRepository.GetSingle(x => x.email == email);
            if (user != null)
            {
                isExists = true;
            }
            return isExists;
        }

        /// <summary>
        //  This Method will check if same IRCTC credentials for this merchant exists with another agent's. 
        /// </summary>
        /// <param name="subUserId">login id provided by irctc to registering agent.</param>
        /// <param name="subUserPassword">password provided by irctc to registering agent.</param>
        /// <param name="merchantId">merchant id provided by irctc to registering agent.</param>
        /// <param name="merchantAccount">merchant id provided by oxigent to registering agent.</param>
        /// <returns>this will return true or false</returns>
        public Boolean UserCredentialExists(string subUserId, string subUserPassword, string merchantId, string merchantAccount)
        {
            bool isExists = false;
            var userExist = _userRegistrationRepository.GetSingle(x => x.subUserId == subUserId && x.subUserPassword == subUserPassword && x.merchantId == merchantId && x.merchantAccount == merchantAccount);
            if (userExist != null)
            {
                isExists = true;
            }
            return isExists;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}