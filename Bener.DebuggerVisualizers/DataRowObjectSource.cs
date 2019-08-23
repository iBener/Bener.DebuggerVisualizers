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
    public class DataRowObjectSource : DataTableObjectSource
    {
        public override DataTable CreateDataTable(object data)
        {
            var row = data as DataRow;
            var dt = row.Table.Clone();
            dt.ImportRow(row);
            return dt;
        }
    }
}
