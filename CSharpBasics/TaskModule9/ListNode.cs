using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule9
{
    /// <summary>
    /// List Node for Linked List
    /// </summary>
    /// <typeparam name="T">Type of list element</typeparam>
    public class ListNode<T>
    {
        /// <summary>
        /// Creates a node for list
        /// </summary>
        /// <param name="info">Information in the node</param>
        /// <param name="next">Next node</param>
        public ListNode(T info, ListNode<T> next = null)
        {
            Info = info;
            Next = next;
        }
        /// <summary>
        /// Information in the node
        /// </summary>
        public T Info { get; set; }
        /// <summary>
        /// Next node of the list
        /// </summary>
        public ListNode<T> Next { get; set; }
    }
}
