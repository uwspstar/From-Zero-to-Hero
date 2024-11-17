### Multithreading & Asynchronous Programming in C#/.NET

---

- [Download 500 files using threading in C#](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/Download%20500%20files%20using%20threading%20in%20C%23.md)
- [Environment.CurrentManagedThreadId and Thread.CurrentThread.ManagedThreadId](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/Environment.CurrentManagedThreadId%20vs%20Thread.CurrentThread.ManagedThreadId.md)
- [Why Multithreading Causes Non-Sequential Output](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/Why%20Multithreading%20Causes%20Non-Sequential%20Output.md)
- [Thread priority](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/Thread%20priority.md)
- [Why `using` Doesn’t Work with `lock` and `Monitor`](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/Why%20%60using%60%20Doesn%E2%80%99t%20Work%20with%20%60lock%60%20and%20%60Monitor%60.md)
- [HTTP request and response handling process](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/HTTP%20request%20and%20response%20handling%20process.md)
- [Why and How to Use ConfigureAwait(false) in C#](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/Why%20and%20How%20to%20Use%20%60ConfigureAwait(false)%60%20in%20C%23.md)


---

### Table of Contents

1. Introduction
2. [CPU, Thread, and Thread Scheduler](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/002.%20CPU%2C%20Thread%2C%20and%20Thread%20Scheduler%20in%20C%23.md)
3. [Basic Syntax to Start a Thread](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/003.%20Basic%20Syntax%20to%20Start%20a%20Thread%20in%20C%23.md)
4. [Why Use Threading for Divide and Conquer](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20&%20Asynchronous%20Programming/004.%20Why%20Use%20Threading%20for%20Divide%20and%20Conquer%20in%20C%23.md)
5. [Why Use Threading to Offload Long-Running Tasks](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/005.%20Why%20Use%20Threading%20to%20Offload%20Long-Running%20Tasks%20in%20C%23.md)
6. Assignment: [Create a Web Server](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/006.%20Create%20a%20Web%20Server.md)
7. [Threads Synchronization Overview](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/007.%20Overview%20of%20Thread%20Synchronization.md)
8. [Critical Section and Atomic Operation](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/008.%20Critical%20Section.md)
9. [Exclusive Lock](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/009.%20Exclusive%20Lock.md)
10. Assignment: [Airplane Seats Booking System](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/010.%20Airplane%20Seats%20Booking%20System.md)
11. [Using Monitor to Add Timeout for Locks](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/011.%20Using%20%60Monitor%60%20to%20Add%20Timeout%20for%20Locks.md)
12. [Using Mutex to Synchronize Across Processes](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/012.%20Using%20%60Mutex%60%20to%20Synchronize%20Across%20Processes.md)
13. [Reader and Writer Lock](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/013.%20Reader-Writer%20Lock.md)
14. [Using Semaphore to Limit Number of Threads](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/014.%20Using%20%60Semaphore%60%20to%20Limit%20the%20Number%20of%20Threads.md)
15. [Using AutoResetEvent for Signaling](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/015.%20Using%20%60AutoResetEvent%60%20for%20Signaling.md)
16. [Using ManualResetEvent to Release Multiple Threads](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/016.%20Using%20%60ManualResetEvent%60%20to%20Release%20Multiple%20Threads.md)
17. Assignment: [Two-Way Signaling in Producer-Consumer Scenario](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/017.%20Two-Way%20Signaling%20in%20Producer-Consumer%20Scenario.md)
18. [Thread Affinity](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/018.%20Thread%20Affinity.md)
19. [Thread Safety](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/019.%20Thread%20Safety.md)
20. [Nested Locks and Deadlock](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/020.%20Nested%20Locks%20and%20Deadlock.md)
21. [Multithreading MISC](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/021.%20Multithreading%20Miscellaneous%20Topics.md)
22. [Debugging Programs with Multiple Threads](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/022.%20Debugging%20Programs%20with%20Multiple%20Threads.md)
23. [States of a Thread](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/023.%20States%20of%20a%20Thread%3A%20Understanding%20Thread%20Lifecycle.md)
24. [Make Thread Wait for Some Time](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/024.%20%60Thread.SpinWait%60%20vs%20%60Thread.Sleep%60%20vs%20%60Thread.SpinUntil%60.md)
25. [Returning Results from a Thread](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20&%20Asynchronous%20Programming/025.%20Returning%20Results%20from%20a%20Thread.md)
26. [Canceling a Thread](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/026.%20Canceling%20a%20Thread.md)
27. [Thread Pool](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/027.%20Thread%20Pool.md)
28. [Exception Handling in Threads](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/028.%20Exception%20Handling%20in%20Threads.md)
29. [Task-Based Asynchronous Programming](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20&%20Asynchronous%20Programming/029.%20Task-Based%20Asynchronous%20Programming.md)
30. [Multithreading vs. Asynchronous Programming](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/030.%20Multithreading%20vs.%20Asynchronous%20Programming.md)
31. [Basic Syntax of Using Task](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/031.%20Basic%20Syntax%20of%20Using%20Task.md)
32. [Task vs. Thread](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/032.%20Task%20vs.%20Thread.md)
33. [Task Uses Thread Pool by Default](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/033.%20Task%20Uses%20Thread%20Pool%20by%20Default.md)
34. [Returning Result from Task](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/034.%20Returning%20Result%20from%20Task.md)
35. [Task Continuation: Wait, WaitAll, Result](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/035.%20Task%20Continuation%20in%20C%23%3A%20%60Wait%60%2C%20%60WaitAll%60%2C%20%60Result%60.md)
36. [Task Continuation: ContinueWith](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/036.%20Task%20Continuation%3A%20%60ContinueWith%60.md)
37. [Task Continuation: WhenAll, WhenAny](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/037.%20Task%20Continuation%3A%20%60Task.WhenAll%60%20and%20%60Task.WhenAny%60.md)
38. [Task Continuation: Continuation Chain & Unwrap](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/038.%20Continuation%20Chain%20and%20Unwrap.md)
39. [Exception Handling in Tasks](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/039.%20Exception%20Handling%20in%20Tasks%20in%20C%23.md)
40. [Tasks Synchronization](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/040.%20Task%20Synchronization%20in%20C%23.md)
41. [Task Cancelation](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/041.%20Task%20Cancellation%20in%20C%23.md)
42. [Async and Await](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/042.%20Understanding%20%60async%60%20and%20%60await%60%20in%20C%23.md)
43. [Overview of Async & Await](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/043.%20Overview%20of%20Async%20%26%20Await.md)
44. [Basic Syntax of Async and Await](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/044.%20Basic%20Syntax%20of%20Async%20and%20Await.md)
45. [Which Thread is Used in Async](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/045.%20Which%20Thread%20is%20Used%20in%20Async.md)
46. [Continuation After Returning Value](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/046.%20Continuation%20After%20Returning%20Value.md)
47. [Exception Handling with Async and Await](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/047.%20Exception%20Handling%20with%20Async%20and%20Await.md)
48. [Await and Synchronization Context](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/048.%20Await%20and%20Synchronization%20Context.md)
49. [What Await Does](https://github.com/uwspstar/From-Zero-to-Hero/blob/main/C%23.NET/Multithreading%20%26%20Asynchronous%20Programming/049.%20What%20Await%20Does.md)
50. Parallel Loops
51. Parallel Loops Overview and Basic Syntax
52. Behind the Scenes of Parallel Loops
53. Exception Handling in Parallel Loops
54. Stop and Break in Parallel Loops
55. ParallelLoopResult
56. Cancelation in Parallel Loops
57. Thread Local Storage
58. Performance Considerations
59. PLINQ (Parallel LINQ)
60. Basics of PLINQ
61. Producer, Consumer, and Buffer in PLINQ
62. `foreach` vs. `ForAll` in PLINQ
63. Exception Handling in PLINQ
64. Cancelation in PLINQ
65. Concurrent Collections
66. ConcurrentQueue
67. ConcurrentStack
68. BlockingCollection and Producer-Consumer Scenario
