using System.Text;

namespace HomeWork_Strings
{
    internal class Program
    {
       

        public static string ConcatenateStrings(string string1, string string2)
        {
            return string1 + string2;

        }


        public static string GreetUser(string name, int age) => $"Hello, {name}!\nYou are {age} years old.";

        
        public static string CounterChars(string text)

        {
           
           return text.Length.ToString();
           
        }

        public static string GetFirstFiveChars(string text)

        {
            text = text.Substring(0, 5);
            return text;
        }

        public static string SentenceBuilder(string[] arraystrings)

        {
            StringBuilder sentencebuilder = new();

            for (int i = 0; i < arraystrings.Length; i++)
            {
                sentencebuilder.Append(arraystrings[i]).Append(" ");
            }
            // Console.WriteLine(sentencebuilder.ToString());

            return sentencebuilder.ToString();
        }

        public static string ReplaceWords(string input, string wordToReplace, string replacementWord) 

        {
            
            string original = input;
            string modified = original.Replace(wordToReplace, replacementWord);
            return modified;

        }

        static void Main(string[] args)
        {
            // Задание 1

            Console.WriteLine("Task1");

            Console.WriteLine("Input the first string:");
            string? first = Console.ReadLine();
            Console.WriteLine("Input the second string:");
            string? second = Console.ReadLine();
            if (first != null && second != null)
            {
                string result = ConcatenateStrings(first, second);
                Console.WriteLine(result);
            }

            Console.WriteLine();

            // Задание 2

            Console.WriteLine("Task2");
            Console.WriteLine("Enter the name:");
            string? Name = Console.ReadLine();
            Console.WriteLine("Enter age:");
            int Age = Convert.ToInt32(Console.ReadLine());
            
            if (Age > 0 && Name != null)
            {
             
                Console.WriteLine(GreetUser(Name, Age));
            }

            Console.WriteLine();

            // Задание 3


            Console.WriteLine("Task3");
            Console.WriteLine("Enter the string:");
            string? Text = Console.ReadLine();

            if (Text != null)
            {
                Console.WriteLine("In the original string of characters:" + CounterChars(Text));
                Console.WriteLine(Text.ToUpper());
                Console.WriteLine(Text.ToLower());
            }

            Console.WriteLine();

            // Задание 4

            Console.WriteLine("Task4");
            Console.WriteLine("Enter the string:");
            string? Text2 = Console.ReadLine();

            if (Text2 != null && Text2.Length >= 5)
            {
                
                Console.WriteLine(GetFirstFiveChars(Text2));
                
            }
            else Console.WriteLine("The entered string is less than 5 characters.");

            Console.WriteLine();


            // Задание 5

            Console.WriteLine("Task5");
            Console.Write("Enter the number of strings: ");
            int number = Convert.ToInt32(Console.ReadLine());
            
            string[] strs = new string[number];
            
            for (int i = 0; i < number; i++)
            {
                Console.Write("Enter the string №{0}: ", i + 1);
                strs[i] = Console.ReadLine();
            }

            Console.WriteLine();

            Console.WriteLine(SentenceBuilder(strs));

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
                
                Console.WriteLine(ReplaceWords(Str1, Str2, Str3));
            
            }

        }
    }
}

