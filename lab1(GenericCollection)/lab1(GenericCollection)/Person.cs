using System;
using System.Collections.Generic;
using System.Text;

namespace lab1_GenericCollection_
{
    public class Person
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
        public override int GetHashCode()
        {
            return FirstName.GetHashCode() + LastName.GetHashCode() + Email.GetHashCode();
        }
        public override string ToString()
        {
            return $"{FirstName[0]}. {LastName[0]}. {Email}";
        }
    }
}
