**Technical Design Document (TDD)**

---

**Project Name**: Order Management Microservice
**Author**: John Doe
**Date**: 2025-06-26
**Version**: v1.0
**Reviewed By**: Jane Smith (Architect)

---

### 1. Objective

The Order Management Microservice is designed to handle creation, retrieval, update, and tracking of customer orders. It acts as a core part of the e-commerce system, interfacing with Inventory, Payment, and Shipping services.

---

### 2. High-Level Architecture

* **Frontend** → API Gateway → **OrderService (C#)**
* OrderService uses PostgreSQL for persistence.
* Events published to Apache Kafka.
* Deployed via Docker containers to Kubernetes.

---

### 3. Microservice Context

| Item          | Description                                |
| ------------- | ------------------------------------------ |
| Service Name  | OrderService                               |
| Domain        | Order Management                           |
| Communication | REST API, Kafka Events                     |
| Dependencies  | InventoryService, PaymentService           |
| Tech Stack    | .NET 8, EF Core, Kafka, PostgreSQL, Docker |

---

### 4. API Design

#### 4.1 REST Endpoints

**POST /api/orders**
Create a new order

Request Body:

```json
{
  "customerId": "uuid",
  "items": [
    { "productId": "uuid", "quantity": 2 }
  ]
}
```

Response:

```json
{
  "orderId": "uuid",
  "status": "CREATED"
}
```

**GET /api/orders/{id}**
Fetch order details by ID

#### 4.2 Kafka Event

Topic: `order.created`

```json
{
  "orderId": "uuid",
  "timestamp": "2025-06-26T14:45:00Z",
  "status": "CREATED"
}
```

---

### 5. Data Model

**Order**

* OrderId (Guid)
* CustomerId (Guid)
* Status (enum: CREATED, PAID, SHIPPED)
* CreatedAt (DateTime)

**OrderItem**

* OrderItemId (Guid)
* OrderId (FK)
* ProductId (Guid)
* Quantity (int)

---

### 6. Internal Architecture

* **Controller**: OrderController.cs
* **Service Layer**: OrderService.cs
* **Repository**: OrderRepository.cs (EF Core)
* **EventPublisher**: KafkaProducer.cs
* **DTOs**: OrderDto.cs, CreateOrderRequest.cs

---

### 7. Security

* JWT Authentication via IdentityServer
* Role-based authorization (Admin, Customer)
* HTTPS enforced

---

### 8. Port and Access Specification

| Attribute           | Value                                                                                           |
| ------------------- | ----------------------------------------------------------------------------------------------- |
| Service Name        | OrderService                                                                                    |
| Protocol            | HTTPS                                                                                           |
| Application Port    | 5001                                                                                            |
| API Gateway Routing | Enabled (via path-based routing: `/api/orders`)                                                 |
| Internal DNS        | `orderservice.default.svc.cluster.local` (Kubernetes internal)                                  |
| External Access     | Only through API Gateway; no direct public exposure                                             |
| Notes               | Actual IP and NodePort assignments are managed by the Infra/DevOps team in deployment manifests |

---

### 9. Deployment

* Dockerfile for building container
* Kubernetes YAML for deployment
* Secrets managed via Kubernetes Secrets
* CI/CD via GitHub Actions (build, test, push image)

---

### 10. Testing Strategy

| Layer       | Tool                  |
| ----------- | --------------------- |
| Unit        | xUnit                 |
| Integration | Testcontainers-dotnet |
| Contract    | PactNet               |
| Load        | k6 (external script)  |

---

### 11. Error Handling

* 400: Validation errors
* 404: Order not found
* 500: Unexpected server errors
* Kafka retry with exponential backoff + DLQ

---

### 12. Timeline

| Milestone       | Date       |
| --------------- | ---------- |
| Design Sign-off | 2025-06-28 |
| Dev Start       | 2025-07-01 |
| Code Freeze     | 2025-07-20 |
| Release         | 2025-08-01 |

---

### 13. Appendix

* Link to GitHub Repo: `https://github.com/org/orderservice`
* OpenAPI Spec: `openapi.yaml`
* ERD Diagram: `erd.png`
* Kafka Contract JSON: `order.created.schema.json`
