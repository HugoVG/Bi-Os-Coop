using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Bi_Os_Coop
{
    class CPeople
    {
        public static void Poggers()
        {
            People people = new People();
            people.addPersonByFunction(1, "Henk", "henk@henkino.com", "H3nko", 0);

            people.writePeople();
            Console.WriteLine(people.ToJson());
            People newPeople = people.FromJson(people.ToJson());

            newPeople.writePeople();
            people = people.FromJson(people.ToJson());

        }
        /// <summary>
        /// Person class
        /// Fields:
        ///     id, name?, email?, password?, age
        /// </summary>
        public class Person
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public int age { get; set; }
            //If you gonna edit this EDIT ALL
            public void setPerson(int id, string name, string email, string password, int age)
            {
                this.id = id;
                this.name = name;
                this.email = email;
                this.password = password;
                this.age = age;
            }
        }

        public class Admin : Person
        {

        }

        public class Employee : Person
        {

        }


        /// <summary>
        /// People class for JSON
        /// </summary>
        public class People
        {
            public List<Person> peopleList { get; set; }
            public List<Admin>  adminList { get; set; }
            public List<Employee> employeeList { get; set; }

            /// <summary>
            /// adds an Person class Object to the peopleList
            /// adds an Admin class Object to the adminList
            /// adds an Employee class Object to the employeeList
            /// </summary>
            /// <param name="personToAdd"></param>
            public void AddPerson(Person personToAdd)
            {
                if (peopleList == null)
                {
                    List<Person> newPerson = new List<Person>();
                    newPerson.Add(personToAdd);
                    peopleList = newPerson;
                }
                else
                {
                    peopleList.Add(personToAdd);
                }
            }

            public void AddAdmin(Admin personToAdd)
            {
                if (adminList == null)
                {
                    List<Admin> newAdmin = new List<Admin>();
                    newAdmin.Add(personToAdd);
                    adminList = newAdmin;
                }
                else
                {
                    adminList.Add(personToAdd);
                }
            }

            public void AddEmployee(Employee personToAdd)
            {
                if (employeeList == null)
                {
                    List<Employee> newEmployee = new List<Employee>();
                    newEmployee.Add(personToAdd);
                    employeeList = newEmployee;
                }
                else
                {
                    employeeList.Add(personToAdd);
                }
            }


            /// <summary>
            ///     Makes a new Person using a function and adding it to the object,
            ///     so it will put the json good
            /// </summary>
            /// <param name="id"></param>
            /// <param name="name"></param>
            /// <param name="email"></param>
            /// <param name="password"></param>
            /// <param name="age"></param>
            public void addPersonByFunction(int id, string name, string email, string password, int age)
            {
                Person temp = new Person();
                temp.setPerson(id, name, email, password, age);
                AddPerson(temp);
            }


            public void writePeople()
            {
                foreach (Person person in this.peopleList)
                {
                    Console.Write($"id:{person.id} \t");
                    Console.Write($"name:{person.name} \t");
                    Console.Write($"email:{person.email} \t");
                    Console.Write($"password:{person.password} \t");
                    Console.Write($"age:{person.age} \n");
                }
            }


            /// <summary>
            /// Will return This object
            /// </summary>
            /// <returns></returns>
            public string ToJson()
            {
                return JsonSerializer.Serialize(this);
            }
            public People FromJson(string json)
            {
                return JsonSerializer.Deserialize<People>(json);
            }
        }
    }
}
