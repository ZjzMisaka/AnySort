using AnySort;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnySortTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<string> list = new List<string>();
            //List<string> list1 = new List<string>();
            //List<string> newList1 = new List<string>();
            //List<string> newList2 = new List<string>();
            //list.Add("b");
            //list.Add("a");
            //list.Add("z");
            //list.Add("c");
            //list.Add("d");
            //list.Add("d");
            //list.Add("e");
            //list.Add("d");
            //list1.Add("b");
            //list1.Add("a");
            //list1.Add("z");
            //list1.Add("c");
            //list1.Add("d");
            //list1.Add("d");
            //list1.Add("e");
            //list1.Add("d");
            //SortOption sortOption = new SortOption();
            //sortOption.CompareStringOrdinal = true;
            //List<int> sortInfo = AnySort.AnySort.QuickSort(list, sortOption);

            //foreach (int index in sortInfo)
            //{
            //    newList1.Add(list1[index]);
            //}

            //Console.WriteLine("---");
            //foreach (string str in newList1)
            //{
            //    Console.WriteLine(str);
            //}
            //Console.WriteLine("---");
            //foreach (string str in list)
            //{
            //    Console.WriteLine(str);
            //}
            //Console.ReadLine();



            //List<int> list = new List<int>();
            //List<int> newList1 = new List<int>();
            //List<int> newList2 = new List<int>();
            //list.Add(2);
            //list.Add(1);
            //list.Add(9);
            //list.Add(3);
            //list.Add(4);
            //list.Add(4);
            //list.Add(5);
            //list.Add(4);
            //List<int> sortInfo = AnySort.AnySort.BinarySort(list);
            //AnySort.AnySort.BinarySort(list, newList2);

            //foreach (int index in sortInfo)
            //{
            //    newList1.Add(list[index]);
            //}

            //Console.WriteLine("---");
            //foreach (int val in newList1)
            //{
            //    Console.WriteLine(val);
            //}
            //Console.WriteLine("---");
            //foreach (int val in newList2)
            //{
            //    Console.WriteLine(val);
            //}

            //List<DateTime> list = new List<DateTime>();
            //list.Add(DateTime.Now);
            //Thread.Sleep(20);
            //list.Add(DateTime.Now);
            //Thread.Sleep(20);
            //list.Add(DateTime.Now);
            //Thread.Sleep(20);
            //list.Add(DateTime.Now);
            //List<int> sortInfo = AnySort.AnySort.BinarySort(list);
            //List<DateTime> newList = new List<DateTime>();
            //foreach (int index in sortInfo)
            //{
            //    newList.Add(list[index]);
            //}
            //foreach (DateTime val in newList)
            //{
            //    Console.WriteLine(val.Ticks);
            //}

            //List<Object> list = new List<Object>();
            //list.Add(new object());
            //list.Add(new object());
            //list.Add(new object());
            //list.Add(new object());
            //try
            //{
            //    List<int> sortInfo = AnySort.AnySort.BinarySort(list);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}


            //List<string> list1 = new List<string>();
            //List<string> list2 = new List<string>();
            //List<string> resList = new List<string>();
            //for (int i = 0; i < 10000; ++i)
            //{
            //    string str = GetRandomString(20);
            //    list1.Add(str);
            //    list2.Add(str);
            //}

            //DateTime dateTime1 = DateTime.Now;
            //list1.Sort();
            //DateTime dateTime2 = DateTime.Now;
            //AnySort.AnySort.BinarySort(list2, resList, true);
            //DateTime dateTime3 = DateTime.Now;

            //Console.WriteLine((dateTime2 - dateTime1).TotalSeconds);
            //Console.WriteLine((dateTime3 - dateTime2).TotalSeconds);
            //Console.ReadLine();
            //for (int i = 0; i < list1.Count; ++i)
            //{
            //    Console.WriteLine(list1[i] + "    :    " + resList[i]);
            //}

            List<List<TimeSpan>> origList1 = new List<List<TimeSpan>>();
            List<List<TimeSpan>> origList2 = new List<List<TimeSpan>>();
            List<List<string>> origList3 = new List<List<string>>();
            List<List<string>> origList4 = new List<List<string>>();
            for (int i = 0; i < 10000; ++i)
            {
                List<TimeSpan> tsList = GetTimeSpanList(15000);
                //List<string> strList = GetRandomString(20);
                origList1.Add(tsList);
                List<TimeSpan> newTsList = new List<TimeSpan>();
                foreach (TimeSpan ts in tsList)
                {
                    newTsList.Add(ts);
                }
                origList2.Add(newTsList);
            }
            SortOption sortOption = new SortOption();
            sortOption.CompareStringOrdinal = false;
            CostTest(origList1, origList2, sortOption);
        }

        private static List<TimeSpan> GetTimeSpanList(int count)
        {
            List<TimeSpan> list = new List<TimeSpan>();
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            for (int i = 0; i < count; ++i)
            {
                int n = r.Next(-1000, 1000);
                TimeSpan ts = DateTime.Now - DateTime.FromBinary(n);
                list.Add(ts);
            }
            return list;
        }

        public static List<string> GetRandomString(int count, int length = 20, string custom = "", bool useNum = true, bool useLow = true, bool useUpp = true, bool useSpe = true)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < count; ++i)
            {
                byte[] b = new byte[4];
                new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
                Random r = new Random(BitConverter.ToInt32(b, 0));
                string s = null, str = custom;
                if (useNum == true) { str += "0123456789"; }
                if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
                if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
                if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
                for (int j = 0; j < length; j++)
                {
                    s += str.Substring(r.Next(0, str.Length - 1), 1);
                }
                list.Add(s);
            }
            return list;
        }

        public static void CostTest<T>(List<List<T>> origList1, List<List<T>> origList2, SortOption sortOption) where T : IComparable
        {
            TimeSpan ts1 = new TimeSpan(0);
            TimeSpan ts2 = new TimeSpan(0);
            for (int i = 0; i < origList1.Count; ++i)
            {
                List<T> orig = origList1[i];
                DateTime dateTime1 = DateTime.Now;
                AnySort.AnySort.QuickSort(orig, sortOption);
                DateTime dateTime2 = DateTime.Now;
                origList2[i].Sort();
                DateTime dateTime3 = DateTime.Now;

                ts1 += dateTime2 - dateTime1;
                ts2 += dateTime3 - dateTime2;
                Console.WriteLine(i);
            }

            Console.WriteLine(ts1.TotalMilliseconds / origList1.Count + "    :    " + ts2.TotalMilliseconds / origList1.Count);
            Console.ReadLine();
        }

        public static void CheckSame<T>(List<List<T>> origList1, List<List<T>> origList2, SortOption sortOption) where T : IComparable
        {
            for (int i = 0; i < origList1.Count; ++i)
            {
                List<T> orig1 = origList1[i];
                AnySort.AnySort.QuickSort(orig1, sortOption);
                List<T> orig2 = origList2[i];
                orig2.Sort();

                for (int j = 0; j < orig1.Count; ++j)
                {
                    if (!orig1[j].Equals(orig2[j]))
                    {
                        Console.WriteLine("NO: " + orig1[j] + ", " + orig2[j]);
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("YES: " + orig1[j] + ", " + orig2[j]);
                    }
                }
            }

            Console.WriteLine("CHECK OVER");
            Console.ReadLine();
        }
    }
}
