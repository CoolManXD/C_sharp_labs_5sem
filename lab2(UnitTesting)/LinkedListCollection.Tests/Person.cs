using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedListCollection.Tests
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Person temp = obj as Person;
            if (temp == null)
                return false;
            if (FirstName == temp.FirstName && LastName == temp.LastName)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return FirstName.GetHashCode() + LastName.GetHashCode();
        }
        public override string ToString()
        {
            return $"{FirstName[0]}. {LastName[0]}";
        }
    }
}
