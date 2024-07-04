# API Security

### 1. Authentication 🕵️‍♀️ (身份验证)
- **English**: Ensuring users are who they claim to be. 
  - **Example**: Two-factor authentication (2FA) adds an extra layer by requiring a code sent to the user's mobile device.
- **中文**: 确保用户是他们声称的身份。
  - **示例**: 两因素认证（2FA）通过要求发送到用户移动设备的代码增加了一个额外的层次。

### 2. Authorization 🚦 (授权)
- **English**: Defining user permissions post-authentication.
  - **Example**: In a forum, only moderators are allowed to pin or delete threads.
- **中文**: 认证后定义用户权限。
  - **示例**: 在论坛中，只有版主才被允许置顶或删除主题。

### 3. Data Redaction 🖍️ (数据脱敏)
- **English**: Concealing specific data within a dataset.
  - **Example**: In user profiles, personal addresses might be hidden except to privileged personnel.
- **中文**: 在数据集中隐藏特定数据。
  - **示例**: 在用户档案中，个人地址可能被隐藏，除非对特权人员。

### 4. Encryption 🔒 (加密)
- **English**: Transforming data into a secret code to protect it.
  - **Example**: Messaging apps might use end-to-end encryption, ensuring only the sender and receiver can read the content.
- **中文**: 将数据转化为秘密代码以保护它。
  - **示例**: 消息应用程序可能使用端到端加密，确保只有发送者和接收者可以阅读内容。

### 5. Error Handling ❌ (错误处理)
- **English**: Managing unexpected issues in a system without revealing sensitive info.
  - **Example**: Rather than exposing a stack trace, a service might display a generic error message.
- **中文**: 在系统中管理意外问题，而不透露敏感信息。
  - **示例**: 服务可能显示一个通用的错误消息，而不是暴露堆栈跟踪。

### 6. Input Validation & Data Sanitization 🧹 (输入验证与数据清理)
- **English**: Reviewing and purifying user data to maintain safety.
  - **Example**: Removing script tags from user input to prevent XSS attacks.
- **中文**: 检查并净化用户数据以保持安全。
  - **示例**: 从用户输入中删除脚本标签以防止XSS攻击。

### 7. Intrusion Detection Systems 👀 (入侵检测系统)
- **English**: Surveillance systems that detect unauthorized activities.
  - **Example**: An IDS might alert administrators of unusually high traffic from a single IP, signaling a potential DDoS attack.
- **中文**: 检测未经授权的活动的监视系统。
  - **示例**: IDS可能会警告管理员从单个IP的流量异常高，这是DDoS攻击的一个潜在信号。

### 8. IP Whitelisting 📝 (IP白名单)
- **English**: Ensuring only specific, trusted IP addresses have access.
  - **Example**: An admin dashboard may only be accessible from company IP addresses.
- **中文**: 确保只有特定的受信任的IP地址有访问权限。
  - **示例**: 管理员仪表板可能只能从公司的IP地址访问。

### 9. Logging and Monitoring 🖥️ (日志记录与监控)
- **English**: Storing and analyzing system activity to ensure everything's working as it should.
  - **Example**: Logging failed login attempts to detect and prevent potential brute force attacks.
- **中文**: 存储和分析系统活动，以确保一切都按预期工作。
  - **示例**: 记录失败的登录尝试，以检测和防止潜在的暴力攻击。

### 10. Rate Limiting ⏱️ (速率限制)
- **English**: Restricting the number of requests a user can make within a certain time frame.
  - **Example**: An API might allow only 1000 requests per hour from a single user.
- **中文**: 限制用户在一定时间内可以发出的请求数量。
  - **示例**: API可能只允许单个用户每小时发出1000次请求。

### 11. Secure Dependencies 📦 (安全依赖)
- **English**: Ensuring third-party code integrated into your system is free from vulnerabilities.
  - **Example**: Regularly updating libraries to versions that have patched known vulnerabilities.
- **中文**: 确保集成到系统中的第三方代码没有漏洞。
  - **示例**: 定期更新到已修复已知漏洞的库版本。

### 12. Security Headers 📋 (安全头部)
- **English**: Implementing HTTP headers to provide added web security.
  - **Example**: Using the Content-Security-Policy header to prevent XSS attacks.
- **中文**: 实施HTTP头部以提供额外的网络安全。
  - **示例**: 使用Content-Security-Policy头部防止XSS攻击。

### 13. Token Expiry ⏳ (令牌过期)
- **English**: Ensuring authentication tokens are valid only for a specific duration.
  - **Example**: JWT tokens that expire after 1 hour, requiring users to re-authenticate.
- **中文**: 确保认证令牌只在特定的持续时间内有效。
  - **示例**: 在1小时后过期的JWT令牌，要求用户重新验证身份。

### 14. Use of Security Standards and Frameworks 📘 (使用安全标准和框架)
- **English**: Adopting industry-approved guidelines to build and maintain secure systems.
  - **Example**: Following the OWASP Top Ten as a guideline for web application security.
- **中文**: 采用行业批准的指南来构建和维护安全系统。
  - **示例**: 作为网络应用程序安全的指导方针，遵循OWASP十大安全威胁。

### 15. Web Application Firewall 🔥 (网络应用防火墙)
- **English**: A protective layer that sits between a website and the internet, filtering malicious traffic.
  - **Example**: A WAF might block requests containing SQL injection attack patterns.
- **中文**: 位于网站和互联网之间的保护层，过滤恶意流量。
  - **示例**: WAF可能会阻止包含SQL注入攻击模式的请求。

### 16. API Versioning 🔄 (API版本控制)
- **English**: Maintaining different versions of an API, often to introduce new features without breaking old implementations.
  - **Example**: Offering both `v1` and `v2` of an API, where `v2` introduces new endpoints.
- **中文**: 维护API的不同版本，通常是为了在不破坏旧实现的情况下引入新功能。
  - **示例**: 同时提供API的`v1`和`v2`，其中`v2`引入了新的端点。
