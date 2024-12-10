import { useState } from "react";
import "./Registratie.css";
import { useNavigate } from "react-router-dom";

const Registratie = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [adres, setAdres] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        if (name === "email") setEmail(value);
        else if (name === "password") setPassword(value);
        else if (name === "confirmPassword") setConfirmPassword(value);
        else if (name === "adres") setAdres(value);
        else if (name === "phoneNumber") setPhoneNumber(value);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Basic validation
        if (!email || !password || !confirmPassword || !adres || !phoneNumber) {
            setError("All fields are required");
            return;
        }
        if (password !== confirmPassword) {
            setError("Passwords do not match");
            return;
        }
        if (!/^\+?[0-9]{7,15}$/.test(phoneNumber)) {
            setError("Invalid phone number format");
            return;
        }

        setError("");
        setLoading(true);

        try {
            const response = await fetch("https://localhost:7065/api/gebruikers/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email, password, confirmPassword, adres, phoneNumber }),
            });

            if (response.ok) {
                alert("Registration successful");
                navigate("/login");
            } else {
                const data = await response.json();
                setError(data.message || "Registration failed");
            }
        } catch (error) {
            setError("An error occurred during registration");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="containerbox">
            <h3>Register</h3>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="email">Email:</label>
                </div>
                <div>
                    <input
                        type="email"
                        id="email"
                        name="email"
                        value={email}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="password">Password:</label>
                </div>
                <div>
                    <input
                        type="password"
                        id="password"
                        name="password"
                        value={password}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="confirmPassword">Confirm Password:</label>
                </div>
                <div>
                    <input
                        type="password"
                        id="confirmPassword"
                        name="confirmPassword"
                        value={confirmPassword}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="adres">Adres:</label>
                </div>
                <div>
                    <input
                        type="text"
                        id="adres"
                        name="adres"
                        value={adres}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="phoneNumber">Phone Number:</label>
                </div>
                <div>
                    <input
                        type="tel"
                        id="phoneNumber"
                        name="phoneNumber"
                        value={phoneNumber}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <button type="submit" disabled={loading}>
                        {loading ? "Registering..." : "Register"}
                    </button>
                </div>
                <div>
                    <button type="button" onClick={() => navigate("/login")}>
                        Go to Login
                    </button>
                </div>
            </form>
            {error && <p className="error">{error}</p>}
        </div>
    );
};

export default Registratie;
