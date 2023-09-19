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


