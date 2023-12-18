# Use a Node.js base image
FROM node:14 AS development

# Set the working directory in the container
WORKDIR /app

# Install dependencies
COPY _client/package.json _client/package-lock.json ./
RUN npm install

# Copy the rest of the codebase into the container
COPY _client/ .

# Expose the port that create-react-app normally runs on
EXPOSE 3000

# Copy the entrypoint script into the container
COPY _client/entrypoint.sh /usr/local/bin/

# Make the entrypoint script executable
RUN chmod +x /usr/local/bin/entrypoint.sh

# Start the application using the entrypoint script for development
ENTRYPOINT ["entrypoint.sh"]
