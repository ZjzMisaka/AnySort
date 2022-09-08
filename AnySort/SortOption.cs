using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnySort
{
    public class SortOption
    {
        private bool compareStringOrdinal;
        private int rangeFrom;
        private int rangeTo;

        public bool CompareStringOrdinal { get => compareStringOrdinal; set => compareStringOrdinal = value; }
        public int RangeFrom { get => rangeFrom; set => rangeFrom = value; }
        public int RangeTo { get => rangeTo; set => rangeTo = value; }

        public SortOption()
        {
            CompareStringOrdinal = false;
            RangeFrom = -1;
            RangeTo = -1;
        }

        public SortOption(SortOption sortOption)
        {
            CompareStringOrdinal = sortOption.CompareStringOrdinal;
            rangeFrom = sortOption.RangeFrom;
            rangeTo = sortOption.RangeTo;
        }
    }
}
