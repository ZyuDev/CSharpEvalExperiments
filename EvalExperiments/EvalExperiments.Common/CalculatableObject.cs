using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EvalExperiments.Common
{
    public class CalculatableObject
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();
        private Dictionary<string, DataTable> _tables = new Dictionary<string, DataTable>();

        public object GetPropertyValue(string name)
        {
            object result = null;

            if (_properties.ContainsKey(name))
            {
                result = _properties[name];
            }

            return result;
        }

        public T GetPropertyValue<T>(string name)
        {

            var result = GetDictionaryValue<T>(name);

            return result;
        }

        public void SetPropertyValue(string name, object value)
        {
            if (_properties.ContainsKey(name))
            {
                _properties[name] = value;
            }
            else
            {
                _properties.Add(name, value);
            }
        }

        public object GetTable(string name)
        {
            DataTable table = null;

            if (_tables.ContainsKey(name))
            {
                table = _tables[name];
            }

            return table;
        }

        public void SetTable(string name, DataTable table)
        {
            if (_properties.ContainsKey(name))
            {
                _properties[name] = table;
            }
            else
            {
                _properties.Add(name, table);
            }
        }

        private T GetDictionaryValue<T>(string key)
        {
            if (_properties.TryGetValue(key.ToLower(), out var val))
            {
                if (val is T property)
                {
                    return property;
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }

        public static object GetPropertyValue(CalculatableObject cObject, string name)
        {
            return cObject.GetPropertyValue(name);
        }

        public static T GetPropertyValue<T>(CalculatableObject cObject, string name)
        {
            return cObject.GetPropertyValue<T>(name);
        }
    }
}
