import React from "react";
import { createRoot } from 'react-dom/client';
import App from "./App"; // Make sure the path to your App component is correct

// Import your main SCSS file
import '../scss/main.scss'; // Adjust the path to point to your main SCSS file

const root = document.getElementById('root');
if (!root) {
    throw new Error('Root element not found');
}

const reactRoot = createRoot(root);
reactRoot.render(<App />);



