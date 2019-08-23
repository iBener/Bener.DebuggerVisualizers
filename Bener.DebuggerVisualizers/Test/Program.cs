using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bener.DebuggerVisualizers.Test
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            //object targetObject = MockDataSource.GetPeopleList();
            //object targetObject = MockDataSource.GetStringList();
            object targetObject = MockDataSource.GetPeopleDictionary();
            //object targetObject = MockDataSource.GetStringDictionary();

            var visualizerHost = new VisualizerDevelopmentHost(targetObject, typeof(BenerDebuggerVisualizer), typeof(GenericObjectSource));
            visualizerHost.ShowVisualizer();
        }

    }
}
