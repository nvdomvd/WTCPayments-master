using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WBPayments_Logic
{
    public class SettingsDao : ISettingsDao
    {
        public Settings GetSettingByKey(String key) {
            using (var db = new wbpaymentsEntities())
            {
                //get from database the setting with key as stting_id
                var L2EQuery = from s in db.Settings
                               where s.setting_id == key
                               select s;
                var setting = L2EQuery.FirstOrDefault<Settings>();
                return setting;
            }
        }
    }
}