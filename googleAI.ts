// googleAI.ts

import { GoogleGenerativeAI } from "@google/generative-ai";
import dotenv from 'dotenv';

// Configure dotenv pour charger les variables d'environnement à partir de votre fichier .env
dotenv.config();

// Vérifiez que la clé API est définie
if (!process.env.API_KEY) {
  throw new Error('API_KEY is not defined in the environment variables');
}

// Initialisation du modèle Google Generative AI avec la clé API
const genAI = new GoogleGenerativeAI(process.env.API_KEY);

// Remplacez 'MODEL_NAME' par le nom réel du modèle que vous souhaitez utiliser
const model = genAI.getGenerativeModel({ model: "models/aqa" });

// Exportez `model` pour l'utiliser dans d'autres fichiers
export { model };
