using System;
using System.Collections.Generic;
using Mpower.Rail.Model.Rail;

namespace Mpower.Rail.Processor.Master
{
    public interface IStationCacheProcessor : IDisposable
    {
        /// <summary>
        //  This Method will return the list of Statons. 
        /// </summary>
        /// <returns>this will return the Station list object</returns>
        List<StationsCache> GetStationsList(string key);   

        /// <summary>
        /// returns all list of station
        /// </summary>
        /// <returns></returns>
        List<StationsCache> GetStationsList(); 

        /// <summary>
        /// returns all list of station on the basis of Station Codes
        /// </summary>
        /// <returns></returns>
        List<StationsCache> GetStationsList(string[] stationCodes); 
    }
}