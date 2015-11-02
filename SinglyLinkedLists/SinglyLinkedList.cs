using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinglyLinkedLists
{
    public class SinglyLinkedList
    {
        private SinglyLinkedListNode firstNode;
        public SinglyLinkedList()
        {
            // NOTE: This constructor isn't necessary, once you've implemented the constructor below.
        }

        // READ: http://msdn.microsoft.com/en-us/library/aa691335(v=vs.71).aspx
        public SinglyLinkedList(params object[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                string value = new string(values[i].ToString().ToCharArray());
                this.AddLast(value.ToString());
            }
        }

        // READ: http://msdn.microsoft.com/en-us/library/6x16t2tx.aspx
        public string this[int i]
        {
            get { return this.ElementAt(i); }
            set
            {
                this.NodeAt(i).Value = value;
            }
        }

        public void AddAfter(string existingValue, string value)
        {
            SinglyLinkedListNode current = firstNode;
            bool test = false;
            while (current != null)
            {
                test = (current.Value == existingValue);
                if (test)
                { break; }
                current = current.Next;
            }

            if (!test)
            { throw new ArgumentException(); }
            if (test)
            {
                SinglyLinkedListNode nextNode = current.Next;
                SinglyLinkedListNode newNode = new SinglyLinkedListNode(value);
                current.Next = newNode;
                newNode.Next = nextNode;
            }
        }

        public void AddFirst(string value)
        {
            SinglyLinkedListNode currentNode = firstNode;
            firstNode = new SinglyLinkedListNode(value);
            firstNode.Next = currentNode;
        }

        public void AddLast(string value)
        {
            if (this.First() == null)
            {
                firstNode = new SinglyLinkedListNode(value);
            }
            else
            {
                SinglyLinkedListNode nextNode = firstNode;
                while (!nextNode.IsLast())
                {
                    nextNode = nextNode.Next;
                }
                nextNode.Next = new SinglyLinkedListNode(value);
            }
        }

        // NOTE: There is more than one way to accomplish this.  One is O(n).  The other is O(1).
        public int Count()
        {
            throw new NotImplementedException();
        }

        public string ElementAt(int index)
        {
            if (firstNode == null)
            { throw new ArgumentOutOfRangeException(); }
                
            SinglyLinkedListNode currentNode = firstNode;
            for (int i = 0; i < index; i++)
            {
                if (currentNode.IsLast())
                { throw new ArgumentOutOfRangeException(); }
                currentNode = currentNode.Next;
            }
            return currentNode.ToString();
        }

        public SinglyLinkedListNode NodeAt(int index)
        {
            if (firstNode == null)
            { throw new ArgumentOutOfRangeException(); }

            SinglyLinkedListNode currentNode = firstNode;
            for (int i = 0; i < index; i++)
            {
                if (currentNode.IsLast())
                { throw new ArgumentOutOfRangeException(); }
                currentNode = currentNode.Next;
            }
            return currentNode;
        }

        public string First()
        {
            if (this.firstNode == null)
            { return null; }
            else
            { return this.firstNode.Value; }
        }

        public int IndexOf(string value)
        {
            throw new NotImplementedException();
        }

        public bool IsSorted()
        {
            throw new NotImplementedException();
        }

        // HINT 1: You can extract this functionality (finding the last item in the list) from a method you've already written!
        // HINT 2: I suggest writing a private helper method LastNode()
        // HINT 3: If you highlight code and right click, you can use the refactor menu to extract a method for you...
        public string Last()
        {
            if (this.firstNode == null)
            { return null; }
            SinglyLinkedListNode currentNode = firstNode;
            while (!currentNode.IsLast())
            {
                currentNode = currentNode.Next;
            }
            return currentNode.ToString();
        }

        public void Remove(string value)
        {
            throw new NotImplementedException();
        }

        public void Sort()
        {
            throw new NotImplementedException();
        }

        public string[] ToArray()
        {
            if (this.firstNode == null)
            { return new string[] { }; }

            char[] charToTrim = new char[] { '{', ' ', '}', '"' };
            string[] charToSplit = new string[] { "\", \"" };
            var input = this.ToString().Trim(charToTrim);
            string[] output = input.Split(charToSplit, StringSplitOptions.RemoveEmptyEntries);
            return output;
        }

        public override string ToString()
        {
            SinglyLinkedListNode currentNode = this.firstNode;
            string output = "{ ";

            if (currentNode != null)
            {
                while (!currentNode.IsLast())
                {
                    output += "\"" + currentNode.Value + "\", ";
                    currentNode = currentNode.Next;
                }

                if (currentNode.IsLast())
                {
                    output += "\"" + currentNode.Value + "\" ";
                    currentNode = currentNode.Next;
                }
            }
            return output + "}";
        }
    }
}
