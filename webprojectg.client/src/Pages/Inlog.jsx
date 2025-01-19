import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useUser } from '../componements/UserContext';

function Login() {
    const [emailOrUsername, setEmailOrUsername] = useState("");
    const [password, setPassword] = useState("");
    const [rememberMe, setRememberMe] = useState(false);
    const [error, setError] = useState("");

    const { setUser } = useUser(); 
    const navigate = useNavigate();

    const onRegisterClick = () => {
        navigate("/Registratie");
    };

    // Handle form submission
    const onSubmit = async (e) => {
        e.preventDefault();
        setError("");

        if (!emailOrUsername || !password) {
            setError("Please fill in all fields.");
            return;
        }

        try {
            console.log('Attempting to log in with : ', emailOrUsername); // Debug log for login attempt
            console.log("Remember Me:", rememberMe);

            const response = await fetch("https://localhost:7065/api/gebruikers/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                credentials: "include",
                body: JSON.stringify({ emailOrUsername, password, rememberMe }),
            });

            const responseText = await response.text();
            console.log("Raw response text:", responseText);


            if (response.ok) {
                console.log("Login successful. Waiting for session establishment...");

                await new Promise((resolve) => setTimeout(resolve, 200)); // Wait 200ms

                const userResponse = await fetch("https://localhost:7065/api/gebruikers/me", {
                    credentials: "include",
                });

                if (userResponse.ok) {
                    const userData = await userResponse.json();
                    const UserId = userData.UserId;
                    sessionStorage.setItem("UserId", UserId);
                    setUser(userData); // Update UserContext
                    console.log("User context updated:", userData);
                    navigate("/");
                } else {
                    console.error("Failed to fetch user details after login.");
                    setError("Failed to retrieve user information.");
                }
            } else {
                const errorData = JSON.parse(responseText);
                console.error("Login failed:", errorData);
                setError(errorData.message || "Login failed.");
            }
        } catch (err) {
            console.error("Unexpected error during login:", err);
            setError("An unexpected error occurred. Please try again.");
        }
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
                            type="text"
                            id="emailOrUsername"
                            name="emailOrUsername"
                            value={emailOrUsername}
                            onChange={(e) => setEmailOrUsername(e.target.value)}
                            placeholder="Email or Username"
                        />
                    </div>
                    <div className="input">
                        <input
                            type="password"
                            id="password"
                            name="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            placeholder="Wachtwoord"
                        />
                    </div>
                    <div>
                        <input
                            type="checkbox"
                            id="rememberme"
                            name="rememberme"
                            checked={rememberMe}
                            onChange={(e) => setRememberMe(e.target.checked)}
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