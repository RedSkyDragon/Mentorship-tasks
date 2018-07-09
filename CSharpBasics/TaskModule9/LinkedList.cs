using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule9
{
    /// <summary>
    /// Generic linked list. Implements IEnumerable interface.
    /// </summary>
    /// <typeparam name="T">Type of list element</typeparam>
    public class LinkedList<T> : IEnumerable<T>
    {
        private ListNode<T> _head;
        private ListNode<T> _tail;
        private int _count;

        /// <summary>
        /// Creates an empty linked list
        /// </summary>
        public LinkedList() { }
        /// <summary>
        /// Creates linked list from IEnumerable collection
        /// </summary>
        /// <param name="collection">IEnumerable collection of elements</param>
        public LinkedList(IEnumerable<T> collection)
        {
            foreach (T elem in collection)
            {
                Add(elem);
            }
        }

        /// <summary>
        /// Number of elements in the list
        /// </summary>
        public int Count { get { return _count; } }

        /// <summary>
        /// Adds new element at the end of the list
        /// </summary>
        /// <param name="elem">New element</param>
        public void Add(T elem)
        {
            var newNode = new ListNode<T>(elem);
            if (_head == null)
            {
                _head = newNode;
                _tail = _head;
            }
            else
            {
                _tail.Next = newNode;
                _tail = newNode;
            }
            _count++;
        }
        /// <summary>
        /// Inserts new element at requested position of the list
        /// </summary>
        /// <param name="position">Zero-based position</param>
        /// <param name="elem">New element</param>
        public void InsertAt(int position, T elem)
        {
            if (position < 0 || position > _count)
            {
                throw new IndexOutOfRangeException();
            }
            ListNode<T> newNode;
            if (position == 0)
            {
                newNode = new ListNode<T>(elem, _head);
                _head = newNode;
            }
            else
            {
                ListNode<T> current = _head;
                while (position-- > 1)
                {
                    current = current.Next;
                }
                newNode = new ListNode<T>(elem, current.Next);
                current.Next = newNode;
            }
            if (newNode.Next == null)
            {
                _tail = newNode;
            }
            _count++;                 
        }
        /// <summary>
        /// Removes element at the specified position of the list
        /// </summary>
        /// <param name="position">Zero-based position</param>
        public void RemoveAt(int position)
        {
            if (position < 0 || position >= _count)
            {
                throw new IndexOutOfRangeException();
            }
            if (position == 0)
            {
                _head = _head.Next;
                if (_head.Next == null)
                {
                    _tail = _head;
                }
            }
            else
            {
                ListNode<T> current = _head;
                while (position-- > 1)
                {
                    current = current.Next;
                }
                current.Next = current.Next.Next;
                if (current.Next == null)
                {
                    _tail = current;
                }
            }
            _count--;
        }
        /// <summary>
        /// Removes the first occurrence of a specific element
        /// </summary>
        /// <param name="elem">Element to remove</param>
        public void Remove(T elem)
        {
            if (_head.Info.Equals(elem))
            {
                _head = _head.Next;
                _count--;
            }
            else
            {
                ListNode<T> current = _head;
                while (current.Next != null && !current.Next.Info.Equals(elem))
                {
                    current = current.Next;
                }
                if (current.Next != null)
                {
                    current.Next = current.Next.Next;
                    _count--;
                    if (current.Next == null)
                    {
                        _tail = current;
                    }
                }                
            }
            
        }
        /// <summary>
        /// Searches for an element at specified position of the list
        /// </summary>
        /// <param name="position">Position of the search</param>
        /// <returns>Element</returns>
        public T FindAt(int position)
        {
            if (position < 0 || position >= _count)
            {
                throw new IndexOutOfRangeException();
            }
            var current = _head;
            while (position-- > 0)
            {
                current = current.Next;
            }
            return current.Info;
        }
        /// <summary>
        /// Searches for an specified element of the list
        /// </summary>
        /// <param name="elem">Element for search</param>
        /// <returns>Zero-based index of first occurrence or -1 if not found</returns>
        public int IndexOf(T elem)
        {
            int index = 0;
            var current = _head;
            while (current != null && !current.Info.Equals(elem))
            {
                current = current.Next;
                index++;
            }
            return current == null ? -1 : index;
        }
        /// <summary>
        /// Determines whether element is in the list
        /// </summary>
        /// <param name="elem">Element for search</param>
        /// <returns>True if list contains the element, false if not</returns>
        public bool Contains(T elem)
        {
            return IndexOf(elem) != -1;
        }
        /// <inheritdoc/>
        public override string ToString()
        {
            string result = "";
            var current = _head;
            while (current != null)
            {
                result += current.Info.ToString() + " -> ";
                current = current.Next;
            }
            return result.TrimEnd(' ', '-', '>');
        }
        /// <summary>
        /// Clears the list
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _head = null;
            _tail = null;
        }
        /// <summary>
        /// Returns an enumerator that iterates through the list
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnum<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
    }
}
