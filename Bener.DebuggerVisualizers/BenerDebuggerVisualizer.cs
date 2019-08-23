using Bener.DebuggerVisualizers;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(BenerDebuggerVisualizer),
typeof(GenericObjectSource),
Target = typeof(List<>),
Description = "Bener List<> Visualizer")]

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(BenerDebuggerVisualizer),
typeof(GenericObjectSource),
Target = typeof(Dictionary<,>),
Description = "Bener Dictionary<,> Visualizer")]

[assembly: System.Diagnostics.DebuggerVisualizer(
typeof(BenerDebuggerVisualizer),
typeof(DataRowObjectSource),
Target = typeof(DataRow),
Description = "Bener DataRow Visualizer")]

namespace Bener.DebuggerVisualizers
{
    public class BenerDebuggerVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            VisualizerForm.ShowData(windowService, objectProvider.GetObject());
        }
    }
}
