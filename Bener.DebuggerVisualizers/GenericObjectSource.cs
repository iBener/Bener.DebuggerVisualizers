using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bener.DebuggerVisualizers
{
    public class GenericObjectSource : DataTableObjectSource
    {
        public override DataTable CreateDataTable(object data)
        {
            var type = data.GetType();

            var method = typeof(GenericObjectSource).GetMethods().FirstOrDefault(
                m => m.IsGenericMethod && m.IsPublic && 
                m.GetParameters().FirstOrDefault(p => p.ParameterType.GUID == type.GUID) != null);

            if (method == null)
            {
                throw new MissingMethodException();
            }

            MethodInfo genericMethod = method.MakeGenericMethod(type.GenericTypeArguments);
            return genericMethod.Invoke(this, new[] { data }) as DataTable;
        }

        public DataTable CreateDataTable<T>(List<T> data)
        {
            var dt = new DataTable("List");
            dt.Columns.Add("#", typeof(int))
                .AutoIncrement = true;
            AddColumns(dt, typeof(T));
            AddRows(dt, data);
            return dt;
        }

        public DataTable CreateDataTable<TKey, TValue>(Dictionary<TKey, TValue> data)
        {
            var dt = new DataTable("Dictionary");
            dt.Columns.Add("#", typeof(int))
                .AutoIncrement = true;
            AddColumns(dt, typeof(TKey), "Key");
            AddColumns(dt, typeof(TValue));
            AddRows(dt, data);
            return dt;
        }

    }
}
