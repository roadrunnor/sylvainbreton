# Use a Node.js base image
FROM node:18-alpine

# Set environment to development
ENV NODE_ENV=development

# Set the working directory in the container
WORKDIR /app

# Install dependencies
COPY package*.json ./
RUN npm install

# Expose the port that create-react-app normally runs on
EXPOSE 3000

# Copy the entrypoint script into the container
COPY entrypoint.sh /usr/local/bin/

# Make the entrypoint script executable
RUN chmod +x /usr/local/bin/entrypoint.sh

# Start the application using the entrypoint script for development
ENTRYPOINT ["entrypoint.sh"]