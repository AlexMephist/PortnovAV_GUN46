class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Занятие 1: Логические операции");

        if (!int.TryParse(Console.ReadLine(), out int a))
        {
            Console.WriteLine("Ошибка: это не число!");
            return;
        }
        if (!int.TryParse(Console.ReadLine(), out int b))
        {
            Console.WriteLine("Ошибка: это не число!");
            return;
        }

        var s = Console.ReadLine();

        switch (s[0])
        {
            case '&':
                Console.WriteLine("Результат {0} & {1} = {2}", a, b, a & b);
                Console.WriteLine(Convert.ToString(a & b, 2));
                Console.WriteLine(Convert.ToString(a & b, 16));
                break;
            case '|':
                Console.WriteLine("Результат {0} | {1} = {2}", a, b, a | b);
                Console.WriteLine(Convert.ToString(a | b, 2));
                Console.WriteLine(Convert.ToString(a | b, 16));
                break;
            case '^':
                Console.WriteLine("Результат {0} ^ {1} = {2}", a, b, a ^ b);
                Console.WriteLine(Convert.ToString(a ^ b, 2));
                Console.WriteLine(Convert.ToString(a ^ b, 16));
                break;
            default:
                Console.WriteLine("Неверный символ");
                break;
        }
    }
}

