using System;
using System.Collections.Generic;
using Mpower.Rail.Model.Rail.Master;

namespace Mpower.Rail.Processor.Master
{
    public interface IIDCardTypeProcessor : IDisposable
    {
        /// <summary>
        //  This Method will return the list of Merchant's agent's list. 
        /// </summary>
        /// <param name="MerchantId">MerchantId of logged-In merchant, who want to access their agents list.</param>
        /// <returns>this will return the UserRegistration list object</returns>
        List<IDCardType> GetIDCardType();     
    }
}