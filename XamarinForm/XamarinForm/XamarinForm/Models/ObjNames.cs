using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.Models
{
    public class ObjNames
    {
        public string SurName { get; set; }
        public string MidName { get; set; }
        public string FamName { get; set; }

        public string FulName 
        {
            get
            {
                return $"{SurName} {MidName} {FamName}";
            }
            set
            {
                value = $"{SurName} {MidName} {FamName}";
            }
        }
    }
}
