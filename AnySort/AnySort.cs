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

        static public List<int> BinarySort<T>(ref List<T> origList, SortOption sortOption) where T : IComparable
        {
            // todo try dic
            List<int> sortInfo = new List<int>(origList.Count);
            if (origList.Count != 0)
            {
                sortInfo.Add(0);
            }
            if (origList.Count > 1)
            {
                for (int i = 1; i < origList.Count; i++)
                {
                    int from = 0;
                    int to = i - 1;
                    int mid = (from + to) / 2;
                    int index;
                    if (sortOption.CompareStringOrdinal)
                    {
                        index = Half(origList, from, to, mid, origList[i] as string, sortInfo);
                    }
                    else
                    {
                        index = Half(origList, from, to, mid, origList[i] as IComparable, sortInfo);
                    }

                    sortInfo.Insert(index, i);
                }
            }

            List<T> resList = new List<T>();
            foreach (int index in sortInfo)
            {
                resList.Add(origList[index]);
            }
            origList = resList;

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


        static public List<int> QuickSort<T>(List<T> origList, SortOption sortOption) where T : IComparable
        {
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

        // To do 三数取中 多线程
        static private void SplitCompare<T>(List<T> origList, int leftIndex, int rightIndex, int[] sortInfo, SortOption sortOption) where T : IComparable
        {
            int i = leftIndex;
            int j = rightIndex;
            IComparable pivot = origList[leftIndex] as IComparable;
            List<int> sameList = new List<int>();
            while (i <= j)
            {
                while (pivot.CompareTo(origList[i]) > 0)
                {
                    i++;
                }

                while (pivot.CompareTo(origList[j]) < 0)
                {
                    j--;
                }
                if (i <= j)
                {
                    T temp = origList[i];
                    origList[i] = origList[j];
                    origList[j] = temp;

                    sortInfo[i] = j;
                    sortInfo[j] = i;

                    i++;
                    j--;

                    if (pivot.Equals(origList[i]))
                    { 
                        sameList.Add(i);
                    }
                    if (pivot.Equals(origList[i]))
                    {
                        sameList.Add(j);
                    }
                }
            }
            foreach (int index in sameList)
            {
                if (index < j)
                {
                    --j;
                    T temp = origList[index];
                    origList[index] = origList[j];
                    origList[j] = temp;

                    sortInfo[index] = j;
                    sortInfo[j] = index;
                }
                else if (index > i)
                {
                    ++i;
                    T temp = origList[index];
                    origList[index] = origList[i];
                    origList[i] = temp;

                    sortInfo[index] = i;
                    sortInfo[i] = index;
                }
            }

            if (leftIndex < j)
            {
                SplitCompare(origList, leftIndex, j, sortInfo, sortOption);
                //if (j - leftIndex > 50)
                //{
                //    SplitCompare(origList, leftIndex, j, sortInfo, sortOption);
                //}
                //else
                //{
                //    List<T> partList = origList.GetRange(leftIndex, j - leftIndex + 1);
                //    BinarySort(ref partList, sortOption);
                //    foreach (T res in partList)
                //    {
                //        origList[leftIndex++] = res;
                //    }
                //}
            }
            if (i < rightIndex)
            {
                SplitCompare(origList, i, rightIndex, sortInfo, sortOption);
                //if (rightIndex - i > 50)
                //{
                //    SplitCompare(origList, i, rightIndex, sortInfo, sortOption);
                //}
                //else
                //{
                //    List<T> partList = origList.GetRange(i, rightIndex - i + 1);
                //    BinarySort(ref partList, sortOption);
                //    foreach (T res in partList)
                //    {
                //        origList[i++] = res;
                //    }
                //}
            }
        }

        static private void SplitCompareOrdinal<T>(List<T> origList, int leftIndex, int rightIndex, int[] sortInfo, SortOption sortOption) where T : IComparable
        {
            int i = leftIndex;
            int j = rightIndex;
            string pivot = origList[leftIndex].ToString();
            List<int> sameList = new List<int>();
            while (i <= j)
            {
                while (String.CompareOrdinal(pivot, origList[i].ToString()) > 0)
                {
                    i++;
                }

                while (String.CompareOrdinal(pivot, origList[j].ToString()) < 0)
                {
                    j--;
                }
                if (i <= j)
                {
                    T temp = origList[i];
                    origList[i] = origList[j];
                    origList[j] = temp;

                    sortInfo[i] = j;
                    sortInfo[j] = i;

                    i++;
                    j--;

                    if (pivot.Equals(origList[i]))
                    {
                        sameList.Add(i);
                    }
                    if (pivot.Equals(origList[i]))
                    {
                        sameList.Add(j);
                    }
                }
            }
            foreach (int index in sameList)
            {
                if (index < j)
                {
                    --j;
                    T temp = origList[index];
                    origList[index] = origList[j];
                    origList[j] = temp;

                    sortInfo[index] = j;
                    sortInfo[j] = index;
                }
                else if (index > i)
                {
                    ++i;
                    T temp = origList[index];
                    origList[index] = origList[i];
                    origList[i] = temp;

                    sortInfo[index] = i;
                    sortInfo[i] = index;
                }
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
                //    List<T> partList = origList.GetRange(leftIndex, j - leftIndex + 1);
                //    BinarySort(ref partList, sortOption);
                //    foreach (T res in partList)
                //    {
                //        origList[leftIndex++] = res;
                //    }
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
                //    List<T> partList = origList.GetRange(i, rightIndex - i + 1);
                //    BinarySort(ref partList, sortOption);
                //    foreach (T res in partList)
                //    {
                //        origList[i++] = res;
                //    }
                //}
            }
        }
    }
}
