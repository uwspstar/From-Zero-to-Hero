# OSI Model Overview
### OSI模型概述

The OSI (Open Systems Interconnection) model is a conceptual framework used to understand and implement computer networking. It divides the networking process into seven distinct layers, each with specific functions and protocols. Below is a detailed explanation of each layer with examples.
OSI（开放系统互联）模型是一个用于理解和实现计算机网络的概念框架。它将网络过程分为七个不同的层，每层都有特定的功能和协议。以下是每一层的详细解释和示例。

---

| Layer | 层级 | Function | 功能 | Protocols & Examples | 协议及示例 |
|-------|------|----------|------|----------------------|------------|
| 1. Physical | 1. 物理层 | Transmits raw bit streams over a physical medium | 通过物理介质传输原始比特流 | Hubs, Fiber Optic, Cables | 集线器、光纤、电缆 |
| 2. Data Link | 2. 数据链路层 | Provides node-to-node data transfer and handles error correction from the physical layer | 提供节点到节点的数据传输，并处理物理层的错误校正 | MAC Addresses, Switches, Ethernet | MAC地址、交换机、以太网 |
| 3. Network | 3. 网络层 | Routes the data packet across the network from the source to the destination | 从源到目的地跨网络路由数据包 | IP, ICMP, Routers | IP、ICMP、路由器 |
| 4. Transport | 4. 传输层 | Provides reliable data transfer services to the upper layers | 为上层提供可靠的数据传输服务 | TCP, UDP | TCP、UDP |
| 5. Session | 5. 会话层 | Manages sessions and controls connections between computers | 管理会话并控制计算机之间的连接 | NetBIOS, PPTP | NetBIOS、PPTP |
| 6. Presentation | 6. 表示层 | Translates data formats between the application and the network | 在应用程序和网络之间转换数据格式 | SSL/TLS, JPEG | SSL/TLS、JPEG |
| 7. Application | 7. 应用层 | Provides network services directly to end-user applications | 直接为最终用户应用提供网络服务 | HTTP, FTP, SMTP | HTTP、FTP、SMTP |

---

### Detailed Examples and Functions
### 详细示例和功能

#### Physical Layer (Layer 1)
#### 物理层（第1层）

- **Function:**
  - **功能：**
    - Transmits raw bit streams over a physical medium.
    - 通过物理介质传输原始比特流。
- **Examples:**
  - **示例：**
    - Hubs, Fiber Optic Cables, Twisted Pair Cables.
    - 集线器、光纤电缆、双绞线电缆。

---

#### Data Link Layer (Layer 2)
#### 数据链路层（第2层）

- **Function:**
  - **功能：**
    - Provides node-to-node data transfer and handles error correction from the physical layer.
    - 提供节点到节点的数据传输，并处理物理层的错误校正。
- **Examples:**
  - **示例：**
    - MAC Addresses, Switches, Ethernet.
    - MAC地址、交换机、以太网。

---

#### Network Layer (Layer 3)
#### 网络层（第3层）

- **Function:**
  - **功能：**
    - Routes the data packet across the network from the source to the destination.
    - 从源到目的地跨网络路由数据包。
- **Examples:**
  - **示例：**
    - IP (Internet Protocol), ICMP (Internet Control Message Protocol), Routers.
    - IP（互联网协议）、ICMP（互联网控制消息协议）、路由器。

---

#### Transport Layer (Layer 4)
#### 传输层（第4层）

- **Function:**
  - **功能：**
    - Provides reliable data transfer services to the upper layers.
    - 为上层提供可靠的数据传输服务。
- **Examples:**
  - **示例：**
    - TCP (Transmission Control Protocol), UDP (User Datagram Protocol).
    - TCP（传输控制协议）、UDP（用户数据报协议）。

---

#### Session Layer (Layer 5)
#### 会话层（第5层）

- **Function:**
  - **功能：**
    - Manages sessions and controls connections between computers.
    - 管理会话并控制计算机之间的连接。
- **Examples:**
  - **示例：**
    - NetBIOS, PPTP (Point-to-Point Tunneling Protocol).
    - NetBIOS、PPTP（点对点隧道协议）。

---

#### Presentation Layer (Layer 6)
#### 表示层（第6层）

- **Function:**
  - **功能：**
    - Translates data formats between the application and the network.
    - 在应用程序和网络之间转换数据格式。
- **Examples:**
  - **示例：**
    - SSL/TLS (Secure Sockets Layer / Transport Layer Security), JPEG (Joint Photographic Experts Group).
    - SSL/TLS（安全套接层/传输层安全）、JPEG（联合图像专家组）。

---

#### Application Layer (Layer 7)
#### 应用层（第7层）

- **Function:**
  - **功能：**
    - Provides network services directly to end-user applications.
    - 直接为最终用户应用提供网络服务。
- **Examples:**
  - **示例：**
    - HTTP (HyperText Transfer Protocol), FTP (File Transfer Protocol), SMTP (Simple Mail Transfer Protocol).
    - HTTP（超文本传输协议）、FTP（文件传输协议）、SMTP（简单邮件传输协议）。

---

### Markdown Diagram

```markdown
graph TD
    A[Application Layer]
    B[Presentation Layer]
    C[Session Layer]
    D[Transport Layer]
    E[Network Layer]
    F[Data Link Layer]
    G[Physical Layer]

    A --> |HTTP, FTP, SMTP| A1[Provides network services directly to end-user applications]
    B --> |SSL/TLS, JPEG| B1[Translates data formats between the application and the network]
    C --> |NetBIOS, PPTP| C1[Manages sessions and controls connections between computers]
    D --> |TCP, UDP| D1[Provides reliable data transfer services to the upper layers]
    E --> |IP, ICMP, Routers| E1[Routes the data packet across the network from the source to the destination]
    F --> |MAC Addresses, Switches, Ethernet| F1[Provides node-to-node data transfer and handles error correction from the physical layer]
    G --> |Hubs, Fiber Optic, Cables| G1[Transmits raw bit streams over a physical medium]
```
