using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule9
{
    /// <summary>
    /// Enumerator class for Linked List
    /// </summary>
    /// <typeparam name="T">Type of list element</typeparam>
    public class ListEnumerator<T> : IEnumerator<T>
    {
        private ListNode<T> _head;
        private ListNode<T> _current;

        /// <summary>
        /// Constructor for list enumerator
        /// </summary>
        /// <param name="head">Head of the list</param>
        public ListEnumerator(ListNode<T> head)
        {
            _head = head;
            Reset();
        }

        /// <summary>
        /// Returns current element of the collection
        /// </summary>
        public T Current => _current.Value;

        /// <summary>
        /// Returns current element
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// Advances the enumerator to the next element of the collection
        /// </summary>
        /// <returns>False if the end of the collection</returns>
        public bool MoveNext()
        {
            _current = _current?.Next;
            return _current != null;
        }

        /// <summary>
        /// Resets current position
        /// </summary>
        public void Reset()
        {
            _current = new ListNode<T>(default(T), _head);
        }
    }
}
