using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule9
{
    class Program
    {
        static void Main(string[] args)
        {
            ///Test
            var list = new LinkedList<int>();
            list.Add(4);
            list.Add(5);
            list.Add(7);
            Console.WriteLine(list.ToString());

            list.InsertAt(0, 1);
            list.InsertAt(1, 2);
            list.InsertAt(2, 3);
            list.InsertAt(5, 6);
            list.InsertAt(list.Count, 77);
            Console.WriteLine(list.ToString());

            list.RemoveAt(0);
            list.RemoveAt(5);
            list.RemoveAt(list.Count - 1);
            Console.WriteLine(list.ToString());

            list.Clear();
            Console.WriteLine(list.ToString());

            list.InsertAt(0, 1);
            list.InsertAt(1, 2);
            list.InsertAt(2, 3);
            list.Add(6);
            list.InsertAt(list.Count, 77);
            Console.WriteLine(list.ToString());
            Console.WriteLine(list.Contains(7));
            Console.WriteLine(list.FindAt(3));
            Console.WriteLine(list.Contains(10));
            Console.WriteLine(list.FindAt(0));
            Console.WriteLine(list.FindAt(list.Count -1));
            Console.WriteLine(list.IndexOf(3));
            Console.WriteLine(list.IndexOf(1));
            Console.WriteLine(list.IndexOf(77));
            Console.WriteLine(list.IndexOf(10));

            Console.WriteLine(list.ToString());
            list.Remove(1);
            Console.WriteLine(list.ToString());
            list.Remove(10);
            Console.WriteLine(list.ToString());
            list.Remove(3);
            Console.WriteLine(list.ToString());
            list.Remove(77);
            Console.WriteLine(list.ToString());
            list.Add(4);
            list.Add(5);
            list.Add(7);
            Console.WriteLine(list.ToString());

            foreach(var elem in list)
            {
                Console.WriteLine(elem);
            }
            Console.ReadLine();

        }
    }
}
