using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeWork_Arrays
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Вывод певрого массива (задание 1 задача А)
            int[] numFibonachi = { 0, 1, 1, 2, 3, 5, 8, 13 };
            Console.WriteLine("Task A");
            Console.WriteLine("");
            Console.WriteLine("Output of the first array:");
            Console.WriteLine(string.Join(" ", numFibonachi));

            // Вывод второго массива (задание 2 задача А)
            Console.WriteLine("");
            string[] Month = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            Console.WriteLine("Output of the second array:");
            Console.WriteLine(string.Join(" ", Month));

            // Вывод третьего массива (задание 3 задача А)
            Console.WriteLine("");
            int[,] numbers = new int[3, 3] { { 2, 3, 4 }, { (int)Math.Pow(2,2), (int)Math.Pow(3,2), (int)Math.Pow(4,2) }, { (int)Math.Pow(2, 3), (int)Math.Pow(3, 3), (int)Math.Pow(4, 3) } };
            Console.WriteLine("Output of the third array:");
            Console.Write(numbers[0,0]);
            Console.Write(",");
            Console.Write(numbers[0,1]);
            Console.Write(",");
            Console.Write(numbers[0,2]);
            Console.WriteLine("");
            Console.Write(numbers[1, 0]);
            Console.Write(",");
            Console.Write(numbers[1, 1]);
            Console.Write(",");
            Console.Write(numbers[1, 2]);
            Console.WriteLine("");
            Console.Write(numbers[2, 0]);
            Console.Write(",");
            Console.Write(numbers[2, 1]);
            Console.Write(",");
            Console.Write(numbers[2, 2]);
            Console.WriteLine("");

            // Вывод четвертого массива (задание 4 задача А)
            Console.WriteLine("");
            double[][] stepArray = new double[3][] { new double[5] { 1, 2, 3, 4, 5 }, new double[2] { Math.E, Math.PI }, new double[4] { Math.Log10(1), Math.Log10(10), Math.Log10(100), Math.Log10(1000) }, };
            Console.WriteLine("Output of the fourth array:");
            Console.WriteLine(string.Join(" ", stepArray[0]));
            Console.WriteLine(string.Join(" ", stepArray[1]));
            Console.WriteLine(string.Join(" ", stepArray[2]));
            Console.WriteLine("");

                        // массивы для заданий 5 и 6 (задача Б)
            Console.WriteLine("Task B");
            Console.WriteLine("");
            int[] array = { 1, 2, 3, 4, 5 };
            int[] array2 = { 7, 8, 9, 10, 11, 12, 13 };
            Console.WriteLine("The first array:");
            Console.WriteLine(string.Join(" ", array));
            Console.WriteLine("The second array:");
            Console.WriteLine(string.Join(" ", array2));
            Console.WriteLine("");
            Console.WriteLine("The result of copying the first three elements of the first array to the second array:");
            Array.Copy(array,array2,3);
            Console.WriteLine(string.Join(" ", array2));
            Console.WriteLine("");

            // Изменме размера первого массива в 2 раза
            Console.WriteLine("Output of the first array after doubling its size:");
            Array.Resize(ref array, array.Length+5);
            Console.WriteLine(string.Join(" ", array));
        }
    }
}