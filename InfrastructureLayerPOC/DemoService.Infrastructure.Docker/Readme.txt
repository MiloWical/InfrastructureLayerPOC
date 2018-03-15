To run the containerized service:

1) Build the DemoService Contract and Implementation projects. This will force the output of
   those builds into local reference locations for the Docker build process.

2) Run the following command in the Docker CLI from the directory containing Dockerfile:

   docker build . -t <IMG_NAME> -f <DOCKERFILE_NAME>

   where:
	* <IMG_NAME> is the name of the image that you want to use locally.
	* <DOCKERFILE_NAME> is one of: Dockerfile.Alpine, Dockerfile.Nanoserver

3) Run the following command once the container is built:

   docker images

   to make sure that your image is correctly represented in the images collection.

4) Run the following command to start a container:

   docker run -it -p <HOST_PORT>:54321 --name <CONT_NAME> <IMG_NAME>

   where <IMG_NAME> is from step (2) above.

   For reference, this is what the switches do:

   * docker run: Specifies the Docker runtime to create a new container with the specified image and start it
   * -it: Tells the runtime to enter interactive mode and assign a pseudo-TTY interface (keyboard) to the container
   * -p <HOST_PORT>:54321: Maps host TCP port <HOST_PORT> on the host to the container port 54321.
     (NOTE: Port 54321 MUST be that SPECIFIC port because it's the port that's exposed by the Docker image, and
	 is the one used by the code to respond to requests.)
   * --name <CONT_NAME>: The name you want to give the container. It's not necessary, but it's helpful if you
     want to have mulitple containers running on the same host built from the same image.

5) Run the TcpTestClient using the folliwng command:

   dotnet TcpTestClient <DOCKER_HOST> <HOST_PORT>

   where <DOCKER_HOST> is the host name or IP address of the target Docker host and <HOST_PORT> is the same as 
   the one specified in step (4) above.

6) Test the container. Once the TcpTestClient application connects, use the following protocol to test settings behaviors:

   * GET - Gets the latest setting.

   * GET_VER::<VERSION> - Gets the setting with the specified version. The versions begin at 1.
     (NOTE: If the version doesn't exist, the container WILL crash and will need to be stopped and reset.)

   * ADD::<SETTING> - Adds the value <SETTING> as the latest setting, incrementing the version.

   * UPD::<VERSION>::<SETTING> - Updates the specified versioned setting with a new value.
     (NOTE: If the version doesn't exist, the container WILL crash and will need to be stopped and reset.)

   * DEL::<VERSION> - Removes the settign with the specified version.
     (NOTE: If the version doesn't exist, the container WILL crash and will need to be stopped and reset.)

   The output on the Docker host should correspond to the messages sent from the client.

7) When completed, press CTRL+P+Q to exit interactive mode. The container will still be running, but you won't see the oputput.
   The settings, however, will be preserved in the container. Teh container can be stopped with the following command:

   docker stop <CONT_NAME>

   and restarted with:

   docker start <CONT_NAME>

   You can also re-enter interactive mode once the container is running with:

   docker attach <CONT_NAME>

   Interactive mode can be exited again with CTRL+P+Q