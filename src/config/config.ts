/**
 * Typage du fichier de configuration (Optionnel) : Si vous souhaitez tirer 
 * parti du typage fort de TypeScript, vous pouvez définir une interface pour 
 * votre configuration. Par exemple :
 */

// src/config/config.ts
interface Config {
  API_BASE_URL: string;
  // D'autres propriétés de configuration...
}

const config: Config = {
  API_BASE_URL: "https://localhost:7199/api",
  // D'autres configurations...
};

export default config;
