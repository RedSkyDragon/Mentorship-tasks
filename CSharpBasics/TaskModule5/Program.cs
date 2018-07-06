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
            trie.Add(new KeyValue("556438", "123d-00="));
            trie.Add(new KeyValue("asd", "asd"));
            trie.Add(new KeyValue("a", "a"));
            trie.Add(new KeyValue("as", "as"));
            trie.AddRange(array);

            try
            {
                trie.Add(new KeyValue("as", "as"));
            }
            catch (KeyReAddingException ex)
            {
                Console.WriteLine(ex.Message + $" Key: \"{ex.Value.Key}\", Value: \"{ex.Value.Value}\"");
            }

            var list = trie.ToIEnumerable();
            foreach (var elem in list.OrderBy(elem => elem.Key))
            {
                Console.WriteLine($"{elem.Key} : {elem.Value}");
            }

            Console.WriteLine();

            list = trie.Find("asd");
            foreach (var elem in list.OrderBy(elem => elem.Key))
            {
                Console.WriteLine($"{elem.Key} : {elem.Value}");
            }

            Console.WriteLine();

            trie.Remove("as");
            list = trie.ToIEnumerable();
            foreach (var elem in list.OrderBy(elem => elem.Key))
            {
                Console.WriteLine($"{elem.Key} : {elem.Value}");
            }

            Console.WriteLine();

            trie.RemoveWithChildren("1");
            list = trie.ToIEnumerable();
            foreach (var elem in list.OrderBy(elem => elem.Key))
            {
                Console.WriteLine($"{elem.Key} : {elem.Value}");
            }

            Console.ReadLine();
        }
    }
}
