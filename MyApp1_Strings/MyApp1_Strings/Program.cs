using System.Text;

namespace HomeWork_Strings
{
    internal class Program
    {
        private static readonly string[]? stringsArray;
        private static int IntCountStrings;
        private static string[] strs;

        // public static string? String1 { get; private set; }
        // public static string? String2 { get; private set; }

        public static void ConcatenateStrings(string string1, string string2)

        {
            Console.WriteLine(string1 + string2);
        }

        public static void GreetUser(string name, int age)

        {
            Console.WriteLine($"Hello, {name}!\nYou are {age} years old");
        }

        public static void CounterChars(string text)

        {
            int uppCount = text.Count(Char.IsUpper);
            int lowCount = text.Count(Char.IsLower);

            Console.WriteLine($"The number of characters in the string {text} in uppercase: {uppCount}");
            Console.WriteLine($"The number of characters in the string {text} in lowercase: {lowCount}");

        }

        public static void SymbolsofSubstr(string text)

        {
            text = text.Substring(0, 5);
            Console.WriteLine(text);

        }

        public static void SentenceBuilder(string[] arraystrings)

        {
            StringBuilder sentencebuilder = new();

            for (int i = 0; i < arraystrings.Length; i++)
            {
                sentencebuilder.Append(arraystrings[i] + " ");
            }
            Console.WriteLine(sentencebuilder.ToString());
        }

        public static void ChangeWord(string str1, string str2, string str3)

        {
            
            string original = str1;
            string modified = original.Replace(str2, str3);
            Console.WriteLine(modified);

        }

        static void Main(string[] args)
        {
            // Задание 1

            Console.WriteLine("Task1");

            Console.WriteLine("Input the first string:");
            string? String1 = Console.ReadLine();
            Console.WriteLine("Input the second string:");
            string? String2 = Console.ReadLine();

            if (String1 != null && String2 != null)
            {
                ConcatenateStrings(String1, String2);
            }

            Console.WriteLine();

            // Задание 2

            Console.WriteLine("Task2");
            Console.WriteLine("Enter the name:");
            string? Name = Console.ReadLine();
            Console.WriteLine("Enter age:");
            int Age = Convert.ToInt32(Console.ReadLine());

            if (Name != null)
            {
                // int IntAge = Convert.ToInt32(Age);
                GreetUser(Name, Age);
            }

            Console.WriteLine();

            // Задание 3


            Console.WriteLine("Task3");
            Console.WriteLine("Enter the string:");
            string? Text = Console.ReadLine();

            if (Text != null)
            {
                CounterChars(Text);
            }

            Console.WriteLine();

            // Задание 4

            Console.WriteLine("Task4");
            Console.WriteLine("Enter the string:");
            string? Text2 = Console.ReadLine();

            if (Text2 != null)
            {
                SymbolsofSubstr(Text2);
            }

            Console.WriteLine();


            // Задание 5

            Console.WriteLine("Task5");
            Console.Write("Enter the number of strings: ");
            int number = Convert.ToInt32(Console.ReadLine());

            strs = new string[number];

            for (int i = 0; i < number; i++)
            {
                Console.Write("Enter the string №{0}: ", i + 1);
                strs[i] = Console.ReadLine();
            }

            Console.WriteLine();

            SentenceBuilder(strs);

            Console.WriteLine();

            // Задание 6

            Console.WriteLine("Task6");
            Console.Write("Enter the string: ");
            string? Str1 = Console.ReadLine();
            Console.Write("Enter the word to search for: ");
            string? Str2 = Console.ReadLine();
            Console.Write("Enter the word to be replaced: ");
            string? Str3 = Console.ReadLine();

            if (Str1 != null && Str2 != null && Str3 != null)
            {
                ChangeWord(Str1, Str2, Str3);
            }

        }
    }
}

