```csharp
using System;
using System.Collections.Generic;
using System.Threading;

public class GlobalConfigurationCache
{
    // 使用 ReaderWriterLockSlim 实例，允许线程安全的读写锁定
    private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

    // 用于存储缓存数据的字典
    private Dictionary<int, string> _cache = new Dictionary<int, string>();

    // 添加数据到缓存的方法
    public void Add(int key, string value)
    {
        bool lockAcquired = false; // 标志变量，用于跟踪是否成功获取写锁
        try
        {
            _lock.EnterWriteLock(); // 获取写锁，阻止其他线程的读写操作
            lockAcquired = true;    // 表示成功获取写锁
            _cache[key] = value;    // 将键值对添加或更新到缓存中
        }
        finally
        {
            if (lockAcquired) _lock.ExitWriteLock(); // 释放写锁，允许其他线程继续
        }
    }

    // 从缓存中获取数据的方法
    public string? Get(int key)
    {
        bool lockAcquired = false; // 标志变量，用于跟踪是否成功获取读锁
        try
        {
            _lock.EnterReadLock(); // 获取读锁，允许多个线程同时读取
            lockAcquired = true;   // 表示成功获取读锁
            // 尝试获取缓存中的键值对，如果存在则返回对应的值，否则返回 null
            return _cache.TryGetValue(key, out var value) ? value : null;
        }
        finally
        {
            if (lockAcquired) _lock.ExitReadLock(); // 释放读锁，允许其他线程继续
        }
    }
}
```

### 代码说明

1. **`private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();`**
   - 使用 `ReaderWriterLockSlim` 实例 `_lock`，用于实现线程安全的读写锁定。它允许多个线程读取资源，但只有一个线程可以写入资源，防止数据竞争。

2. **`private Dictionary<int, string> _cache = new Dictionary<int, string>();`**
   - 定义一个字典 `_cache`，用于存储键值对形式的缓存数据。

3. **`public void Add(int key, string value)`**
   - 定义一个 `Add` 方法，将新的键值对添加到缓存中，或更新现有的键值对。

4. **`bool lockAcquired = false;`**
   - 定义一个标志变量 `lockAcquired`，用于跟踪是否成功获取锁，以确保在获取锁后才释放锁。

5. **`_lock.EnterWriteLock();`**
   - 获取写锁，确保在进行写入操作时没有其他线程可以访问缓存数据。
   - `lockAcquired` 设置为 `true` 表示成功获取写锁。

6. **`_cache[key] = value;`**
   - 将提供的键值对存储或更新到 `_cache` 字典中。

7. **`finally { if (lockAcquired) _lock.ExitWriteLock(); }`**
   - 在 `finally` 块中释放写锁，即使在添加过程中发生异常也会释放。确保其他线程可以继续访问缓存。

8. **`public string? Get(int key)`**
   - 定义 `Get` 方法，用于从缓存中获取特定键的值。

9. **`_lock.EnterReadLock();`**
   - 获取读锁，允许多个线程同时读取缓存数据，但不能进行写入操作。

10. **`return _cache.TryGetValue(key, out var value) ? value : null;`**
    - 尝试从 `_cache` 中获取指定键的值，如果存在则返回对应的值，否则返回 `null`。

11. **`finally { if (lockAcquired) _lock.ExitReadLock(); }`**
    - 在 `finally` 块中释放读锁，确保读取完成后锁会被释放，允许其他线程进行读写操作。