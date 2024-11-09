For **Windows Communication Foundation (WCF)** code, unit testing presents unique challenges due to WCF’s reliance on service contracts and networked communication. Here are some recommended practices and tools for effectively unit testing WCF code:

### 1. **Testing Framework Recommendation**
Since WCF is primarily associated with the **.NET Framework** and often involves complex service-oriented designs, I recommend using **NUnit** along with a mocking library (like **Moq** or **Rhino Mocks**) for flexibility in setting up and isolating service dependencies.

- **NUnit**: This framework is ideal for WCF testing because of its mature ecosystem, broad feature set, and compatibility with a wide range of .NET versions, including .NET Framework, which is common for WCF.
- **MSTest**: MSTest is an alternative if you’re working directly within Visual Studio and need integration with built-in tooling. It’s simpler but has fewer testing features.
- **xUnit**: While xUnit is a good modern option, it may require more setup to integrate seamlessly with legacy WCF projects compared to NUnit and MSTest.

**Recommendation**: For most WCF projects, I recommend using **NUnit** combined with a mocking library.

### 2. **Approach to Testing WCF Code**
Testing WCF code usually involves two main parts:
   - **Unit Testing WCF Service Logic**: This tests the internal logic of the service, typically isolating dependencies.
   - **Integration Testing WCF Services**: This validates that the service works end-to-end by deploying and running the WCF service in a test environment.

### 3. **Unit Testing WCF Services with NUnit and Moq**
Below is an example setup for testing a simple WCF service using **NUnit** and **Moq**.

#### Sample WCF Service Code
Let’s assume we have a WCF service for a `Calculator` with a `Add` method:

```csharp
[ServiceContract]
public interface ICalculatorService
{
    [OperationContract]
    int Add(int a, int b);
}

public class CalculatorService : ICalculatorService
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}
```

#### Unit Test for WCF Service Logic
1. Create an interface for any external dependencies, if applicable, to simplify testing.
2. Use a mocking library to mock dependencies.

```csharp
using NUnit.Framework;
using Moq;

namespace MyWcfService.Tests
{
    [TestFixture]
    public class CalculatorServiceTests
    {
        private CalculatorService _calculatorService;

        [SetUp]
        public void SetUp()
        {
            _calculatorService = new CalculatorService();
        }

        [Test]
        public void Add_ShouldReturnSum_WhenGivenTwoIntegers()
        {
            // Arrange
            int a = 5;
            int b = 3;

            // Act
            int result = _calculatorService.Add(a, b);

            // Assert
            Assert.AreEqual(8, result);
        }
    }
}
```

### 4. **Mocking Dependencies in WCF Service Code**
If your WCF service has dependencies, you can inject them via constructor injection. Then, mock these dependencies to isolate your unit test.

```csharp
public class CalculatorService : ICalculatorService
{
    private readonly ILoggingService _loggingService;

    public CalculatorService(ILoggingService loggingService)
    {
        _loggingService = loggingService;
    }

    public int Add(int a, int b)
    {
        _loggingService.Log("Adding numbers");
        return a + b;
    }
}
```

#### Testing with Moq
```csharp
[TestFixture]
public class CalculatorServiceTests
{
    private Mock<ILoggingService> _mockLoggingService;
    private CalculatorService _calculatorService;

    [SetUp]
    public void SetUp()
    {
        _mockLoggingService = new Mock<ILoggingService>();
        _calculatorService = new CalculatorService(_mockLoggingService.Object);
    }

    [Test]
    public void Add_ShouldCallLog_WhenAddingTwoNumbers()
    {
        // Arrange
        int a = 5;
        int b = 3;

        // Act
        _calculatorService.Add(a, b);

        // Assert
        _mockLoggingService.Verify(l => l.Log(It.IsAny<string>()), Times.Once);
    }
}
```

### 5. **Integration Testing with WCF Services**
For integration testing, use a framework like **Microsoft’s Test Server** or **Local Test Server** to spin up the WCF service locally and perform end-to-end tests. Integration tests validate the whole WCF service pipeline.

Here’s a brief example:
1. Host the WCF service using `ServiceHost` in a test setup.
2. Use an `HttpClient` to call the WCF service and verify the expected behavior.

### Summary
For unit testing WCF services, **NUnit with Moq** provides a robust, flexible setup to isolate and validate your service logic. When combined with integration tests, you ensure that your WCF services are reliable and perform as expected.
