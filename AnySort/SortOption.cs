using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnySort
{
    public class SortOption
    {
        private bool isString = false;
        private bool isNumber = false;
        private bool compareOrdinal = true;

        public bool IsString { get => isString; set => isString = value; }
        public bool IsNumber { get => isNumber; set => isNumber = value; }
        public bool CompareOrdinal { get => compareOrdinal; set => compareOrdinal = value; }
    }
}
