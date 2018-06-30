
using System;
using Mpower.Rail.Data;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;

namespace Mpower.Rail.Processor.Application
{
    public class ApplicationProcessor : IApplicationProcessor
    {
        private IApplicationSettingsRepository _applicationSettingRepository;
        private readonly ApplicationDbContext _dbcontext = null;

        public ApplicationProcessor(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
            _applicationSettingRepository = new ApplicationSettingsRepository(dbContext);
        }

        ~ApplicationProcessor()
        {
            _dbcontext.Dispose();
            _applicationSettingRepository = null;
        }

        /// <summary>
        //  This Method will return regular expression for password policy validation. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>this will return regular expression string</returns>
        public string GetPasswordRegularExpression()
        {
            var settings = _applicationSettingRepository.GetSingle(x => x.key == "regExpression_password");
            if (settings != null)
            {
              return settings.value;
            }
            return string.Empty;
        }


        /// <summary>
        //  This Method will return true or false based on password policy validation. 
        /// </summary>
        /// <param name="Key">key for application setting .</param>
        /// <returns>this will return value for key</returns>
        public string GetApplicationSettingByKey(string key)
        {
            return _applicationSettingRepository.GetSingle(x=>x.key==key).value;
        }

         public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}