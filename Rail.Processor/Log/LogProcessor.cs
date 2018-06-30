
using System;
using Mpower.Rail.Data;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;
using Mpower.Rail.Model.Rail;

namespace Mpower.Rail.Processor.Application
{
    public class LogProcessor : ILogProcessor
    {
       private ILogsRepository _logsRepository;
        private readonly ApplicationDbContext _dbcontext = null;

        public LogProcessor(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
           _logsRepository = new LogsRepository(dbContext);
        }

        ~LogProcessor()
        {
            _dbcontext.Dispose();
           _logsRepository = null;
        }

        /// <summary>
        //  This Method will return regular expression for password policy validation. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>this will return regular expression string</returns>
        public bool InsertLogs(Logs log)
        {
           
            // Logs logs = new Logs();
            // logs.type=log.type;
            // logs.description=log.description;
            // logs.source=log.source;
            // logs.host=log.host;
           // logs.session=log.session;
            // logs.logDate=log.logDate;

          return  _logsRepository.Insert(log);
            //_logsRepository.Commit();
        }


        
         public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}