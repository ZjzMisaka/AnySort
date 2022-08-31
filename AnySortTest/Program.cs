using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnySortTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<string> list = new List<string>();
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
            //List<int> sortInfo = AnySort.AnySort.BinarySort(list);
            //AnySort.AnySort.BinarySort(list, newList2);

            //foreach (int index in sortInfo)
            //{
            //    newList1.Add(list[index]);
            //}

            //Console.WriteLine("---");
            //foreach (string str in newList1)
            //{
            //    Console.WriteLine(str);
            //}
            //Console.WriteLine("---");
            //foreach (string str in newList2)
            //{
            //    Console.WriteLine(str);
            //}



            List<int> list = new List<int>();
            List<int> newList1 = new List<int>();
            List<int> newList2 = new List<int>();
            list.Add(2);
            list.Add(1);
            list.Add(9);
            list.Add(3);
            list.Add(4);
            list.Add(4);
            list.Add(5);
            list.Add(4);
            List<int> sortInfo = AnySort.AnySort.BinarySort(list);
            AnySort.AnySort.BinarySort(list, newList2);

            foreach (int index in sortInfo)
            {
                newList1.Add(list[index]);
            }

            Console.WriteLine("---");
            foreach (int val in newList1)
            {
                Console.WriteLine(val);
            }
            Console.WriteLine("---");
            foreach (int val in newList2)
            {
                Console.WriteLine(val);
            }



            Console.ReadLine();
        }
    }
}
