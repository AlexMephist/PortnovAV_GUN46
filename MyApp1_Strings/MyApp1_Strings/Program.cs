namespace HomeWork_Strings
{
    internal class Program
    {
        public static string? String1 { get; private set; }
        public static string? String2 { get; private set; }

        public static void ConcatenateStrings(string string1, string string2)
        
        {
            Console.WriteLine(string1+ string2);
        }
        
        
        static void Main(string[] args)
        {
            // Задание 1

            Console.WriteLine("Task1");
            
            Console.WriteLine("Input the first string:");
            String1=Console.ReadLine();
            Console.WriteLine("Input the second string:");
            String2 = Console.ReadLine();

            if (String1 != null && String2 != null)
            {
                ConcatenateStrings(String1, String2);
            }
            
            // Задание 2



            // Задание 3



            // Задание 4



            // Задание 5



            // Задание 6



        }
    }
}
