using System;
using System.IO;
using LinkedListCollection;

namespace lab1_GenericCollection_
{
    class Program
    {
        static void Main(string[] args)
        {
            //var list2 = new LoopSingleLinkList<int>();
            //NodeWithLink<int> n = null;

            checkValueType();
            Console.WriteLine("\n");
            checkReferenceType();
            Console.WriteLine("\n");
            checkReferenceTypeWithNull();
            Console.WriteLine("\n");

            Console.WriteLine("Пустая коллекция:");
            var list = new LoopSingleLinkList<int>();
            ShowList(list);

            Console.WriteLine("\n");
            Console.WriteLine("Коллекция с одного элемента:");
            list.AddFirst(1);
            ShowList(list);

            Console.ReadKey();
        }
        public static void SaveList<T>(NodeWithLink<T> node)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("list.txt", false, System.Text.Encoding.Default))
                {
                    if (node != null) // список пуст
                    {
                        NodeWithLink<T> currentNode = node;
                        do
                        {
                            if (currentNode.Value != null) // значение в узле равно null
                                sw.WriteLine(currentNode.Value);
                            currentNode = currentNode.Next;
                        } while (currentNode != node); // перебор циклического списка                  
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            }
        }
        public static void checkValueType()
        {
            Console.WriteLine("Проверка value type\n");
            var list = new LoopSingleLinkList<int>();
            list.Save += SaveList;

            try 
            {
                list.AddFirst(3);
                list.AddFirst(2);
                list.AddFirst(new NodeWithLink<int>(1));
                list.AddLast(4);

                NodeWithLink<int> node = list.Find(3);
                list.AddAfter(node, 5);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            }
            
            Console.WriteLine($"First: {list.First?.Value} Last: {list.Last?.Value}");
            Console.WriteLine($"Есть 5: {list.Contains(5)} \nЕсть 6: {list.Contains(6)}");
            Console.WriteLine($"Длина: {list.Length}");
            //var a = list.First;
            //a.Next = null;
            ShowList(list);           
            
            Console.WriteLine($"\nУдалено 1: {list.Remove(1)} \nFirst: {list.First?.Value} Last: {list.Last?.Value} Длина: {list.Length}");
            ShowList(list);

            Console.WriteLine($"\nУдалено 4: {list.Remove(4)} \nFirst: {list.First?.Value} Last: {list.Last?.Value} Длина: {list.Length}");
            ShowList(list);

            Console.WriteLine($"\nУдалено 10: {list.Remove(10)}");
            ShowList(list);
            list.Clear();
        }
        public static void checkReferenceType()
        {
            Console.WriteLine("Проверка reference type\n");
            var list = new LoopSingleLinkList<Person>();

            list.Save += SaveList;

            try 
            {
                list.AddFirst(new Person("Serhii", "Yanchuk", "nv3@gmail.com"));
                list.AddFirst(new Person("Serhii", "Yanchuk", "nv2@gmail.com"));
                list.AddFirst(new NodeWithLink<Person>(new Person("Serhii", "Yanchuk", "nv1@gmail.com")));
                list.AddLast(new Person("Serhii", "Yanchuk", "nv4@gmail.com"));

                NodeWithLink<Person> node = list.Find(new Person("Serhii", "Yanchuk", "nv3@gmail.com"));
                list.AddAfter(node, new Person("Serhii", "Yanchuk", "nv5@gmail.com"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            }

            Console.WriteLine($"First: {list.First?.Value} \nLast: {list.Last?.Value}");
            Console.WriteLine($"Есть 5: {list.Contains(new Person("Serhii", "Yanchuk", "nv5@gmail.com"))} \nЕсть 6: {list.Contains(new Person("Serhii", "Yanchuk", "nv6@gmail.com"))}");
            Console.WriteLine($"Длина: {list.Length}");
            ShowList(list);

            Console.WriteLine($"\nУдалено 1: {list.Remove(new Person("Serhii", "Yanchuk", "nv1@gmail.com"))} \nFirst: {list.First?.Value} \nLast: {list.Last?.Value} Длина: {list.Length}");
            ShowList(list);

            Console.WriteLine($"\nУдалено 4: {list.Remove(new Person("Serhii", "Yanchuk", "nv4@gmail.com"))} \nFirst: {list.First?.Value} \nLast: {list.Last?.Value} Длина: {list.Length}");
            ShowList(list);

            Console.WriteLine($"\nУдалено 10: {list.Remove(new Person("Serhii", "Yanchuk", "nv10@gmail.com"))}");
            ShowList(list);
            list.Clear();
        }
        public static void checkReferenceTypeWithNull()
        {
            Console.WriteLine("Проверка reference type c null\n");
            var list = new LoopSingleLinkList<Person>();

            try
            {
                list.AddLast(new Person("Serhii", "Yanchuk", "nv1@gmail.com"));
                Person serhii = null;
                list.AddLast(serhii);
                list.AddLast(new Person("Serhii", "Yanchuk", "nv3@gmail.com"));
                list.AddLast(new Person("Serhii", "Yanchuk", "nv4@gmail.com"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            }           

            Console.WriteLine($"First: {list.First?.Value} \nLast: {list.Last?.Value}");
            Console.WriteLine($"Длина: {list.Length}");
            ShowList(list);

            NodeWithLink<Person> node = list.Find(null);
            Console.WriteLine($"\nНайден null value: {node != null}");

            list.Remove(null);
            Console.WriteLine("\nУдалено узел с null value");
            ShowList(list);
        }
        public static void ShowList<T>(LoopSingleLinkList<T> list)
        {
            try
            {
                int i = 0;
                foreach (var temp in list)
                    Console.WriteLine($"{++i}). {temp}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
            }
        }
    } 
}