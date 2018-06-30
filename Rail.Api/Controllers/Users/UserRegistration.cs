using System;
using Microsoft.AspNetCore.Mvc;
using Mpower.Rail.Data.IRepository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mpower.Rail.Data;
using Microsoft.AspNetCore.Authorization;
using Mpower.Rail.Data.Repository;
using System.Collections.Generic;
using Mpower.Rail.Api.Models;
using Mpower.Rail.Model.Rail;
using Mpower.Rail.Model.Application;
using Mpower.Rail.Processor.User;
using Microsoft.AspNetCore.Identity;
using Mpower.Rail.Model.ViewModel;
using System.Text.RegularExpressions;
using Mpower.Rail.Processor.Application;

namespace Mpower.Rail.Api.Users
{
    [AllowAnonymous]
    [Route("Mpower/Rail/UserRegistration")]
    public class UserRegistrationController : Controller
    {
        private readonly UserManager<UserViewModel> _userManager;
        private ApplicationDbContext _applicationDbContext;
        private IUserRegistrationRepository _userRegistrationRepository;
        private IApplicationErrorRepository _errorRepository;
        public UserRegistrationController(UserManager<UserViewModel> userManager, DbContextOptions<ApplicationDbContext> applicationDBContext)
        {
            _userManager = userManager;
            _applicationDbContext = new ApplicationDbContext(applicationDBContext);
            _userRegistrationRepository = new UserRegistrationRepository(_applicationDbContext);
            _errorRepository = new ApplicationErrorsRepository(_applicationDbContext);
        }

        /// <summary>
        /// This API will register new User
        /// </summary>
        /// <param name="req">req is an object type of UserRegistration class</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpPostAttribute]
        [RouteAttribute("Create")]
        public IActionResult CreateUser([FromBody]Mpower.Rail.Model.Request.UserRegistration userDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (IApplicationProcessor _processor = new ApplicationProcessor(_applicationDbContext))
                    {
                        string pattern = _processor.GetPasswordRegularExpression();
                        if (!string.IsNullOrEmpty(pattern))
                        {
                            Regex regex = new Regex(pattern);
                            if (!regex.IsMatch(userDetail.password))
                            {                                
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "1010", ResponseMessage = "Password not validated!", Status = "failed" });
                            }
                        }
                        else
                        {
                            _errorRepository.
                    Add(new Application_Errors
                    {
                        applicationID = 1,
                        errorDescription = "Password regular expression not found.",
                        errorType = "Log",
                        logDate = System.DateTime.Now,
                        pageID = 0,
                        Source = "Mpower/Rail/UserRegistration/Create"
                    });
                            _errorRepository.Commit();
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "1010", ResponseMessage = "Password not validated!", Status = "failed" });
                        }
                    }

                    UserRegistration _userDetail = null;

                    using (IUserProcessor _userProcessor = new UserProcessor(_applicationDbContext))
                    {

                        if (_userProcessor.UserExists(userDetail.email))
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "1012", ResponseMessage = "Email already registered!", Status = "failed" });
                        }
                        if (_userProcessor.UserCredentialExists(userDetail.subUserId, userDetail.subUserPassword, userDetail.merchantId, userDetail.merchantAccount))
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "1011", ResponseMessage = "User already registered!", Status = "failed" });
                        }
                        _userDetail = _userProcessor.RegisterAgent(userManager: _userManager, userDetail: userDetail);
                    }
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = _userDetail });
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }
            }
            catch (Exception ex)
            {
                _errorRepository.
                    Add(new Application_Errors
                    {
                        applicationID = 1,
                        errorDescription = ex.StackTrace,
                        errorType = "Exception",
                        logDate = System.DateTime.Now,
                        pageID = 0,
                        Source = "Mpower/Rail/UserRegistration/Create"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }

        }


        /// <summary>
        /// This API will Activate new created User
        /// </summary>
        /// <param name="merchantId">This Api Accept MerchantId As a query string</param>
        /// <param name="userId">This Api Accept Userid As a query string</param>
        /// <param name="status">This actionmethod accepts boolean value to Activate/De-Activate user in query string</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpGetAttribute]
        [RouteAttribute("ChangeStatus/{merchantId}/{userId}/{status}")]
        public IActionResult ChangeUserStatus(string merchantId, string userId, bool status)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    using (IUserProcessor _userProcessor = new UserProcessor(_applicationDbContext))
                    {
                        if (_userProcessor.ChangeUserStatus(merchantId, userId, status))
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = status });
                        }
                        else
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "User not found", Status = "failed" });
                        }
                    }
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }
            }
            catch (Exception ex)
            {
                _errorRepository.
                    Add(new Application_Errors
                    {
                        applicationID = 1,
                        errorDescription = ex.StackTrace,
                        errorType = "Exception",
                        logDate = System.DateTime.Now,
                        pageID = 0,
                        Source = "Mpower/Rail/UserRegistration/ChangeStatus"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }

        /// <summary>
        /// This Api will Update existing User
        /// </summary>
        /// <param name="userUpdate">req is an object type of userUpdate class</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpPostAttribute]
        [RouteAttribute("Update")]
        public IActionResult UpdateUser([FromBody]Mpower.Rail.Model.Request.UserUpdate userUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (IUserProcessor _userProcessor = new UserProcessor(_applicationDbContext))
                    {
                        if (_userProcessor.UpdateUser(userUpdate))
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = userUpdate });
                        }
                        else
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "User does not exist.", Status = "failed" });
                        }
                    }
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }
            }
            catch (Exception ex)
            {
                _errorRepository.
                    Add(new Application_Errors
                    {
                        applicationID = 1,
                        errorDescription = ex.StackTrace,
                        errorType = "Exception",
                        logDate = System.DateTime.Now,
                        pageID = 0,
                        Source = "Mpower/Rail/UserRegistration/Update"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }

        }

        /// <summary>
        /// This API will delete Existing User
        /// </summary>
        /// <param name="userId">This Api Accept Userid As a query string</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        // [HttpGetAttribute]
        // [RouteAttribute("Delete/{userId}")]
        public IActionResult DeleteUser(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    UserRegistration user = _userRegistrationRepository.FindBy(m => m.UserId == userId).FirstOrDefault();
                    if (user != null)
                    {
                        user.isActive = false;
                        user.UpdatedDate = DateTime.Now;
                        _userRegistrationRepository.Update(user);
                        _userRegistrationRepository.Commit();
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = "User Deleted." });
                    }
                    else
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Record not found", Status = "failed" });
                    }
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }
            }
            catch (Exception ex)
            {
                _errorRepository.
                    Add(new Application_Errors
                    {
                        applicationID = 1,
                        errorDescription = ex.ToString(),
                        errorType = "Exception",
                        logDate = System.DateTime.Now,
                        pageID = 0,
                        Source = "Mpower/Rail/UserRegistration/Delete"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }

        }

        /// <summary>
        /// This Api will provide the all users detail
        /// </summary>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpGetAttribute]
        [RouteAttribute("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<UserRegistration> userlst = _userRegistrationRepository.GetAll().ToList();
                if (userlst.Count > 0)
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = userlst });
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Record not found", Status = "failed" });
                }
            }
            catch (Exception ex)
            {
                _errorRepository.
                    Add(new Application_Errors
                    {
                        applicationID = 1,
                        errorDescription = ex.StackTrace,
                        errorType = "Exception",
                        logDate = System.DateTime.Now,
                        pageID = 0,
                        Source = "Mpower/Rail/UserRegistration/GetAllUsers"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }

        /// <summary>
        /// This action method will provide the list of merchants agent list.
        /// </summary>
        /// <param name="MerchantId">This action method accepts MerchantId in query string.</param>
        /// <returns>This Api will rerurn object of UserRegistration list</returns>
        [HttpGetAttribute]
        [RouteAttribute("GetMerchantAgents/{MerchantId}")]
        public IActionResult GetMerchantAgents(string MerchantId)
        {
            try
            {
                List<UserRegistration> userlist = new List<UserRegistration>();
                using (IUserProcessor _userProcessor = new UserProcessor(_applicationDbContext))
                {
                    userlist = _userProcessor.GetMerchantAgent(MerchantId);
                }

                if (userlist.Count() > 0)
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = userlist.ToList<UserRegistration>() });
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Record not found", Status = "failed" });
                }
            }
            catch (Exception ex)
            {
                _errorRepository.
                    Add(new Application_Errors
                    {
                        applicationID = 1,
                        errorDescription = ex.ToString(),
                        errorType = "Exception",
                        logDate = System.DateTime.Now,
                        pageID = 0,
                        Source = "Mpower/Rail/UserRegistration/GetMerchantAgents"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }

        /// <summary>
        //  This Method will return the agent's detail by AccountNumber. 
        /// </summary>
        /// <param name="AccountNumber">AccountNumber of logged-In agent.</param>
        /// <returns>this will return the UserRegistration object</returns>
        [HttpGetAttribute]
        [RouteAttribute("GetAgentByAccountNumber/{AccountNumber}")]
        public IActionResult GetAgentByAccountNumber(string AccountNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(AccountNumber))
                {
                    using (IUserProcessor _userProcessor = new UserProcessor(_applicationDbContext))
                    {
                        UserRegistration user = _userProcessor.GetAgentByAccountNumber(AccountNumber);
                        if (user != null)
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = user });
                        }
                        else
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Result not found.", Status = "failed" });
                        }
                    }
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid request", Status = "failed" });
                }
            }
            catch (Exception ex)
            {
                _errorRepository.
                    Add(new Application_Errors
                    {
                        applicationID = 1,
                        errorDescription = ex.ToString(),
                        errorType = "Exception",
                        logDate = System.DateTime.Now,
                        pageID = 0,
                        Source = "Mpower/Rail/UserRegistration/GetAgentByAccountNumber"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }
    }
}