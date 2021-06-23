using System;
using System.Collections.Generic;

namespace ValueReferenceTypeExercise
{
    class Program
    {
        public class DetailedInteger : ICloneable
        {
            public int Number { get; set; }
            public List<string> Details { get; set; }

            public DetailedInteger(int number)
            {
                Number = number;
                Details = new List<string>();
            }

            public DetailedInteger(DetailedInteger detailedInteger)
            {
                this.Number = detailedInteger.Number;
                this.Details = detailedInteger.Details;
            }

            public void AddDetail(string detail)
            {
                Details.Add(detail);
            }

            public override string ToString()
            {
                return $"{Number} [{ string.Join(',', Details) }]";
            }

            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }

        static void Main(string[] args)
        {
            var n1 = new DetailedInteger(0);
            n1.AddDetail("A");
            Console.WriteLine(n1);

            var n2 = n1;
            n2.Number = 7;
            n2.AddDetail("B");

            Console.WriteLine(n1);
            Console.WriteLine(n2);
        }
    }
}
