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

        static public List<int> BinarySort<T>(ref List<T> origList, SortOption sortOption)
        {
            List<int> sortInfo = new List<int>();
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
                    int index = -1;
                    if (sortOption.IsString && sortOption.CompareOrdinal)
                    {
                    }
                    else if (sortOption.IsNumber)
                    {
                    }
                    else if (typeof(T).GetInterface(nameof(IComparable)) != null)
                    {
                        index = Half(origList, from, to, mid, origList[i] as IComparable, sortInfo);
                    }
                    else
                    {
                        throw new NotSupportedException("origList must be implemented from IComparable");
                    }

                    if (index == -1)
                    {
                        continue;
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


        private static int Half<T>(List<T> origList, int from, int to, int mid, IComparable value, List<int> sortInfo)
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


        static public List<int> QuickSort<T>(List<T> origList, SortOption sortOption)
        {
            int[] sortInfo = new int[origList.Count];
            QuickSort(origList, 0, origList.Count - 1, sortInfo, sortOption);
            return sortInfo.ToList();
        }

        // To do 三数取中 多线程
        static private void QuickSort<T>(List<T> origList, int leftIndex, int rightIndex, int[] sortInfo, SortOption sortOption)
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
                //QuickSort(origList, leftIndex, j, sortInfo, sortOption);
                if (j - leftIndex > 50)
                {
                    QuickSort(origList, leftIndex, j, sortInfo, sortOption);
                }
                else
                {
                    List<T> partList = origList.GetRange(leftIndex, j - leftIndex + 1);
                    BinarySort(ref partList, sortOption);
                    foreach (T res in partList)
                    {
                        origList[leftIndex++] = res;
                    }
                }
            }
            if (i < rightIndex)
            {
                //QuickSort(origList, i, rightIndex, sortInfo, sortOption);
                if (rightIndex - i > 50)
                {
                    QuickSort(origList, i, rightIndex, sortInfo, sortOption);
                }
                else
                {
                    List<T> partList = origList.GetRange(i, rightIndex - i + 1);
                    BinarySort(ref partList, sortOption);
                    foreach (T res in partList)
                    {
                        origList[i++] = res;
                    }
                }
            }
        }
    }
}
