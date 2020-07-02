using LoadData;
using System;
using System.IO;
using System.Reflection;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
               string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
               string projectPath = appDirectory.Substring(0, appDirectory.IndexOf("\\bin"));
                Console.WriteLine(projectPath);
        }
    }
}
