using System;



namespace Mpower.Rail.Processor.Application
{
    public interface IApplicationProcessor : IDisposable
    {
        /// <summary>
        //  This Method will return regular expression for password policy validation. 
        /// </summary>
        /// <param name=""></param>
        /// <returns>this will return regular expression string</returns>
        string GetPasswordRegularExpression();


        /// <summary>
        //  This Method will return true or false based on password policy validation. 
        /// </summary>
        /// <param name="Key">key for application setting .</param>
        /// <returns>this will return value for key</returns>
        string GetApplicationSettingByKey(string key);
    }
}