using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MyLinkedList
{
    public sealed class MyLinkedListNode<T>
    {
        public MyLinkedListNode (T value)
        {
            Value = value;
        }
        public T Value { get; set; }        
        public MyLinkedListNode<T> Next { get; set; }
        public MyLinkedListNode<T> Previous { get; set; }
    }

    public class MyLinkedList<T> : IEnumerable<T>, ICollection<T>, ICloneable, IComparable
    {
        int count;
        public MyLinkedListNode<T> head;
        public MyLinkedListNode<T> tail;

        public int Count { get { return count; } }
        public MyLinkedListNode<T> Head { get { return head; } }
        public MyLinkedListNode<T> Tail { get { return tail; } }

        public MyLinkedList()
        {
            count = 0;
            head = null;
            tail = null;
        }
        public MyLinkedList(IEnumerable<T> Enumerable)
        {
            foreach (var item in Enumerable)
            {
                MyLinkedListNode<T> node = new MyLinkedListNode<T>(item);
                this.AddLast(node);
            }
        }

        public void AddAfter(MyLinkedListNode<T> given_node, MyLinkedListNode<T> new_node) //Добавляет заданный новый узел после заданного существующего узла в MyLinkedList<T>.
        {
            try
            {
                if (!this.Contains(new_node.Value))
                {
                    if (given_node.Next == null) //если заданный узел - хвост списка
                    {
                        this.AddLast(new_node);
                    }
                    else
                    {
                        MyLinkedListNode<T> after_given_node = given_node.Next;
                        new_node.Previous = given_node;
                        new_node.Next = after_given_node;
                        given_node.Next = new_node;
                        after_given_node.Previous = new_node;
                        count++;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddAfter(MyLinkedListNode<T> given_node, T value)
        {
            MyLinkedListNode<T> new_node = new MyLinkedListNode<T>(value);
            this.AddAfter(given_node, new_node);
        }
        public void AddBefore(MyLinkedListNode<T> given_node, MyLinkedListNode<T> new_node) //Добавляет заданный новый узел перед заданным существующим узлом в MyLinkedList<T>.
        {
            try
            {
                if (!this.Contains(new_node.Value))
                {
                    if (given_node.Previous == null) //если заданный узел - голова списка
                    {
                        this.AddFirst(new_node);
                    }
                    else
                    {
                        MyLinkedListNode<T> before_given_node = given_node.Previous;
                        new_node.Next = given_node;
                        new_node.Previous = before_given_node;
                        before_given_node.Next = new_node;
                        given_node.Previous = new_node;
                        count++;
                    }
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddBefore(MyLinkedListNode<T> given_node, T value)
        {
            MyLinkedListNode<T> new_node = new MyLinkedListNode<T>(value);
            this.AddBefore(given_node, new_node);
        }
        public void AddLast(MyLinkedListNode<T> new_node) //Добавляет заданный НОВЫЙ узел в конец MyLinkedList<T>
        {
            try
            {
                //обнуляем ссылки узла, так как в методе Test(MyLinkedList<string>) передаётся НЕ новый узел  
                new_node.Next = null;
                new_node.Previous = null;

                if (head == null)
                {
                    head = new_node;
                }
                else
                {
                    tail.Next = new_node;
                    new_node.Previous = tail;
                }
                tail = new_node;
                count++;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddLast(T value)
        {
            MyLinkedListNode<T> new_node = new MyLinkedListNode<T>(value);
            this.AddLast(new_node);
        }
        public void AddFirst(MyLinkedListNode<T> new_node) //Добавляет заданный НОВЫЙ узел в начало MyLinkedList<T>
        {
            try
            {
                //обнуляем ссылки узла, так как в методе Test(MyLinkedList<string>) передаётся НЕ новый узел  
                new_node.Next = null;
                new_node.Previous = null;

                MyLinkedListNode<T> temp = head;
                new_node.Next = temp;
                head = new_node;
                if (count == 0)
                {
                    tail = new_node;
                }
                else
                {
                    temp.Previous = new_node;
                }
                count++;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddFirst(T value)
        {
            MyLinkedListNode<T> new_node = new MyLinkedListNode<T>(value);
            this.AddFirst(new_node);
        }
        public void Clear()
        {
            /*
            while (count != 0)
            {
                this.RemoveLast();
            }*/
            count = 0;
            head = null;
            tail = null;
        }
        public bool Contains(T item) //Определяет, принадлежит ли значение объекту MyLinkedList<T>
        {
            if (this.Find(item) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Remove(MyLinkedListNode<T> node) //Удаляет заданный узел из объекта MyLinkedList<T> 
        {
            try
            {
                if (node.Previous != null)
                {
                    node.Previous.Next = node.Next;
                }
                else
                {
                    head = node.Next;
                }

                if (node.Next != null)
                {
                    node.Next.Previous = node.Previous;
                }
                else
                {
                    tail = node.Previous;
                }
                count--;
            }
            catch (InvalidOperationException ex) //Параметр node не находится в текущем объекте MyLinkedList<T>.
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex) //Параметр node равен null
            {
                Console.WriteLine(ex.Message);
            }
        }
        public bool Remove(T value) //Удаляет первое вхождение заданного значения из MyLinkedList<T>
        {
            MyLinkedListNode<T> current = this.Find(value);
            if (current != null)
            {
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    head = current.Next;
                }

                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    tail = current.Previous;
                }
                count--;
                return true;
            }
            return false;
        }
        public void RemoveFirst()
        {
            try
            {
                head = head.Next;
                head.Previous = null;
                count--;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void RemoveLast()
        {
            try
            {
                tail = tail.Previous;
                tail.Next = null;
                count--;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public MyLinkedListNode<T> Find(T value) //Находит первый узел, содержащий указанное значение
        {
            MyLinkedListNode<T> current = head;
            while (current != null)
            {
                if ((current.Value).Equals(value))
                {
                    return current;
                }
                else
                {
                    current = current.Next;
                }
            }
            return null;
        }
        public MyLinkedListNode<T> FindLast(T value) //Находит последний узел, содержащий указанное значение
        {
            MyLinkedListNode<T> current = tail;
            while (current != null)
            {
                if ((current.Value).Equals(value))
                {
                    return current;
                }
                else
                {
                    current = current.Previous;
                }
            }
            return null;
        }
        public object Clone()
        {
            T[] array = new T[count];
            int arrayIndex = 0;
            this.CopyTo(array, arrayIndex);
            return new MyLinkedList<T>(array);
        }
        public void CopyTo(T[] array, int arrayIndex) //Копирует целый массив MyLinkedList<T> в совместимый одномерный массив Array, начиная с заданного индекса целевого массива.
        {
            try
            {
                MyLinkedListNode<T> current = head;
                while (current != null)
                {
                    array[arrayIndex] = current.Value;
                    arrayIndex++;
                    current = current.Next;
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void sort(IComparer<T> comparer)
        {
            //будем делегировать запрос на сортировку классу System.Array
            T[] array = new T[count];
            int arrayIndex = 0;
            this.CopyTo(array, arrayIndex);
            Array.Sort(array, comparer);
            MyLinkedList<T> tmp = new MyLinkedList<T>(array);
            MyLinkedListNode<T> current = this.Head;
            MyLinkedListNode<T> current_tmp = tmp.Head;
            while (current != null)
            {
                current.Value = current_tmp.Value;
                current = current.Next;
                current_tmp = current_tmp.Next;
            }
        }
        int IComparable.CompareTo(object obj)
        {
            MyLinkedList<T> temp = obj as MyLinkedList<T>;
            if (temp != null)
            {
                if (this.Count > temp.Count)
                {
                    return 1;
                }
                if (this.Count < temp.Count)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
                throw new ArgumentException("Argument isn't a MyLinkedList");
        }
        void ICollection<T>.Add(T item)
        {
            this.AddLast(item);
        }
        bool ICollection<T>.IsReadOnly { get; }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
        //итераторный метод
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            MyLinkedListNode<T> current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }

        }
    }
    
    public class StringComparer : IComparer<string>
    {
        int IComparer<string>.Compare(string s1, string s2)
        {
            return String.Compare(s1, s2);
        }
    }
    public class IntComparer : IComparer<int>
    {
        int IComparer<int>.Compare(int x, int y)
        {
            if (x > y)
            {
                return 1;
            }
            if (x < y)
            {
                return -1;
            }
            return 0;
        }
    }

    class Program
    {
        public static void TestCollection(ICollection<string> collection)
        {
            Console.WriteLine("Add in collection items: ");
            collection.Add("the");
            collection.Add("old");
            collection.Add("magazine");
            foreach(var i in collection)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.WriteLine("Collection is Read only?: {0}",collection.IsReadOnly);
            Console.WriteLine("Items in collection: {0}",collection.Count);
            Console.WriteLine("Contains 'old'?: {0}",collection.Contains("old"));
            string[] array = new string[collection.Count];
            collection.CopyTo(array, 0);
            Console.Write("Copy in Array: ");
            foreach(string s in array)
            {
                Console.Write("{0} ",s);
            }
            Console.WriteLine();
            Console.WriteLine("Remove 'the' from collection: ");
            collection.Remove("the");
            foreach(string s in collection)
            {
                Console.Write("{0} ", s);
            }
            Console.WriteLine();
            Console.WriteLine("clear collection!");
            collection.Clear();
            Console.WriteLine("item in collection {0}", collection.Count);
        }
        public static void Test(LinkedList<string> sentence)
        {
            // Create the link list.
            Display(sentence, "LinkedList: ");
            Console.WriteLine("sentence.Contains(\"jumps\") = {0}",
                sentence.Contains("jumps"));

            // Add the word 'today' to the beginning of the linked list.
            sentence.AddFirst("today");
            Display(sentence, "Test 1: Add 'today' to beginning of the list:");

            // Move the first node to be the last node.
            LinkedListNode<string> mark1 = sentence.First;
            sentence.RemoveFirst();
            sentence.AddLast(mark1);
            Display(sentence, "Test 2: Move first node to be last node:");

            // Change the last node to 'yesterday'.
            sentence.RemoveLast();
            sentence.AddLast("yesterday");
            Display(sentence, "Test 3: Change the last node to 'yesterday':");

            // Move the last node to be the first node.
            mark1 = sentence.Last;
            sentence.RemoveLast();
            sentence.AddFirst(mark1);
            Display(sentence, "Test 4: Move last node to be first node:");

            // Indicate the last occurence of 'the'.
            sentence.RemoveFirst();
            LinkedListNode<string> current = sentence.FindLast("the");
            IndicateNode(current, "Test 5: Indicate last occurence of 'the':");

            // Add 'lazy' and 'old' after 'the' (the LinkedListNode named current).
            sentence.AddAfter(current, "old");
            sentence.AddAfter(current, "lazy");
            IndicateNode(current, "Test 6: Add 'lazy' and 'old' after 'the':");

            // Indicate 'fox' node.
            current = sentence.Find("fox");
            IndicateNode(current, "Test 7: Indicate the 'fox' node:");

            // Add 'quick' and 'brown' before 'fox':
            sentence.AddBefore(current, "quick");
            sentence.AddBefore(current, "brown");
            IndicateNode(current, "Test 8: Add 'quick' and 'brown' before 'fox':");

            // Keep a reference to the current node, 'fox',
            // and to the previous node in the list. Indicate the 'dog' node.
            mark1 = current;
            LinkedListNode<string> mark2 = current.Previous;
            current = sentence.Find("dog");
            IndicateNode(current, "Test 9: Indicate the 'dog' node:");

            // The AddBefore method throws an InvalidOperationException
            // if you try to add a node that already belongs to a list.
            Console.WriteLine("Test 10: Throw exception by adding node (fox) already in the list:");
            try
            {
                sentence.AddBefore(current, mark1);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Exception message: {0}", ex.Message);
            }
            Console.WriteLine();

            // Remove the node referred to by mark1, and then add it
            // before the node referred to by current.
            // Indicate the node referred to by current.
            sentence.Remove(mark1);
            sentence.AddBefore(current, mark1);
            IndicateNode(current, "Test 11: Move a referenced node (fox) before the current node (dog):");

            // Remove the node referred to by current.
            sentence.Remove(current);
            IndicateNode(current, "Test 12: Remove current node (dog) and attempt to indicate it:");

            // Add the node after the node referred to by mark2.
            sentence.AddAfter(mark2, current);
            IndicateNode(current, "Test 13: Add node removed in test 11 after a referenced node (brown):");

            // The Remove method finds and removes the
            // first node that that has the specified value.
            sentence.Remove("old");
            Display(sentence, "Test 14: Remove node that has the value 'old':");

            // When the linked list is cast to ICollection(Of String),
            // the Add method adds a node to the end of the list.
            sentence.RemoveLast();
            ICollection<string> icoll = sentence;
            icoll.Add("rhinoceros");
            Display(sentence, "Test 15: Remove last node, cast to ICollection, and add 'rhinoceros':");

            Console.WriteLine("Test 16: Copy the list to an array:");
            // Create an array with the same number of
            // elements as the inked list.
            string[] sArray = new string[sentence.Count];
            sentence.CopyTo(sArray, 0);

            foreach (string s in sArray)
            {
                Console.WriteLine(s);
            }

            // Release all the nodes.
            sentence.Clear();

            Console.WriteLine();
            Console.WriteLine("Test 17: Clear linked list. Contains 'jumps' = {0}",
                sentence.Contains("jumps"));

            Console.WriteLine();
        }
        public static void Test(MyLinkedList<string> sentence)
        {
            // Create the link list.
            Display(sentence, "MyLinkedList: ");
            Console.WriteLine("sentence.Contains(\"jumps\") = {0}",
                sentence.Contains("jumps"));

            // Add the word 'today' to the beginning of the linked list.
            sentence.AddFirst("today");
            Display(sentence, "Test 1: Add 'today' to beginning of the list:");
            
            // Move the first node to be the last node.
            MyLinkedListNode<string> mark1 = sentence.Head;
            sentence.RemoveFirst();
            sentence.AddLast(mark1);
            Display(sentence, "Test 2: Move first node to be last node:");
            
            // Change the last node to 'yesterday'.
            sentence.RemoveLast();
            sentence.AddLast("yesterday");
            Display(sentence, "Test 3: Change the last node to 'yesterday':");
            
            // Move the last node to be the first node.
            mark1 = sentence.Tail;
            sentence.RemoveLast();
            sentence.AddFirst(mark1);
            Display(sentence, "Test 4: Move last node to be first node:");
            
            // Indicate the last occurence of 'the'.
            sentence.RemoveFirst();
            MyLinkedListNode<string> current = sentence.FindLast("the");
            IndicateNode(sentence, current, "Test 5: Indicate last occurence of 'the':");

            // Add 'lazy' and 'old' after 'the' (the LinkedListNode named current).
            sentence.AddAfter(current, "old");
            sentence.AddAfter(current, "lazy");
            IndicateNode(sentence, current, "Test 6: Add 'lazy' and 'old' after 'the':");

            // Indicate 'fox' node.
            current = sentence.Find("fox");
            IndicateNode(sentence, current, "Test 7: Indicate the 'fox' node:");

            // Add 'quick' and 'brown' before 'fox':
            sentence.AddBefore(current, "quick");
            sentence.AddBefore(current, "brown");
            IndicateNode(sentence, current, "Test 8: Add 'quick' and 'brown' before 'fox':");

            // Keep a reference to the current node, 'fox',
            // and to the previous node in the list. Indicate the 'dog' node.
            mark1 = current;
            MyLinkedListNode<string> mark2 = current.Previous;
            current = sentence.Find("dog");
            IndicateNode(sentence, current, "Test 9: Indicate the 'dog' node:");
            
            // The AddBefore method throws an InvalidOperationException
            // if you try to add a node that already belongs to a list.
            
            Console.WriteLine("Test 10: Throw exception by adding node (fox) already in the list:");
            try
            {
                sentence.AddBefore(current, mark1);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Exception message: {0}", ex.Message);
            }
            Console.WriteLine();
            
            // Remove the node referred to by mark1, and then add it
            // before the node referred to by current.
            // Indicate the node referred to by current.
            sentence.Remove(mark1);
            sentence.AddBefore(current, mark1);
            IndicateNode(sentence, current, "Test 11: Move a referenced node (fox) before the current node (dog):");

            // Remove the node referred to by current.
            sentence.Remove(current);
            IndicateNode(sentence, current, "Test 12: Remove current node (dog) and attempt to indicate it:");

            // Add the node after the node referred to by mark2.
            sentence.AddAfter(mark2, current);
            IndicateNode(sentence, current, "Test 13: Add node removed in test 11 after a referenced node (brown):");
            
            // The Remove method finds and removes the
            // first node that that has the specified value.
            sentence.Remove("old");
            Display(sentence, "Test 14: Remove node that has the value 'old':");

            // When the linked list is cast to ICollection(Of String),
            // the Add method adds a node to the end of the list.
            sentence.RemoveLast();
            ICollection<string> icoll = sentence;
            icoll.Add("rhinoceros");
            Display(sentence, "Test 15: Remove last node, cast to ICollection, and add 'rhinoceros':");

            Console.WriteLine("Test 16: Copy the list to an array:");
            // Create an array with the same number of
            // elements as the inked list.
            string[] sArray = new string[sentence.Count];
            sentence.CopyTo(sArray, 0);

            foreach (string s in sArray)
            {
                Console.WriteLine(s);
            }
            
            // Release all the nodes.
            sentence.Clear();

            Console.WriteLine();
            Console.WriteLine("Test 17: Clear linked list. Contains 'jumps' = {0}",
                sentence.Contains("jumps"));

            Console.WriteLine();
        }
        private static void Display(LinkedList<string> words, string test)
        {
            Console.WriteLine(test);
            foreach (string word in words)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        private static void Display(MyLinkedList<string> words, string test)
        {
            Console.WriteLine(test);
            foreach (string word in words)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void IndicateNode(LinkedListNode<string> node, string test)
        {
            Console.WriteLine(test);
            if (node.List == null)
            {
                Console.WriteLine("Node '{0}' is not in the list.\n",
                    node.Value);
                return;
            }

            StringBuilder result = new StringBuilder("(" + node.Value + ")");
            LinkedListNode<string> nodeP = node.Previous;

            while (nodeP != null)
            {
                result.Insert(0, nodeP.Value + " ");
                nodeP = nodeP.Previous;
            }

            node = node.Next;
            while (node != null)
            {
                result.Append(" " + node.Value);
                node = node.Next;
            }

            Console.WriteLine(result);
            Console.WriteLine();
        }
        private static void IndicateNode(MyLinkedList<string> list, MyLinkedListNode<string> node, string test)
        {
            Console.WriteLine(test);
            if (!list.Contains(node.Value))
            {
                Console.WriteLine("Node '{0}' is not in the list.\n",
                    node.Value);
                return;
            }

            StringBuilder result = new StringBuilder("(" + node.Value + ")");
            MyLinkedListNode<string> nodeP = node.Previous;

            while (nodeP != null)
            {
                result.Insert(0, nodeP.Value + " ");
                nodeP = nodeP.Previous;
            }

            node = node.Next;
            while (node != null)
            {
                result.Append(" " + node.Value);
                node = node.Next;
            }

            Console.WriteLine(result);
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            string[] words = { "the", "fox", "jumps", "over", "the", "dog" };
            LinkedList <string> list = new LinkedList<string>(words);
            MyLinkedList<string> my_list = new MyLinkedList<string>(words);
            Test(list);
            Test(my_list);

            int[] BigArrOfInt = new int[10000];
            for (int i = 0; i<10000; i++)
            {
                BigArrOfInt[i] = i;
            }
            Console.WriteLine("Производительность LinkedList: ");
            Stopwatch performance = new Stopwatch();
            performance.Start();
            LinkedList<int> new_list = new LinkedList<int>(BigArrOfInt);
            for (int i = 0; i<10000; i++)
            {
                if (i % 3 == 0)
                {
                    new_list.Remove(i);
                }
            }
            for (int i = 0; i<1000; i++)
            {
                new_list.AddLast(i * i * i);
            }
            performance.Stop();
            Console.WriteLine("{0} in ms", performance.ElapsedMilliseconds);
            performance.Reset();

            Console.WriteLine("Производительность MyLinkedList: ");
            performance.Start();
            LinkedList<int> new_my_list = new LinkedList<int>(BigArrOfInt);
            for (int i = 0; i < 10000; i++)
            {
                if (i % 3 == 0)
                {
                    new_my_list.Remove(i);
                }
            }
            for (int i = 0; i < 1000; i++)
            {
                new_my_list.AddLast(i * i * i);
            }
            performance.Stop();
            Console.WriteLine("{0} in ms", performance.ElapsedMilliseconds);
            Console.WriteLine();
            Console.WriteLine("Test collection LinkedList: ");
            TestCollection(new LinkedList<string>());
            Console.WriteLine();
            Console.WriteLine("Test collection MyLinkedList: ");
            TestCollection(new MyLinkedList<string>());
            Console.WriteLine();
            Console.WriteLine("test IComporable in MyLinkedList");
            int[] arr0 = { 1, 3, 5};
            int[] arr1 = { 1 };
            int[] arr2 = { 4, 8 };
            MyLinkedList<int> list0 = new MyLinkedList<int>(arr0);
            MyLinkedList<int> list1 = new MyLinkedList<int>(arr1);
            MyLinkedList<int> list2 = new MyLinkedList<int>(arr2);
            MyLinkedList<int>[] arr = {list0, list1, list2};
            Console.WriteLine("Current list: ");
            for(int i = 0; i < 3; i++)
            {
                Console.Write("{0}) ", i+1);
                MyLinkedList<int> tmp = new MyLinkedList<int>(arr[i]);
                
                foreach(var item in tmp)
                {
                    Console.Write("{0} ", item);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Sorted List (По числу элементов в списке) : ");
            Array.Sort(arr);
            for (int i = 0; i < 3; i++)
            {
                Console.Write("{0}) ", i + 1);
                MyLinkedList<int> tmp = new MyLinkedList<int>(arr[i]);

                foreach (var item in tmp)
                {
                    Console.Write("{0} ", item);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Test IClonable and Sort() in MyLinkedList");
            int[] nums = { 7, 5, 9, 15, 11, 4, 24, 1, 8 };
            MyLinkedList<int> my_link_list = new MyLinkedList<int>(nums);
            MyLinkedList<int> clone_of_my_link_list = (MyLinkedList<int>) my_link_list.Clone();
            Console.Write("List: ");
            foreach (var i in my_link_list)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.Write("Cloned List: ");
            foreach (var i in clone_of_my_link_list)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Change Cloned List (Remove 5 and 11) :");
            clone_of_my_link_list.Remove(5);
            clone_of_my_link_list.Remove(11);
            Console.Write("List: ");
            foreach (var i in my_link_list)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.Write("Cloned List: ");
            foreach (var i in clone_of_my_link_list)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Now we will sort() our original list : ");
            my_link_list.sort(new IntComparer());
            Console.Write("List: ");
            foreach (var i in my_link_list)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.Write("Cloned List: ");
            foreach (var i in clone_of_my_link_list)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
