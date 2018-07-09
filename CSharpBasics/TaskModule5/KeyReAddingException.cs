using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule5
{
    /// <summary>
    /// Exception class for case of adding element with existing key
    /// </summary>
    public class KeyReAddingException : Exception
    { 
        /// <summary>
        /// Constructor for exception
        /// </summary>
        /// <param name="value">Element which coused an exception</param>
        public KeyReAddingException(KeyValue value) : base("Element with this key has already exist")
        {
            Value = value;
        }
        /// <summary>
        /// Element which coused an exception
        /// </summary>
        public KeyValue Value { get; }     
    }
}
