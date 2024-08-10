

### Comparison of Session, JWT, Token, SSO, and OAuth 2.0

When you login to a website, your identity needs to be managed. Here is how different solutions work:
| Mechanism | Description (English)                                                                                                                                                             | Description (Chinese)                                                                                                                              |
|-----------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------|
| Session   | The server stores your identity information and assigns a session ID, which is stored in a cookie. This session ID allows the server to track the user's login state across requests. | 服务器存储您的身份信息并分配一个会话ID，该ID存储在cookie中。此会话ID允许服务器跨请求跟踪用户的登录状态。                                                   |
| Token     | Instead of storing session data on the server, the identity is encoded into a token. This token is sent to the client and used for authentication in subsequent requests.              | 服务器不存储会话数据，而是将身份信息编码到一个令牌中。此令牌发送给客户端，并用于后续请求中的身份验证。                                                   |
| JWT       | JSON Web Tokens (JWT) are a compact, URL-safe means of representing claims to be transferred between two parties. They are self-contained and do not require session storage.           | JSON网络令牌（JWT）是一种紧凑、URL安全的方式，用于表示在两方之间传输的声明。它们是自包含的，不需要会话存储。                                            |
| SSO       | Single Sign-On (SSO) allows a user to log in with a single ID to any of several related, yet independent, software systems.                                                           | 单一登录（SSO）允许用户使用单一ID登录到几个相关但独立的软件系统中。                                                                                      |
| OAuth2    | OAuth 2.0 is a protocol that lets external apps request authorization to private details in a user's account without getting their password.                                          | OAuth 2.0是一个协议，允许外部应用程序请求授权以获取用户帐户中的私人详细信息，而无需获取其密码。                                                      |
| QR Code   | A QR Code can encode a random token that, when scanned, authenticates a user for login, allowing quick and secure access without the need for typing a password.                        | QR码可以编码一个随机令牌，扫描后可用于用户登录认证，允许用户快速、安全地访问，无需输入密码。                                                            |

#### Sessions
- **Description**: The server stores your identity and gives the browser a session ID cookie. This allows the server to track login state.
- **Pros**: Simple to implement, widely supported.
- **Cons**: Cookies don't work well across devices.
- **Example**:
  ```javascript
  const session = require('express-session');
  app.use(session({
    secret: 'secret-key',
    resave: false,
    saveUninitialized: true,
    cookie: { secure: true }
  }));
  ```

#### Tokens
- **Description**: Your identity is encoded into a token sent to the browser. The browser sends this token on future requests for authentication.
- **Pros**: No server session storage is required.
- **Cons**: Tokens need encryption/decryption.
- **Example**:
  ```javascript
  const jwt = require('jsonwebtoken');
  const token = jwt.sign({ userId: 'user1' }, 'secret-key', { expiresIn: '1h' });
  ```

#### JWT (JSON Web Tokens)
- **Description**: JWT standardizes identity tokens using digital signatures for trust. The signature is contained in the token so no server session is needed.
- **Pros**: Secure and self-contained.
- **Cons**: Can become large and complex.
- **Example**:
  ```javascript
  const jwt = require('jsonwebtoken');
  const token = jwt.sign({ userId: 'user1' }, 'secret-key', { expiresIn: '1h' });
  ```

#### SSO (Single Sign-On)
- **Description**: SSO uses a central authentication service. This allows a single login to work across multiple sites.
- **Pros**: Convenient for users.
- **Cons**: Complex to implement.
- **Example**:
  ```javascript
  // Example implementation would require setting up a centralized authentication server.
  ```

#### OAuth 2.0
- **Description**: Allows limited access to your data on one site by another site, without giving away passwords.
- **Pros**: Secure and flexible.
- **Cons**: Requires understanding of OAuth flows.
- **Example**:
  ```javascript
  const { AuthorizationCode } = require('simple-oauth2');
  const client = new AuthorizationCode({
    client: {
      id: 'client-id',
      secret: 'client-secret'
    },
    auth: {
      tokenHost: 'https://authorization-server.com'
    }
  });
  ```

#### Comparison Table

| Method     | Pros                                     | Cons                                 | Use Case                               |
|------------|------------------------------------------|--------------------------------------|----------------------------------------|
| Sessions   | Simple, widely supported                 | Doesn't work well across devices     | Web applications                       |
| Tokens     | No server storage required               | Needs encryption/decryption          | Stateless APIs                         |
| JWT        | Secure, self-contained                   | Can be large and complex             | Authentication for APIs                |
| SSO        | Convenient for users                     | Complex to implement                 | Enterprise applications                |
| OAuth 2.0  | Secure, flexible                         | Requires understanding of OAuth flows| Third-party application authorization  |

