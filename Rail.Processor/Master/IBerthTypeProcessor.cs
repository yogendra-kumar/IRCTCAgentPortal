using System;
using System.Collections.Generic;

using Mpower.Rail.Model.Rail.Master;

namespace Mpower.Rail.Processor.Master
{
    public interface IBerthTypeProcessor : IDisposable
    {
        /// <summary>
        //  This Method is used to fetch the BerthTypes. 
        /// </summary>        
        /// <returns>this will return the list of BerthTypes</returns>
        List<BerthType> GetBerthType();     
    }
}