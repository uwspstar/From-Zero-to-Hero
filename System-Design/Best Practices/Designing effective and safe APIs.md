### **Recommended Best Practices for API Design**

Here is the improved table based on best practices and refined examples:

| **Design Principle**              | **Incorrect Usage**                  | **Recommended Best Practice**                  | **Reason/Description**                                                                                       |
|------------------------------------|--------------------------------------|-----------------------------------------------|-------------------------------------------------------------------------------------------------------------|
| **Use resource names (nouns)**    | `GET /getCartDetails/123`            | `GET /carts/123`                              | Use nouns to represent resources, avoid verbs in API paths to ensure clarity and consistency.               |
| **Use plurals**                   | `GET /cart/123`                      | `GET /carts/123`                              | Use plural names for collections to maintain consistency across APIs.                                       |
| **Idempotency for POST**          | `POST /carts`                        | `POST /carts {requestId: "1234"}`             | Add unique identifiers (e.g., `requestId`) for idempotent POST requests to prevent duplicate operations.    |
| **Versioning**                    | `GET /carts/v1/123`                  | `GET /v1/carts/123`                           | Use versioning in the URL path (e.g., `/v1`) for managing API evolution and breaking changes effectively.    |
| **Query after soft deletion**     | `GET /carts`                         | `GET /carts?includeDeleted=true`              | Provide query parameters like `includeDeleted` to fetch soft-deleted records explicitly.                    |
| **Pagination**                    | `GET /items`                         | `GET /items?page=1&limit=20`                  | Implement pagination using query parameters like `page` and `limit` to handle large datasets efficiently.   |
| **Sorting**                       | `GET /items`                         | `GET /items?sort=createdDate,desc`            | Use query parameters (e.g., `sort=field,order`) for flexible sorting capabilities.                          |
| **Filtering**                     | `GET /items`                         | `GET /items?status=active&category=books`     | Enable filtering using query parameters to retrieve specific subsets of data based on client needs.         |
| **Secure Access**                 | `X-API-KEY=abc123`                   | `X-API-KEY=abc123`, `X-SIGNATURE=generatedHMAC`, `X-EXPIRY=2024-12-31T23:59:59Z` | Secure APIs with HMAC-based signatures, API keys, and expiration to prevent tampering and unauthorized use. |
| **Resource cross-reference**      | `GET /cart/123?item=456`             | `GET /carts/123/items/456`                    | Use hierarchical paths to show relationships between resources instead of query parameters.                 |
| **Add an item to a cart**         | `POST /carts/123?addItem=456`        | `POST /carts/123/items { "itemId": "456" }`   | Use sub-resources for logical operations like adding an item to a cart for better readability and RESTful.  |
| **Rate limiting**                 | No rate limiting                     | Apply rate limits per `IP`, `user`, or `action`. Example: 100 requests per minute per user. | Protect the API from abuse (e.g., DDoS attacks) by implementing rate limits at user or IP level.            |

---

### **Detailed Explanation of Best Practices**

1. **Use resource names (nouns)**:
   - APIs should represent entities or objects in the system, like `/carts`, `/users`, or `/orders`.
   - Avoid using verbs (e.g., `/getCartDetails`) as actions are implied by HTTP methods (GET, POST, etc.).

2. **Use plurals for collections**:
   - Consistent naming improves readability. For example, `/carts` clearly represents a collection, whereas `/cart` could confuse users.

3. **Idempotency for POST**:
   - Adding `requestId` ensures that if the same request is retried, it will not create duplicate entries.

4. **Versioning**:
   - Always include a version in the URL (e.g., `/v1/`). This approach ensures backward compatibility and easy adoption of new features.

5. **Soft deletion query**:
   - Explicitly allow clients to include soft-deleted data in responses by adding parameters like `includeDeleted`.

6. **Pagination**:
   - Use query parameters `page` and `limit` to fetch large datasets in smaller chunks, improving API performance.

7. **Sorting**:
   - Allow users to sort responses by specific fields in ascending or descending order (e.g., `sort=createdDate,desc`).

8. **Filtering**:
   - Use query parameters to filter data (e.g., `status=active`). This approach minimizes over-fetching and optimizes performance.

9. **Secure Access**:
   - Use signatures, expiration timestamps, and secure API keys to prevent unauthorized access and request tampering.
   - Example: Use HMAC (hashed message authentication code) for request verification.

10. **Resource cross-reference**:
    - Use hierarchical paths (e.g., `/carts/123/items/456`) to show relationships between resources rather than passing IDs in query parameters.

11. **Add an item to a cart**:
    - Use logical sub-resources (e.g., `/items`) in POST requests instead of embedding actions (e.g., `addItem`) in the URL.

12. **Rate limiting**:
    - Implement limits based on IP, user, or action type to ensure fair usage and protect against abuse.

---

### **Conclusion**

These best practices ensure that APIs are consistent, user-friendly, and scalable. Following these principles makes APIs easier to maintain and improves developer and consumer experience.
