Q: Given a linked list, swap the position of the 1st and 2nd node, then swap the position of the 3rd and 4th node etc.

```py
class Node:
  def __init__(self, value, next=None):
    self.value = value
    self.next = next

  def __repr__(self):
    return f"{self.value}, ({self.next.__repr__()})"

def swap_every_two(llist):
  # Fill this in.

llist = Node(1, Node(2, Node(3, Node(4, Node(5)))))
print(swap_every_two(llist))
# 2, (1, (4, (3, (5, (None)))))
```
- Here's how we can implement the swap_every_two function:

```txt
If the linked list has 0 or 1 node, we don't need to swap anything.
Initialize two pointers, say prev and current. prev is set to None initially and current is set to the head of the linked list.
For each pair of nodes, we will:
Re-point prev.next to the next node of current.
Swap the next pointers of current and current.next.
Move the prev pointer to the position of current.
Move the current pointer two nodes ahead.
Let's implement this:
```
```py

class Node:
    def __init__(self, value, next=None):
        self.value = value
        self.next = next

    def __repr__(self):
        return f"{self.value}, ({self.next.__repr__()})"

def swap_every_two(llist):
    if not llist or not llist.next:
        return llist

    # Dummy node is used to handle edge cases, like swapping the first two nodes.
    dummy = Node(-1)
    dummy.next = llist
    prev = dummy

    while prev.next and prev.next.next:
        # Initializations for the nodes to be swapped
        current = prev.next
        after = current.next
        
        # Swap the nodes.
        current.next = after.next
        after.next = current
        prev.next = after
        
        # Move forward in the list
        prev = current

    return dummy.next

llist = Node(1, Node(2, Node(3, Node(4, Node(5)))))
print(swap_every_two(llist))
```

```css
This code will print:

2, (1, (4, (3, (5, (None)))))
```
- The given code performs swaps on adjacent nodes in a linked list. Let's analyze its time complexity step by step.

1. **Setting Up Pointers and Checks**:
    ```python
    if not llist or not llist.next:
        return llist

    dummy = Node(-1)
    dummy.next = llist
    prev = dummy
    ```

    - The checks and the pointer assignments are all constant-time operations: \(O(1)\).

2. **Main Loop**:
    ```python
    while prev.next and prev.next.next:
        current = prev.next
        after = current.next
        
        current.next = after.next
        after.next = current
        prev.next = after
        
        prev = current
    ```

    - The loop runs for every pair of nodes in the linked list. If there are \(N\) nodes in the linked list, the loop roughly runs \(N/2\) times. But in terms of big-O notation, we represent this as \(O(N)\) since we ignore constant factors.
    
    - Inside the loop, all the operations (assignments, pointer changes, etc.) are constant-time operations, i.e., \(O(1)\).

Therefore, combining everything, the overall time complexity is:

\(O(1) + O(N) \times O(1) = O(N)\)

This means that the time complexity of the `swap_every_two` function is linear in the size of the linked list.

Here's a line-by-line breakdown of the code:

1. `if not llist or not llist.next:` - Check if the list is empty or has a single node: \(O(1)\)
2. `return llist` - Return the original list if no swaps needed: \(O(1)\)
3. `dummy = Node(-1)` - Create a dummy node: \(O(1)\)
4. `dummy.next = llist` - Link dummy node to the head of the list: \(O(1)\)
5. `prev = dummy` - Set `prev` to the dummy node: \(O(1)\)

6. `while prev.next and prev.next.next:` - Start of loop; runs for every pair of nodes: \(O(N)\)
    - `current = prev.next` - Set the current node: \(O(1)\)
    - `after = current.next` - Set the node following the current node: \(O(1)\)
    - `current.next = after.next` - Update the next pointer of the current node: \(O(1)\)
    - `after.next = current` - Update the next pointer of the 'after' node: \(O(1)\)
    - `prev.next = after` - Update the next pointer of the 'prev' node: \(O(1)\)
    - `prev = current` - Move the 'prev' pointer to the next node: \(O(1)\)

- As you can see, all operations inside the loop are \(O(1)\), and the loop runs for \(O(N)\) iterations. Hence, the overall complexity is \(O(N)\).

- 代码的功能是在链表中交换相邻的节点。我们逐步分析其时间复杂度。

1. **设置指针和检查**:
    ```python
    if not llist or not llist.next:
        return llist

    dummy = Node(-1)
    dummy.next = llist
    prev = dummy
    ```

    - 检查和指针赋值都是常数时间操作: \(O(1)\)。

2. **主循环**:
    ```python
    while prev.next and prev.next.next:
        current = prev.next
        after = current.next
        
        current.next = after.next
        after.next = current
        prev.next = after
        
        prev = current
    ```

    - 循环每次运行都针对链表中的两个节点。如果链表中有\(N\)个节点，循环大约运行\(N/2\)次。但在大O表示法中，我们将其表示为\(O(N)\)，因为我们忽略常数因子。
    
    - 循环内的所有操作（赋值、指针更改等）都是常数时间操作，即 \(O(1)\)。

所以，综合上述，总的时间复杂度是:

\(O(1) + O(N) \times O(1) = O(N)\)

这意味着`swap_every_two`函数的时间复杂度是链表大小的线性时间。

以下是代码的逐行分析：

1. `if not llist or not llist.next:` - 检查链表是否为空或只有一个节点: \(O(1)\)
2. `return llist` - 如果不需要交换，则返回原始列表: \(O(1)\)
3. `dummy = Node(-1)` - 创建一个虚拟节点: \(O(1)\)
4. `dummy.next = llist` - 将虚拟节点链接到列表的头部: \(O(1)\)
5. `prev = dummy` - 将`prev`设置为虚拟节点: \(O(1)\)

6. `while prev.next and prev.next.next:` - 开始循环；针对每对节点运行: \(O(N)\)
    - `current = prev.next` - 设置当前节点: \(O(1)\)
    - `after = current.next` - 设置当前节点的下一个节点: \(O(1)\)
    - `current.next = after.next` - 更新当前节点的下一个指针: \(O(1)\)
    - `after.next = current` - 更新'after'节点的下一个指针: \(O(1)\)
    - `prev.next = after` - 更新'prev'节点的下一个指针: \(O(1)\)
    - `prev = current` - 将'prev'指针移动到下一个节点: \(O(1)\)

正如你看到的，循环内的所有操作都是 \(O(1)\)，循环进行 \(O(N)\) 次迭代。因此，总的复杂性是 \(O(N)\)。


## Recursive version of the `swap_every_two` function. 

Here's a recursive implementation based on the given code:

```python
class Node:
    def __init__(self, value, next=None):
        self.value = value
        self.next = next

    def __repr__(self):
        return f"{self.value}, ({self.next.__repr__()})"

def swap_every_two(node):
    # Base case: If the node is None or next node is None, just return the node.
    if not node or not node.next:
        return node

    # Nodes to be swapped.
    first = node
    second = node.next

    # Swap the nodes.
    first.next = swap_every_two(second.next)
    second.next = first

    # Now, the head is the second node.
    return second

llist = Node(1, Node(2, Node(3, Node(4, Node(5)))))
print(swap_every_two(llist))
```

Here's a breakdown of how the recursive function works:

1. If the current node (`node`) is `None` or the next node is `None`, just return the `node`. This is the base case for the recursive function and handles the cases where the linked list has an odd length or we have reached the end of the list.

2. Swap the current node with the next node.

3. Recursively call the `swap_every_two` function for the node after the next node (`second.next`). This is because we've already handled the current node and the next node.

4. Update the next pointer of the `second` node to point to the `first` node to complete the swap.

5. Return the `second` node as it's now the head of the modified sublist.

The function continues to process and swap pairs of nodes recursively until it reaches the end of the list.

The recursive approach to swap pairs of nodes in a linked list traverses the list once, visiting each node exactly once.

Let's analyze the time complexity:

For every recursive call to `swap_every_two`:
1. There's a constant amount of work done, which includes swapping the nodes and adjusting the pointers. This work is \(O(1)\).
2. The recursive function is called once for every pair of nodes. Since there are \(N\) nodes, there will be roughly \(N/2\) recursive calls.

Combining these, the overall time complexity for the recursive function is:

\[O(1) \times \frac{N}{2} = O(N)\]

So, the time complexity of the recursive approach to swap every two nodes in the linked list is \(O(N)\), where \(N\) is the number of nodes in the list. This is linear with respect to the size of the list, similar to the iterative approach.

## Converting a loop (`while` or `for`) into recursion is a common task in algorithms and data structures. Here's a general guideline to help with this transition:

1. **Base Case(s)**:
    - Identify the condition(s) under which the loop terminates.
    - These conditions will typically translate into the base case(s) for your recursion.
    - The base case is the scenario where the recursion will not make further recursive calls and will just return a result.

2. **Recursive Case**:
    - Identify the main body of the loop. What does each iteration of the loop do?
    - Translate this action into a recursive function call.
    - Adjust the arguments of the function to reflect the state or progress, such as advancing to the next node or decrementing a count.

3. **State Maintenance**:
    - Consider how the loop maintains state, such as updating loop counters or advancing pointers.
    - Ensure that the recursive function is designed to maintain and update this state. This usually means passing this state as arguments to the recursive function.

4. **Avoid Global Variables**:
    - While it might be tempting to use global variables to maintain state across recursive calls, it's usually cleaner and more intuitive to pass state information as function arguments.

5. **Testing**:
    - After writing the recursive function, test it on various cases to ensure it behaves as expected. Recursive functions can sometimes introduce unexpected behaviors if not properly defined.

**Example:**

Let's consider a simple `while` loop that counts down from a given number to 1 and print each number:

```python
def countdown_while(n):
    while n > 0:
        print(n)
        n -= 1
```

To convert this to a recursive function:

1. Base Case: `n` reaches 0 or less (the loop termination condition).
2. Recursive Case: Print `n` and then make a recursive call with `n-1`.

Here's how it looks:

```python
def countdown_recursive(n):
    if n <= 0:  # Base case
        return
    print(n)
    countdown_recursive(n-1)  # Recursive case with updated state
```

In this simple example, the recursive function closely mirrors the structure and logic of the original loop. The iterative loop's exit condition becomes the base case, and the loop body becomes the core of the recursive function.

## Let's go step by step, using the `swap_every_two` function from the linked list example to illustrate the general guidelines:

### 1. **Base Case(s)**:
**Iterative version:**
```python
while prev.next and prev.next.next:
```
The loop continues as long as we have pairs of nodes left to swap. When we're left with a single node or no nodes, the loop stops.

**Recursive version:**
```python
if not node or not node.next:
    return node
```
When there isn't a pair of nodes left (i.e., when we're left with one or no nodes), this serves as our base case. We return the node without making a recursive call.

### 2. **Recursive Case**:
**Iterative version:**
Inside the loop, you perform the swap and move to the next pair by jumping two nodes.
```python
current = prev.next
after = current.next
...
prev = current
```

**Recursive version:**
After performing the swap, you call the function recursively to handle the rest of the list:
```python
first.next = swap_every_two(second.next)
```
This is like moving to the next pair in the iterative version.

### 3. **State Maintenance**:
**Iterative version:**
The state (i.e., the current pair being processed) is maintained using the `prev`, `current`, and `after` pointers.

**Recursive version:**
The state is maintained implicitly in the function call stack. Every recursive call processes a pair of nodes and passes the rest of the list to the next call. The state is updated by moving to the node after the next (`second.next`).

### 4. **Avoid Global Variables**:
In both the iterative and recursive versions of `swap_every_two`, we did not use global variables. Instead, we passed the current state (node pointers) as function arguments or local variables.

### 5. **Testing**:
After converting the iterative function to a recursive one, test it on various cases to ensure it works correctly.

---

To summarize, the iterative loop's termination condition (no pairs left) became our recursive function's base case. The main action of the loop (swapping a pair) became the core of our recursive function. And the state (current pair being processed) was maintained through function arguments and the call stack in the recursive version.
