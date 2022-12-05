using System;

namespace DigitalConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] myArray = new int[5] { 5, 25, 3, 12, 15 };
            int numeroMayor = 0;
            for (int i = 0; i < myArray.Length; i++)
            {
                numeroMayor = myArray[i] > numeroMayor ? myArray[i] : numeroMayor;
            }
            Console.WriteLine(numeroMayor);
        }
    }
}
