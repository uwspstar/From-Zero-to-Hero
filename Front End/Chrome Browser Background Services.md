# Chrome Browser Background Services Chrome 浏览器后台服务

## 1. Back/Forward Cache 前进/后退缓存

### What is Back/Forward Cache?
The Back/Forward Cache (bfcache) is a browser optimization that allows instant navigation to pages that have already been visited by caching the entire page, including its JavaScript and DOM state. This means that when a user navigates back or forward to a previously visited page, the page can be restored instantly from memory instead of being reloaded from the network.

### 特点 Features

- **Instant Navigation**: Provides near-instant page load times when navigating back and forward.
- **State Preservation**: Retains the JavaScript execution state, scroll position, and more.
- **Automatic Management**: The browser automatically manages bfcache, and developers typically don’t need to make changes to benefit from it.

- **即时导航**：在前进和后退导航时提供近乎即时的页面加载时间。
- **状态保留**：保留 JavaScript 执行状态、滚动位置等。
- **自动管理**：浏览器自动管理 bfcache，开发人员通常无需进行更改即可受益。

### Use Cases 用例

- Enhancing user experience by providing faster back and forward navigation in single-page applications (SPAs) or dynamic web pages.
- Reducing network load by reusing already fetched resources.

- 通过在单页应用程序（SPAs）或动态网页中提供更快的前进和后退导航，增强用户体验。
- 通过重复使用已获取的资源来减少网络负载。

## 2. Background Fetch 后台提取

### What is Background Fetch?
Background Fetch allows web applications to fetch large files or multiple files in the background, even if the user closes the browser or navigates away from the page. This is particularly useful for downloading large assets such as videos or large datasets that may take a long time to complete.

### 特点 Features

- **Persistent Downloads**: Downloads continue even if the browser is closed or the user navigates away.
- **Notification Integration**: The user can be notified when the download is complete.
- **Progress Monitoring**: Allows monitoring of the download progress.

- **持久下载**：即使浏览器关闭或用户离开，下载仍会继续。
- **通知集成**：下载完成后可以通知用户。
- **进度监控**：允许监控下载进度。

### Use Cases 用例

- Downloading large video files or datasets that require persistent download capability.
- Offline-ready web applications that need to fetch content in the background.

- 下载需要持久下载功能的大型视频文件或数据集。
- 需要在后台提取内容的离线就绪 Web 应用程序。

## 3. Background Sync 后台同步

### What is Background Sync?
Background Sync is a service worker feature that allows web applications to defer tasks until the user has a stable internet connection. This is useful for actions such as sending messages or syncing data that should be completed even after the user leaves the site.

### 特点 Features

- **Deferred Tasks**: Tasks can be queued and executed when the network connection is stable.
- **Reliable Syncing**: Ensures important actions like message sending or form submissions are completed.
- **Offline Capability**: Enhances offline functionality by syncing data once connectivity is restored.

- **延迟任务**：任务可以排队并在网络连接稳定时执行。
- **可靠同步**：确保重要操作（如发送消息或提交表单）得以完成。
- **离线功能**：通过在恢复连接后同步数据增强离线功能。

### Use Cases 用例

- Syncing user data, such as form submissions or messages, when the user is offline.
- Ensuring critical tasks are completed once connectivity is reestablished.

- 在用户离线时同步用户数据，如表单提交或消息。
- 确保一旦恢复连接就完成关键任务。

## 4. Bounce Tracking Mitigations 跳出跟踪缓解

### What is Bounce Tracking Mitigation?
Bounce tracking is a technique used by advertisers and analytics services to track users across different sites by redirecting them through an intermediate domain. Chrome’s bounce tracking mitigation aims to limit this kind of tracking by identifying and limiting the ability of these intermediate domains to store and share tracking data.

### 特点 Features

- **Privacy Protection**: Helps prevent cross-site tracking by limiting the capabilities of intermediate domains.
- **Automatic Handling**: Chrome handles this automatically, reducing the need for developer intervention.

- **隐私保护**：通过限制中间域的功能，帮助防止跨站跟踪。
- **自动处理**：Chrome 自动处理，减少开发人员干预的需要。

### Use Cases 用例

- Enhancing user privacy by preventing bounce tracking between websites.
- Protecting users from being tracked by third-party domains used in advertising.

- 通过防止网站之间的跳出跟踪来增强用户隐私。
- 保护用户不被用于广告的第三方域跟踪。

## 5. Notifications 通知

### What are Notifications?
Notifications in Chrome allow web applications to send alerts to users, even when the browser is not actively being used. These notifications can be used to inform users about new messages, updates, or reminders, and are often integrated with the operating system’s notification system.

### 特点 Features

- **User Engagement**: Keeps users engaged by providing timely updates and alerts.
- **Cross-Platform**: Works across different operating systems with native integration.
- **Customizable**: Notifications can be customized with actions, images, and more.

- **用户参与**：通过提供及时的更新和警报，使用户保持参与。
- **跨平台**：在不同的操作系统上工作，并与本地集成。
- **可定制**：通知可以通过操作、图像等进行自定义。

### Use Cases 用例

- Sending reminders or alerts for events, tasks, or messages.
- Keeping users informed about real-time updates such as news or social media notifications.

- 发送活动、任务或消息的提醒或警报。
- 让用户了解实时更新，如新闻或社交媒体通知。

## 6. Payment Handler 支付处理程序

### What is a Payment Handler?
The Payment Handler API allows web applications to process payments by integrating with different payment methods. It enables the creation of custom payment handlers that can interact with various payment services, providing a consistent user experience.

### 特点 Features

- **Integration with Payment Methods**: Supports integration with credit cards, digital wallets, and other payment services.
- **Custom Payment Flows**: Allows developers to create custom payment experiences.
- **Secure Transactions**: Provides a secure environment for handling payments.

- **与支付方式集成**：支持与信用卡、数字钱包和其他支付服务集成。
- **自定义支付流程**：允许开发人员创建自定义支付体验。
- **安全交易**：为处理支付提供安全环境。

### Use Cases 用例

- Enabling in-app purchases or online transactions with a streamlined, consistent interface.
- Integrating multiple payment methods into a single checkout experience.

- 通过简化、一致的界面启用应用内购买或在线交易。
- 将多种支付方式集成到单一结账体验中。

## 7. Periodic Background Sync 定期后台同步

### What is Periodic Background Sync?
Periodic Background Sync allows web applications to periodically sync data in the background, even when the web app is not open. This ensures that the app's data remains up-to-date without requiring the user to interact with the app.

### 特点 Features

- **Periodic Updates**: Ensures data is synced at regular intervals.
- **Offline Functionality**: Supports offline applications by updating data when the connection is available.
- **Automatic Handling**: Managed by the browser without requiring user intervention.

- **定期更新**：确保数据定期同步。
- **离线功能**：通过在连接可用时更新数据，支持离线应用程序。
- **自动处理**：由浏览器管理，无需用户干预。

### Use Cases 用例

- Keeping content like news, weather, or stock updates current in web applications.
- Ensuring that user-generated data is synced even if the app is not actively being used.

- 让新闻、天气或股票更新等内容在 Web 应用程序中保持最新。
- 确保即使应用程序未被积极使用，用户生成的数据也能同步。

## 8. Speculative Loads 推测加载

### What are Speculative Loads?
Speculative Loads are a performance optimization in Chrome where resources are preloaded based on predicted user behavior. This can include loading resources before they are actually needed, based on the likelihood that the user will navigate to a particular page or trigger a specific action.

### 特点 Features

- **Performance Improvement**: Reduces perceived load times by preloading resources.
- **User Behavior Prediction**: Leverages predictive algorithms to load content likely to be needed.
- **Transparent to User**: Works in the background, unnoticed by the user.

- **性能提升**：通过预加载资源减少感知加载时间。
- **用户行为预测**：利用预测算法加载可能需要的内容。
- **对用户透明**：在后台工作，不被用户察觉。

### Use Cases 用例

- Preloading resources like images, scripts, or stylesheets for the next page in a user’s likely navigation path.
- Enhancing the performance of single-page applications by preloading components or data.

- 预加载用户可能导航路径中下一个页面的资源，如图像、脚本或样式表。
- 通过预加载组件或数据提升单页应用程序的性能。

## 9. Push Messaging 推送消息

### What is Push Messaging?
Push Messaging allows web applications to send notifications or updates to users even when the browser is closed. It uses the Push API and Service Workers to deliver real-time messages, making it ideal for applications that need to notify users of new content, messages, or other events.

### 特点 Features

- **Real-Time Communication**: Enables instant delivery of messages to users.
- **Persistent Delivery**: Messages are delivered even when the user is not actively using the web application.
- **Integration with Notifications**: Often used in conjunction with the Notifications API for a seamless user experience.

- **实时通信**：实现即时向用户传递消息。
- **持久传递**：即使用户未积极使用 Web 应用程序，消息也会传递。
- **与通知集成**：通常与通知 API 结合使用，提供无缝的用户体验。

### Use Cases 用例

- Sending real-time notifications for new messages, social media updates, or breaking news.
- Keeping users engaged with timely alerts even when they are not using the app.

- 发送新消息、社交媒体更新或突发新闻的实时通知。
- 通过及时的警报让用户即使在不使用应用程序时也保持参与。

## 10. Reporting API 报告 API

### What is the Reporting API?
The Reporting API allows web applications to send reports about various events, such as security violations, deprecations, and feature usage, back to the server. This enables developers to monitor the performance and security of their applications in real-time.

### 特点 Features

- **Real-Time Monitoring**: Sends reports about security issues, performance metrics, and other events in real-time.
- **Configurable Endpoints**: Developers can specify where the reports should be sent.
- **Improved Security**: Helps identify and mitigate security issues through timely reporting.

- **实时监控**：实时发送有关安全问题、性能指标和其他事件的报告。
- **可配置的端点**：开发人员可以指定报告应发送的位置。
- **提高安全性**：通过及时报告帮助识别和缓解安全问题。

### Use Cases 用例

- Monitoring security violations such as Content Security Policy (CSP) violations.
- Tracking deprecated API usage or browser feature usage across user sessions.

- 监控安全违规行为，如内容安全策略 (CSP) 违规。
- 跟踪弃用的 API 使用情况或跨用户会话的浏览器功能使用情况。

## 总结 Conclusion

Chrome's background services are essential for enhancing the functionality, performance, and security of web applications. These services allow for a variety of advanced features, from offline data synchronization and real-time notifications to performance optimizations and privacy protections. Understanding these services and their capabilities allows developers to create more robust, responsive, and user-friendly web applications.

Chrome 的后台服务对于增强 Web 应用程序的功能、性能和安全性至关重要。这些服务支持多种高级功能，从离线数据同步和实时通知到性能优化和隐私保护。了解这些服务及其功能，可以帮助开发人员创建更健壮、响应更快且更用户友好的 Web 应用程序。
