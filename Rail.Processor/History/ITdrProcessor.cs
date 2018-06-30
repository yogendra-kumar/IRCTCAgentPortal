using System;
using System.Collections.Generic;
using Mpower.Rail.Model.ViewModel;
using Mpower.Rail.Model.Rail;

namespace Mpower.Rail.Processor.Application
{
    public interface ITdrProcessor : IDisposable
    {
        /// <summary>
        //  This Method will return tdr details list
        /// </summary>
        /// <param name="RoId"> Id of Ro filling TDR</param>
        /// <returns>this will return Tdr View Model</returns>
         List<TdrViewModel> GetTdrDetailList(string RoId);  

        /// <summary>
        /// This Method will return tdr reasons list
        /// </summary>
        /// <param name=""> </param>
        /// <returns>this will return Tdr Reasons Model</returns>
         List<TdrReasons> GetTdrReasonsList();
    }
}