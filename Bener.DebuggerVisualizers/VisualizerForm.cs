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
    }
}
