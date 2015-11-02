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
            set { this.NodeAt(i).Value = value; }
        }

        public int this[string query]
        {
            get { return this.IndexOf(query); }
            set { }
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
            if (this.firstNode == null)
            {
                return 0;
            }
            SinglyLinkedListNode currentNode = this.firstNode;
            int count = 1;
            while(!currentNode.IsLast())
            {
                currentNode = currentNode.Next;
                count++;
            }
            return count;
        }

        public string ElementAt(int index)
        {
            return NodeAt(index).ToString();
        }

        public SinglyLinkedListNode NodeAt(int index)
        {
            if (firstNode == null)
            { throw new ArgumentOutOfRangeException(); }

            if (index < 0)
            { index = this.Count() + index; }

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
            SinglyLinkedListNode currentNode = this.firstNode;
            for (var i = 0; i < this.Count(); i++ )
            {
                if (value.CompareTo(currentNode.ToString()) == 0)
                { return i; }
                currentNode = currentNode.Next;
            }
            return -1;
        }

        public bool IsSorted()
        {
            int count = this.Count();
            if (count > 1)
            {
                for (var i = 0; i < count-1; i++ )
                {
                    if (NodeAt(i) > NodeAt(i+1))
                    { return false; }
                }
            }
            return true;
        }

        // HINT 1: You can extract this functionality (finding the last item in the list) from a method you've already written!
        // HINT 2: I suggest writing a private helper method LastNode()
        // HINT 3: If you highlight code and right click, you can use the refactor menu to extract a method for you...
        public string Last()
        {
            if (this.firstNode == null)
            { return null; }
            return this.ElementAt(-1);
        }

        public void Remove(string value)
        {
            int currentIndex = this[value];
            int count = this.Count();
            if (currentIndex == -1)
            { return; }
            if (currentIndex == 0)
            { this.firstNode = this.firstNode.Next; }
            else if (currentIndex +1 == this.Count())
            {
                SinglyLinkedListNode previousNode = NodeAt(currentIndex - 1);
                previousNode.Next = null;
            }
            else
            {
                SinglyLinkedListNode previousNode = NodeAt(currentIndex - 1);
                SinglyLinkedListNode nextNode = NodeAt(currentIndex + 1);
                previousNode.Next = nextNode;
            }
        }

        public void Sort()
        {
            int count = this.Count();
            if (count < 2)
            { return; }
            if (this.IsSorted())
            { return; }
            SinglyLinkedListNode currentNode = this.firstNode;
            while (this.IsSorted() == false)
            {
                for (int i = 0; i < count-1; i++)
                {
                    Console.WriteLine("Iteration i: {0}", i);
                    if(NodeAt(i) > NodeAt(i+1))
                    {
                        Console.WriteLine("Node {0} > Node {1}", NodeAt(i), NodeAt(i + 1));
                        string currentValue = NodeAt(i).ToString();
                        NodeAt(i).Value = NodeAt(i + 1).Value;
                        NodeAt(i + 1).Value = currentValue;
                    }
                }
                //1. compare node0, node1
                    // a. node0 < node1, continue
                    // b. node0 > node1, swap values


            }
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
