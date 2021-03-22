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
        public User(string name, int age, string phoneNumber, string emailAddress)
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

        public string PhoneNumber
        { get; set; }

        public string EmailAddress
        { get; set; }

        // methods
        public static void Login()
        {
        }

        public static void Logout()
        {
        }

        public static void ChangePassword()
        {
        }

        public static void ViewMovies()
        {
        }

        public static void BookTicket()
        {
        }

        public static void CancelTicket()
        {
        }
    }


    public class Admin : User
    {
        private bool isAdmin = true;

        public Admin(string name, int age, string phoneNumber, string emailAddress)
            : base(name, age, phoneNumber, emailAddress)
        {
        }

        public static void AddMovies()
        {
        }

        public static void UpdateMovies()
        {
        }

        public static void DeleteMovies()
        {
        }

        public static void ChangeCinemaHalls()
        {
        }
    }


    public class Employee : User
    {
        private bool isEmployee = true;

        public Employee(string name, int age, string phoneNumber, string emailAddress)
            : base(name, age, phoneNumber, emailAddress)
        {
        }
    }
}
