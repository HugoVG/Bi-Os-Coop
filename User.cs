using System;
namespace Bi_Os_Coop
{
    public class User
    {
        // fields
        private int age;
        private bool isAdmin = false;
        private bool isEmployee = false;

        // constructor
        public User(string name, int age, int phoneNumber, string emailAddress)
        {
            this.Name = name;
            this.Age = age;
            this.PhoneNumber = phoneNumber;
            this.EmailAddress = emailAddress;
        }

        // properties
        public string Name
        { get; set; }

        public int Age
        { get; set; }

        public int PhoneNumber
        { get; set; }

        public string EmailAddress
        { get; set; }

        // methods
        public static void Login()
        {

        }
    }


    public class Admin : User
    {
        private bool isAdmin = true;
    }


    public class Employee : User
    {
        private bool isEmployee = true;
    }
}
