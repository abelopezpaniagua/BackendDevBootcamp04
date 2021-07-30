using OutlookClientExercise.UserInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookClientExercise
{
    public static class ExtensionGenericMethods
    {
        public static T Convert<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    return (T)converter.ConvertFromString(input);
                }
                return default(T);
            }
            catch (NotSupportedException)
            {
                ConsoleManager.ShowError("Conversion type not supported.");
                return default(T);
            }
        }
    }
}
