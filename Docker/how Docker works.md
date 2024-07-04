# How Docker works:

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
