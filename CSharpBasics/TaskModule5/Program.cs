/*Create a simple trie structure and write a method to show all items.
  Also please implement methods to remove node (with and without children).
  Make sure your trie support any string key.
  Make sure you cover the case, when 1 key has 2 values.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule5
{
    class Program
    {
        static void Main(string[] args)
        {
            //test
            var trie = new Trie();
            var array = new KeyValue[] 
                {
                    new KeyValue ("3","One3"),
                    new KeyValue ("12","One12"),
                    new KeyValue ("345","One345"),
                    new KeyValue ("556","One556")
                };

            trie.Add(new KeyValue("1", "qwqe"));
            trie.Add(new KeyValue("2", "qwqrdsfge"));
            trie.Add(new KeyValue("111", "qwfdgdfhhhdfghqe"));
            trie.Add(new KeyValue("556438", "qwfdgdfhhhdfghqe"));
            trie.AddRange(array);

            var list = trie.ShowAll();
            foreach (var elem in list.OrderBy(elem => elem.Key))
            {
                Console.WriteLine($"{elem.Key} : {elem.Value}");
            }

            Console.ReadLine();
        }
    }
}
