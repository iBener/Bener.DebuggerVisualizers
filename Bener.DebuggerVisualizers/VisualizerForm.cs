using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bener.DebuggerVisualizers
{
    public partial class VisualizerForm : Form
    {
        public VisualizerForm()
        {
            InitializeComponent();
        }

        private Font italicDataGridFont;

        public static void ShowData(IDialogVisualizerService windowService, object data)
        {
            var frm = new VisualizerForm();
            frm.dataGridView.DataSource = data;
            windowService.ShowDialog(frm);
        }

        private void DataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridView.Columns.Contains("#"))
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    dataGridView.Rows[i].HeaderCell.Value = dataGridView.Rows[i].Cells["#"].Value.ToString();
                }
                dataGridView.Columns["#"].Visible = false;
            }
        }

        private void VisualizerForm_Load(object sender, EventArgs e)
        {
            ConfigureColumns();
        }

        private void ConfigureColumns()
        {
            var dt = dataGridView.DataSource as DataTable;
            if (dt != null)
            {
                foreach (DataColumn cln in dt.Columns)
                {
                    if (Boolean.TryParse(cln.ExtendedProperties["ReadOnly"]?.ToString(), out bool readOnly))
                    {
                        dataGridView.Columns[cln.ColumnName].ReadOnly = readOnly;
                        dataGridView.Columns[cln.ColumnName].DefaultCellStyle.Font = ItalicDataGridFont;
                    }
                }
            }
        }

        public Font ItalicDataGridFont
        {
            get
            {
                if (italicDataGridFont == null)
                {
                    italicDataGridFont = new Font(dataGridView.Font.FontFamily.Name, dataGridView.Font.SizeInPoints, FontStyle.Italic, dataGridView.Font.Unit, dataGridView.Font.GdiCharSet, dataGridView.Font.GdiVerticalFont);
                }
                return italicDataGridFont;
            }
        }
    }
}
