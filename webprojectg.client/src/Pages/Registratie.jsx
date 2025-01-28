import { useState } from "react";
import "./Registratie.css";
import { useNavigate } from "react-router-dom";

const Registratie = () => {
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [adres, setAdres] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [role, setRole] = useState("Particulier");
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const goBack = () => {
        navigate('/Inlog');
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        if (name === "username") setUsername(value);
        else if (name === "email") setEmail(value);
        else if (name === "password") setPassword(value);
        else if (name === "confirmPassword") setConfirmPassword(value);
        else if (name === "adres") setAdres(value);
        else if (name === "phoneNumber") setPhoneNumber(value);
        else if (name === "role") setRole(value);
    };

    const onSubmit = async (e) => {
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
                body: JSON.stringify({ username, email, password, confirmPassword, adres, phoneNumber, role }),
            });

            if (response.ok) {
                alert("Registration successful");
                navigate("/Inlog");
            } else {
                const data = await response.json();
                setError(data.message || "Registration failed");
            }
        } catch (error) {
            setError("An error occurred during registration", error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="container">
            <div className="header">
                <div className="text">Sign Up</div>
            </div>

            <form onSubmit={onSubmit}>
                <div className="inputs">
                    <div className="input">
                        <input
                            placeholder="username*"
                            type="username"
                            id="username"
                            name="username"
                            value={username}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="input">
                        <input
                            placeholder="email*"
                            type="email"
                            id="email"
                            name="email"
                            value={email}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="input">
                        <input
                            placeholder="password*"
                            type="password"
                            id="password"
                            name="password"
                            value={password}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="input">
                        <input
                            placeholder="confirmPassword*"
                            type="password"
                            id="confirmPassword"
                            name="confirmPassword"
                            value={confirmPassword}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="input">
                        <input
                            placeholder="adres*"
                            type="text"
                            id="adres"
                            name="adres"
                            value={adres}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="input">
                        <input
                            placeholder="phoneNumber*"
                            type="tel"
                            id="phoneNumber"
                            name="phoneNumber"
                            value={phoneNumber}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="roleinput">
                        <label htmlFor="role">Kies uw rol:</label>
                        <select
                            placeholder="Select your role"
                            id="role"
                            name="role"
                            value={role}
                            onChange={handleChange}
                            required
                        >
                            <option value="Particulier">Particulier</option>
                            <option value="ZakelijkeHuurder">ZakelijkeHuurder</option>
                            <option value="WagenparkBeheerder">WagenparkBeheerder</option>
                        </select>
                    </div>
                </div>
                <div className="submit-container">
                    <div className="submit">
                        <button type="button" className="buttons" onClick={goBack}>
                            Ga terug
                        </button>
                    </div>
                    <div className="submit">
                        <button type="submit" className="buttons" disabled={loading}>
                            {loading ? "Registering..." : "Registreer"}
                        </button>
                    </div>
                </div>
                {error && <p className="error">{error}</p>}
            </form>

        </div>
    );
};

export default Registratie;