using System;
using Mpower.Rail.Data;
using System.Collections.Generic;
using Mpower.Rail.Data.Repository;
using Mpower.Rail.Model.Rail;
using System.Linq;

namespace Mpower.Rail.Processor.Master
{
    public class StationCacheProcessor : IStationCacheProcessor
    {
        private StationCacheRepository _stationCacheRepository;
        private readonly ApplicationDbContext _dbcontext = null;        
        public StationCacheProcessor(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _stationCacheRepository = new StationCacheRepository(dbcontext);
        }

        ~StationCacheProcessor()
        {
            _dbcontext.Dispose();
            _stationCacheRepository = null;
        }

        /// <summary>
        /// This Api method will return the list of All stations for key provided
        /// </summary>
        public List<StationsCache> GetStationsList(string key)
        {
            return _stationCacheRepository.FindBy(p => p.stnName.Contains(key) || p.stnCode.Contains(key)).Take(10).OrderBy(c=>c.stnName).ToList();            
        }

         /// <summary>
        /// This Api method will return the list of All stations
        /// </summary>
        public List<StationsCache> GetStationsList()
        {
            return _stationCacheRepository.GetAll().OrderBy(c=>c.stnName).ToList();            
        }

         /// <summary>
        /// This Api method will return the list of All stations for station codes provided
        /// </summary>
        public List<StationsCache> GetStationsList(string[] stationCodes)
        {
            var stationsCacheList =new List<StationsCache>();
            foreach(string stationCode in stationCodes)
            {
                stationsCacheList.Add(_stationCacheRepository.FindBy(x=>x.stnCode == "("+stationCode+")").First());
            }
            return stationsCacheList;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}