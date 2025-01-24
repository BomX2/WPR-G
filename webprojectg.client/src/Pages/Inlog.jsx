import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useUser } from '../componements/UserContext';

function Login() {
    const [emailOrUsername, setEmailOrUsername] = useState("");
    const [password, setPassword] = useState("");
    const [rememberMe, setRememberMe] = useState(false);
    const [error, setError] = useState("");

    // 2FA-related state:
    const [requiresTwoFactor, setRequiresTwoFactor] = useState(false);
    const [twoFactorCode, setTwoFactorCode] = useState("");
    const [twoFactorUserId, setTwoFactorUserId] = useState("");

    const { setUser } = useUser();
    const navigate = useNavigate();

    const onRegisterClick = () => {
        navigate("/Registratie");
    };

    // Normal login submission
    const onSubmit = async (e) => {
        e.preventDefault();
        setError("");

        if (!emailOrUsername || !password) {
            setError("Please fill in all fields.");
            return;
        }

        try {
            console.log("Attempting login for:", emailOrUsername);
            console.log("Remember Me:", rememberMe);

            const response = await fetch("https://localhost:7065/api/gebruikers/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                credentials: "include", // for cookie-based auth
                body: JSON.stringify({ emailOrUsername, password, rememberMe }),
            });

            const responseText = await response.text();
            console.log("Raw response text:", responseText);

            if (response.ok) {
                const data = JSON.parse(responseText);

                // If server indicates 2FA is required
                if (data.requiresTwoFactor) {
                    setRequiresTwoFactor(true);
                    setTwoFactorUserId(data.userId || "");
                } else {
                    console.log("Login successful. Getting user info...");

                    // Wait briefly for cookie to set
                    await new Promise((resolve) => setTimeout(resolve, 200));

                    const userResponse = await fetch("https://localhost:7065/api/gebruikers/me", {
                        credentials: "include",
                    });
                    if (userResponse.ok) {
                        const userData = await userResponse.json();
                        sessionStorage.setItem("UserId", userData.UserId);
                        setUser(userData);
                        console.log("User context updated:", userData);
                        navigate("/");
                    } else {
                        console.error("Failed to fetch user details after login.");
                        setError("Failed to retrieve user information.");
                    }
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

    // 2FA code verification
    const handle2faVerify = async (e) => {
        e.preventDefault();
        setError("");

        if (!twoFactorCode) {
            setError("Please enter your 2FA code.");
            return;
        }

        try {
            const response = await fetch("https://localhost:7065/api/gebruikers/verify-2fa-login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                credentials: "include",
                body: JSON.stringify({
                    userId: twoFactorUserId,
                    twoFactorCode: twoFactorCode,
                    rememberMe: rememberMe
                }),
            });

            const responseText = await response.text();
            console.log("2FA verify raw response:", responseText);

            if (response.ok) {
                console.log("2FA verification successful. Fetching user...");
                // Again, wait briefly
                await new Promise((resolve) => setTimeout(resolve, 200));

                const userResponse = await fetch("https://localhost:7065/api/gebruikers/me", {
                    credentials: "include",
                });
                if (userResponse.ok) {
                    const userData = await userResponse.json();
                    sessionStorage.setItem("UserId", userData.UserId);
                    setUser(userData);
                    console.log("User context updated after 2FA:", userData);
                    navigate("/");
                } else {
                    console.error("Failed to fetch user details after 2FA login.");
                    setError("Failed to retrieve user information after 2FA.");
                }
            } else {
                const errorData = JSON.parse(responseText);
                console.error("2FA verification failed:", errorData);
                setError(errorData.message || "2FA verification failed.");
            }
        } catch (err) {
            console.error("Error during 2FA verification:", err);
            setError("An unexpected error occurred during 2FA verification.");
        }
    };

    return (
        <div className="container">
            <div className="header">
                <div className="text">Login</div>
            </div>

            {!requiresTwoFactor && (
                <form onSubmit={onSubmit}>
                    <div className="inputs">
                        <div className="input">
                            <input
                                type="text"
                                value={emailOrUsername}
                                onChange={(e) => setEmailOrUsername(e.target.value)}
                                placeholder="Email or Username"
                            />
                        </div>
                        <div className="input">
                            <input
                                type="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                placeholder="Password"
                            />
                        </div>
                        <div>
                            <input
                                type="checkbox"
                                checked={rememberMe}
                                onChange={(e) => setRememberMe(e.target.checked)}
                            />
                            <span>Remember Me</span>
                        </div>
                    </div>
                    <div className="submit-container">
                        <div className="submit">
                            <button type="button" className="buttons" onClick={onRegisterClick}>
                                Register
                            </button>
                        </div>
                        <div className="submit">
                            <button type="submit" className="buttons">
                                Log In
                            </button>
                        </div>
                    </div>
                </form>
            )}

            {requiresTwoFactor && (
                <form onSubmit={handle2faVerify}>
                    <p>2FA is required. Please enter the code from your authenticator app:</p>
                    <div className="inputs">
                        <div className="input">
                            <input
                                type="text"
                                value={twoFactorCode}
                                onChange={(e) => setTwoFactorCode(e.target.value)}
                                placeholder="123456"
                            />
                        </div>
                    </div>
                    <div className="submit-container">
                        <div className="submit">
                            <button type="submit" className="buttons">
                                Verify 2FA
                            </button>
                        </div>
                    </div>
                </form>
            )}

            {error && <p className="error">{error}</p>}
        </div>
    );
}

export default Login;