using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashLib
{
    public class MyTreeMap<Key, Value> where Key : IComparable<Key>
    {
        private class Node
        {
            public Key key { get; set; }
            public Value value { get; set; }
            public Node left { get; set; }
            public Node right { get; set; }
            public Node(Key key, Value value)
            {
                this.key = key;
                this.value = value;
            }
        }
        private Node root;
        private int size;
        private Comparer<Key> comparer;

        // helping methods
        public void Print()
        {
            if (root == null)
            {
                Console.WriteLine("TreeMap is empty");
                return;
            }
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node node = stack.Pop();
                Console.WriteLine($"{node.key}: {node.value}");
                if (node.right != null) stack.Push(node.right);
                if (node.left != null) stack.Push(node.left);
            }
        }

        private Node RemoveNode(Node node, Key key)
        {
            if (node == null)
                return null;
            if (node.key.CompareTo(key) < 0)
                node.left = RemoveNode(node.left, key);
            else if (node.key.CompareTo(key) > 0)
                node.right = RemoveNode(node.right, key);
            else
            {
                if (node.left == null)
                    return node.right;
                else if (node.right == null)
                    return node.left;
                else
                {
                    Node tmp = GetMin(node.right);
                    node.key = tmp.key;
                    node.value = tmp.value;
                    node.right = RemoveNode(node.right, tmp.key);
                }
            }
            return node;
        }

        private Node GetMin(Node node)
        {
            while (node.left != null)
                node = node.left;
            return node;
        }

        private void IterEntrySet(List<KeyValuePair<Key, Value>> entry)
        {
            Stack<Node> stack = new Stack<Node>();
            Node curr = null;
            while (curr != null || stack.Count != 0)
            {
                while (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.left;
                }
                curr = stack.Pop();
                entry.Add(new KeyValuePair<Key, Value>(curr.key, curr.value));
                curr = curr.right;
            }
        }

        private void IterKeySet(List<Key> key)
        {
            Stack<Node> stack = new Stack<Node>();
            Node curr = null;
            while (curr != null || stack.Count > 0)
            {
                while (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.left;
                }
                curr = stack.Pop();
                key.Add(curr.key);
                curr = curr.right;
            }
        }

        // 1, 2, 3, 4, 8, 12, 13
        public MyTreeMap() => root = null;
        public MyTreeMap(Comparer<Key> comp) => comparer = comp;
        public bool ContainsKey(Key key) => Get(key) != null;
        public void Clear() => size = 0;
        public bool IsEmpty() => size == 0;
        public int Size() => size;
        public Key FirstKey() => root.key;

        // 5
        public bool ContainsValue(Value value)
        {
            if (root == null)
                return false;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.value.Equals(value))
                    return true;
                if (curr.left != null)
                    stack.Push(curr.left);
                if (curr.right != null)
                    stack.Push(curr.right);
            }
            return false;
        }

        // 6
        public List<KeyValuePair<Key, Value>> EntrySet()
        {
            List<KeyValuePair<Key, Value>> entry = new List<KeyValuePair<Key, Value>>();
            IterEntrySet(entry);
            return entry;
        }

        // 7
        public Value Get(Key key)
        {
            Node curr = root;
            while (curr != null)
            {
                if (key.CompareTo(curr.key) < 0)
                    curr = curr.left;
                else if (key.CompareTo(curr.key) > 0)
                    curr = curr.right;
                else
                    return curr.value;
            }
            return default(Value);
        }

        // 9
        public List<Key> KeySet()
        {
            List<Key> keys = new List<Key>();
            IterKeySet(keys);
            return keys;
        }

        // 10
        public void Put(Key key, Value value)
        {
            if (root == null)
            {
                root = new Node(key, value);
                size++;
                return;
            }
            Node curr = root;
            Node parent = null;
            while (curr != null)
            {
                parent = curr;
                if (key.CompareTo(curr.key) < 0)
                    curr = curr.left;
                else if (key.CompareTo(curr.key) > 0)
                    curr = curr.right;
                else
                {
                    curr.value = value;
                    return;
                }
            }
            Node newNode = new Node(key, value);
            if (parent != null)
            {
                if (key.CompareTo(parent.key) < 0)
                    parent.left = newNode;
                else
                    parent.right = newNode;
            }
            size++;
        }

        // 11
        public Value Remove(Key key)
        {
            Node curr = root;
            Node parent = null;
            Node nodeToRemove = null;
            bool isLeft = false;
            while (curr != null)
            {
                if (key.CompareTo(curr.key) < 0)
                {
                    parent = curr;
                    curr = curr.left;
                }
                else if (key.CompareTo(curr.key) > 0)
                {
                    parent = curr;
                    curr = curr.right;
                }
                else
                {
                    nodeToRemove = curr;
                    break;
                }
            }
            if (nodeToRemove == null)
                return default(Value);
            Value valueToReturn = nodeToRemove.value;
            if (nodeToRemove.left == null && nodeToRemove.right == null)
            {
                if (nodeToRemove == root)
                    root = null;
                else if (isLeft)
                    parent.left = null;
                else parent.right = null;
            }
            else if (nodeToRemove.left == null)
            {
                if (nodeToRemove == root)
                    root = nodeToRemove.right;
                else if (isLeft)
                    parent.left = nodeToRemove.right;
                else parent.right = nodeToRemove.right;
            }
            else if (nodeToRemove.right == null)
            {
                if (nodeToRemove == root)
                    root = nodeToRemove.left;
                else if (isLeft)
                    parent.left = nodeToRemove.left;
                else parent.right = nodeToRemove.left;
            }
            else
            {
                Node child = GetMin(nodeToRemove.right);
                nodeToRemove.key = child.key;
                nodeToRemove.value = child.value;
                Remove(child.key);
            }
            return valueToReturn;
        }

        // 14
        public Key LastKey()
        {
            if (root == null)
                throw new InvalidOperationException();
            return GetMin(root).key;
        }

        // 15
        public MyTreeMap<Key, Value> HeadMap(Key end)
        {
            MyTreeMap<Key, Value> head = new MyTreeMap<Key, Value>();
            if (root == null)
                return head;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.key.CompareTo(end) < 0)
                {
                    head.Put(curr.key, curr.value);
                    if (curr.left != null)
                        stack.Push(curr.left);
                    if (curr.right != null)
                        stack.Push(curr.right);
                }
                else
                {
                    if (curr.left != null)
                        stack.Push(curr.left);
                }
            }
            return head;
        }

        // 16
        public MyTreeMap<Key, Value> SubMap(Key start, Key end)
        {
            MyTreeMap<Key, Value> head = new MyTreeMap<Key, Value>();
            if (root == null)
                return head;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.key.CompareTo(start) >= 0 && curr.key.CompareTo(end) < 0)
                {
                    head.Put(curr.key, curr.value);
                    if (curr.left != null)
                        stack.Push(curr.left);
                    if (curr.right != null)
                        stack.Push(curr.right);
                }
                else if (curr.key.CompareTo(end) >= 0)
                {
                    if (curr.left != null)
                        stack.Push(curr.left);
                }
                else
                {
                    if (curr.right != null)
                        stack.Push(curr.right);
                }
            }
            return head;
        }

        // 17
        public MyTreeMap<Key, Value> TailMap(Key end)
        {
            MyTreeMap<Key, Value> head = new MyTreeMap<Key, Value>();
            if (root == null)
                return head;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.key.CompareTo(end) >= 0)
                {
                    head.Put(curr.key, curr.value);
                    if (curr.left != null)
                        stack.Push(curr.left);
                    if (curr.right != null)
                        stack.Push(curr.right);
                }
                else
                {
                    if (curr.right != null)
                        stack.Push(curr.right);
                }
            }
            return head;
        }

        // 18
        public KeyValuePair<Key, Value> LowerEntry(Key key)
        {
            KeyValuePair<Key, Value>? entry = null;
            if (root == null)
                return entry ?? default;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.key.CompareTo(key) < 0)
                {
                    entry = new KeyValuePair<Key, Value>(curr.key, curr.value);
                    stack.Push(curr.right);
                    stack.Push(curr.left);
                }
                else
                    stack.Push(curr.left);
            }
            return entry ?? default;
        }

        // 19
        public KeyValuePair<Key, Value> FloorEntry(Key key)
        {
            KeyValuePair<Key, Value>? entry = null;
            if (root == null)
                return entry ?? default;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.key.CompareTo(key) <= 0)
                {
                    entry = new KeyValuePair<Key, Value>(curr.key, curr.value);
                    stack.Push(curr.right);
                    stack.Push(curr.left);
                }
                else
                    stack.Push(curr.left);
            }
            return entry ?? default;
        }

        // 20
        public KeyValuePair<Key, Value> HigherEntry(Key key)
        {
            Node curr = root;
            KeyValuePair<Key, Value>? entry = null;
            while (curr != null)
            {
                if (key.CompareTo(curr.key) < 0)
                {
                    entry = new KeyValuePair<Key, Value>(curr.key, curr.value);
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return entry ?? default;
        }

        // 21
        public KeyValuePair<Key, Value> CeilingEntry(Key key)
        {
            Node curr = root;
            KeyValuePair<Key, Value>? entry = null;
            while (curr != null)
            {
                if (key.CompareTo(curr.key) <= 0)
                {
                    entry = new KeyValuePair<Key, Value>(curr.key, curr.value);
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return entry ?? default;
        }

        // 22
        public Key LowerKey(Key key)
        {
            Node curr = root;
            Key result = default(Key);
            while (curr != null)
            {
                if (key.CompareTo(curr.key) > 0)
                {
                    result = curr.key;
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return result;
        }

        // 23
        public Key FloorKey(Key key)
        {
            Node curr = root;
            Key result = default(Key);
            while (curr != null)
            {
                if (key.CompareTo(curr.key) >= 0)
                {
                    result = curr.key;
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return result;
        }

        // 24
        public Key HigherKey(Key key)
        {
            Node curr = root;
            Key result = default(Key);
            while (curr != null)
            {
                if (key.CompareTo(curr.key) < 0)
                {
                    result = curr.key;
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return result;
        }

        // 25
        public Key CeilingKey(Key key)
        {
            Node curr = root;
            Key result = default(Key);
            while (curr != null)
            {
                if (key.CompareTo(curr.key) <= 0)
                {
                    result = curr.key;
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return result;
        }

        // 26
        public KeyValuePair<Key, Value>? PollFirstEntry()
        {
            if (root == null)
                return default;
            KeyValuePair<Key, Value>? entry = FirstEntry();
            root = RemoveNode(root, entry.Value.Key);
            size--;
            return entry;
        }

        // 27
        public KeyValuePair<Key, Value>? PollLastEntry()
        {
            if (root == null)
                return default;
            KeyValuePair<Key, Value>? entry = LastEntry();
            root = RemoveNode(root, entry.Value.Key);
            size--;
            return entry;
        }

        // 28
        public KeyValuePair<Key, Value> FirstEntry()
        {
            if (root == null)
                return default;
            Node minNode = GetMin(root);
            return new KeyValuePair<Key, Value>(minNode.key, minNode.value);
        }

        // 29
        public KeyValuePair<Key, Value> LastEntry()
        {
            if (root == null)
                return default;
            Node curr = root;
            Node maxNode = null;
            while (curr != null)
            {
                maxNode = curr;
                curr = curr.right;
            }
            return new KeyValuePair<Key, Value>(maxNode.key, maxNode.value);
        }
    }
}
