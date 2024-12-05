### Determine how the BI Report system authenticates users:

- Cookie-Based Authentication: A session cookie (JSESSIONID or similar) is set after login.
- Form-Based Login: Users submit a form with username and password to authenticate.
- SSO/OAuth: The system uses Single Sign-On (SSO) or an OAuth provider.


If both URLs (`https://bireport` and `https://bireport/logon.jsp`) show the same login page, it indicates that the BI Report system requires authentication to access any resource, and unauthenticated users are redirected to the login page.

To resolve this issue and authenticate with the BI Report system, follow these steps:

---

### **Steps to Handle the Login Page**

#### **1. Understand the Authentication Mechanism**
1. **Login Form Submission**:
   - The system likely uses a form on the login page (`logon.jsp`) to collect credentials (`username`, `password`, etc.).
   - Submitting the form creates a session and issues a session cookie (e.g., `JSESSIONID`).

2. **Session Cookie**:
   - Once authenticated, the BI Report server sets a cookie in the browser (e.g., `JSESSIONID`), which is used for subsequent requests.

---

#### **2. Simulate Login in the Proxy**

You need to programmatically log in to the BI Report server and reuse the session in subsequent requests.

##### **Step 2.1: Inspect the Login Form**
1. Open `https://bireport/logon.jsp` in your browser.
2. Use Developer Tools (F12) → **Network Tab**:
   - Submit the login form with valid credentials.
   - Capture the request details:
     - URL (e.g., `https://bireport/login`).
     - HTTP method (`POST` or `GET`).
     - Form fields (`username`, `password`, etc.).
     - Headers and cookies sent.

##### **Step 2.2: Implement Login in the Proxy**
1. Use the captured details to log in via the proxy:
   ```csharp
   [ApiController]
   [Route("api/[controller]")]
   public class ProxyController : ControllerBase
   {
       private readonly HttpClient _httpClient;

       public ProxyController(HttpClient httpClient)
       {
           _httpClient = httpClient;
       }

       [HttpGet("fetch")]
       public async Task<IActionResult> Fetch([FromQuery] string url)
       {
           if (string.IsNullOrEmpty(url))
               return BadRequest("URL is required.");

           try
           {
               // Step 1: Log in to BI Report
               var loginData = new Dictionary<string, string>
               {
                   { "username", "your-username" },
                   { "password", "your-password" }
               };

               var loginResponse = await _httpClient.PostAsync(
                   "https://bireport-dev/BOE/BI/login",
                   new FormUrlEncodedContent(loginData)
               );
               loginResponse.EnsureSuccessStatusCode();

               // Step 2: Fetch content with authenticated session
               var response = await _httpClient.GetAsync(url);
               response.EnsureSuccessStatusCode();

               // Step 3: Rewrite relative URLs if needed
               var content = await response.Content.ReadAsStringAsync();
               content = content.Replace("src=\"/BOE/", "src=\"https://bireport-dev/BOE/");
               content = content.Replace("href=\"/BOE/", "href=\"https://bireport-dev/BOE/");

               return Content(content, response.Content.Headers.ContentType?.ToString());
           }
           catch (HttpRequestException ex)
           {
               return BadRequest($"Error fetching the URL: {ex.Message}");
           }
       }
   }
   ```

---

#### **3. Verify Authentication**

1. Test the proxy with your credentials:
   ```plaintext
   http://localhost:5058/api/proxy/fetch?url=https://bireport-dev/BOE/BI
   ```
2. If the content still redirects to `logon.jsp`:
   - Check if the login response contains cookies (`JSESSIONID`) and verify if they are reused in subsequent requests.
   - Update the proxy to manually add cookies if needed.

---

#### **4. Reuse Session Cookies**
Once logged in, ensure the session cookie is passed with every request. Add cookie handling to the `ProxyController`:

```csharp
var handler = new HttpClientHandler
{
    UseCookies = true,
    CookieContainer = new CookieContainer()
};
handler.CookieContainer.Add(new Uri("https://bireport-dev"), new Cookie("JSESSIONID", "your-session-id"));

var client = new HttpClient(handler);
```

---

#### **5. Debugging Tips**
- **Inspect Response Headers**:
   Check for headers like `Set-Cookie` to confirm session cookies are being issued.
   ```csharp
   foreach (var header in response.Headers)
   {
       Console.WriteLine($"{header.Key}: {string.Join(",", header.Value)}");
   }
   ```

- **Enable Redirects**:
   Allow `HttpClient` to handle redirects:
   ```csharp
   var handler = new HttpClientHandler
   {
       AllowAutoRedirect = true
   };
   ```

---

### **Advanced: Handle SSO or OAuth**
If the BI Report uses SSO (Single Sign-On) or OAuth:
1. Integrate with the identity provider (e.g., Active Directory or an OAuth provider).
2. Retrieve the access token or SSO cookie.
3. Attach it to requests:
   ```csharp
   _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "your-access-token");
   ```

---

### **Summary of Steps**

| **Step**                       | **Action**                                                                 |
|--------------------------------|-----------------------------------------------------------------------------|
| Inspect login mechanism        | Identify the login endpoint and required credentials or tokens.            |
| Simulate login                 | Programmatically log in using the proxy and capture session cookies.       |
| Reuse session cookies          | Forward session cookies (e.g., `JSESSIONID`) with all subsequent requests. |
| Debug with network logs        | Verify redirections, cookies, and headers using browser DevTools.          |
| Advanced authentication        | If using SSO/OAuth, integrate with the appropriate provider.               |

With these steps, your reverse proxy should authenticate successfully and fetch the desired BI Report content. Let me know if you need further assistance!
