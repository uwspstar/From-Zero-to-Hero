### **HTTP Response Code Categories**

HTTP response codes are divided into **five categories**, each indicating the nature of the response sent by the server to the client. Below is a detailed explanation:

### **Summary Table**

| **Category**       | **Range**  | **Meaning**                                   | **Example Codes**            |
|---------------------|------------|-----------------------------------------------|------------------------------|
| Informational       | 100-199    | The server has received the request and is continuing. | 100 (Continue), 101 (Switching Protocols) |
| Success             | 200-299    | The request was successful.                   | 200 (OK), 201 (Created), 204 (No Content) |
| Redirection         | 300-399    | Further action is required to complete the request. | 301 (Moved Permanently), 304 (Not Modified) |
| Client Error        | 400-499    | The request contains an error from the client side. | 400 (Bad Request), 404 (Not Found)        |
| Server Error        | 500-599    | The server failed to process a valid request. | 500 (Internal Server Error), 503 (Service Unavailable) |


### **Tips for Understanding HTTP Status Codes**
1. **First Digit**: The first digit of the status code indicates the category.
   - 1xx: Informational
   - 2xx: Success
   - 3xx: Redirection
   - 4xx: Client Error
   - 5xx: Server Error
2. **Common Codes**: Familiarize yourself with frequently encountered codes (e.g., 200, 404, 500).
3. **Debugging**: Use response codes to identify the source of issues in client-server interactions.
4. **API Design**: When designing APIs, use appropriate status codes to make your API more intuitive and robust.

---

### **1. Informational Responses (100-199)**
- **Purpose**: These codes indicate that the server has received the request and is continuing the process.
- **Examples**:
  - **100 Continue**: The server has received the request headers and the client should proceed to send the request body.
  - **101 Switching Protocols**: The server is switching to a protocol requested by the client.

---

### **2. Success Responses (200-299)**
- **Purpose**: These codes indicate that the request was successfully received, understood, and processed by the server.
- **Examples**:
  - **200 OK**: The request was successful, and the response contains the requested data.
  - **201 Created**: The request was successful, and a new resource was created.
  - **204 No Content**: The request was successful, but there is no content to send back.

---

### **3. Redirection Responses (300-399)**
- **Purpose**: These codes indicate that further action is needed to complete the request, often involving URL redirection.
- **Examples**:
  - **301 Moved Permanently**: The requested resource has been permanently moved to a new URL.
  - **302 Found**: The requested resource is temporarily located at a different URL.
  - **304 Not Modified**: The resource has not been modified, so the client can use the cached version.

---

### **4. Client Error Responses (400-499)**
- **Purpose**: These codes indicate that there was an error in the client's request.
- **Examples**:
  - **400 Bad Request**: The server cannot process the request due to invalid syntax.
  - **401 Unauthorized**: Authentication is required and has failed or has not been provided.
  - **403 Forbidden**: The client does not have permission to access the resource.
  - **404 Not Found**: The requested resource could not be found on the server.

---

### **5. Server Error Responses (500-599)**
- **Purpose**: These codes indicate that the server encountered an error while trying to process the request.
- **Examples**:
  - **500 Internal Server Error**: The server encountered an unexpected condition.
  - **502 Bad Gateway**: The server received an invalid response from an upstream server.
  - **503 Service Unavailable**: The server is temporarily unable to handle the request due to maintenance or overload.
