import React, { Component } from "react";
import Header from "./components/Header"; // Adjust the path based on your directory structure
import ErrorTestComponent from "./components/tests/ErrorTestComponent";

class App extends Component {
	render() {
		return (
			<div>
				<Header />
				{/* <ErrorTestComponent /> */}
				{/* Other components or content can be added here */}

				{/* Ceci va seulement rendre ErrorTestComponent en mode développement */}
				{process.env.NODE_ENV === "development" && <ErrorTestComponent />}
			</div>
		);
	}
}

export default App;
