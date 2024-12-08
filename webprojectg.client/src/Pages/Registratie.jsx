// eslint-disable-next-line no-unused-vars
import { useState } from 'react';
import "./Registratie.css";
import { useNavigate } from 'react-router-dom';

const Register = () => {
    // State variables for email and passwords
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [error, setError] = useState("");
    const navigate = useNavigate();

    // Handle change events for input fields
    const handleChange = (e) => {
        const { name, value } = e.target;

        if (name === "email") {
            setEmail(value);
        } else if (name === "password") {
            setPassword(value);
        } else if (name === "confirmPassword") {
            setConfirmPassword(value);
        }
    };

    // Handle submit event for the form
    const handleSubmit = (e) => {
        e.preventDefault();

        // Basic validation
        if (!email || !password || !confirmPassword) {
            setError("All fields are required");
            return;
        }

        if (password !== confirmPassword) {
            setError("Passwords do not match");
            return;
        }

        setError(""); // Clear error if all checks pass
        alert("Registration successful");
        navigate("/login");
    };

    const handleLoginClick = () => {
        navigate("/login");
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
                    <button type="submit">Register</button>
                </div>
                <div>
                    <button type="button" onClick={handleLoginClick}>
                        Go to Login
                    </button>
                </div>
            </form>

            {error && <p className="error">{error}</p>}
        </div>
    );
};

export default Register;
/*
const Registratie = () => {

    const navigate = useNavigate();

    const goBack = () => {
        navigate('/Inlog')
    }

    return (
        <>
            <div className='container'>
                <div className="header">
                    <div className="text">Sign Up</div>

                </div>
                <div className="inputs">
                    <div className="input">
                    <input type="naam" placeholder = "naam"/>
                    </div>
                    <div className="input">
                        <input type="adress" placeholder="adress"/>
                    </div>
                    <div className="input">
                        <input type="telefoon nummer" placeholder= "telefoon nummer" />
                    </div>
                    <div className="input">
                        <input type="email" placeholder="email"/>
                    </div>
                    <div className="input">
                        <input type="wachtwoord" placeholder= "wachtwoord"/>
                    </div>
                </div>
                <div className="submit-container">
                    <div className="submit">
                        <button className="buttons" onClick={goBack}>ga terug</button>
                    </div>
                    <div className="submit">
                        <button className = "buttons">registreer</button>
                    </div>
                </div>
            </div>
        </>
    );
};
export default Registratie*/