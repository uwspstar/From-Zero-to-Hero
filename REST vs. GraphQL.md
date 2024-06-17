### REST vs. GraphQL 比较

#### Overview 概述

REST and GraphQL are two different approaches to building APIs. REST (Representational State Transfer) is an architectural style for designing networked applications, while GraphQL is a query language for APIs and a runtime for executing those queries by using a type system you define for your data.

REST 和 GraphQL 是构建 API 的两种不同方法。REST（表述性状态转移）是一种设计网络应用程序的架构风格，而 GraphQL 是一种用于 API 的查询语言和运行时，通过使用您为数据定义的类型系统来执行这些查询。

#### Key Differences 主要区别

1. **Data Fetching 数据获取**
    - **REST**: Multiple endpoints for different resources, often leading to over-fetching or under-fetching of data.
    - **GraphQL**: Single endpoint, clients can specify exactly what data they need, reducing over-fetching and under-fetching.

    - **REST**: 针对不同资源的多个端点，通常导致数据的过度获取或不足获取。
    - **GraphQL**: 单个端点，客户端可以准确指定所需的数据，减少过度获取和不足获取。

2. **Flexibility 灵活性**
    - **REST**: Requires multiple requests to different endpoints to gather related data.
    - **GraphQL**: Allows clients to request exactly what they need in a single request.

    - **REST**: 需要对不同端点进行多次请求以收集相关数据。
    - **GraphQL**: 允许客户端在单个请求中请求所需的确切数据。

3. **Development Speed 开发速度**
    - **REST**: Slower iteration as changes to the API often require updates to multiple endpoints.
    - **GraphQL**: Faster iteration as changes can be made by simply modifying the schema.

    - **REST**: 迭代速度较慢，因为对 API 的更改通常需要更新多个端点。
    - **GraphQL**: 迭代速度较快，因为可以通过简单修改模式进行更改。

#### Node.js Code Example 示例 Node.js 代码

##### REST API Example REST API 示例

```javascript
const express = require('express');
const app = express();
const port = 3000;

let users = {
  '123': { id: '123', name: 'Bob', gender: 'male' }
};

let orders = {
  '456': { id: '456', product: 'abc', quantity: 2, price: 100 }
};

app.get('/users/:id', (req, res) => {
  const user = users[req.params.id];
  res.json(user);
});

app.get('/orders/:id', (req, res) => {
  const order = orders[req.params.id];
  res.json(order);
});

app.listen(port, () => {
  console.log(`REST API listening at http://localhost:${port}`);
});
```

##### GraphQL API Example GraphQL API 示例

```javascript
const { ApolloServer, gql } = require('apollo-server');
const port = 3000;

const typeDefs = gql`
  type User {
    id: ID!
    name: String
    gender: String
    orders: [Order]
  }

  type Order {
    id: ID!
    product: String
    quantity: Int
    price: Float
  }

  type Query {
    user(id: ID!): User
    order(id: ID!): Order
  }
`;

const users = {
  '123': { id: '123', name: 'Bob', gender: 'male' }
};

const orders = {
  '456': { id: '456', product: 'abc', quantity: 2, price: 100 }
};

const resolvers = {
  Query: {
    user: (_, { id }) => {
      const user = users[id];
      user.orders = [orders['456']]; // Sample order data
      return user;
    },
    order: (_, { id }) => orders[id]
  }
};

const server = new ApolloServer({ typeDefs, resolvers });

server.listen({ port }).then(({ url }) => {
  console.log(`GraphQL API ready at ${url}`);
});
```

### Markdown Comparison Table 比较表

| Feature 功能          | REST                           | GraphQL                       |
|-----------------------|--------------------------------|-------------------------------|
| **Endpoints 端点**    | Multiple endpoints 多个端点   | Single endpoint 单个端点      |
| **Data Fetching 数据获取** | Over-fetching or under-fetching 过度获取或不足获取 | Exact data needs 精确的数据需求 |
| **Flexibility 灵活性** | Less flexible 不太灵活        | Highly flexible 非常灵活        |
| **Development Speed 开发速度** | Slower iteration 迭代较慢    | Faster iteration 迭代较快       |

Both REST and GraphQL have their strengths and weaknesses. Choosing between them depends on the specific requirements and constraints of your project.

REST 和 GraphQL 都有各自的优缺点。选择它们之间取决于项目的具体需求和约束。
