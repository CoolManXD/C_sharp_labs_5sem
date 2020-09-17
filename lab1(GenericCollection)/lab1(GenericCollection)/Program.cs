using System;
using System.Collections.Generic;
using LinkedListCollection;

namespace lab1_GenericCollection_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var s1 = new LoopSingleLinkList<int>();
            s1.AddFirst(3);
            s1.AddFirst(2);
            s1.AddFirst(1);
            s1.AddLast(4);
            NodeWithLink<int> node = s1.Find(3);
            s1.AddAfter(node, 5);
            foreach (var temp in s1)
            {
                Console.WriteLine(temp);
            }
            Console.WriteLine($"First: {s1.First.Value} Last: {s1.Last.Value}");
            Console.WriteLine($"Есть 5: {s1.Contains(5)} Есть 6: {s1.Contains(6)}");
            Console.WriteLine($"Длина: {s1.Length}");
            Console.WriteLine($"Удалил 1: {s1.Remove(1)} First: {s1.First.Value} Last: {s1.Last.Value} Длина: {s1.Length}");
            foreach (var temp in s1)
            {
                Console.WriteLine(temp);
            }
            Console.WriteLine($"Удалил 4: {s1.Remove(4)} First: {s1.First.Value} Last: {s1.Last.Value} Длина: {s1.Length}");
            foreach (var temp in s1)
            {
                Console.WriteLine(temp);
            }

            Console.WriteLine();
            Console.WriteLine();

            var s2 = new LoopSingleLinkList<Person>();
            s2.AddFirst(new Person("Serhii", "Yanchuk", "nv3@gmail.com"));
            s2.AddFirst(new Person("Serhii", "Yanchuk", "nv2@gmail.com"));
            s2.AddFirst(new Person("Serhii", "Yanchuk", "nv1@gmail.com"));
            s2.AddLast(new Person("Serhii", "Yanchuk", "nv4@gmail.com"));
            NodeWithLink<Person> node2 = s2.Find(new Person("Serhii", "Yanchuk", "nv3@gmail.com"));
            s2.AddAfter(node2, new Person("Serhii", "Yanchuk", "nv5@gmail.com"));
            foreach (var temp in s2)
            {
                Console.WriteLine(temp);
            }
            Console.WriteLine($"First: {s2.First.Value} Last: {s2.Last.Value}");
            Console.WriteLine($"Есть 5: {s2.Contains(new Person("Serhii", "Yanchuk", "nv5@gmail.com"))} Есть 6: {s2.Contains(new Person("Serhii", "Yanchuk", "nv6@gmail.com"))}");
            Console.WriteLine($"Длина: {s2.Length}");
            Console.WriteLine($"Удалил 1: {s2.Remove(new Person("Serhii", "Yanchuk", "nv1@gmail.com"))} First: {s2.First.Value} Last: {s2.Last.Value} Длина: {s2.Length}");
            foreach (var temp in s2)
            {
                Console.WriteLine(temp);
            }
            Console.WriteLine($"Удалил 4: {s2.Remove(new Person("Serhii", "Yanchuk", "nv4@gmail.com"))} First: {s2.First.Value} Last: {s2.Last.Value} Длина: {s2.Length}");
            foreach (var temp in s2)
            {
                Console.WriteLine(temp);
            }
            Console.ReadKey();
        }
    }
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Person(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Person temp = obj as Person;
            if (temp == null)
                return false;
            if (FirstName == temp.FirstName && LastName == temp.LastName && Email == temp.Email)
                return true;
            return false;
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} {Email}";
        }
    }
    
    

    
}
