using System;
using System.Reflection;

namespace ReflectionExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            //Strings Example
            Console.WriteLine("STRING EXAMPLE");
            string stringVar = "my custom string";

            Type stringType = stringVar.GetType();
            FieldInformation(stringType);
            MethodInformation(stringType);
            PropertiesInformation(stringType);

            //DateTime Example
            Console.WriteLine("\n\n\nDATETIME EXAMPLE");
            DateTime dateTimeVar = DateTime.Now;

            Type dateTimeType = dateTimeVar.GetType();
            FieldInformation(dateTimeType);
            MethodInformation(dateTimeType);
            PropertiesInformation(dateTimeType);

            //Custom Class Example
            Console.WriteLine("\n\n\nCUSTOM CLASS EXAMPLE");
            Product myNewClass = new(1, "my class object description", 200.0M);

            Type myNewClassType = myNewClass.GetType();
            FieldInformation(myNewClassType);
            MethodInformation(myNewClassType);
            PropertiesInformation(myNewClassType);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\nMore Class INFO");
            Console.WriteLine($"Class BaseClass: {myNewClassType.BaseType}");
            Console.WriteLine($"Is Abstract: {myNewClassType.IsAbstract}");
            Console.WriteLine($"Is Sealed: {myNewClassType.IsSealed}");
            Console.WriteLine($"Is Interface: {myNewClassType.IsInterface}");

            Console.ForegroundColor = ConsoleColor.White;

            //Method Loaded
            MethodInfo myMethod = myNewClassType.GetMethod("MyCustomMethod");
            object obj = Activator.CreateInstance(myNewClassType, new object[] { 2, "dynamic instance of class", 100.0M });
            Console.WriteLine("Dynamic Method Invoked: ");
            Console.WriteLine(myMethod.Invoke(obj, new object[] { "my string parameter" }));

            //GET Assembly Info of class Math
            Console.WriteLine("\n\n\nGET ASSEMBLIE OF CLASS MATH");
            Assembly assemblyInfo = typeof(Math).Assembly;
            Console.WriteLine(assemblyInfo);

            //GET Assembly Info of custom class
            Console.WriteLine("\n\n\nGET ASSEMBLIE OF CUSTOM CLASS Product");
            Assembly assemblyInfoCustom = typeof(Product).Assembly;
            Console.WriteLine(assemblyInfoCustom);

            //LOAD Assemblies
            Console.WriteLine("\n\n\nLOADING ASSEMBLIES EXAMPLE");
            try
            {
                Assembly assembly = Assembly.Load(assemblyInfo.ToString());
                DisplayAssembly(assembly);
            }
            catch
            {
                Console.WriteLine("Can't Load Assembly");
            }


            //LOAD External Assemblies
            Console.WriteLine("\n\n\nLOADING EXTERNAL ASSEMBLIES EXAMPLE");
            try
            {
                Assembly externalAssembly = Assembly.LoadFrom(@"C:\netProjects\BackendDevBootcamp04\ReflectionExercise\MyCustomLibrary\bin\Release\net5.0\MyCustomLibrary.dll");
                DisplayAssembly(externalAssembly);
            }
            catch
            {
                Console.WriteLine("Can't Load External Assembly");
            }
        }

        //Method for Display Assembly
        static void DisplayAssembly(Assembly assembly)
        {
            Console.WriteLine($"Name: {assembly.GetName().Name}");
            Console.WriteLine($"Version: {assembly.GetName().Version}");
            Console.WriteLine($"Culture: {assembly.GetName().CultureInfo.DisplayName}");
        }


        //Method for explore fields of a type
        static void FieldInformation(Type type)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("************ Fields ***********");
            Console.ForegroundColor = ConsoleColor.White;
            FieldInfo[] fieldInfos = type.GetFields();

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                Console.WriteLine($"{fieldInfo.Name}");
            }
        }

        //Method for explore methods of a type
        static void MethodInformation(Type type)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("************ Methods ***********");
            Console.ForegroundColor = ConsoleColor.White;
            MethodInfo[] methodInfos = type.GetMethods();

            foreach (MethodInfo methodInfo in methodInfos)
            {
                Console.WriteLine($"{methodInfo.Name}");
            }
        }

        //Method for explore properties of a type
        static void PropertiesInformation(Type type)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("************ Properties ***********");
            Console.ForegroundColor = ConsoleColor.White;
            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Console.WriteLine($"{propertyInfo.Name}");
            }
        }
    }

    class BaseProduct
    {
        public string Code { get; set; }
    }

    class Product : BaseProduct
    {
        private string _myField1;
        public int _myPublicField1;
        protected bool _myProtectedField1;

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Product(int id, string description, decimal price)
        {
            Id = id;
            Description = description;
            Price = price;

            _myField1 = "my field";
            _myPublicField1 = 1;
            _myProtectedField1 = true;
        }

        public string MyCustomMethod(string data)
        {
            return data;
        }

        private bool MyPrivateMethod(bool condition)
        {
            return condition;
        }
    }
}
