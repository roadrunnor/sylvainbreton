import React, { useState } from "react";
import apiService, { useApiService } from "../../services/apiService";
import { UserLogin } from "../../models/forms/UserLogin";

const LoginForm = () => {
	const { apiService } = useApiService();
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault();
		const userData: UserLogin = { email, password };
		const result = await apiService.loginUser(userData);
		if (result) {
			console.log("Login successful", result);
			// Handle success (e.g., store the token, redirect to the dashboard)
		} else {
			console.log("Login failed");
			// Handle failure (show error message)
		}
	};

	return (
		<form onSubmit={handleSubmit}>
			<input type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Email" required />
			<input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" required />
			<button type="submit">Log In</button>
		</form>
	);
};
