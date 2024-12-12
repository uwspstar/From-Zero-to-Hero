### **Integrating SSRS REST API**

Using the SSRS REST API, you can dynamically fetch report data in your Blazor app and render it flexibly. Below are beginner-friendly detailed steps to implement this solution.

---

#### **1. What is SSRS REST API?**

The SSRS REST API is a set of HTTP interfaces provided by SQL Server Reporting Services (SSRS) that allows you to:
- List reports or resources
- Execute reports and fetch results
- Export reports in various formats (PDF, HTML, Excel, etc.)

**Advantages**:
- Supports dynamic data fetching, suitable for modern applications.
- Provides multiple formats, making it highly adaptable.

---

#### **2. Preparations**

**Steps**:
1. Ensure you are using **SSRS 2017 or later**, as the REST API is only available in these versions.
2. Verify that your SSRS server is configured correctly and that the **Web Service URL** is enabled.
   - Open **Reporting Services Configuration Manager**.
   - Check if the "Web Service URL" is accessible through a browser, e.g., `http://your-ssrs-server/reports/api/v1.0/`.

3. Create and publish an SSRS report to the server.
   - Use SQL Server Data Tools (SSDT) to design and deploy your report.
   - Assume the report path is `/YourFolder/YourReportName.rdl`.

4. Confirm the authentication mechanism used by SSRS:
   - If SSRS uses **Windows Authentication**, handle credentials in your code.
   - If using **custom authentication**, provide a token.

---

#### **3. Implementing SSRS REST API in a Blazor App**

**Step 1: Install `HttpClient`**

Blazor already includes `HttpClient`. Ensure it is injected and configured in your project.

**Step 2: Create a Service Class**

Add a service class in your Blazor project to call the SSRS REST API.

**Code Example**:
```csharp
using System.Net.Http.Headers;

public class SSRSService
{
    private readonly HttpClient _httpClient;

    public SSRSService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Fetch report as PDF
    public async Task<byte[]> GetReportAsPdfAsync(string reportPath)
    {
        var baseUrl = "http://your-ssrs-server/reports/api/v1.0";
        var requestUrl = $"{baseUrl}/Reports({reportPath})/Export?format=PDF";

        // Set authentication
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
                System.Text.Encoding.ASCII.GetBytes("username:password")));

        var response = await _httpClient.GetAsync(requestUrl);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsByteArrayAsync();
        }

        throw new Exception($"Failed to fetch report. Status code: {response.StatusCode}");
    }
}
```

**Step 3: Register the Service**

Register the service in `Program.cs`:
```csharp
builder.Services.AddScoped<SSRSService>();
```

**Step 4: Use the Service in a Page**

Create a page, e.g., `FetchReport.razor`:
```razor
@inject SSRSService SSRSService
@inject NavigationManager Navigation

<h3>Fetch Report</h3>
<button @onclick="DownloadReport">Download PDF Report</button>

@code {
    private async Task DownloadReport()
    {
        try
        {
            var reportPath = "/YourFolder/YourReportName";
            var pdfBytes = await SSRSService.GetReportAsPdfAsync(reportPath);

            // Convert the PDF to a downloadable URL
            var base64 = Convert.ToBase64String(pdfBytes);
            var blobUrl = $"data:application/pdf;base64,{base64}";
            Navigation.NavigateTo(blobUrl, forceLoad: true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to fetch report: {ex.Message}");
        }
    }
}
```

---

#### **4. Core REST API Details**

1. **Fetch Report List**:
   - URL: `http://your-ssrs-server/reports/api/v1.0/Folders('YourFolder')/Reports`
   - Method: GET
   - Returns: JSON format containing report details

2. **Export Report**:
   - URL: `http://your-ssrs-server/reports/api/v1.0/Reports('YourReportPath')/Export`
   - Method: GET
   - Parameters:
     - `format`: Export format (e.g., `PDF`, `HTML`)

3. **Response Details**:
   - For `PDF` requests, the response content is a binary file.
   - For `HTML` requests, the response content is HTML, suitable for embedding in your app.

---

#### **5. Key Considerations for REST API Usage**

1. **Authentication**:
   - Ensure your app has permissions to access the SSRS server.
   - For Windows Authentication, use `HttpClientHandler` to pass credentials:
     ```csharp
     var handler = new HttpClientHandler
     {
         Credentials = new NetworkCredential("username", "password", "domain")
     };
     var httpClient = new HttpClient(handler);
     ```

2. **Report Path**:
   - The report path must match the deployed path on the SSRS server, e.g., `/YourFolder/YourReportName`.

3. **Report Formats**:
   - Confirm that the requested format is supported by your application.

---

#### **6. Common Mistakes for Beginners**

1. **CORS Issues**:
   - If calling the SSRS API directly from the frontend, you might encounter CORS issues. Use a backend proxy to handle the requests.

2. **Incorrect Paths**:
   - Ensure the report path matches the SSRS deployment structure (e.g., `/FolderName/ReportName`).

3. **Authentication Problems**:
   - Verify the authentication mechanism used by your SSRS server and configure your application accordingly.

---

#### **Summary**

By following the steps above, you can successfully integrate the SSRS REST API into your Blazor app to display dynamic reports. The service class (`SSRSService`) fetches and processes reports, while the page (`FetchReport.razor`) demonstrates how to use the service and display the report to the user. If you encounter issues or need further clarification, feel free to ask!
