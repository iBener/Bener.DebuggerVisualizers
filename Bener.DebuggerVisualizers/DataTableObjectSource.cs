using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bener.DebuggerVisualizers
{
    public abstract class DataTableObjectSource : VisualizerObjectSource
    {
        public abstract DataTable CreateDataTable(object data);

        public override void GetData(object target, Stream outgoingData)
        {
            var data = CreateDataTable(target);
            base.GetData(data, outgoingData);
        }

        protected void AddColumns(DataTable dt, Type type, string columnName = "Value")
        {
            if (IsValueType(type))
            {
                dt.Columns.Add(columnName, typeof(byte[]) != type ? type : typeof(string));
            }
            else
            {
                foreach (var prp in type.GetProperties())
                {
                    if (IsValueType(prp.PropertyType))
                    {
                        dt.Columns.Add(prp.Name, typeof(byte[]) != prp.PropertyType ? prp.PropertyType : typeof(string)); 
                    }
                    else
                    {
                        dt.Columns.Add(prp.Name, typeof(string)).ExtendedProperties.Add("ReadOnly", true);
                    }
                }
            }
        }

        protected void AddRows<T>(DataTable dt, List<T> list)
        {
            foreach (var item in list)
            {
                var row = dt.Rows.Add();
                FillRow(row, item.GetType(), item);
            }
        }

        protected void AddRows<TKey, TValue>(DataTable dt, Dictionary<TKey, TValue> dict)
        {
            foreach (var item in dict)
            {
                var row = dt.Rows.Add();
                FillRow(row, item.Key.GetType(), item.Key, "Key");
                FillRow(row, item.Value.GetType(), item.Value);
            }
        }

        private void FillRow(DataRow row, Type type, object item, string columnName = "Value")
        {
            if (IsValueType(type))
            {
                row[columnName] = typeof(byte[]) != type ? item : "0x" + String.Concat(Array.ConvertAll(item as byte[], x => x.ToString("x2")));
            }
            else
            {
                foreach (var prp in type.GetProperties())
                {
                    if (IsValueType(prp.PropertyType))
                    {
                        row[prp.Name] = typeof(byte[]) != prp.PropertyType ? prp.GetValue(item) : "0x" + String.Concat(Array.ConvertAll(prp.GetValue(item) as byte[], x => x.ToString("x2"))); 
                    }
                    else
                    {
                        row[prp.Name] = "<Object>";
                    }
                }
            }
        }

        private static bool IsValueType(Type type) =>
            typeof(byte) == type ||
            typeof(sbyte) == type ||
            typeof(short) == type ||
            typeof(ushort) == type ||
            typeof(int) == type ||
            typeof(uint) == type ||
            typeof(long) == type ||
            typeof(ulong) == type ||
            typeof(float) == type ||
            typeof(double) == type ||
            typeof(decimal) == type ||
            typeof(bool) == type ||
            typeof(string) == type ||
            typeof(char) == type ||
            typeof(Guid) == type ||
            typeof(DateTime) == type ||
            typeof(DateTimeOffset) == type ||
            typeof(TimeSpan) == type ||
            typeof(byte[]) == type ||
            typeof(byte?) == type ||
            typeof(sbyte?) == type ||
            typeof(short?) == type ||
            typeof(ushort?) == type ||
            typeof(int?) == type ||
            typeof(uint?) == type ||
            typeof(long?) == type ||
            typeof(ulong?) == type ||
            typeof(float?) == type ||
            typeof(double?) == type ||
            typeof(decimal?) == type ||
            typeof(bool?) == type ||
            typeof(char?) == type ||
            typeof(Guid?) == type ||
            typeof(DateTime?) == type ||
            typeof(DateTimeOffset?) == type ||
            typeof(TimeSpan?) == type;
    }
}
