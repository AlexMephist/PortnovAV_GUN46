using MyApp1_List;

namespace HomeWork
{
    internal class Program
    {
        private class ListTask
        {
            private readonly List<string> _listOfStrings = ["The", "Developer", "of", "Netology."]; // Тип данных любой

            // public string? NewString { get; private set; }
            // public int MiddleList; 

            public void TaskLoop()
            {
                string? NewString;

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
                //MiddleList = _listOfStrings.Count / 2;

                if (NewString != null)
                {
                    _listOfStrings.Insert(_listOfStrings.Count / 2, NewString);
                    Console.WriteLine(string.Join(" ", _listOfStrings));
                }

                Console.WriteLine();
                Console.WriteLine("If you’re done with the list, write “-exit”:");

                while (Console.ReadLine() != "-exit") ;


            }
        }

        private class DictionaryTask
        {

            private string? NameStudent;
            private string? Grade;

            private string? answer;
            private string? searchedName;
            private int IntGrade;

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
                    _ = int.TryParse(Grade, out int IntGrade);

                    if (IntGrade >= 2 && IntGrade <= 5)
                    {
                        _dictionary.TryAdd(NameStudent, IntGrade);
                    }
                    else
                        Console.WriteLine("Score outside the 2–5 range!");
                    
                    // Console.WriteLine(string.Join(" ", _dictionary));
                }

                Console.WriteLine("Will you continue to enter the data? 'y'/'n'");
                answer = Console.ReadLine();

                do
                {

                    if (answer == "y")
                    {
                        Console.WriteLine("Enter the student’s name:");
                        NameStudent = Console.ReadLine();
                        Console.WriteLine("Enter the student’s grade:");
                        Grade = Console.ReadLine();
                        if (NameStudent != null && Grade != null && NameStudent != "" && Grade != "")
                        {
                            _ = int.TryParse(Grade, out int IntGrade);

                            if (IntGrade >= 2 && IntGrade <= 5)
                            {
                                _dictionary.TryAdd(NameStudent, IntGrade);
                            }
                            else
                                Console.WriteLine("Score outside the 2–5 range!");

                            Console.WriteLine("Will you continue to enter the data? 'y'/'n'");
                            answer = Console.ReadLine();
                        }

                    }
                    if (answer != "y" && answer != "n")
                    {
                        Console.WriteLine("Will you continue to enter the data? 'y'/'n'");
                        answer = Console.ReadLine();
                    }

                }

                while (answer != "n");

                Console.WriteLine("Enter the student’s name from your dictionary:");
                searchedName = Console.ReadLine();

                if (!string.IsNullOrEmpty(searchedName))
                    if (_dictionary.TryGetValue(searchedName, out int Grade))
                    {
                        Console.WriteLine(Grade);
                    }
                    else Console.WriteLine("The student is not in the dictionary.");
            }

        }


        public class DoublyLinkedList<T>
        {
            private Node<T>? head;
            private Node<T>? tail;

            public string? Element { get; private set; }


            // Добавление элемента
            public void AddElement(T data)
            {
                var newNode = new Node<T>(data)
                {
                    Next = head,
                    Previous = null
                };

                if (head != null)
                {
                    head.Previous = newNode;
                }
                else
                {
                    tail = newNode; // Если список был пустым, новый узел становится и tail
                }

                head = newNode;

            }

            // Прямой обход (от начала к концу)
            public void GoForward()
            {
                Node<T> current = head;
                while (current != null)
                {
                    Console.Write(current.Data + " ");
                    current = current.Next;
                }
                Console.WriteLine();
            }

            // Обратный обход (от конца к началу)
            public void GoBackward()
            {
                Node<T> currentNode = tail;
                while (currentNode != null)
                {
                    Console.Write(currentNode.Data + " ");
                    currentNode = currentNode.Previous;
                }
                Console.WriteLine();
            }

            internal void TaskLoop()
            {
                Console.WriteLine("Enter 3 to 6 elements of a doubly linked list:");
                
                var DoublylinkedList = new DoublyLinkedList<string>();
                string? Count;
                int Number = 0;
                Count = Console.ReadLine();
                                
                if (Count != null && Count != "" && Convert.ToInt32(Count) >= 3 && Convert.ToInt32(Count) <= 6)

                {
                    _ = int.TryParse(Count, out Number);
                    
                }

                for (int i = 0; i < Number; i++)
                
                {
                    Element = Console.ReadLine();
                    if (Element != null && Element != "")
                    DoublylinkedList.AddElement(Element);
                    
                }

                Console.WriteLine("Forward:");
                DoublylinkedList.GoForward();
                Console.WriteLine("Backward:");
                DoublylinkedList.GoBackward();

            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter 1,2 or 3 to check task 1,2 or 3");

            string? task;

            task = Console.ReadLine();

            do
            {
                if (task != null && task != "")

                {
                    _ = int.TryParse(task, out _);
                
                }
                
                 
                switch (task)
                    {
                    case "1":
                        CheckTaskFirst(); // Выполнение задания в отдельном методе
                        break;
                    case "2":
                        CheckTaskSecond(); // Выполнение задания в отдельном методе
                        break;
                    case "3":
                        CheckTaskThird(); // Выполнение задания в отдельном методе
                        break;
                    }                   
            
                task = Console.ReadLine();
            }
            while (task != "1" || task != "2" || task != "3");
                                    
                       
            
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
        private static void CheckTaskThird()
        {
            var DoublylinkedList = new DoublyLinkedList<string>();
            DoublylinkedList.TaskLoop();
        }



    }
}