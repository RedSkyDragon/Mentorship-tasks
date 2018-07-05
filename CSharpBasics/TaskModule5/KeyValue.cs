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
        public string Key { get; }
        public string Value { get; set; }

        public KeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public KeyValue(string key) : this(key, null) { }
    }
}
