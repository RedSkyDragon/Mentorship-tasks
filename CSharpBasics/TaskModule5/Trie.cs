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
        /// Initialize trie-tree from IEnumerable collection
        /// </summary>
        /// <param name="elems">IEnumerable collection</param>
        public Trie(IEnumerable<KeyValue> elems)
        {
            _roots = new List<Node>();
            AddRange(elems);
        }

        /// <summary>
        /// Initialize trie-tree from another trie-tree
        /// </summary>
        /// <param name="elems">Trie-tree</param>
        public Trie(Trie elems)
        {
            _roots = elems._roots;
        }

        /// <summary>
        /// Adds range of Key-Value elements to the trie-tree
        /// </summary>
        /// <param name="elems">IEnumereble collection of KeyValue elements</param>
        public void AddRange(IEnumerable<KeyValue> elems)
        {
            foreach(var elem in elems)
            {
                Add(elem);
            }
        }

        /// <summary>
        /// Adds range of Key-Value elements to the trie-tree
        /// </summary>
        /// <param name="elems">Another trie-tree</param>
        public void AddRange(Trie elems)
        {
            var listOfElems = elems.ShowAll();
            AddRange(listOfElems);
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
        /// <returns>IEnumerable collection of KeyValue elements</returns>
        public IEnumerable<KeyValue> ShowAll()
        {
            var result = new List<KeyValue>();
            foreach (var root in _roots)
            {
                root.ShowAll(result);
            }
            return result;
        }

        /// <summary>
        /// Finds elements of tree, whose key is substring of requested key
        /// </summary>
        /// <param name="key">Requested key</param>
        /// <returns>IEnumerable collection of KeyValue elements</returns>
        public IEnumerable<KeyValue> Find(string key)
        {
            var result = new List<KeyValue>();
            _roots.Find(item => item.Item.Key == key.Substring(0, 1)).Find(key, 2, ref result);
            return result;
        }

        /// <summary>
        /// Removes node with requested key (without children)
        /// </summary>
        /// <param name="key">Requested key</param>
        public void Remove(string key)
        {
            var next = _roots.Find(item => item.Item.Key == key.Substring(0, 1));
            if (next?.Remove(key, 2) ?? false)
            {
                _roots.Remove(next);
            }
        }

        /// <summary>
        /// Removes node with requested key and all its children
        /// </summary>
        /// <param name="key">Requested key</param>
        public void RemoveWithChildren(string key)
        {
            var next = _roots.Find(item => item.Item.Key == key.Substring(0, 1));
            if (next?.RemoveWithChildren(key, 2) ?? false)
            {
                _roots.Remove(next);
            }
        }
    }
}
