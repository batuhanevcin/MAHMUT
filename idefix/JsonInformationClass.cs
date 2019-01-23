using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idefix
{
    public class JsonInformationClass
    {
        public string[] SubLowerr { get; set; }
        public string[] SubUpper { get; set; }
        public string Path { get; set; }
        public int SearchLine { get; set; }
        public string SearchTerm { get; set; }
        public bool IsSucces { get; set; }
    }
}
