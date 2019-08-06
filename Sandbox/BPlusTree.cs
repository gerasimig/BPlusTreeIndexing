using System;
using System.Collections.Generic;

namespace Sandbox
{
    public class BPlusTree<TKey, TValue> where TKey : IComparable
    {
        private Node<TKey, TValue> root = new Leaf<TKey, TValue>();


    }

    public class Node<TKey, TValue> where TKey: IComparable
    {
        public const int TreeOrder = 3;

        public LinkedList<Node<TKey, TValue>> Pointers = new LinkedList<Node<TKey, TValue>>();
        public int PointersSize;

        public LinkedList<TKey> Keys = new LinkedList<TKey>();

        public virtual Tuple<TKey, Node<TKey, TValue>> Insert(TKey key, TValue value)
        {
            Tuple<TKey, Node<TKey, TValue>> newChildEntry = null;
            if (key.CompareTo(Keys.First) < 0)
                newChildEntry = Pointers.First.Value.Insert(key, value);
            else
            {
                if (key.CompareTo(Keys.Last) >= 0)
                    newChildEntry = Pointers.Last.Value.Insert(key, value);
                else
                {
                    var keyNode = Pointers.First.Next;
                    while(key.CompareTo(keyNode.Value) >= 0) 
                    {
                        keyNode = keyNode.Next;
                    }
                    newChildEntry = keyNode.Value.Insert(key, value);
                }
            }

            if (newChildEntry == null) return null;

            // Continue from here ...
            if(Keys.Count <= TreeOrder * 2)
            {
                var keyNode = Keys.First;
                var pointerNode = Pointers.First;

                while(keyNode.Next != null &&
                    (key.CompareTo(keyNode.Value) < 0
                    || key.CompareTo(keyNode.Next.Value) >= 0))
                {
                    keyNode = keyNode.Next;
                    pointerNode = pointerNode.Next;
                }

                if (keyNode.Next == null) pointerNode = pointerNode.Next;
                
                Keys.AddAfter(keyNode, newChildEntry.Item1);
                Pointers.AddAfter(pointerNode, newChildEntry.Item2);

                return null;
            }

            var newNode = new Node<TKey, TValue>();

            return null;
        }
    }

    public class Leaf<TKey, TValue> : Node<TKey, TValue> where TKey : IComparable
    {
        public Leaf<TKey, TValue> Prev;
        public Leaf<TKey, TValue> Next;
        
        public TValue[] DataValues = new TValue[2 * TreeOrder];
        public int DataValuesSize;

        public override Tuple<TKey, Node<TKey, TValue>> Insert(TKey key, TValue value)
        {
            if (DataValuesSize == DataValues.Length) throw new Exception();
            DataValues[DataValuesSize++] = value;
            Array.Sort(DataValues, 0, DataValuesSize);

            return null;
        }
    }
}
