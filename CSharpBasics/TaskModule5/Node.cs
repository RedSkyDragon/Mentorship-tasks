using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule5
{
    /// <summary>
    /// Class for one node of trie-tree
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Key-Value elem
        /// </summary>
        public KeyValue Item { get; }

        /// <summary>
        /// List of next Nodes
        /// </summary>
        private List<Node> _nextNodes;

        /// <summary>
        /// Constructor for node
        /// </summary>
        /// <param name="item">Key-Value elem for this node</param>
        public Node(KeyValue item)
        {
            Item = item;
            _nextNodes = new List<Node>();
        }

        /// <summary>
        /// Recursive method to add new elem
        /// </summary>
        /// <param name="elem">Element to adding</param>
        /// <param name="level">Recursive level to cut key</param>
        public void Add(KeyValue elem, int level)
        {
            if (elem.Key == Item.Key)
            {
                if (Item.Value == null)
                {
                    Item.Value = elem.Value;
                }
                else
                {
                    throw new KeyReAddingException(elem);
                }
            }
            else
            {
                if (_nextNodes.Find(node => node.Item.Key == elem.Key.Substring(0, level)) == null)
                {
                    _nextNodes.Add(new Node(new KeyValue(elem.Key.Substring(0, level))));
                }
                _nextNodes.Find(node => node.Item.Key == elem.Key.Substring(0, level)).Add(elem, level + 1);
            }
        }

        /// <summary>
        /// Recursive search for all elements in trie-tree 
        /// </summary>
        /// <returns>List of KeyValue objects</returns>
        public List<KeyValue> ShowAll()
        {
            var result = new List<KeyValue>();
            foreach (var node in _nextNodes)
            {
                result.AddRange(node.ShowAll());
            }
            if (Item.Value != null)
            {
                result.Add(Item);
            }
            return result;
        }

    }
}
