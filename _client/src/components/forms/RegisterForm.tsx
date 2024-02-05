import React, { useState } from 'react';
import apiService, { useApiService } from "../../services/apiService";
import { UserRegistration } from "../../models/forms/UserRegistration";

const RegisterForm = () => {
    const { apiService } = useApiService();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
		const [confirmPassword, setConfirmPassword] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        const userData: UserRegistration = { email, password, confirmPassword };
        const result = await apiService.registerUser(userData);
        if (result) {
            console.log('Registration successful', result);
            // Handle success (e.g., redirect to login page or dashboard)
        } else {
            console.log('Registration failed');
            // Handle failure (show error message)
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Email" required />
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" required />
						<input type="password" value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} placeholder="Confirm Password" required />
            <button type="submit">Register</button>
        </form>
    );
};

export default RegisterForm;
