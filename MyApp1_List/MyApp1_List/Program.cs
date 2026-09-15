using System.Linq.Expressions;

namespace HomeWork
{
    internal class Program
    {
        private class ListTask
        {
            private readonly List<string> _listOfStrings = ["The", "Developer", "of", "Netology."]; // Тип данных любой

            public string? NewString { get; private set; }
            public int MiddleList; // { get; private set; }

            public void TaskLoop()
            {
                // проверка ввода и вывод результата

                Console.WriteLine(string.Join(" ", _listOfStrings));
                _listOfStrings.Add("I have a hobby — heavy metal music.");
                _listOfStrings.Add("I work as an IT specialist.");
                Console.WriteLine(string.Join(" ", _listOfStrings));

                Console.WriteLine("Please enter more information about yourself:");
                NewString = Console.ReadLine();

                if (NewString != null)
                {
                    _listOfStrings.Add(NewString);
                    Console.WriteLine(string.Join(" ", _listOfStrings));
                }

                Console.WriteLine("Please enter more information about yourself:");
                NewString = Console.ReadLine();
                MiddleList = _listOfStrings.Count / 2;

                if (NewString != null)
                {
                    _listOfStrings[MiddleList] = NewString;
                    Console.WriteLine(string.Join(" ", _listOfStrings));
                }

                Console.WriteLine();
                Console.WriteLine("If you’re done with the list, write “-exit”:");
                Console.ReadLine();

                do
                {
                    Console.ReadLine();
                }
                while (Console.ReadLine() != "-exit");
            }
        }

        private class DictionaryTask
        {
            
            private string? NameStudent;
            private string? Grade;

            private string? ch;
            private string? NameStudent_check;

            public void TaskLoop()
            {
                // проверка ввода и вывод результата

                var _dictionary = new Dictionary<string, int>();

                Console.WriteLine("Create a dictionary (students and their grades)");

                Console.WriteLine("Enter the student’s name:");
                NameStudent = Console.ReadLine();
                Console.WriteLine("Enter the student’s grade:");
                Grade = Console.ReadLine();

                if (NameStudent != null && Grade != null && NameStudent != "" && Grade != "")
                {
                    int IntGrade = int.Parse(Grade);

                    if (IntGrade >= 2 && IntGrade <= 5)
                    {
                        _dictionary.Add(NameStudent, IntGrade);
                    }
                    Console.WriteLine(string.Join(" ", _dictionary));
                }
                
                Console.WriteLine("Will you continue to enter the data? 'y'/'n'");
                ch = Console.ReadLine();

                do
                {
                    
                    if (ch == "y")
                    {
                        Console.WriteLine("Enter the student’s name:");
                        NameStudent = Console.ReadLine();
                        Console.WriteLine("Enter the student’s grade:");
                        Grade = Console.ReadLine();
                        if (NameStudent != null && Grade != null && NameStudent != "" && Grade != "")
                        {
                            int IntGrade = int.Parse(Grade);

                            if (IntGrade >= 2 && IntGrade <= 5)
                            {
                                _dictionary.Add(NameStudent, IntGrade);
                            }
                            //Console.WriteLine(string.Join(" ", _dictionary));
                            Console.WriteLine("Will you continue to enter the data? 'y'/'n'");
                            ch = Console.ReadLine();
                        }
                       
                    }
                    if (ch != "y" && ch != "n")
                    {
                        Console.WriteLine("Will you continue to enter the data? 'y'/'n'");
                        ch = Console.ReadLine();
                    }
                    
                }

                while (ch != "n");

                Console.WriteLine("Enter the student’s name from your dictionary:");
                NameStudent_check = Console.ReadLine();
                
                if (!string.IsNullOrEmpty(NameStudent_check))
                if (_dictionary.TryGetValue(NameStudent_check, out _)) 
                {
                    Console.WriteLine(Grade);
                }
                else Console.WriteLine("The student is not in the dictionary.");
            }

            private class DoublylinkedList
            {
                public string? Element { get; set; }

                public void TaskLoop()
                {

                    LinkedList<string> list = new();

                    Console.WriteLine("Create a doubly linked list.");
                    Console.WriteLine();

                    for (int i = 0; i < 6; i++)
                    
                    { 
                        Console.WriteLine("Enter the element:");
                        Element = Console.ReadLine();

                        if (Element != null)
                        {
                            list.AddLast(Element);
                        }
                    }
                    
                    Console.WriteLine("Elements in the forward direction:");
                    foreach (var Element in list)
                    {
                        Console.Write(Element + "\t");
                    }
                    
                    Console.WriteLine();
                    Console.WriteLine("Elements in reverse order:");
                    foreach (var Element in list.Reverse())
                    {
                        Console.Write(Element + "\t");
                    }
                }
            }
            
            static void Main(string[] args)
            {
                Console.WriteLine("Enter 1,2 or 3 to check task 1,2 or 3");
                int task = Convert.ToInt32(Console.ReadLine());
                switch (task)
                {
                    case 1:
                        CheckTaskFirst(); // Выполнение задания в отдельном методе
                        break;
                    case 2:
                        CheckTaskSecond(); // Выполнение задания в отдельном методе
                        break;
                    case 3:
                        CheckTaskFird(); // Выполнение задания в отдельном методе
                        break;
                }
            }

            private static void CheckTaskFirst()
            {
                var listTask = new ListTask();
                listTask.TaskLoop();
            }
            private static void CheckTaskSecond()
            {
                var DictionaryTask = new DictionaryTask();
                DictionaryTask.TaskLoop();
            }
            private static void CheckTaskFird()
            {
                var DoublylinkedList = new DoublylinkedList();
                DoublylinkedList.TaskLoop();
            }
        }
    }
}

