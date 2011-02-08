using System;

//Demonstrates automatic properties,
//             implicitly typed variables,
//             object initialization
namespace NewLanguageFeatures3
{
    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public bool Active { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            var p = new Person { Name = "Jens", Gender = "Male", Active = true };
        }
    }
}
