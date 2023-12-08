// src/config/config.ts
interface Config {
  API_BASE_URL: string;
  // ... other config properties
}

const config: Config = {
  API_BASE_URL: process.env.REACT_APP_API_BASE_URL || "https://localhost:7199/api",
  // ... other config settings
};

export default config;

