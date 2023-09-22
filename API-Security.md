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


