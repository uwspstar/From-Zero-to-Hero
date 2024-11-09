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
