# Use a Node.js base image
FROM node:18-alpine

# Set the working directory in the container
WORKDIR /app

# Install dependencies
# No need to install python or build tools since we are not building node-sass or similar packages
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of your project's files into the container
COPY . .

# Expose the port that create-react-app normally runs on
EXPOSE 3000

# Copy the entrypoint script into the container
COPY entrypoint.sh /usr/local/bin/

# Make the entrypoint script executable
RUN chmod +x /usr/local/bin/entrypoint.sh

# Start the application using the entrypoint script for development
ENTRYPOINT ["entrypoint.sh"]



# TERMINAL COMMANDS: 

# 1. Build your Docker image for development
# docker build -t breton-dev .

# 2. Run your Docker container, mapping your host's port 3000 to the container's port 3000
# docker run --name breton-dev -p 3000:3000 breton-dev

# 3. Find the Container ID or Name:
# docker ps

# 4. Stop the container:
# docker stop <container-id-or-name>

# 5. Remove the container:
# docker rm <container-id-or-name>

# 6. Remove the image:	
# docker rmi breton-dev

# 7. Build your Docker image for production
# docker build -f Dockerfile.prod -t breton-prod .

# 8. Run your Docker container, mapping your host's port 80 to the container's port 80
# docker run --name breton-prod -p 80:80 breton-prod

# 9. Verify your container has stopped:
# docker ps -a

# 10. Stop and remove the container:
# docker stop breton-dev && docker rm breton-dev
