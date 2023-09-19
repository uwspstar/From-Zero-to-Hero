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
