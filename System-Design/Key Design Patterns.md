### **Key Design Patterns**

Design patterns are **reusable solutions** to common design challenges in software development. They serve as blueprints that streamline development processes, improve code organization, and promote best practices. Below is a detailed explanation of 18 key design patterns, categorized by their purpose, along with **Mermaid sequence diagrams** to illustrate their workflows.

---

### **1. Abstract Factory: Family Creator**

**Purpose**: Creates families of related objects without specifying their concrete classes.

**How It Works**:
1. A client requests an abstract factory to produce related objects.
2. Concrete factories implement the abstract factory interface to create specific products.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant AbstractFactory as Abstract Factory
    participant ConcreteFactory as Concrete Factory
    participant Product as Product

    Client->>AbstractFactory: Request Object
    AbstractFactory->>ConcreteFactory: Delegate Creation
    ConcreteFactory->>Product: Create Product
    Product-->>Client: Return Object
```

---

### **2. Builder: Lego Master**

**Purpose**: Constructs complex objects step by step, separating the creation process from the object's representation.

**How It Works**:
1. The client requests the director to build an object.
2. The director guides the builder through the construction steps.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant Director as Director
    participant Builder as Builder
    participant Product as Product

    Client->>Director: Request Object
    Director->>Builder: Step 1
    Builder->>Product: Add Part 1
    Director->>Builder: Step 2
    Builder->>Product: Add Part 2
    Builder-->>Client: Return Completed Product
```

---

### **3. Prototype: Clone Maker**

**Purpose**: Creates new objects by cloning existing ones.

**How It Works**:
1. The client clones a prototype object instead of creating a new one.
2. The prototype is copied to produce a new object.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant Prototype as Prototype

    Client->>Prototype: Clone Object
    Prototype-->>Client: Return New Object
```

---

### **4. Singleton: One and Only**

**Purpose**: Ensures a class has only one instance and provides a global access point.

**How It Works**:
1. The client checks if the singleton instance exists.
2. If not, a new instance is created and returned.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant Singleton as Singleton

    Client->>Singleton: Get Instance
    Singleton-->>Client: Return Single Instance
```

---

### **5. Adapter: Universal Plug**

**Purpose**: Converts the interface of one class to another.

**How It Works**:
1. The client uses an adapter to work with an incompatible interface.
2. The adapter translates requests to the target interface.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant Adapter as Adapter
    participant Target as Target

    Client->>Adapter: Request
    Adapter->>Target: Translate Request
    Target-->>Adapter: Response
    Adapter-->>Client: Return Response
```

---

### **6. Bridge: Function Connector**

**Purpose**: Separates an object’s interface from its implementation.

**How It Works**:
1. The client interacts with the abstraction layer.
2. The abstraction delegates work to the implementation.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant Abstraction as Abstraction
    participant Implementation as Implementation

    Client->>Abstraction: Call Function
    Abstraction->>Implementation: Delegate Work
    Implementation-->>Abstraction: Return Result
    Abstraction-->>Client: Return Response
```

---

### **7. Composite: Tree Builder**

**Purpose**: Creates tree-like structures of simple and composite objects.

**How It Works**:
1. The client interacts with objects uniformly, regardless of whether they are simple or composite.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant Composite as Composite
    participant Leaf as Leaf

    Client->>Composite: Perform Operation
    Composite->>Leaf: Delegate to Leaf
    Leaf-->>Composite: Operation Done
    Composite-->>Client: Return Result
```

---

### **8. Decorator: Customizer**

**Purpose**: Dynamically adds features to objects without changing their core implementation.

**How It Works**:
1. The client wraps an object with decorators to add features.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant Decorator as Decorator
    participant Component as Component

    Client->>Decorator: Add Feature
    Decorator->>Component: Perform Operation
    Component-->>Decorator: Result
    Decorator-->>Client: Enhanced Result
```

---

### **9. Facade: One-Stop-Shop**

**Purpose**: Provides a simplified interface to a complex subsystem.

**How It Works**:
1. The client interacts with the facade instead of directly accessing subsystem components.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant Facade as Facade
    participant Subsystem as Subsystem

    Client->>Facade: Request
    Facade->>Subsystem: Simplify Request
    Subsystem-->>Facade: Return Result
    Facade-->>Client: Simplified Response
```

---

### **10. Flyweight: Space Saver**

**Purpose**: Shares small, reusable objects to save memory.

**How It Works**:
1. The client requests a shared flyweight object from a factory.
2. The factory returns an existing object or creates a new one if needed.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant FlyweightFactory as Flyweight Factory
    participant Flyweight as Flyweight

    Client->>FlyweightFactory: Request Object
    FlyweightFactory-->>Client: Return Shared Object
```

---

### **11. Proxy: Stand-In Actor**

**Purpose**: Acts as a placeholder or proxy for another object.

**How It Works**:
1. The client interacts with the proxy, which controls access to the real object.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant Proxy as Proxy
    participant RealObject as Real Object

    Client->>Proxy: Request
    Proxy->>RealObject: Forward Request
    RealObject-->>Proxy: Response
    Proxy-->>Client: Return Response
```

---

### **12. Chain of Responsibility: Request Relay**

**Purpose**: Passes a request along a chain of handlers until it is handled.

**How It Works**:
1. The client sends a request to the first handler in the chain.
2. Each handler processes or forwards the request.

**Mermaid Sequence Diagram**:
```mermaid
sequenceDiagram
    participant Client as Client
    participant Handler1 as Handler 1
    participant Handler2 as Handler 2

    Client->>Handler1: Request
    Handler1->>Handler2: Forward Request (if unhandled)
    Handler2-->>Client: Response
```

---

### **Conclusion**

These 18 design patterns are fundamental tools for building efficient, reusable, and maintainable software systems. By understanding their purposes and workflows, developers can address common design challenges with proven solutions. Each pattern offers a specific way to improve the software structure, whether it's simplifying interactions, optimizing memory usage, or enhancing modularity.
