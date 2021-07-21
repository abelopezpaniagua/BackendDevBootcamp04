using System;

namespace MyCustomLibrary
{
    public class CustomClass
    {
        public int CustomIntProperty { get; set; }
        public string CustomStringProperty { get; set; }

        public CustomClass()
        {
            CustomIntProperty = int.MaxValue;
            CustomStringProperty = "Int32 Max Value:";
        }

        public void Show()
        {
            Console.WriteLine($"{CustomStringProperty} {CustomIntProperty}");
        }
    }
}
