using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinglyLinkedLists
{
    public class SinglyLinkedList
    {
        private SinglyLinkedListNode firstNode;
        //public SinglyLinkedList()
        //{
            // NOTE: This constructor isn't necessary, once you've implemented the constructor below.
        //}

        // READ: http://msdn.microsoft.com/en-us/library/aa691335(v=vs.71).aspx
        public SinglyLinkedList(params object[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                this.AddLast(values[i].ToString());
            }
        }

        // READ: http://msdn.microsoft.com/en-us/library/6x16t2tx.aspx
        public string this[int i]
        {
            get { return ElementAt(i); }
            // Only makes sense because Node is a trivial object
            // Replace the Node entirely for more complicated data
            set { this.NodeAt(i).Value = value; }
        }

        public int this[string query]
        {
            get { return this.IndexOf(query); }
        }

        public void AddAfter(string existingValue, string value)
        {
            SinglyLinkedListNode currentNode = firstNode;
            bool found = false;
            while (currentNode != null)
            {
                if (currentNode.Value == existingValue)
                {
                    found = true;
                    SinglyLinkedListNode nextNode = currentNode.Next;
                    SinglyLinkedListNode newNode = new SinglyLinkedListNode(value);
                    currentNode.Next = newNode;
                    newNode.Next = nextNode;
                    return;
                }
                currentNode = currentNode.Next;
            }
            if (!found)
            { throw new ArgumentException(); }
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

        private SinglyLinkedListNode NodeAt(int index)
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
                if (value.Equals(currentNode.Value))
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
            else if (currentIndex + 1 == count)
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

        public void BubbleSort()
        {
            int count = this.Count();
            if (count < 2)
            { return; }
            SinglyLinkedListNode previous = null;
            SinglyLinkedListNode current = this.firstNode;
            SinglyLinkedListNode next = firstNode.Next;
            bool swapOccurred = false;
            while (next != null)
            {
                if (current > next)
                {
                    SwapWithNext(previous, current);
                    swapOccurred = true;
                }
                previous = current;
                current = next;
                next = current.Next;

            }
            if (swapOccurred)
            {
                BubbleSort();
            }
        }

        private void SwapWithNext(SinglyLinkedListNode previous, SinglyLinkedListNode swapee)
        {
            SinglyLinkedListNode swapWith = swapee.Next;
            if (previous == null)
            {
                firstNode = swapWith;
            }
            else
            {
                previous.Next = swapWith;
            }
            swapee.Next = swapWith.Next;
            swapWith.Next = swapee;
        }

        private void BaseSort(SinglyLinkedList input)
        {
            if (NodeAt(0) > NodeAt(1))
            {
                string currentValue = NodeAt(0).ToString();
                NodeAt(0).Value = NodeAt(1).Value;
                NodeAt(1).Value = currentValue;
            }
        }

        public void MergeSort(SinglyLinkedList input)
        {
            // Incomplete
            int count = this.Count();
            Console.WriteLine("Original List: {0}", this);
            SinglyLinkedList listOne = new SinglyLinkedList();
            SinglyLinkedList listTwo = new SinglyLinkedList();
            decimal split = count / 2;
            for (int i = 0; i < split; i++)
            {
                Console.WriteLine("Iteration {0}", i);
                Console.WriteLine("Split {0}", split);
                listOne.AddLast(this[i]);
                listTwo.AddLast(this[i + (int)split]);
            }
            Console.WriteLine("listOne: {0}", listOne);
            Console.WriteLine("listTwo: {0}", listTwo);
        }

        public void UnnamedSort()
        {
            int count = this.Count();
            if (count < 2)
            { return; }
            SinglyLinkedList sorted = new SinglyLinkedList();
            for (int i = 0; i < count; i++)
            {
                SinglyLinkedListNode least = this.firstNode;
                SinglyLinkedListNode currentNode = firstNode.Next;
                while (currentNode != null)
                {
                    if (least > currentNode)
                    {
                        least = currentNode;
                    }
                    currentNode = currentNode.Next;
                }
                sorted.AddLast(least.Value);
                this.Remove(least.Value);
            }
            this.firstNode = sorted.firstNode;
        }
        
        public void Sort()
        {
            this.UnnamedSort();
        }

        public string[] ToArray()
        {
            int count = this.Count();
            string[] output = new string[count];
            for (int i = 0; i < count; i++)
            {
                output[i] = ElementAt(i);
            }
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
