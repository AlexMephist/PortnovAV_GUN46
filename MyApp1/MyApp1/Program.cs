class Program
{
    private static int x;

    static void Main(string[] args)
    {
        Console.WriteLine("Task 1: Logical operations");
        Console.WriteLine("");
        
        Console.WriteLine("Enter the first number:");
        if (!int.TryParse(Console.ReadLine(), out int a))
        {
            Console.WriteLine("Error: this is not a number!");
            return;
        }

        Console.WriteLine("Enter the second number:");
        if (!int.TryParse(Console.ReadLine(), out int b))
        {
            Console.WriteLine("Error: this is not a number!");
            return;
        }

        Console.WriteLine("");
        Console.WriteLine("Enter the operator &, |, or ^:");

        char.TryParse(Console.ReadLine(), out char ch);
        
        
        switch (ch)
        {
            case '&':
                x = a & b;
                break;
            case '|':
                x = a | b;
                break;
            case '^':
                x = a ^ b;
                break;
            default:
                Console.WriteLine("Error: invalid operator!");
                break;
        }
        Console.WriteLine("Result Decimal form:");
        Console.WriteLine(Convert.ToString(x));
        Console.WriteLine("Binary form:");
        Console.WriteLine(Convert.ToString(x, 2));
        Console.WriteLine("Hexadecimal form:");
        Console.WriteLine(Convert.ToString(x, 16));
    }
}

