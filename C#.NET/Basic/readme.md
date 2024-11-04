### commonly used .NET C# data structures
---

- [Understanding Top-Level Statements in .NET 8 C#](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Basic/Understanding%20Top-Level%20Statements.md)
- [Understanding Lambda Expressions in C#](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Basic/Understanding%20Lambda%20Expressions%20in%20C%23.md)

---

| Data Structure        | Description                                      | Example Code                                                                                  |
|-----------------------|--------------------------------------------------|-----------------------------------------------------------------------------------------------|
| **Array**             | Fixed-size collection of elements of the same type.  | ```csharp\nint[] numbers = { 1, 2, 3, 4, 5 };\n```                                           |
| **List<T>**           | Dynamic array; automatically resizes as elements are added. | ```csharp\nList<int> numbers = new List<int> { 1, 2, 3 };\nnumbers.Add(4);\n```              |
| **LinkedList<T>**     | Doubly linked list; allows fast insertion and deletion. | ```csharp\nLinkedList<string> names = new LinkedList<string>();\nnames.AddLast("Alice");\n```|
| **Dictionary<TKey, TValue>** | Key-value pairs; fast lookup by key.            | ```csharp\nDictionary<string, int> ages = new Dictionary<string, int> { {"Alice", 25} };\n```|
| **HashSet<T>**        | Unordered collection of unique elements.           | ```csharp\nHashSet<int> uniqueNumbers = new HashSet<int> { 1, 2, 3 };\n```                   |
| **Queue<T>**          | FIFO (First-In-First-Out) collection; allows enqueue and dequeue. | ```csharp\nQueue<string> queue = new Queue<string>();\nqueue.Enqueue("First");\n```         |
| **Stack<T>**          | LIFO (Last-In-First-Out) collection; allows push and pop. | ```csharp\nStack<string> stack = new Stack<string>();\nstack.Push("Top");\n```               |
| **SortedList<TKey, TValue>** | Key-value pairs sorted by key; supports fast lookups and ordered iteration. | ```csharp\nSortedList<int, string> sortedList = new SortedList<int, string>();\n```           |
| **SortedDictionary<TKey, TValue>** | Similar to Dictionary, but keeps elements sorted by key. | ```csharp\nSortedDictionary<int, string> sortedDict = new SortedDictionary<int, string>();\n``` |
| **SortedSet<T>**      | A sorted collection of unique elements.           | ```csharp\nSortedSet<int> sortedSet = new SortedSet<int> { 1, 3, 2 };\n```                   |
| **BitArray**          | Array of bits (true/false values); used for memory-efficient storage. | ```csharp\nBitArray bits = new BitArray(8);\nbits.SetAll(true);\n```                         |
| **Tuple**             | Data structure to store a fixed-size collection of items of different types. | ```csharp\nTuple<int, string> person = new Tuple<int, string>(1, "Alice");\n```             |
| **ValueTuple**        | Lightweight alternative to Tuple; value type and supports deconstruction. | ```csharp\nvar person = (1, "Alice");\nvar (id, name) = person;\n```                         |
| **ArrayList**         | Non-generic, dynamically resizable array; can store any object type. | ```csharp\nArrayList list = new ArrayList();\nlist.Add(1);\nlist.Add("string");\n```         |
| **Queue**             | Non-generic version of Queue; FIFO collection.   | ```csharp\nQueue queue = new Queue();\nqueue.Enqueue("First");\n```                          |
| **Stack**             | Non-generic version of Stack; LIFO collection.   | ```csharp\nStack stack = new Stack();\nstack.Push("Top");\n```                               |
| **ConcurrentDictionary<TKey, TValue>** | Thread-safe version of Dictionary; allows concurrent access. | ```csharp\nConcurrentDictionary<int, string> dict = new ConcurrentDictionary<int, string>();\n``` |
| **BlockingCollection<T>** | Thread-safe collection; useful for producer-consumer scenarios. | ```csharp\nBlockingCollection<int> collection = new BlockingCollection<int>();\n```         |
| **ImmutableList<T>**  | Read-only list that cannot be modified once created. | ```csharp\nvar list = ImmutableList.Create(1, 2, 3);\n```                                    |
| **ImmutableDictionary<TKey, TValue>** | Read-only dictionary; cannot be modified once created. | ```csharp\nvar dict = ImmutableDictionary.CreateBuilder<int, string>();\n```                 |
| **ImmutableHashSet<T>** | Read-only set of unique elements.               | ```csharp\nvar set = ImmutableHashSet.Create(1, 2, 3);\n```                                  |
| **ImmutableQueue<T>** | Read-only queue that supports enqueue operations but returns new queue on each modification. | ```csharp\nvar queue = ImmutableQueue.Create<int>();\nqueue = queue.Enqueue(1);\n``` |
| **ImmutableStack<T>** | Read-only stack that returns a new stack on each modification. | ```csharp\nvar stack = ImmutableStack.Create<int>();\nstack = stack.Push(1);\n```           |

Each data structure provides specific functionalities tailored for different scenarios, from general-purpose collections like `List<T>` to specialized structures like `ImmutableStack<T>`.
