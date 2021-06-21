using System;

namespace VariableDeclaration
{
    class Program
    {
        static void Main(string[] args)
        {
            int integer = -100;
            int integer2 = -128;
            int integer3 = 3540;
            int integer4 = 64876;
            long integer5 = 2147483648;
            int integer6 = -1141583228;
            long integer7 = -1223372036854770;
            int integer8 = 808;
            int integer9 = 2_000_000;
            int integer10 = 0b_0001_1110_1000_0100_1000_0000;
            int integer11 = 0x_001E_8480;

            Console.WriteLine($"the value of {nameof(integer)} with type: {integer.GetType()} is: {integer}");
            Console.WriteLine($"the value of {nameof(integer2)} with type: {integer2.GetType()} is: {integer2}");
            Console.WriteLine($"the value of {nameof(integer3)} with type: {integer3.GetType()} is: {integer3}");
            Console.WriteLine($"the value of {nameof(integer4)} with type: {integer4.GetType()} is: {integer4}");
            Console.WriteLine($"the value of {nameof(integer5)} with type: {integer5.GetType()} is: {integer5}");
            Console.WriteLine($"the value of {nameof(integer6)} with type: {integer6.GetType()} is: {integer6}");
            Console.WriteLine($"the value of {nameof(integer7)} with type: {integer7.GetType()} is: {integer7}");
            Console.WriteLine($"the value of {nameof(integer8)} with type: {integer8.GetType()} is: {integer8}");
            Console.WriteLine($"the value of {nameof(integer9)} with type: {integer9.GetType()} is: {integer9}");
            Console.WriteLine($"the value of {nameof(integer10)} with type: {integer10.GetType()} is: {integer10}");
            Console.WriteLine($"the value of {nameof(integer11)} with type: {integer11.GetType()} is: {integer11}");

            decimal decimalNumber = 3.141592653589793238M;
            double doubleNumber = 1.60217657;
            decimal decimalNumber2 = 7.8184261974584555216535342341M;

            Console.WriteLine($"the value of {nameof(decimalNumber)} with type: {decimalNumber.GetType()} is: {decimalNumber}");
            Console.WriteLine($"the value of {nameof(doubleNumber)} with type: {doubleNumber.GetType()} is: {doubleNumber}");
            Console.WriteLine($"the value of {nameof(decimalNumber2)} with type: {decimalNumber2.GetType()} is: {decimalNumber2}");
        }
    }
}
