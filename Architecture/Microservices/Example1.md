以下系统架构图：

```mermaid
flowchart LR
    subgraph Client [Client Application]
        A[Client UI]
    end

    subgraph Gateway [API Gateway]
        G[Evently.Gateway]
    end

    subgraph API[Evently.API Services]
        E[Events]
        AT[Attendance]
        U[Users]
    end

    subgraph Ticketing[Ticketing Service]
        T[Evently.Ticketing.Api]
    end

    subgraph DB[Database + Schema]
        S1[[<schema>\nevents.*]]
        S2[[<schema>\nattendance.*]]
        S3[[<schema>\nusers.*]]
        S4[[<schema>\nticketing.*]]
    end

    subgraph Auth[Authentication]
        IDP[Identity Provider]
    end

    subgraph Infra[Infrastructure]
        CACHE[Distributed Cache]
        LOG[Logging Seq]
        OBS[Observability Jaeger]
    end

    subgraph MQ[Message Broker]
        MB[(Message Broker)]
    end

    A --> G
    G --> E
    G --> AT
    G --> U
    G --> T

    IDP --> CACHE
    IDP --> E
    IDP --> AT
    IDP --> U
    IDP --> T

    E --> S1
    AT --> S2
    U --> S3
    T --> S4

    E --> MB
    AT --> MB
    U --> MB
    T --> MB

    MB -->|Events| S1
    MB -->|Attendance| S2
    MB -->|Users| S3
    MB -->|Ticketing| S4

    LOG --> OBS
```

此图反映了以下关键点：

- 客户端通过 API Gateway 与后端交互。
- 后端由多个服务组成（Events, Attendance, Users, Ticketing）。
- 身份验证由 Identity Provider 提供，并使用分布式缓存优化访问。
- 每个服务均与消息代理通信，并发布特定 schema 的消息。
- 使用分布式日志与可观察性工具（如 Seq, Jaeger）监控系统运行状态。

需要我为你生成实际图像或进一步解释某个部分吗？
