### **Semaphore vs Mutex vs Monitor 的区别**

在 C# 中，`Semaphore`、`Mutex` 和 `Monitor` 都是用于多线程同步的工具，但它们的功能和使用场景有所不同。

---

### **对比表**

| 特性                | **Semaphore**                          | **Mutex**                               | **Monitor**                            |
|---------------------|-----------------------------------------|-----------------------------------------|----------------------------------------|
| **控制范围**         | 控制多个线程同时访问资源               | 控制单线程对资源的独占访问               | 控制单线程对资源的独占访问              |
| **线程数量**         | 可同时允许多个线程访问                 | 仅允许一个线程访问                      | 仅允许一个线程访问                     |
| **跨进程支持**       | 支持（通过 `Semaphore` 或 `SemaphoreSlim`） | 支持（适用于跨进程场景）                 | 不支持，只适用于单进程                  |
| **性能**            | 性能高，但比 `Monitor` 略低            | 性能低于 `Monitor` 和 `Semaphore`      | 性能最高                              |
| **超时支持**         | 支持超时                              | 支持超时                              | 支持超时                              |
| **自动释放**         | 不支持，需要手动调用 `Release`          | 支持（通过 `using` 块）                | 不支持，需要手动调用 `Monitor.Exit`    |
| **典型使用场景**     | 限制多个线程同时访问资源，如线程池管理   | 跨线程或跨进程的独占资源访问             | 单线程独占访问共享资源                 |

---

### **详细比较**

#### **1. Semaphore（信号量）**
- **特点**：
  - 控制多个线程同时访问资源。
  - 支持跨进程使用。
  - 使用计数器记录当前可用信号量数量。
- **适用场景**：
  - 用于限制资源的并发访问数量，例如连接池、线程池。
- **优缺点**：
  - 优点：灵活控制线程数量，支持跨进程。
  - 缺点：需要手动管理 `Wait` 和 `Release`，易出错。

#### **代码示例**：
```csharp
static Semaphore semaphore = new Semaphore(3, 3); // 最多允许 3 个线程同时访问

void UseResource()
{
    semaphore.WaitOne(); // 获取信号量
    try
    {
        Console.WriteLine("线程正在使用资源...");
    }
    finally
    {
        semaphore.Release(); // 释放信号量
    }
}
```

---

#### **2. Mutex（互斥量）**
- **特点**：
  - 控制单个线程对资源的独占访问。
  - 支持跨进程同步。
  - 自动释放机制（通过 `using` 块）。
- **适用场景**：
  - 用于跨线程或跨进程保护共享资源，例如文件写入锁。
- **优缺点**：
  - 优点：支持跨进程，使用简单。
  - 缺点：性能低于 `Monitor` 和 `Semaphore`。

#### **代码示例**：
```csharp
static Mutex mutex = new Mutex();

void UseResource()
{
    mutex.WaitOne(); // 获取互斥锁
    try
    {
        Console.WriteLine("线程正在使用资源...");
    }
    finally
    {
        mutex.ReleaseMutex(); // 释放互斥锁
    }
}
```

---

#### **3. Monitor（监视器）**
- **特点**：
  - 控制单线程对资源的独占访问。
  - 仅适用于单进程。
  - 性能最高，适合轻量级线程同步。
- **适用场景**：
  - 线程内对共享数据的独占访问。
- **优缺点**：
  - 优点：性能高，适合单进程。
  - 缺点：不支持跨进程，需要手动管理 `Monitor.Enter` 和 `Monitor.Exit`。

#### **代码示例**：
```csharp
static readonly object lockObject = new object();

void UseResource()
{
    Monitor.Enter(lockObject); // 获取锁
    try
    {
        Console.WriteLine("线程正在使用资源...");
    }
    finally
    {
        Monitor.Exit(lockObject); // 释放锁
    }
}
```

---

### **总结**

1. **Semaphore**：
   - 适合控制多个线程访问资源的数量（并发访问限制）。
   - 支持跨进程。

2. **Mutex**：
   - 用于单线程独占资源，支持跨进程。
   - 适合需要跨进程同步的场景。

3. **Monitor**：
   - 适合单线程对共享资源的独占访问，性能最佳。
   - 仅适用于单进程。

**选择建议**：
- **轻量级同步（单进程）**：`Monitor`
- **跨线程且支持多个访问者**：`Semaphore`
- **跨线程且只允许一个访问者**：`Mutex`
