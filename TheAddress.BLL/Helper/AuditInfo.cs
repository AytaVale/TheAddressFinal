using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAddress.BLL.Helper
{
    public static class AuditInfo
    {
        public static void SetValue<T>(this T sender, string propertyName, object value)
        {
            var propertyInfo = sender.GetType().GetProperty(propertyName);

            propertyInfo.SetValue(sender, value, null);

        }
    }
}
