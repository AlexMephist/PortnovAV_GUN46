namespace HomeWork_Cycles
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Вывод певрых 10 чисел Фиббоначи (задание 1)
            
            int[] numFibonacci = { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            
            Console.WriteLine("Output of the first 10 Fibonacci numbers (task 1):");
            Console.WriteLine();

            for (int i = 0; i < numFibonacci.Length; i++)
            {
                Console.Write(numFibonacci[i]);
                Console.Write(" ");
            }
            Console.WriteLine();

            // Вывод четных чисел от 2 до 20 (задание 2)

            int[] evennumbers = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
            
            Console.WriteLine();    
            Console.WriteLine("Output of even numbers from 2 to 20 (task 2):");
            Console.WriteLine();

            for (int i = 0; i < evennumbers.Length; i++)
            {
                Console.Write(evennumbers[i]);
                Console.Write(" ");
            }
            Console.WriteLine();

            // Вывод таблицы умножения от 1 до 5 (задание 3)

            int[,] MultTabl = new int[5, 5] { { 1, 2, 3, 4, 5 }, { 2, 4, 6, 8, 10 }, { 3, 6, 9, 12, 15 }, { 4, 8, 12, 16, 20 }, { 5, 10, 15, 20, 25 } };

            Console.WriteLine();
            Console.WriteLine("Multiplication table from 1 to 5 (task 3):");
            Console.WriteLine("");
            Console.WriteLine("      1   2   3   4   5");
            Console.WriteLine("-----------------------");

            for (int i = 0; i < 5; i++)
            {
                var num = Convert.ToString(i+1);
                Console.Write(num + "| ");
                
                for (int j = 0; j < 5; j++)
                {
                    Console.Write("{0,4}",MultTabl[i, j]);
                }
                Console.WriteLine();
            }


            // Программа ввода пароля (задание 4)

            string? Passwrd;
            
            Console.WriteLine();
            Console.WriteLine("Password entry program (task 4):");
            Console.WriteLine();
            Console.WriteLine("Enter the password:");

            do 
            {
               Passwrd = Console.ReadLine();
               if (Passwrd != "qwerty")
                  Console.WriteLine("The password is entered incorrectly!");
                  Console.WriteLine("Enter the password:");

            } 
            while (Passwrd != "qwerty");

            if (Passwrd == "qwerty") Console.WriteLine("The password was entered correctly!");
                                  

        }
    }
}
