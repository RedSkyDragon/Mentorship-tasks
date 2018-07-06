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
        /// Key-Value element of node
        /// </summary>
        public KeyValue Item { get; }

        private Node FindNextNode(string key, int curr_length)
        {
            return _nextNodes.Find(item => item.Item.Key == key.Substring(0, curr_length));
        }

        /// <summary>
        /// Recursive method to add new elem
        /// </summary>
        /// <param name="elem">Element to adding</param>
        /// <param name="level">Current reqursive level (number of letters in current key) to cut key</param>
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
                if (FindNextNode(elem.Key, level) == null)
                {
                    _nextNodes.Add(new Node(new KeyValue(elem.Key.Substring(0, level))));
                }
                FindNextNode(elem.Key, level).Add(elem, level + 1);
            }
        }

        /// <summary>
        /// Recursive search for all elements in trie-tree 
        /// </summary>
        /// <returns>List of KeyValue objects</returns>
        public void FillList(List<KeyValue> result)
        {
            foreach (var node in _nextNodes)
            {
                node.FillList(result);
            }
            if (Item.Value != null)
            {
                result.Add(Item);
            }
            return;
        }

        /// <summary>
        /// Finds elements of subtree, whose key is substring of requested key
        /// </summary>
        /// <param name="key">Requested key</param>
        /// <param name="level">Current reqursive level (number of letters in current key)</param>
        /// <returns>List of KeyValue elements</returns>
        public List<KeyValue> Find(string key, int level)
        {
            var result = new List<KeyValue>();
            Find(key, level, result);
            return result;
        }

        /// <summary>
        /// Finds elements of subtree, whose key is substring of requested key
        /// </summary>
        /// <param name="key">Requested key</param>
        /// <param name="level">Current reqursive level (number of letters in current key)</param>
        /// <param name="result">Result list of KeyValue elements</param>
        private void Find(string key, int level, List<KeyValue> result)
        {
            if (Item.Value != null)
            {
                result.Add(Item);
            }
            if (Item.Key == key)
            {
                return;
            }
            var next = FindNextNode(key, level);
            if (next != null)
            {
                next.Find(key, level + 1, result);
            }
            return;
        }

        /// <summary>
        /// Removes node in subtree with requested key
        /// </summary>
        /// <param name="key">Requested key</param>
        /// <param name="level">Current reqursive level (number of letters in current key)</param>
        /// <returns>True if upper reqursive level should remove this node</returns>
        public bool Remove(string key, int level)
        {
            if (Item.Key == key)
            {
                if (_nextNodes.Count() != 0)
                {
                    Item.Value = null;
                    return false;
                }
                return true;
            }
            var next = FindNextNode(key, level);
            if (next?.Remove(key, level + 1) ?? false)
            {
                _nextNodes.Remove(next);
                if (Item.Value == null && _nextNodes.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes node in subtree with requested key and its children
        /// </summary>
        /// <param name="key">Requested key</param>
        /// <param name="level">Current reqursive level (number of letters in current key)</param>
        /// <returns>True if upper reqursive level should remove this node</returns>
        public bool RemoveWithChildren(string key, int level)
        {
            if (Item.Key == key)
            {
                return true;
            }
            var next = FindNextNode(key, level);
            if (next?.RemoveWithChildren(key, level + 1) ?? false)
            {               
                if (Item.Value == null && _nextNodes.Count == 1)
                {
                    return true;
                }
                _nextNodes.Remove(next);
            }
            return false;
        }

    }
}
