### Top 9 HTTP Request Methods 九大HTTP请求方法

#### 1. GET
- **Purpose (目的):** Retrieve a single item or a list of items.
- **Example Request (请求示例):**
  ```plaintext
  GET /v1/products/iphone
  ```
- **Example Response (响应示例):**
  ```html
  <HTML>
    <HEAD><title>iPhone</title></HEAD>
    <BODY>This is an iPhone 14</BODY>
  </HTML>
  ```

#### 2. PUT
- **Purpose (目的):** Update an item.
- **Example Request (请求示例):**
  ```plaintext
  PUT /v1/users/123
  ```
  ```json
  {
    "name": "bob",
    "email": "bob@bytebytego.com"
  }
  ```
- **Example Response (响应示例):**
  ```plaintext
  HTTP/1.1 200 OK
  ```

#### 3. POST
- **Purpose (目的):** Create an item.
- **Example Request (请求示例):**
  ```plaintext
  POST /v1/users
  ```
  ```json
  {
    "firstname": "bob",
    "lastname": "smith"
  }
  ```
- **Example Response (响应示例):**
  ```plaintext
  HTTP/1.1 201 Created
  ```

#### 4. DELETE
- **Purpose (目的):** Delete an item.
- **Example Request (请求示例):**
  ```plaintext
  DELETE /v1/users/123
  ```
- **Example Response (响应示例):**
  ```plaintext
  HTTP/1.1 200 OK
  ```

#### 5. PATCH
- **Purpose (目的):** Partially modify an item.
- **Example Request (请求示例):**
  ```plaintext
  PATCH /v1/users/123
  ```
  ```json
  {
    "email": "bob@bytebytego.com"
  }
  ```
- **Example Response (响应示例):**
  ```plaintext
  HTTP/1.1 200 OK
  ```

#### 6. HEAD
- **Purpose (目的):** Same as GET but without the response body.
- **Example Request (请求示例):**
  ```plaintext
  HEAD /v1/products/iphone
  ```
- **Example Response (响应示例):**
  ```plaintext
  HTTP/1.1 200 OK
  ```

#### 7. CONNECT
- **Purpose (目的):** Establish a tunnel to the server identified by the target resource.
- **Example Request (请求示例):**
  ```plaintext
  CONNECT example.com:80
  ```
- **Example Response (响应示例):**
  ```plaintext
  HTTP/1.1 200 OK
  ```

#### 8. OPTIONS
- **Purpose (目的):** Describe the communication options for the target resource.
- **Example Request (请求示例):**
  ```plaintext
  OPTIONS /v1/users
  ```
- **Example Response (响应示例):**
  ```plaintext
  HTTP/1.1 200 OK
  Allow: GET, POST, DELETE, HEAD
  ```

#### 9. TRACE
- **Purpose (目的):** Perform a message loop-back test along the path to the target resource.
- **Example Request (请求示例):**
  ```plaintext
  TRACE /index.html
  ```
- **Example Response (响应示例):**
  ```plaintext
  HTTP/1.1 200 OK
  ```

### Node.js Code Example for Handling HTTP Requests
### 处理HTTP请求的Node.js代码示例

```javascript
const express = require('express');
const app = express();
app.use(express.json());

// GET
app.get('/v1/products/iphone', (req, res) => {
    res.send('<HTML><HEAD><title>iPhone</title></HEAD><BODY>This is an iPhone 14</BODY></HTML>');
});

// PUT
app.put('/v1/users/:id', (req, res) => {
    const { id } = req.params;
    const user = req.body;
    // Update user logic here
    res.status(200).send('User updated');
});

// POST
app.post('/v1/users', (req, res) => {
    const user = req.body;
    // Create user logic here
    res.status(201).send('User created');
});

// DELETE
app.delete('/v1/users/:id', (req, res) => {
    const { id } = req.params;
    // Delete user logic here
    res.status(200).send('User deleted');
});

// PATCH
app.patch('/v1/users/:id', (req, res) => {
    const { id } = req.params;
    const updates = req.body;
    // Update user logic here
    res.status(200).send('User partially updated');
});

// HEAD
app.head('/v1/products/iphone', (req, res) => {
    res.status(200).end();
});

// CONNECT
app.use('/connect', (req, res) => {
    res.status(200).send('Tunnel established');
});

// OPTIONS
app.options('/v1/users', (req, res) => {
    res.set('Allow', 'GET, POST, DELETE, HEAD').status(200).end();
});

// TRACE
app.trace('/index.html', (req, res) => {
    res.status(200).send('Message received');
});

const PORT = 3000;
app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
```

This code provides a simple implementation of the common HTTP request methods using Express.js.

该代码使用 Express.js 提供了常见HTTP请求方法的简单实现。
