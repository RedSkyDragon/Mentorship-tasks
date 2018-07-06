using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule5
{
    class Trie
    {
        private List<Node> _roots;

        /// <summary>
        /// Initialize empty trie-tree
        /// </summary>
        public Trie()
        {
            _roots = new List<Node>();
        }

        /// <summary>
        /// Adds range of Key-Value elements to the trie-tree
        /// </summary>
        /// <param name="elems">Array of KeyValue elements to add</param>
        public void AddRange(KeyValue[] elems)
        {
            foreach(var elem in elems)
            {
                Add(elem);
            }
        }

        /// <summary>
        /// Adds Key-Value element to the trie-tree
        /// </summary>
        /// <param name="elems">KeyValue element to add</param>
        public void Add(KeyValue elem)
        {
            var node = _roots.Find(root => root.Item.Key == elem.Key.Substring(0, 1));
            if (node == null)
                _roots.Add(new Node(new KeyValue(elem.Key.Substring(0, 1))));
            _roots.Find(root => root.Item.Key == elem.Key.Substring(0, 1)).Add(elem, 2);
        }

        /// <summary>
        /// Converts all elements of trie-tree into list
        /// </summary>
        /// <returns>List of KeyValue elements</returns>
        public List<KeyValue> ShowAll()
        {
            var result = new List<KeyValue>();
            foreach (var root in _roots)
            {
                root.ShowAll(result);
            }
            return result;
        }

        public List<KeyValue> Find(string key)
        {
            var result = new List<KeyValue>();
            _roots.Find(item => item.Item.Key == key.Substring(0, 1)).Find(key, 2, ref result);
            return result;
        }
    }
}
