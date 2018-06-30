using System;
using Mpower.Rail.Data;
using Mpower.Rail.Model.Rail.Master;
using System.Linq;
using System.Collections.Generic;
using Mpower.Rail.Data.Repository.Master;

namespace Mpower.Rail.Processor.Master
{
    public class BerthTypeProcessor : IBerthTypeProcessor
    {
        private BerthTypeRepository _berthTypeRepository;
        private readonly ApplicationDbContext _dbcontext = null;        
        public BerthTypeProcessor(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _berthTypeRepository = new BerthTypeRepository(dbcontext);
        }

        ~BerthTypeProcessor()
        {
            _dbcontext.Dispose();
            _berthTypeRepository = null;
        }

        /// <summary>
        //  This Method is used to fetch the BerthTypes. 
        /// </summary>        
        /// <returns>this will return the list of BerthTypes</returns>
        public List<BerthType> GetBerthType()
        {
            return _berthTypeRepository.GetAll().OrderBy(b=>b.fullName).ToList();            
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}