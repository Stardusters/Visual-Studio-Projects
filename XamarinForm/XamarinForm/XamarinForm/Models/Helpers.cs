using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.Models
{
    public class Helpers
    {
        public static string stringProcess_RemoveSpace(string input)
        {
            string output = input;

            if (input.StartsWith(" "))
                output = input.Remove(0);
            if (input.EndsWith(" "))
                output = input.Remove(input.Length - 1);

            return output;
        }
    }
}
