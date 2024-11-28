### Designing effective and safe APIs

| **Design Principle**              | **Incorrect Usage**                  | **Correct Usage**                              | **Description**                                                                                       |
|------------------------------------|--------------------------------------|-----------------------------------------------|-------------------------------------------------------------------------------------------------------|
| **Use resource names (nouns)**    | `GET /querycarts/123`                | `GET /carts/123`                              | Use nouns to represent resources in URLs instead of actions or queries.                              |
| **Use plurals**                   | `GET /cart/123`                      | `GET /carts/123`                              | Use plural names for collections to maintain consistency.                                            |
| **Idempotency**                   | `POST /carts`                        | `POST /carts {requestId: 4321}`               | Ensure `POST` requests are idempotent by adding a unique identifier like `requestId`.                |
| **Use versioning**                | `GET /carts/v1/123`                  | `GET /v1/carts/123`                           | Implement versioning in the URL to manage API lifecycle changes effectively.                         |
| **Query after soft deletion**     | `GET /carts`                         | `GET /carts?includeDeleted=true`              | Provide query parameters like `includeDeleted` to fetch soft-deleted records.                        |
| **Pagination**                    | `GET /carts`                         | `GET /carts?pageSize=xx&pageToken=xx`         | Implement pagination to handle large data sets using parameters like `pageSize` and `pageToken`.     |
| **Sorting**                       | `GET /items`                         | `GET /items?sort_by=time`                     | Enable sorting using query parameters like `sort_by`.                                                |
| **Filtering**                     | `GET /items`                         | `GET /items?filter=color:red`                 | Allow filtering using query parameters such as `filter`.                                             |
| **Secure Access**                 | `X-API-KEY=xxx`                      | `X-API-KEY=xxx`, `X-EXPIRY=xxx`, `X-REQUEST-SIGNATURE=xxx` | Enhance security by signing requests with HMAC, including keys, expiry, and a signature.            |
| **Resource cross-reference**      | `GET /carts/123?item=321`            | `GET /carts/123/items/321`                    | Use hierarchical paths to reference resources instead of query parameters.                           |
| **Add an item to a cart**         | `POST /carts/123?addItem=321`        | `POST /carts/123/items:add {itemId: "items/321"}` | Use clear and consistent actions for sub-resources when adding items to a cart.                     |
| **Rate limit**                    | `No rate limit - DDoS`               | Design rate-limiting rules based on `IP`, `user`, `action group`, etc. | Implement rate limiting to protect against abuse and ensure API stability.                           |

---

### **Explanation**
- This table demonstrates best practices for designing effective and safe APIs.
- It contrasts **incorrect** and **correct** usage with detailed examples and descriptions to ensure APIs are consistent, secure, and user-friendly.
- Follow these guidelines to build scalable and robust APIs.
