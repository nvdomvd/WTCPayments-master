using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WBPayments_Logic
{
    public class SettingsManager : ISettingsManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SettingsManager));
        private readonly ISettingsDao settingsDao;

        public SettingsManager(ISettingsDao settings)
        {
            settingsDao = settings;
        }

        public bool GetIntSetting(string key, out int intValue)
        {
            try { 
                //get the setting from the DAO
                Settings setting = settingsDao.GetSettingByKey(key);
                //check if the setting exist, if not log.
                if (setting == null)
                {
                    log.WarnFormat("Has not found a setting with the key: {0}", key);
                    intValue = 0;
                    return false;
                } else
                {
                    int intSetting;
                    //parse the setting value getted to int
                    if (int.TryParse(setting.value, out intSetting))
                    {
                        intValue = intSetting;
                        return true;
                    }
                    else
                    {
                        log.WarnFormat("Has not found an int value in the setting with the key: {0}", key);
                        intValue = 0;
                        return false;
                    }
                }
            } catch (Exception e)
            {
                log.ErrorFormat("Exception in GetIntSetting: {0} for key {1}, message: {2} trace: {3}", e, key, e.Message, e.StackTrace);
                intValue = 0;
                return false;
            }
        }
    }
}