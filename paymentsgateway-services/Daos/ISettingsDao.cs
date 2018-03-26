using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPayments_Logic
{
    public interface ISettingsDao
    {
        Settings GetSettingByKey(String key);
    }
}
