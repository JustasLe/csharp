using System;

namespace EmployeeClass
{
    class Employee
    {
        private string firstName, lastName, personalId;

        public Employee()
        {
            firstName = lastName = personalId = "";
        }

        public Employee(string firstName, string lastName, string personalId)
        {
            if (personalId.Length != 11)
            {
                throw new Exception("Personal ID must be of length 11.");
            }
            this.firstName = firstName;
            this.lastName = lastName;
            this.personalId = personalId;
        }

        public void ChangeLastName(string newLastName)
        {
            lastName = newLastName;
        }

        public static bool operator <(Employee first, Employee second)
        {
            if (string.Compare(first.firstName, second.firstName) == 0)
            {
                return string.Compare(first.lastName, second.lastName) < 0;
            }
            return string.Compare(first.firstName, second.firstName) < 0;
        }

        public static bool operator >(Employee first, Employee second)
        {
            if (string.Compare(first.firstName, second.firstName) == 0)
            {
                return string.Compare(first.lastName, second.lastName) > 0;
            }
            return string.Compare(first.firstName, second.firstName) > 0;
        }

        public override string ToString()
        {
            return string.Format("{0, 15} {1, 15}", firstName, lastName);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee firstEmployee = new Employee("John", "Smith", "00000000000");
            Employee secondEmployee = new Employee("James", "Johnson", "00000000001");
            Console.WriteLine(firstEmployee);
            Console.WriteLine(secondEmployee);
            Console.WriteLine("First employee goes {0} second employee in a sorted list of employees.",
                (firstEmployee < secondEmployee ? "before" : "after"));
        }
    }
}
