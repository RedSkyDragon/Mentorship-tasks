using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule5
{
    /// <summary>
    /// Key-Value class for trie-tree
    /// </summary>
    public class KeyValue
    {
        public KeyValue(string key, string value = null)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; }
        public string Value { get; set; }
    }
}
