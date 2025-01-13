import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useUser } from '../componements/userContext';

function Login() {
    // State variables
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [rememberme, setRememberme] = useState(false);
    const [error, setError] = useState("");

    const { setUser } = useUser(); // Access UserContext to set user data
    const navigate = useNavigate();

    // Handles changes in the input fields
    const onChange = (e) => {
        const { name, value, type, checked } = e.target;
        if (name === "email") setEmail(value);
        if (name === "password") setPassword(value);
        if (name === "rememberme") setRememberme(type === "checkbox" ? checked : value);
    };

    // Redirects to the Register page
    const onRegisterClick = () => {
        navigate("/Registratie");
    };

    // Handles the form submission
    const onSubmit = (e) => {
        e.preventDefault();

        if (!email || !password) {
            setError("Please fill in all fields.");
            return;
        }

        setError("");

        const loginurl = rememberme
            ? "https://localhost:7065/api/gebruikers/login?useCookies=true"
            : "https://localhost:7065/api/gebruikers/login?useSessionCookies=true";

        fetch(loginurl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            credentials: "include",
            body: JSON.stringify({ email, password }),
        })
            .then((response) => response.json())
            .then((data) => {
                if (data && data.success) {
                    // Assume the API returns user role in the response (e.g., data.role)
                    const loggedInUser = {
                        id: data.id,
                        name: data.name,
                        email: data.email,
                        role: data.role, // e.g., 'Particulier', 'ZakelijkeHuurder', or 'WagenparkBeheerder'
                    };

                    // Set the user in the context
                    setUser(loggedInUser);

                    // Navigate to the home page or dashboard
                    navigate('/');
                } else {
                    setError(data.message || "Error Logging In.");
                }
            })
            .catch((error) => {
                console.error(error);
                setError("Error Logging in.");
            });
    };

    return (
        <div className="container">
            <div className="header">
                <div className="text">Login</div>
            </div>
            <form onSubmit={onSubmit}>
                <div className="inputs">
                    <div className="input">
                        <input
                            type="email"
                            id="email"
                            name="email"
                            value={email}
                            onChange={onChange}
                            placeholder="Email"
                        />
                    </div>
                    <div className="input">
                        <input
                            type="password"
                            id="password"
                            name="password"
                            value={password}
                            onChange={onChange}
                            placeholder="Wachtwoord"
                        />
                    </div>
                    <div>
                        <input
                            type="checkbox"
                            id="rememberme"
                            name="rememberme"
                            checked={rememberme}
                            onChange={onChange}
                        />
                        <span>Remember Me</span>
                    </div>
                </div>
                <div className="submit-container">
                    <div className="submit">
                        <button type="button" className="buttons" onClick={onRegisterClick}>
                            Registreer
                        </button>
                    </div>
                    <div className="submit">
                        <button type="submit" className="buttons">
                            Log In
                        </button>
                    </div>
                </div>
            </form>
            {error && <p className="error">{error}</p>}
        </div>
    );
}

export default Login;