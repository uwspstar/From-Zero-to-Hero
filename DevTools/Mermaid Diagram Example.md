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
