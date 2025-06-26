## 🧾 是否应该在 TDD 中包含 IP 和端口信息？

---

### ✅ 一、**技术设计文档（TDD）是否应包含 IP/端口？**

| 类型                                  | 是否应包含   | 原因                              |
| ----------------------------------- | ------- | ------------------------------- |
| **服务监听端口（如5000/443）**               | ✅ 建议包含  | 说明服务对外或内部监听的默认端口（如 API、gRPC）    |
| **通信协议（如 HTTP/HTTPS）**              | ✅ 建议包含  | 有助于架构人员理解通信方式与加密要求              |
| **固定 IP、NodePort、Firewall 规则等敏感配置** | ❌ 不建议包含 | 存在安全风险，建议转由 Infra 或 DevOps 团队管理 |

---

### ⚠ 二、**将 IP 和端口写入 TDD 的风险有哪些？**

| 风险类型             | 描述                            |
| ---------------- | ----------------------------- |
| **暴露网络结构**       | TDD 泄漏后攻击者可知公司内网布局或部署架构       |
| **被用于渗透测试或端口扫描** | 黑客可通过端口号推断系统种类（如 MySQL、Redis） |
| **权限错误使用**       | 某些敏感 IP/端口可能被无权限人员访问          |
| **误用生产信息**       | 把生产环境 IP 放入共享文档，增加误操作风险       |

---

### 🛠 三、**推荐做法**

#### 在 TDD 中，仅提供以下非敏感内容：

```yaml
Service: OrderService
Protocol: HTTPS
Port: 5001
Exposure: Internal only via API Gateway
DNS (Prod): orders.api.company.com
Notes: IP & infra setup managed by Infra team in deployment repo.
```

#### 在 Infra/DevOps 部门维护的文档中（私有 Git / Terraform / Helm）：

```yaml
apiVersion: v1
kind: Service
metadata:
  name: orderservice
spec:
  type: NodePort
  ports:
    - port: 5001
      nodePort: 31234
  selector:
    app: orderservice
```

---

### 📂 四、总结表格

| 内容                     | TDD中应包含 | 放在哪里更合适                 | 安全等级 |
| ---------------------- | ------- | ----------------------- | ---- |
| 应用服务端口（如 5001）         | ✅       | TDD                     | 低    |
| 通信协议（如 HTTPS）          | ✅       | TDD                     | 低    |
| 内部 DNS 名称              | ✅       | TDD                     | 中    |
| 固定 IP（如 10.0.1.23）     | ❌       | Infra 文档 / YAML 配置      | 高    |
| NodePort / Firewall ID | ❌       | DevOps YAML 或 Terraform | 高    |

---

### ✅ 五、实用建议（一句话模板）：

你可以回复 Infra 团队：

> “服务监听端口和协议我们已在 TDD 中说明，具体 IP 和部署端口请参考 Infra 的部署清单或 K8s YAML，由你们统一管理以避免配置泄漏风险。”
