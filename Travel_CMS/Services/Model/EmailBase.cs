using System;
using Mpower.Data;
public class EmailBase
{
    Mpower.Data.Repository.IApplication_SettingsRepository _appSetting;
    public EmailBase(ApplicationDbContext db)
    {
        _appSetting = new Mpower.Data.Repository.Application_SettingsRepository(db);
        getSetData();
    }
    public string email { get; set; }
    public string subject { get; set; }
    public string message { get; set; }
    public string mailFrom { get; set; }
    public string senderName { get; set; }
    public string LocalDomain { get; set; }
    public string smtpUrl { get; set; }
    public int port { get; set; }
    public string password{get;set;}
    public bool boolSSL { get; set; }

    /// <summary>
    /// getSetData is to initialize data of base email using appSEtting attribute
    /// </summary>
    private void getSetData()
    {
            this.mailFrom = _appSetting.GetByKey("smtp_emailFrom").value;
            this.port = Convert.ToInt32(_appSetting.GetByKey("smtp_port").value);
            this.smtpUrl = _appSetting.GetByKey("smtp_Url").value;
            this.LocalDomain = _appSetting.GetByKey("localDomainToEmail").value;
            this.senderName = _appSetting.GetByKey("smtp_emailSenderName").value;
            this.boolSSL = Convert.ToBoolean(_appSetting.GetByKey("smtp_isSsl").value);
            this.password=_appSetting.GetByKey("smtp_password").value;
    }

}