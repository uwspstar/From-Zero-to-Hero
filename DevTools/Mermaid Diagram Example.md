### Mermaid Diagram Examples

In Mermaid, you can use various shapes to represent different components or nodes. Here’s a list of the basic shapes available in Mermaid and how to create them:

```mermaid
graph TD
    A[Square] --> B((Circle))
    C[Rectangle] --> D((Round Edges))
    E((Ellipse)) --> F>Asymmetrical Shape]
    G{{Hexagon}} --> H[Trapezoid]
```
```mermaid
graph TD
    I>Rhombus] --> J((Cylinder))
    K[/Parallelogram/] --> L[[Subroutine]]
    M[/Database/] --> N{Diamond}
    O((Cloud))
```

### Explanation of Each Shape

1. **Square**: Defined with `[Label]`. 
    - Example: `A[Square]`

2. **Circle**: Defined with `((Label))`.
    - Example: `B((Circle))`

3. **Rectangle**: Similar to square but often used interchangeably.
    - Example: `C[Rectangle]`

4. **Round Edges (Rounded Rectangle)**: Defined with `([Label])`.
    - Example: `D([Round Edges])`

5. **Ellipse**: Also uses rounded parentheses, creating an elliptical shape.
    - Example: `E((Ellipse))`

6. **Asymmetrical Shape**: Defined with `>Label]` (triangle on one side).
    - Example: `F>Asymmetrical Shape]`

7. **Hexagon**: Defined with `{{Label}}`.
    - Example: `G{{Hexagon}}`

8. **Trapezoid**: Mermaid doesn’t have a direct trapezoid shape, but customizations can be added by creating subgraphs or groups.

9. **Rhombus (Decision)**: Defined with `{Label}` for a diamond shape, often used for decisions.
    - Example: `N{Diamond}`

10. **Cylinder**: Often used for databases or storage.
    - Example: `J((Cylinder))`

11. **Parallelogram**: Created using `[/Label/]` or `[\Label\]`.
    - Example: `K[/Parallelogram/]`

12. **Subroutine**: Represented by double square brackets `[[Label]]`.
    - Example: `L[[Subroutine]]`

13. **Database**: Displayed as a barrel shape.
    - Example: `M[/Database/]`

14. **Cloud**: Simulated by using rounded brackets for a cloud-like shape.
    - Example: `O((Cloud))`

### Usage in Diagrams

Each shape has specific uses and can be used to represent different types of nodes or components in your diagram, enhancing readability and clarity. Use them according to your needs for representing various components in architectural, workflow, or process diagrams.

---

```mermaid
flowchart LR

%% Colors %%
linkStyle default stroke-width:2px
classDef blue fill:#2374f7,stroke:#000,stroke-width:2px,color:#fff
classDef orange fill:#fc822b,stroke:#000,stroke-width:2px,color:#fff
classDef green fill:#16b552,stroke:#000,stroke-width:2px,color:#fff
classDef red fill:#ed2633,stroke:#000,stroke-width:2px,color:#fff
classDef magenta fill:magenta,stroke:#000,stroke-width:2px,color:#fff

%% Goals Database%
%% 0 %%
G[(Goals)]:::blue <===> |Connects To| P[(Projects)]:::blue

%% Projects Database %%
%% Deadline %%
%% 1,2,3,4 %%
P ---o |Has| PD(Deadline):::orange
PD ---x |Is| MT([Met]):::green
PD ---- |Is| OV([Overdue]):::red ---> |Push| FOV{4 Days}:::magenta

%% Tasks %%
%% 5,6,7,8 %%
P ---o |Has| PT(Tasks):::orange
PT ---x |Is| IC([Incomplete]):::red
PT ---- |Is| C([Complete]):::green
C ---> |Needs| R[[Review]]

%% Review %%
%% 9 %%
R -..-> |Creates New| G

%% Link Colors %%
linkStyle 0 stroke:blue
linkStyle 1,5 stroke:orange
linkStyle 2,7 stroke:green
linkStyle 3,6 stroke:red
linkStyle 4 stroke:magenta

%% Clickable Links %%
click P "https://www.notion.so/redgregory/db9274f912e1400a895d51030bc7e680?v=2764569801174438898a704b66599c22"
```

---
Here’s a sample web system architecture diagram using various Mermaid shapes to represent typical components and their relationships in a web system:

```mermaid
graph TD
    CLT[Client] --> LB{{Load Balancer}}
    LB --> WEB[Web Server]
    LB --> CDN((Content Delivery Network))
    WEB --> API[API Gateway]
    API --> AUTH>Authentication Service]
    API --> SRV([Application Server])
    SRV --> DB[(Database)]
    SRV --> CACHE((Cache))
    SRV --> MQ[/Message Queue/]
    SRV --> FILE[[Filesystem]]
    DB --> LOGS((Logging System))
    DB --> MON((Monitoring))
    CDN --> STORAGE((Object Storage))
    EXT[External System] --> API
    AUTH --> SSO{{Single Sign-On}}
```

### Explanation of Each Component

- **Client** (`CLT`): Represented as a basic **Rectangle** to show the client interacting with the system.
- **Load Balancer** (`LB`): A **Hexagon**, as a central distribution point for incoming traffic.
- **Web Server** (`WEB`): Shown as a **Rectangle** to represent the server handling HTTP requests.
- **CDN** (`CDN`): A **Circle** for caching static content close to users.
- **API Gateway** (`API`): A **Rectangle** for routing requests to services.
- **Authentication Service** (`AUTH`): Represented with an **Asymmetrical Shape** to highlight its distinct role in security.
- **Application Server** (`SRV`): A **Rectangle** to represent core application logic.
- **Database** (`DB`): A **Cylinder** as a common symbol for data storage.
- **Cache** (`CACHE`): An **Ellipse** for in-memory caching.
- **Message Queue** (`MQ`): A **Parallelogram**, often used to handle asynchronous tasks.
- **Filesystem** (`FILE`): A **Subroutine** shape, useful for additional storage.
- **Logging System** (`LOGS`): A **Circle** for handling logs.
- **Monitoring System** (`MON`): A **Circle** for tracking system health.
- **Object Storage** (`STORAGE`): Used for large files, depicted as a **Cloud**.
- **External System** (`EXT`): A **Rectangle** to represent integrations with external systems.
- **Single Sign-On (SSO)**: A **Diamond** showing it as an authentication feature connecting to `AUTH`.

### Flow and Connections

- **Client** initiates a connection to the **Load Balancer**.
- The **Load Balancer** distributes traffic to the **Web Server** or **CDN**.
- **Web Server** communicates with the **API Gateway** for routing.
- **API Gateway** routes requests to **Application Server** or **Authentication Service**.
- **Application Server** interacts with the **Database**, **Cache**, **Message Queue**, and **Filesystem**.
- **Database** connects to **Logging** and **Monitoring** systems for tracking and storage management.
- **Object Storage** is linked to **CDN** for efficient content delivery.
- **External System** may interact with the **API Gateway** for integrations.

This layout represents a standard architecture for a web application, covering key components and relationships for scalability, caching, and storage.

---

```mermaid
graph TD
    CLT[(Client)] --> AP[API Gateway]
    AP --> SRV[Server]
    SRV --> DB[(Database)]
    SRV --> CACHE((Cache))
    SRV --> MQ[(Message Queue)]
    EXT[External System] --> LB{Load Balancer}
    LB --> SRV
    AUTH[(Authentication Service)] --> AP
    LOGS((Logging System)) --> MON((Monitoring))
    MON --> SRV
```

---

```mermaid
graph TD
    DB[(Database)]
    AP[API Gateway]
    SRV[Server]
    CLT[(Client)]
    CACHE((Cache))
    MQ[(Message Queue)]
    FILE[Filesystem]
```
```mermaid
graph TD
    EXT[External System]
    LB{Load Balancer}
    AUTH[(Authentication Service)]
    LOGS((Logging System))
    MON((Monitoring))
    CDN((Content Delivery Network))
    WEB[Web Server]
    MAIL((Email Service))
```
```mermaid
graph TD
    ML[(Machine Learning Model)]
    SSO[(Single Sign-On)]
    DC{Data Center}
    STORAGE((Object Storage))
    AGENT[(Agent Service)]
    NS{Namespace}
    CONFIG[(Configuration Service)]
    API[(API Service)]
```
---

Mermaid supports various diagram types beyond graphs and flowcharts. Here’s a list of other types you can use in Mermaid:

1. **Sequence Diagram**  
   - Used to represent interactions between entities (like users, systems, or services) in a time-ordered sequence.
   - Example syntax:
     ```mermaid
     sequenceDiagram
         participant User
         participant Server
         User ->> Server: Request
         Server -->> User: Response
     ```

2. **Class Diagram**  
   - Represents the structure of classes, their attributes, methods, and relationships (often used in object-oriented design).
   - Example syntax:
     ```mermaid
     classDiagram
         class Animal {
             +String name
             +int age
             +makeSound()
         }
         class Dog {
             +String breed
             +bark()
         }
         Animal <|-- Dog
     ```

3. **Entity-Relationship Diagram (ERD)**  
   - Shows relationships between data entities, typically for database design.
   - Example syntax:
     ```mermaid
     erDiagram
         CUSTOMER ||--o{ ORDER : places
         CUSTOMER {
             string name
             string email
         }
         ORDER {
             int id
             date orderDate
         }
     ```

4. **Gantt Chart**  
   - Useful for project planning, scheduling, and task management.
   - Example syntax:
     ```mermaid
     gantt
         title Project Timeline
         dateFormat  YYYY-MM-DD
         section Project Planning
         Task 1 :done,    des1, 2024-01-01, 2024-01-15
         Task 2 :active,  des2, 2024-01-16, 2024-02-15
     ```

5. **State Diagram**  
   - Represents the states and transitions within a system or process.
   - Example syntax:
     ```mermaid
     stateDiagram-v2
         [*] --> Idle
         Idle --> Processing
         Processing --> Completed
         Processing --> Failed
     ```

6. **User Journey Diagram**  
   - Maps a user’s journey through different stages and experiences in a product or service.
   - Example syntax:
     ```mermaid
     journey
         title User Onboarding Journey
         section Sign Up
           User : 5: Sign up on the platform
           System : 3: Send confirmation email
         section Profile Setup
           User : 4: Complete profile details
           System : 4: Verify user information
     ```

7. **Pie Chart**  
   - A simple pie chart for displaying proportions.
   - Example syntax:
     ```mermaid
     pie
         title Project Allocation
         "Development" : 60
         "Marketing" : 20
         "Sales" : 20
     ```

8. **Requirement Diagram**  
   - Displays requirements and their relationships, useful in systems engineering.
   - Example syntax:
     ```mermaid
        requirementDiagram
        requirement req1
        requirement req1: The system shall be user-friendly
        
        functionalRequirement req2
        req2: The system shall have a login feature
        
        req1 --> req2
     ```

9. **Timeline Diagram**  
   - Depicts events along a timeline, useful for historical data or project milestones.
   - Example syntax:
     ```mermaid
     timeline
         title Product Development Timeline
         section Conceptualization
           Idea : 2024-01-01
         section Development
           Alpha : 2024-06-01
           Beta : 2024-09-01
     ```

These types offer diverse ways to visualize data, interactions, processes, and planning, each serving different purposes across project management, system design, and data modeling.
