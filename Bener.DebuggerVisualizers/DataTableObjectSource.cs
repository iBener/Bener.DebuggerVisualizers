using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bener.DebuggerVisualizers
{
    public abstract class DataTableObjectSource : VisualizerObjectSource
    {
        public DataTable CreateDataTable<T>(List<T> data)
        {
            var dt = new DataTable("List");
            dt.Columns.Add("#", typeof(int))
                .AutoIncrement = true;
            AddColumns(dt, typeof(T), "Key");
            AddRows(dt, data);
            return dt;
        }

        public DataTable CreateDataTable<TKey, TValue>(Dictionary<TKey, TValue> data)
        {
            var dt = new DataTable("Dictionary");
            dt.Columns.Add("#", typeof(int))
                .AutoIncrement = true;
            AddColumns(dt, typeof(TKey), "Key");
            AddColumns(dt, typeof(TValue), "Value");
            AddRows(dt, data);
            return dt;
        }

        private void AddColumns(DataTable dt, Type type, string columnName)
        {
            if (IsValueType(type))
            {
                dt.Columns.Add(columnName, typeof(byte[]) != type ? type : typeof(string));
            }
            else
            {
                foreach (var prp in type.GetProperties())
                {
                    AddColumns(dt, prp.PropertyType, prp.Name);
                }
            }
        }

        private void AddRows<T>(DataTable dt, List<T> list)
        {
            foreach (var item in list)
            {
                var row = dt.Rows.Add();
                FillRow(row, item.GetType(), item, "Value");
            }
        }

        private void AddRows<TKey, TValue>(DataTable dt, Dictionary<TKey, TValue> dict)
        {
            foreach (var item in dict)
            {
                var row = dt.Rows.Add();
                FillRow(row, item.Key.GetType(), item.Key, "Key");
                FillRow(row, item.Value.GetType(), item.Value, "Value");
            }
        }

        private void FillRow(DataRow row, Type type, object item, string columnName)
        {
            if (IsValueType(type))
            {
                row[columnName] = typeof(byte[]) != type ? item : "0x" + String.Concat(Array.ConvertAll(item as byte[], x => x.ToString("x2")));
            }
            else
            {
                foreach (var prp in type.GetProperties())
                {
                    FillRow(row, prp.PropertyType, prp.GetValue(item), prp.Name);
                }
            }
        }

        protected static bool IsValueType(Type type) =>
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
