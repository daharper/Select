using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Select.Models;
using Select.Presentation;
using Select.Rtti;

namespace Select
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            var path = Path.Combine(folder, "Files");
            var project = Project.Load(path);
            
            Context.Initialize(Assembly.GetExecutingAssembly());
            Context.Register<Project>(project);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
