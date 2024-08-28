# How Docker works:

Here's a table that outlines the key components and processes of Docker, with explanations in both English and Chinese:

| **Component/Process** | **English Explanation**                                             | **Chinese Explanation**                                               |
|-----------------------|--------------------------------------------------------------------|-----------------------------------------------------------------------|
| **Docker 客户端 (Docker Client)** |                                                                                            |                                                                       |
| 构建 (Build)          | Initiates the process to create a Docker image.                     | 启动创建 Docker 镜像的过程。                                            |
| 拉取 (Pull)           | Fetches an image from the Docker registry.                         | 从 Docker 注册表中获取镜像。                                           |
| 运行 (Run)            | Runs a container from an image.                                    | 从镜像运行容器。                                                       |
| **REST API**          | Acts as the interface between the Docker client and the Docker daemon, enabling communication. | 作为 Docker 客户端与 Docker 守护进程之间的接口，实现通信。               |
| **守护进程 (Daemon Process)** | Manages Docker containers on the host.                                | 在主机上管理 Docker 容器。                                              |
| **Docker 主机 (Docker Host)** |                                                                                          |                                                                       |
| 容器 (Containers)     | Running instances of Docker images.                               | Docker 镜像的运行实例。                                                |
| 镜像 (Images)         | Blueprints for creating Docker containers, stored locally.         | 用于创建 Docker 容器的蓝图，存储在本地。                                 |
| **Docker 注册表 (Docker Registry)** |                                                                                          |                                                                       |
| ubuntu                | An example of an image available in the registry.                  | 注册表中可用的镜像示例。                                                |
| hub                   | Refers to Docker Hub, the default registry.                        | 指的是 Docker Hub，默认的注册表。                                        |
| nginx                 | Another example of an image available in the registry.             | 注册表中可用的另一个镜像示例。                                           |
| **Legend**            |                                                                                          |                                                                       |
| build                 | Green arrows indicate the build process.                          | 绿色箭头表示构建过程。                                                  |
| pull                  | Green arrows also indicate the pull process.                      | 绿色箭头也表示拉取过程。                                                |
| run                   | Red arrows indicate the run process.                              | 红色箭头表示运行过程。                                                  |

------

1. **Docker 客户端 (Docker Client)**:
   - **构建 (Build)**: Initiates the process to create a Docker image.
   - **拉取 (Pull)**: Fetches an image from the Docker registry.
   - **运行 (Run)**: Runs a container from an image.

2. **REST API**:
   - Acts as the interface between the Docker client and the Docker daemon, enabling communication.

3. **守护进程 (Daemon Process)**:
   - Manages Docker containers on the host.

4. **Docker 主机 (Docker Host)**:
   - **容器 (Containers)**: Running instances of Docker images.
   - **镜像 (Images)**: Blueprints for creating Docker containers, stored locally.

5. **Docker 注册表 (Docker Registry)**:
   - **ubuntu**: An example of an image available in the registry.
   - **hub**: Refers to Docker Hub, the default registry.
   - **nginx**: Another example of an image available in the registry.

6. **Legend**:
   - **build**: Green arrows indicate the build process.
   - **pull**: Green arrows also indicate the pull process.
   - **run**: Red arrows indicate the run process.

This diagram provides a high-level overview of how Docker operates, from building and pulling images to running containers, with the Docker daemon and registry playing crucial roles in the process. If you need any specific information or further explanation, feel free to ask!
