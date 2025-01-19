import { useState } from "react";
import "./Registratie.css";
import { useNavigate } from "react-router-dom";

const MedewerkerRegistratie = () => {
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [adres] = useState("CarAndAll 24");
    const [phoneNumber] = useState("062345678");
    const [role, setRole] = useState("FrontOffice");
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const goBack = () => {
        navigate('/Inlog');
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        if (name === "username") {
            setUsername(value);
            setEmail(`${value}@CarAndAll.com`)
        }
        else if (name === "password") setPassword(value);
        else if (name === "confirmPassword") setConfirmPassword(value);
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
                            placeholder="username"
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
                            placeholder="password"
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
                            placeholder="confirmPassword"
                            type="password"
                            id="confirmPassword"
                            name="confirmPassword"
                            value={confirmPassword}
                            onChange={handleChange}
                            required
                        />
                    </div>
                   
                    <div className="input">
                        <label htmlFor="role">Role:</label>
                        <select
                            placeholder="Select your role"
                            id="role"
                            name="role"
                            value={role}
                            onChange={handleChange}
                            required
                        >
                            <option value="FrontOffice">FrontOffice</option>
                            <option value="BackOffice">BackOffice</option>
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

export default MedewerkerRegistratie;