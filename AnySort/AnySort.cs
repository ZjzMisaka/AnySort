using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AnySort
{
    static public class AnySort
    {
        static public List<int> BinarySort<T>(List<T> origList, SortOption sortOption = null) where T : IComparable
        {
            if (sortOption == null)
            {
                sortOption = new SortOption();
            }
            if (sortOption.RangeFrom == -1)
            {
                sortOption.RangeFrom = 0;
            }
            if (sortOption.RangeTo == -1)
            {
                sortOption.RangeTo = origList.Count - 1;
            }
            int rangeLength = sortOption.RangeTo - sortOption.RangeFrom + 1;
            List<int> sortInfo = new List<int>(rangeLength);
            if (rangeLength != 0)
            {
                sortInfo.Add(sortOption.RangeFrom);
            }
            if (rangeLength > 1)
            {
                for (int i = sortOption.RangeFrom + 1; i <= sortOption.RangeTo; i++)
                {
                    int from = 0;
                    int to = i - 1 - sortOption.RangeFrom;
                    int mid = (from + to) / 2;
                    int index;
                    if (sortOption.CompareStringOrdinal)
                    {
                        index = Half(origList, from, to, mid, origList[i] as string, sortInfo);
                    }
                    else
                    {
                        index = Half(origList, from, to, mid, origList[i], sortInfo);
                    }

                    sortInfo.Insert(index, i);
                }
            }

            List<T> newList = new List<T>(origList);
            for (int index = 0; index < sortInfo.Count; ++index)
            {
                origList[sortOption.RangeFrom + index] = newList[sortInfo[index]];
            }

            return sortInfo;
        }



        private static int Half<T>(List<T> origList, int from, int to, int mid, IComparable value, List<int> sortInfo) where T : IComparable
        {
            if (value.CompareTo(origList[sortInfo[mid]]) > 0)
            {
                if (mid == to)
                {
                    return to + 1;
                }
                if (value.CompareTo(origList[sortInfo[mid + 1]]) < 0)
                {
                    return mid + 1;
                }
                from = mid + 1;
                mid = (from + to) / 2;
                return Half(origList, from, to, mid, value, sortInfo);
            }
            else if (value.CompareTo(origList[sortInfo[mid]]) < 0)
            {
                if (mid == from)
                {
                    return from;
                }
                if (value.CompareTo(origList[sortInfo[mid - 1]]) > 0)
                {
                    return mid;
                }
                to = mid;
                mid = (from + to) / 2;
                return Half(origList, from, to, mid, value, sortInfo);
            }
            else
            {
                return mid;
            }
        }

        private static int Half<T>(List<T> origList, int from, int to, int mid, string value, List<int> sortInfo) where T : IComparable
        {
            if (String.CompareOrdinal(value, origList[sortInfo[mid]].ToString()) > 0)
            {
                if (mid == to)
                {
                    return to + 1;
                }
                if (String.CompareOrdinal(value, origList[sortInfo[mid + 1]].ToString()) < 0)
                {
                    return mid + 1;
                }
                from = mid + 1;
                mid = (from + to) / 2;
                return Half(origList, from, to, mid, value, sortInfo);
            }
            else if (String.CompareOrdinal(value, origList[sortInfo[mid]].ToString()) < 0)
            {
                if (mid == from)
                {
                    return from;
                }
                if (String.CompareOrdinal(value, origList[sortInfo[mid - 1]].ToString()) > 0)
                {
                    return mid;
                }
                to = mid;
                mid = (from + to) / 2;
                return Half(origList, from, to, mid, value, sortInfo);
            }
            else
            {
                return mid;
            }
        }


        static public List<int> QuickSort<T>(List<T> origList, SortOption sortOption = null) where T : IComparable
        {
            if (sortOption == null)
            {
                sortOption = new SortOption();
            }
            int[] sortInfo = new int[origList.Count];
            if (sortOption.CompareStringOrdinal)
            {
                SplitCompareOrdinal(origList, 0, origList.Count - 1, sortInfo, sortOption);
            }
            else
            {
                SplitCompare(origList, 0, origList.Count - 1, sortInfo, sortOption);
            }
            return sortInfo.ToList();
        }

        // To do 多线程 Range
        static private void SplitCompare<T>(List<T> origList, int leftIndex, int rightIndex, int[] sortInfo, SortOption sortOption) where T : IComparable
        {
            SortOption sortOptionBs = new SortOption(sortOption);
            int i = leftIndex;
            int j = rightIndex;
            int m = (leftIndex + rightIndex) / 2;
            IComparable pivot = origList[m];
            // 使用list存放相同元素比五路并三路排序性能略优
            List<int> sameList = new List<int>();
            while (i < j)
            {
                while (i < j && pivot.CompareTo(origList[i]) >= 0)
                {
                    if (pivot.Equals(origList[i]))
                    {
                        sameList.Add(i);
                    }
                    i++;
                }

                while (i < j && pivot.CompareTo(origList[j]) <= 0)
                {
                    if (pivot.Equals(origList[j]))
                    {
                        sameList.Add(j);
                    }
                    j--;
                }

                if (i < j)
                {
                    T temp = origList[i];
                    origList[i] = origList[j];
                    origList[j] = temp;

                    sortInfo[i] = j;
                    sortInfo[j] = i;
                }
            }

            if (i == j)
            {
                if (pivot.CompareTo(origList[j]) <= 0)
                {
                    j--;
                }
                else if (pivot.CompareTo(origList[i]) >= 0)
                {
                    i++;
                }
            }

            foreach (int index in sameList)
            {
                if (index < j)
                {
                    if (pivot.Equals(origList[j]))
                    {
                        --j;
                    }
                    T temp = origList[index];
                    origList[index] = origList[j];
                    origList[j] = temp;

                    sortInfo[index] = j;
                    sortInfo[j] = index;
                }
                else if (index > i)
                {
                    if (pivot.Equals(origList[i]))
                    {
                        ++i;
                    }
                    T temp = origList[index];
                    origList[index] = origList[i];
                    origList[i] = temp;

                    sortInfo[index] = i;
                    sortInfo[i] = index;
                }
            }

            if (i < origList.Count && pivot.Equals(origList[i]))
            {
                i++;
            }
            if (j >= 0 && pivot.Equals(origList[j]))
            {
                j--;
            }

            if (leftIndex < j)
            {
                //SplitCompare(origList, leftIndex, j, sortInfo, sortOption);
                if (j - leftIndex > 50)
                {
                    SplitCompare(origList, leftIndex, j, sortInfo, sortOption);
                }
                else
                {
                    List<T> partList = origList.GetRange(leftIndex, j - leftIndex + 1);
                    BinarySort(partList, new SortOption());
                    foreach (T res in partList)
                    {
                        origList[leftIndex++] = res;
                    }
                }
                //if (j - leftIndex > 50)
                //{
                //    SplitCompare(origList, leftIndex, j, sortInfo, sortOption);
                //}
                //else
                //{
                //    sortOptionBs.RangeFrom = leftIndex;
                //    sortOptionBs.RangeTo = j;
                //    BinarySort(origList, sortOptionBs);
                //}
            }
            if (i < rightIndex)
            {
                //SplitCompare(origList, i, rightIndex, sortInfo, sortOption);
                if (rightIndex - i > 50)
                {
                    SplitCompare(origList, i, rightIndex, sortInfo, sortOption);
                }
                else
                {
                    List<T> partList = origList.GetRange(i, rightIndex - i + 1);
                    BinarySort(partList, new SortOption());
                    foreach (T res in partList)
                    {
                        origList[i++] = res;
                    }
                }
                //if (rightIndex - i > 50)
                //{
                //    SplitCompare(origList, i, rightIndex, sortInfo, sortOption);
                //}
                //else
                //{
                //    sortOptionBs.RangeFrom = i;
                //    sortOptionBs.RangeTo = rightIndex;
                //    BinarySort(origList, sortOptionBs);
                //}
            }
        }

        static private void SplitCompareOrdinal<T>(List<T> origList, int leftIndex, int rightIndex, int[] sortInfo, SortOption sortOption) where T : IComparable
        {
            int i = leftIndex;
            int j = rightIndex;
            int m = (leftIndex + rightIndex) / 2;
            string pivot = origList[leftIndex].ToString();
            // 使用list存放相同元素比五路并三路排序性能略优
            List<int> sameList = new List<int>();
            while (i < j)
            {
                while (i < j && String.CompareOrdinal(pivot, origList[i].ToString()) >= 0)
                {
                    if (pivot.Equals(origList[i]))
                    {
                        sameList.Add(i);
                    }
                    i++;
                }

                while (i < j && String.CompareOrdinal(pivot, origList[j].ToString()) <= 0)
                {
                    if (pivot.Equals(origList[j]))
                    {
                        sameList.Add(j);
                    }
                    j--;
                }

                if (i < j)
                {
                    T temp = origList[i];
                    origList[i] = origList[j];
                    origList[j] = temp;

                    sortInfo[i] = j;
                    sortInfo[j] = i;
                }
            }

            if (i == j)
            {
                if (String.CompareOrdinal(pivot, origList[j].ToString()) <= 0)
                {
                    j--;
                }
                else if (String.CompareOrdinal(pivot, origList[i].ToString()) >= 0)
                {
                    i++;
                }
            }

            foreach (int index in sameList)
            {
                if (index < j)
                {
                    if (pivot.Equals(origList[j]))
                    {
                        --j;
                    }
                    T temp = origList[index];
                    origList[index] = origList[j];
                    origList[j] = temp;

                    sortInfo[index] = j;
                    sortInfo[j] = index;
                }
                else if (index > i)
                {
                    if (pivot.Equals(origList[i]))
                    {
                        ++i;
                    }
                    T temp = origList[index];
                    origList[index] = origList[i];
                    origList[i] = temp;

                    sortInfo[index] = i;
                    sortInfo[i] = index;
                }
            }

            if (i < origList.Count && pivot.Equals(origList[i]))
            {
                i++;
            }
            if (j >= 0 && pivot.Equals(origList[j]))
            {
                j--;
            }

            if (leftIndex < j)
            {
                SplitCompareOrdinal(origList, leftIndex, j, sortInfo, sortOption);
                //if (j - leftIndex > 50)
                //{
                //    SplitCompareOrdinal(origList, leftIndex, j, sortInfo, sortOption);
                //}
                //else
                //{
                //    SortOption sortOptionBs = new SortOption(sortOption);
                //    sortOptionBs.RangeFrom = leftIndex;
                //    sortOptionBs.RangeTo = j;
                //    BinarySort(origList, sortOptionBs);
                //}
            }
            if (i < rightIndex)
            {
                SplitCompareOrdinal(origList, i, rightIndex, sortInfo, sortOption);
                //if (rightIndex - i > 50)
                //{
                //    SplitCompareOrdinal(origList, i, rightIndex, sortInfo, sortOption);
                //}
                //else
                //{
                //    SortOption sortOptionBs = new SortOption(sortOption);
                //    sortOptionBs.RangeFrom = i;
                //    sortOptionBs.RangeTo = rightIndex;
                //    BinarySort(origList, sortOptionBs);
                //}
            }
        }
    }
}
