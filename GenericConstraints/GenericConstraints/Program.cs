using System;
using System.Collections;

namespace GenericConstraints
{
    class Program
    {
        static void Main(string[] args)
        {
            MyGeneric<ArrayList> x = new();
        }

        public class MyGeneric<T> where T : ICollection
        {

        }

        public class MyGenericClass
        {
            public MyGenericClass()
            {

            }
        }

        public class AnotherClass : MyGenericClass
        {

        }

        public class AnotherClass2
        {

        }
    }
}
