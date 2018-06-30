using System;
using Mpower.Rail.Data;
using System.Collections.Generic;
using Mpower.Rail.Data.Repository.Master;
using Mpower.Rail.Model.Rail.Master;
using System.Linq;

namespace Mpower.Rail.Processor.Master
{
    public class IDCardTypeProcessor : IIDCardTypeProcessor
    {
        private IDCardTypeRepository _idCardTypeRepository;
        private readonly ApplicationDbContext _dbcontext = null;        
        public IDCardTypeProcessor(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            _idCardTypeRepository = new IDCardTypeRepository(dbcontext);
        }

        ~IDCardTypeProcessor()
        {
            _dbcontext.Dispose();
            _idCardTypeRepository = null;
        }

        /// <summary>
        /// This Api method will return the list of All ID Card Types.
        /// </summary>
        public List<IDCardType> GetIDCardType()
        {
            return _idCardTypeRepository.GetAll().OrderBy(c=>c.name).ToList();            
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}