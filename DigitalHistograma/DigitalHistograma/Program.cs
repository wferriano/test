using System;
using System.Linq;
using System.Text;

namespace DigitalHistograma
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] myArray = new int[10] { 1, 2, 1, 3, 3, 1, 2, 1, 5, 1 };
            var groups = from i in myArray
                         group i by i into g
                         select new { Value = g.Key, Count = g.Count() };
            StringBuilder cantidad = new StringBuilder();
            foreach (var item in groups)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    cantidad.Append("*");
                }
                Console.WriteLine(item.Value.ToString() + ": " + cantidad);
            }
        }
    }
}
