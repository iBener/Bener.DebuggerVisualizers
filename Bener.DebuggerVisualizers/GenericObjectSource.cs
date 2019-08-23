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
        public override void GetData(object target, Stream outgoingData)
        {
            var data = CreateDataTable(target);
            base.GetData(data, outgoingData);
        }

        public DataTable CreateDataTable(object data)
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
    }
}
