import React, { Component } from "react";
import Header from "./components/Header";
import Layout from "./components/Layout";
//import ErrorTestComponent from "./components/tests/ErrorTestComponent";

class App extends Component {
	render() {
		return (
			<div>
				<Header />
				<Layout />
				{/* <ErrorTestComponent /> */}
				{/* Other components or content can be added here */}

				{/* Ceci va seulement rendre ErrorTestComponent en mode développement */}
				{process.env.NODE_ENV === "development" /* && <ErrorTestComponent /> */}
			</div>
		);
	}
}

export default App;
