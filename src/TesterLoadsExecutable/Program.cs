using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TesterLoads;

namespace TesterLoadsExecutable
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadTests tests = new LoadTests();

            var loadTestMethods =
                (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                 from type in assembly.GetTypes().Where(x => x.Name == "LoadTests")
                 from method in type.GetMethods()
                 let attributes = method.GetCustomAttributes(typeof(TestMethodAttribute), true)
                 where attributes != null && attributes.Length > 0
                 select new
                    {
                        Type = type,
                        Method = method,
                        Attributes = attributes.Cast<TestMethodAttribute>()                        
                    }).ToList();

            foreach (var method in loadTestMethods)
            {
                ((Task) method.Method.Invoke(tests, null)).Wait();
            }
        }
    }
}
