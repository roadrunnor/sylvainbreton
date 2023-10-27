import React from "react";
import ReactDOM from "react-dom";
import App from "./App"; // Make sure the path to your App component is correct

// Import your main SCSS file
import '../scss/main.scss'; // Adjust the path to point to your main SCSS file

const root = document.getElementById('root');
if (root) {
    const reactRoot = ReactDOM.createRoot(root);
    reactRoot.render(<App />);
}

