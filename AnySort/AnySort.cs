using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnySort
{
    static public class AnySort
    {
        static public List<int> BinarySort<T>(List<T> origList, List<T> newList = null)
        {
            List<string> stringList = null;
            List<int> intList = null;
            if (typeof(T) == typeof(string))
            {
                stringList = origList as List<string>;
            }
            else if(typeof(T) == typeof(int))
            {
                intList = origList as List<int>;
            }

            List<int> sortInfo = new List<int>();
            if (origList.Count != 0)
            {
                sortInfo.Add(0);
                newList?.Add(origList[0]);
            }
            if (origList.Count > 1)
            {
                for (int i = 1; i < origList.Count; i++)
                {
                    int from = 0;
                    int to = sortInfo.Count - 1;
                    int mid = (from + to) / 2;
                    int index = -1;
                    if (stringList != null)
                    {
                        index = Half(stringList, i, sortInfo, from, to, mid);
                    }
                    else if (intList != null)
                    {
                        index = Half(intList, i, sortInfo, from, to, mid);
                    }

                    if (index == -1)
                    {
                        continue;
                    }

                    if (newList != null)
                    {
                        if (index >= newList.Count)
                        {
                            newList.Add(origList[i]);
                        }
                        else
                        {
                            newList.Insert(index, origList[i]);
                        }
                    }
                }
            }

            return sortInfo;
        }

        private static int Half(List<string> origList, int origListIndex, List<int> sortInfo, int from, int to, int mid)
        {
            if (String.CompareOrdinal(origList[sortInfo[mid]], (origList[origListIndex])) < 0)
            {
                if (mid == to)
                {
                    sortInfo.Add(origListIndex);
                    return sortInfo.Count;
                }
                if (String.CompareOrdinal(origList[sortInfo[mid + 1]], origList[origListIndex]) > 0)
                {
                    sortInfo.Insert(mid + 1, origListIndex);
                    return mid + 1;
                }
                from = mid + 1;
                mid = (from + to) / 2;
                return Half(origList, origListIndex, sortInfo, from, to, mid);
            }
            else if (String.CompareOrdinal(origList[sortInfo[mid]], (origList[origListIndex])) > 0)
            {
                if (mid == from)
                {
                    sortInfo.Insert(from, origListIndex);
                    return from;
                }
                if (String.CompareOrdinal(origList[sortInfo[mid - 1]], origList[origListIndex]) < 0)
                {
                    sortInfo.Insert(mid, origListIndex);
                    return mid;
                }
                to = mid;
                mid = (from + to) / 2;
                return Half(origList, origListIndex, sortInfo, from, to, mid);
            }
            else
            {
                sortInfo.Insert(mid, origListIndex);
                return mid;
            }
        }

        private static int Half(List<int> origList, int origListIndex, List<int> sortInfo, int from, int to, int mid)
        {
            if (origList[sortInfo[mid]] < (origList[origListIndex]))
            {
                if (mid == to)
                {
                    sortInfo.Add(origListIndex);
                    return sortInfo.Count;
                }
                if (origList[sortInfo[mid + 1]] > origList[origListIndex])
                {
                    sortInfo.Insert(mid + 1, origListIndex);
                    return mid + 1;
                }
                from = mid + 1;
                mid = (from + to) / 2;
                return Half(origList, origListIndex, sortInfo, from, to, mid);
            }
            else if (origList[sortInfo[mid]] > (origList[origListIndex]))
            {
                if (mid == from)
                {
                    sortInfo.Insert(from, origListIndex);
                    return from;
                }
                if (origList[sortInfo[mid - 1]] < origList[origListIndex])
                {
                    sortInfo.Insert(mid, origListIndex);
                    return mid;
                }
                to = mid;
                mid = (from + to) / 2;
                return Half(origList, origListIndex, sortInfo, from, to, mid);
            }
            else
            {
                sortInfo.Insert(mid, origListIndex);
                return mid;
            }
        }

    }
}
