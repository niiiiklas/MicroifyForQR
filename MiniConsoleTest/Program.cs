using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Niklas.MicroifyForQR;

namespace MiniConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            Console.WriteLine("Hello!");

            var td = new TestData() { TheNameOfSubject = "Niklas Andersson", SomethingElse = "12", IsWhat="dsadsadsa" };

            foreach(PropertyInfo pi in td.GetType().GetProperties())
            {
                string str = "";
                str += pi.GetValue(td) + " : ";
                str += pi.Name;
                var hasIt = pi.GetCustomAttributes<Niklas.MicroifyForQR.MinifyAs>();
                Console.WriteLine(str + " " + hasIt);
            }

            Console.WriteLine("----------------------");
            string str2 = "" + Niklas.MicroifyForQR.MinifyForQR.CreateString<TestData>(td);
            Console.WriteLine(str2);


            TestData parsed = new TestData();
            var obj = Niklas.MicroifyForQR.MinifyForQR.Parse<TestData>(parsed, str2);

            

            Console.ReadLine();

        }
    }




    public class TestData
    {
        [MinifyAs("tsd")]
        public string TheNameOfSubject { get; set; }

        public string SomethingElse { get; set; }

        [IgnoreProperty]
        public string IsWhat { get; set; }
    }
}
