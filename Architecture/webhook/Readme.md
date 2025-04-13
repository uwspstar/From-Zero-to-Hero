

```mermaid
sequenceDiagram
    autonumber
    participant Client App
    participant Service Provider

    Client App->>Service Provider: 1. Register webhook URL
    Service Provider->>Client App: 2. Initial GET with challenge (webhook verification)
    Client App->>Service Provider: 3. Return challenge token
    Service Provider->>Client App: 4. Confirm registration

    Note over Service Provider: Event occurs
    Service Provider->>Client App: 5. POST webhook payload
    Client App->>Service Provider: 6. HTTP 200 OK

    alt Retry on failure
        Service Provider->>Client App: 7. Retry POST
        Client App->>Service Provider: 8. HTTP 200 OK
    end
```

This diagram:
1. Starts with webhook registration and verification (steps 1-4)
2. Shows the event-triggered payload delivery (steps 5-6)
3. Includes a retry mechanism for failed deliveries (steps 7-8)
4. Uses Mermaid's `autonumber` feature to automatically sequence the steps
5. Maintains the key elements from the original sketch (webhook verification, confirmation, retries) while organizing them in logical order
