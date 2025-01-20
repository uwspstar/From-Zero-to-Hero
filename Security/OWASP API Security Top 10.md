# OWASP API Security Top 10

Below is an overview of the **OWASP API Security Top 10** (2019) risks. Each section includes:

1. **Description** – What the risk is and why it matters.  
2. **Example** – Common real-world scenarios or exploit examples.  
3. **Recommendation** – High-level best practices to mitigate the risk.  
4. **Implementation & Code Example** – Illustrative snippets (mostly in a Node.js/Express style) to help apply secure measures.

---

## 1. Broken Object Level Authorization

**Description**  
APIs often expose endpoints that handle object identifiers (e.g., `/users/:id`). If the server does not properly check user permissions, attackers can manipulate IDs in requests (e.g., changing `123` to `456`) and access or modify data they do not own.

**Example**  
- A user with ID `123` changes a URL from `/api/v1/users/123/orders` to `/api/v1/users/456/orders`, accessing another user’s orders because the API does not verify that `456` belongs to the same authenticated user.

**Recommendation**  
- Enforce strict access control checks at the object level.  
- Validate ownership of the resource before granting access.  
- Use server-side logic (rather than client-provided data) to determine what data a user can access.

**Implementation & Code Example**

```js
// Express.js middleware example for object-level authorization:
app.get('/users/:userId/orders', authenticateToken, async (req, res) => {
  const { userId } = req.params;
  
  // Compare the requested userId with the authenticated user's ID
  if (userId !== req.user.id) {
    return res.status(403).json({ error: 'Forbidden' });
  }

  try {
    const orders = await Order.find({ userId }); // Only fetch the orders for this user
    res.json(orders);
  } catch (err) {
    res.status(500).json({ error: 'Server error' });
  }
});
```

---

## 2. Broken User Authentication

**Description**  
Authentication mechanisms can be misconfigured or insufficient (weak passwords, flawed session tokens, no MFA, etc.), allowing attackers to impersonate legitimate users.

**Example**  
- An API accepts credentials over HTTP without TLS encryption.  
- An API uses unsecure or short-lived tokens that can be easily guessed or brute-forced.

**Recommendation**  
- Use secure password storage (e.g., bcrypt, Argon2).  
- Implement multi-factor authentication (MFA) where possible.  
- Use token-based authentication (JWT, OAuth 2.0), ensuring tokens are securely generated, properly validated, and invalidated on logout or expiration.  
- Enforce strong password policies and lockouts after repeated failures.

**Implementation & Code Example**

```js
// Sample user registration with hashed password (using bcrypt)
const bcrypt = require('bcrypt');

app.post('/register', async (req, res) => {
  const { username, password } = req.body;
  try {
    const saltRounds = 10;
    const hashedPassword = await bcrypt.hash(password, saltRounds);
    const user = new User({ username, password: hashedPassword });
    await user.save();

    res.status(201).json({ msg: 'User created' });
  } catch (err) {
    res.status(500).json({ error: 'Server error' });
  }
});

// Sample JWT issuance
const jwt = require('jsonwebtoken');

app.post('/login', async (req, res) => {
  const { username, password } = req.body;

  const user = await User.findOne({ username });
  if (!user) {
    return res.status(401).json({ error: 'Invalid credentials' });
  }

  const match = await bcrypt.compare(password, user.password);
  if (!match) {
    return res.status(401).json({ error: 'Invalid credentials' });
  }

  // Generate JWT
  const token = jwt.sign({ id: user._id }, process.env.JWT_SECRET, { expiresIn: '1h' });
  res.json({ token });
});
```

---

## 3. Excessive Data Exposure

**Description**  
APIs often return more data than necessary, relying on clients to filter what is displayed. Attackers can intercept the API response and access sensitive fields.

**Example**  
- The API response for a user profile contains internal fields like `passwordHash`, `roles`, `creditCardNumber`, etc., instead of only returning `name` and `email`.

**Recommendation**  
- Only return the data required by the client.  
- Use response filtering or projection on the server side to ensure sensitive attributes are never sent to the client.  
- Avoid passing entire database objects directly.

**Implementation & Code Example**

```js
// Example: Mongoose query with field projection
app.get('/users/me', authenticateToken, async (req, res) => {
  try {
    // Only select the fields needed by the client
    const user = await User.findById(req.user.id).select('username email profilePic');
    if (!user) {
      return res.status(404).json({ error: 'User not found' });
    }
    res.json(user);
  } catch (err) {
    res.status(500).json({ error: 'Server error' });
  }
});
```

---

## 4. Lack of Resources & Rate Limiting

**Description**  
An API that does not enforce limits (e.g., request frequency, payload sizes) can be exploited, leading to denial-of-service (DoS), brute force attempts, or system resource exhaustion.

**Example**  
- Attackers submit a flood of requests (thousands of times per minute) causing the API to crash.  
- Attackers pass very large JSON payloads or file uploads, depleting server memory.

**Recommendation**  
- Implement rate limiting (requests per IP/user) and quotas.  
- Restrict maximum request sizes and timeouts.  
- Carefully handle batch or bulk endpoints.

**Implementation & Code Example**

```js
// Using express-rate-limit for simple rate limiting
const rateLimit = require('express-rate-limit');

const limiter = rateLimit({
  windowMs: 15 * 60 * 1000, // 15 minutes
  max: 100,                // Limit each IP to 100 requests per windowMs
  message: 'Too many requests, please try again later.',
});

app.use('/api/', limiter);
```

---

## 5. Broken Function Level Authorization

**Description**  
Different functions (endpoints) might require different permissions. If role or privilege checks are missing or incorrectly enforced, users with lower privileges can access high-privilege operations.

**Example**  
- A regular user is able to invoke an “admin” endpoint (e.g., `/api/v1/admin/createUser`) because the authorization check is not performed.

**Recommendation**  
- Enforce role-based (RBAC) or attribute-based (ABAC) access controls.  
- Use a centralized authorization mechanism.  
- Clearly separate and label admin vs. user endpoints.

**Implementation & Code Example**

```js
// Simple RBAC middleware
function authorizeRoles(...allowedRoles) {
  return (req, res, next) => {
    const { role } = req.user; // e.g. "admin", "user", etc.
    if (!allowedRoles.includes(role)) {
      return res.status(403).json({ error: 'Forbidden: Insufficient privileges' });
    }
    next();
  };
}

app.post('/admin/createUser', authenticateToken, authorizeRoles('admin'), (req, res) => {
  // Admin-only function
  // ...
  res.json({ message: 'User created' });
});
```

---

## 6. Mass Assignment

**Description**  
Frameworks often allow binding client-provided data directly to data models (e.g., in a single statement). If not properly filtered, attackers can modify sensitive fields (e.g., `isAdmin`).

**Example**  
- A user registers with JSON that includes `"isAdmin": true` and is automatically granted admin privileges due to direct mapping.

**Recommendation**  
- Do not bind request bodies directly to data models without whitelisting or blacklisting fields.  
- Use DTOs (Data Transfer Objects) or manual mapping to control which fields can be updated.

**Implementation & Code Example**

```js
// Avoid direct assignment from req.body to the User model
app.put('/users/:id', authenticateToken, async (req, res) => {
  const allowedUpdates = ['email', 'profilePic', 'name']; // only these can be updated
  const updates = {};
  
  allowedUpdates.forEach((field) => {
    if (req.body.hasOwnProperty(field)) {
      updates[field] = req.body[field];
    }
  });

  try {
    const user = await User.findByIdAndUpdate(req.params.id, updates, { new: true });
    res.json(user);
  } catch (err) {
    res.status(500).json({ error: 'Server error' });
  }
});
```

---

## 7. Security Misconfiguration

**Description**  
APIs or infrastructure may be misconfigured due to insecure default settings, incomplete configurations, or open cloud storage. Attackers exploit these misconfigurations to gain unauthorized access or data leakage.

**Example**  
- Leaving default admin credentials for database or server.  
- Using default or overly permissive CORS settings (e.g., `Access-Control-Allow-Origin: *`).  
- Exposing environment variables or debug endpoints in production.

**Recommendation**  
- Follow secure configuration guidelines for servers, frameworks, containers, etc.  
- Regularly patch and update software.  
- Disable unnecessary features, endpoints, or services.  
- Implement a secure CORS policy.

**Implementation & Code Example**

```js
// Example: Secure CORS configuration using the cors package
const cors = require('cors');

// Only allow requests from a trusted domain
app.use(cors({
  origin: ['https://example.com'],
  methods: ['GET', 'POST', 'PUT', 'DELETE'],
  allowedHeaders: ['Content-Type', 'Authorization'],
  credentials: true
}));
```

---

## 8. Injection

**Description**  
Injection flaws occur when untrusted data is sent to an interpreter (SQL, NoSQL, LDAP, OS commands). Improper validation or escaping can lead to data theft, corruption, or server compromise.

**Example**  
- SQL injection via `?id=1 OR 1=1` to extract database records.  
- NoSQL injection with MongoDB queries like `{"$gt": ""}`.

**Recommendation**  
- Use parameterized queries or prepared statements.  
- Validate and sanitize all user inputs.  
- Use ORM features that automatically handle escaping (e.g., Mongoose, Sequelize).

**Implementation & Code Example**

```js
// Using parameterized queries in Node.js with MySQL
app.get('/user/:id', (req, res) => {
  const userId = req.params.id;

  // Using '?' placeholders to prevent SQL injection
  const query = 'SELECT * FROM users WHERE id = ?';
  db.query(query, [userId], (err, results) => {
    if (err) return res.status(500).json({ error: 'Database error' });
    res.json(results);
  });
});
```

---

## 9. Improper Assets Management

**Description**  
Organizations often expose outdated API versions or unprotected endpoints meant for testing. Attackers discover and exploit these unmaintained or hidden endpoints.

**Example**  
- An old API version (e.g., `/api/v1/`) is still accessible and lacks updated security patches.  
- A “/debug” endpoint reveals sensitive system information or environment variables.

**Recommendation**  
- Maintain an updated inventory of API endpoints (internal/external, older versions).  
- Decommission and remove outdated API versions.  
- Use proper environment separation (dev, staging, production).  
- Keep documentation (Swagger/OpenAPI) in sync.

**Implementation & Code Example**

```txt
// Good practice: Set up dedicated routes for each API version,
// and actively remove or redirect old versions once deprecated.

// Example structure:
// /api/v2/...   <-- Active
// /api/v1/...   <-- Deprecated, schedule removal
// /api/debug   <-- Should NOT exist in production
```

---

## 10. Insufficient Logging & Monitoring

**Description**  
Without adequate logging and monitoring, suspicious or malicious activities can go undetected. Attackers can probe APIs or exfiltrate data without alerts being triggered.

**Example**  
- No records of failed logins or high-volume requests.  
- No real-time monitoring of unusual errors or spikes in traffic.

**Recommendation**  
- Implement centralized logging of security events (authentication, access control failures, etc.).  
- Monitor logs and set up alerting for anomalies.  
- Retain logs for a sufficient period for forensic analysis.

**Implementation & Code Example**

```js
// Example: Logging middleware using Winston
const winston = require('winston');
const logger = winston.createLogger({
  level: 'info',
  transports: [
    new winston.transports.Console(),
    new winston.transports.File({ filename: 'combined.log' })
  ]
});

app.use((req, res, next) => {
  logger.info(`${req.method} ${req.url} - User: ${req.user ? req.user.id : 'anonymous'}`);
  next();
});

// On errors:
app.use((err, req, res, next) => {
  logger.error(`Error ${err.status || 500} - ${err.message}`);
  res.status(err.status || 500).json({ error: 'Internal Server Error' });
});
```

---

# Summary

The **OWASP API Security Top 10** highlights the most critical risks associated with building and maintaining secure APIs. By combining proper **authentication and authorization checks**, **rate limiting**, **secure configurations**, **input validation**, and **robust logging/monitoring**, organizations can significantly reduce the attack surface of their APIs.

**Key Actions**  
- Regularly audit your APIs for these vulnerabilities.  
- Keep dependencies and frameworks updated.  
- Adopt secure coding practices and shift security testing to earlier stages of development (DevSecOps).  
- Provide continuous training for developers and security teams on emerging API threats.  

Staying aligned with these guidelines—and continually revisiting them—is critical for secure, reliable API deployments.
