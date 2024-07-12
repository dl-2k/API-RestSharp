using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ShareData
{
    public static class DataStorage
    {
        private static AsyncLocal<Dictionary<string, object>> _data = new AsyncLocal<Dictionary<string, object>>();

        public static void InitData()
        {
            _data.Value = new Dictionary<string, object>();
        }
        

        public static void SetData(string key, object value)
        {
            _data.Value[key] = value;
        }

        public static object GetData(string key)
        {
            if (!_data.Value.ContainsKey(key))
            {
                return null;
            }
            
            return _data.Value.GetValueOrDefault(key);
        }

        public static void ClearData()
        {
            if (_data.Value is not null) _data.Value.Clear();
        }

        // ClearData xai trong basetest tear down

    }
}
