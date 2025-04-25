# .NET领域驱动设计(DDD)实施模式总结

## 核心架构模式

1. **分层架构**:
   - 表现层 (Presentation/API)
   - 应用层 (Application - 协调工作流)
   - 领域层 (Domain - 核心业务逻辑)
   - 基础设施层 (Infrastructure - 技术实现细节)

## 领域建模模式

### 1. 实体(Entity)模式
```csharp
public class Order : Entity<int>
{
    // 标识符 + 业务属性
    // 封装状态变更
    // 包含业务行为方法
}
```

### 2. 值对象(Value Object)模式
```csharp
public class Address : ValueObject
{
    // 无标识符
    // 不可变
    // 基于属性值比较相等性
}
```

### 3. 聚合根(Aggregate Root)模式
```csharp
public class Order : IAggregateRoot
{
    private List<OrderItem> _items;
    
    // 控制对内部对象的访问
    public void AddItem(Product p, int qty) { ... }
}
```

## 基础设施模式

### 1. 仓储(Repository)模式
```csharp
public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);
    Task AddAsync(Order order);
}

// 实现类在基础设施层
```

### 2. 工作单元(Unit of Work)模式
```csharp
public class OrderDbContext : DbContext, IUnitOfWork
{
    // 封装多个仓储的提交
    public async Task CommitAsync() => await SaveChangesAsync();
}
```

## 应用模式

### 1. CQRS模式
```csharp
// 命令
public class CreateOrderCommand : IRequest<OrderDto> { ... }

// 查询
public class GetOrderQuery : IRequest<OrderDto> { ... }
```

### 2. 领域事件模式
```csharp
public class OrderPlacedEvent : IDomainEvent { ... }

// 在聚合内触发
order.PlaceOrder(); // 内部触发OrderPlacedEvent
```

## 实施流程模板

1. **识别限界上下文** (Bounded Context)
2. **定义聚合根** (Aggregate Roots)
3. **设计领域模型** (Entities + Value Objects)
4. **创建仓储接口** (Domain层)
5. **实现应用服务** (协调领域对象和基础设施)
6. **处理领域事件** (响应业务状态变化)

## 代码结构模板

```
/src
  /Application      // 应用服务、DTOs
  /Domain           // 领域模型、仓储接口
    /Models         // 实体、值对象
    /Services       // 领域服务
    /Events         // 领域事件
    /Interfaces     // 仓储接口
  /Infrastructure   // 持久化实现
  /WebAPI           // 表现层
```

这个模式模板提供了在.NET中实施DDD的标准方法，可根据项目规模适当调整各层的复杂度。
