namespace Suma
{
    using System;
    using System.Linq;
    using System.Text;
    class Program
    {
        static void Main(string[] args)
        {
            int[] myArray = new int[6] { 1, 8, 6, 7, 2, 5 };
            for (int i = 0; i < myArray.Length - 1; i++)
            {
                for (int j = 1; j < myArray.Length; j++)
                {
                    if (myArray[i] + myArray[j] == 10)
                    {
                        Console.WriteLine(myArray[i] + " " + myArray[j]);
                        break;
                    }
                }
            }
        }
    }
}