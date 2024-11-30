### **C# 中的 ManualResetEvent**

**`ManualResetEvent`** 是一种线程同步工具，用于在线程之间协调任务的执行顺序。它允许多个线程等待一个信号，然后根据需要手动重置信号。

---

### **核心功能**

1. **控制线程状态**：  
   通过设置为“信号”或“无信号”状态，控制等待的线程是否可以继续执行。  

2. **手动重置**：  
   当设置为“信号”状态时，所有等待的线程都会被唤醒，需要手动将状态重置为“无信号”。

---

### **关键点**

| **特性**               | **说明**                                                                                     |
|------------------------|---------------------------------------------------------------------------------------------|
| **初始状态**           | 可以设置为 **`true`**（信号状态）或 **`false`**（无信号状态）。                               |
| **线程唤醒**           | 当信号被触发时，唤醒所有等待的线程，直到状态被重置为“无信号”。                               |
| **重置机制**           | 必须通过调用 `Reset()` 方法手动将状态设置为“无信号”。                                         |
| **适用场景**           | 适用于广播信号让多个线程同时继续执行的场景，例如通知多个线程开始处理任务。                     |

---

### **常用方法**

| **方法**                 | **描述**                                                                                       |
|--------------------------|-----------------------------------------------------------------------------------------------|
| **`Set()`**              | 将事件状态设置为“信号”，唤醒所有等待线程。                                                     |
| **`Reset()`**            | 将事件状态设置为“无信号”，使线程在调用 `WaitOne()` 时阻塞。                                      |
| **`WaitOne()`**          | 等待事件信号。如果事件处于“信号”状态，线程立即继续执行；否则线程阻塞直到信号被触发。               |

---

### **基本用法**

#### **创建 ManualResetEvent**

```csharp
ManualResetEvent manualResetEvent = new ManualResetEvent(false);
```

- **`false`**：初始为无信号状态，线程会阻塞直到信号触发。
- **`true`**：初始为信号状态，线程可以立即执行。

---

#### **代码示例**

#### **1. 唤醒多个线程**

```csharp
using System;
using System.Threading;

class Program
{
    static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

    static void Main()
    {
        for (int i = 1; i <= 3; i++)
        {
            int threadId = i;
            Thread thread = new Thread(() => Worker(threadId));
            thread.Start();
        }

        Console.WriteLine("主线程等待3秒后发出信号...");
        Thread.Sleep(3000);

        // 发出信号，所有等待的线程将被唤醒
        manualResetEvent.Set();

        // 重置信号，阻止后续线程继续执行
        Console.WriteLine("等待5秒后重置信号...");
        Thread.Sleep(5000);
        manualResetEvent.Reset();
    }

    static void Worker(int id)
    {
        Console.WriteLine($"线程 {id} 等待信号...");
        manualResetEvent.WaitOne(); // 等待信号
        Console.WriteLine($"线程 {id} 收到信号，继续执行...");
    }
}
```

**运行结果**：
```
线程 1 等待信号...
线程 2 等待信号...
线程 3 等待信号...
主线程等待3秒后发出信号...
线程 1 收到信号，继续执行...
线程 2 收到信号，继续执行...
线程 3 收到信号，继续执行...
等待5秒后重置信号...
```

---

#### **2. 配合线程池使用**

```csharp
using System;
using System.Threading;

class Program
{
    static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

    static void Main()
    {
        Console.WriteLine("启动线程池任务...");

        for (int i = 1; i <= 5; i++)
        {
            ThreadPool.QueueUserWorkItem(Worker, i);
        }

        Console.WriteLine("主线程等待信号...");
        Thread.Sleep(3000);

        // 触发信号
        manualResetEvent.Set();
    }

    static void Worker(object state)
    {
        int id = (int)state;
        Console.WriteLine($"线程 {id} 等待信号...");
        manualResetEvent.WaitOne();
        Console.WriteLine($"线程 {id} 收到信号，开始处理任务...");
    }
}
```

**运行结果**：
```
启动线程池任务...
线程 1 等待信号...
线程 2 等待信号...
线程 3 等待信号...
线程 4 等待信号...
线程 5 等待信号...
主线程等待信号...
线程 1 收到信号，开始处理任务...
线程 2 收到信号，开始处理任务...
线程 3 收到信号，开始处理任务...
线程 4 收到信号，开始处理任务...
线程 5 收到信号，开始处理任务...
```

---

### **与 AutoResetEvent 的对比**

| **对比点**             | **ManualResetEvent**                                 | **AutoResetEvent**                                 |
|------------------------|-----------------------------------------------------|--------------------------------------------------|
| **信号重置**           | 必须手动调用 `Reset()` 将状态设置为“无信号”。         | 自动重置为“无信号”状态。                             |
| **唤醒线程数**         | 唤醒所有等待的线程。                                   | 每次唤醒一个等待线程。                               |
| **适用场景**           | 广播信号让多个线程同时执行，例如通知多个线程处理任务。     | 控制线程按顺序执行，例如生产者-消费者模型。             |

---

### **注意事项**

**提示 (Tips):**
1. 手动调用 `Reset()` 时，应确保没有线程依赖当前信号状态。
2. 如果需要同时唤醒多个线程并再次阻塞后续线程，可以配合 `Set()` 和 `Reset()` 使用。

**警告 (Warning):**
1. **资源释放**：确保 `ManualResetEvent` 在不再使用时被释放，避免内存泄漏。
   ```csharp
   manualResetEvent.Dispose();
   ```
2. **竞态条件**：可能会因为 `Set()` 和 `Reset()` 的调用顺序错误，导致线程逻辑异常。

---

### **总结**

- **`ManualResetEvent`** 是一种线程同步机制，适用于广播信号让多个线程同时执行的场景。
- 与 **`AutoResetEvent`** 不同，`ManualResetEvent` 的信号状态需要手动重置。
- 在需要多个线程同步开始任务的场景中，`ManualResetEvent` 是一个高效的工具。
