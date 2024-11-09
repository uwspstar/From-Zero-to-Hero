### C# using NUnit

Here’s a quick guide on setting up a unit test in C# using **NUnit**, a popular testing framework with a rich set of features for writing, managing, and executing tests.

### Step 1: Set Up Your Test Project
1. **Open your C# project** in Visual Studio.
2. **Add a new NUnit Test Project**:
   - Right-click on your solution in Solution Explorer.
   - Select **Add** > **New Project**.
   - Choose **NUnit Test Project** (or create a regular Class Library and add the NUnit NuGet package).
   - Name the project something like `MyProject.Tests`.

3. **Add a reference to your main project**:
   - Right-click on the test project.
   - Select **Add** > **Project Reference**.
   - Select your main project to include it in the test project.

4. **Install the NUnit NuGet package**:
   - Right-click on the test project, choose **Manage NuGet Packages…**.
   - Search for `NUnit` and install it (and `NUnit3TestAdapter` to integrate with Visual Studio’s test runner).

### Step 2: Write a Class to Test
Let’s assume we have a simple `Calculator` class in our main project:

```csharp
namespace MyProject
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }
    }
}
```

### Step 3: Create NUnit Unit Tests for the Calculator Class
In your test project, create a test file named `CalculatorTests.cs` and write NUnit tests for the `Calculator` class.

```csharp
using NUnit.Framework;
using MyProject;

namespace MyProject.Tests
{
    [TestFixture] // Marks this class as containing NUnit tests
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp] // Runs before each test
        public void SetUp()
        {
            _calculator = new Calculator();
        }

        [Test] // Marks this as a test method
        public void Add_ShouldReturnSum_WhenGivenTwoIntegers()
        {
            // Arrange
            int a = 5;
            int b = 3;

            // Act
            int result = _calculator.Add(a, b);

            // Assert
            Assert.AreEqual(8, result);
        }

        [Test]
        public void Subtract_ShouldReturnDifference_WhenGivenTwoIntegers()
        {
            // Arrange
            int a = 5;
            int b = 3;

            // Act
            int result = _calculator.Subtract(a, b);

            // Assert
            Assert.AreEqual(2, result);
        }
    }
}
```

### Explanation of Key Parts
- **[TestFixture]**: This attribute tells NUnit that this class contains tests.
- **[SetUp]**: This method is run before each test to set up any necessary test conditions (e.g., initializing the `Calculator` object).
- **[Test]**: This attribute marks methods as individual tests.
- **Assert.AreEqual(expected, actual)**: Verifies that the actual result matches the expected value. NUnit provides many other assertion methods, like `Assert.IsTrue`, `Assert.IsNull`, and `Assert.Throws`.

### Step 4: Run the Tests
1. Open the **Test Explorer** in Visual Studio (View > Test Explorer).
2. Click **Run All** to execute your tests.
3. Test results will appear in the Test Explorer, showing whether each test passed or failed.

### Using Parameterized Tests with NUnit
NUnit supports parameterized tests using the `[TestCase]` attribute, which makes it easy to test multiple cases without writing separate methods.

```csharp
[TestFixture]
public class CalculatorTests
{
    private Calculator _calculator;

    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
    }

    [TestCase(3, 5, 8)]
    [TestCase(2, 4, 6)]
    [TestCase(0, 0, 0)]
    public void Add_ShouldReturnSum_WhenGivenTwoIntegers(int a, int b, int expected)
    {
        int result = _calculator.Add(a, b);
        Assert.AreEqual(expected, result);
    }
}
```

Each `[TestCase]` provides different inputs to the `Add` method, testing it with multiple values.

### Summary
This example demonstrates the basics of using NUnit for unit testing in C#. NUnit’s **attributes** (`[Test]`, `[SetUp]`, `[TestCase]`, etc.) and **assertions** make it flexible and powerful for verifying that your code behaves as expected. NUnit also integrates well with CI/CD tools and Visual Studio, making it a reliable choice for unit testing in .NET projects.
