import React, { useState } from 'react'; 
import { useNavigate } from 'react-router-dom'; 
function Login() {
    // State variables to store user input
    const [email, setEmail] = useState(""); 
    const [password, setPassword] = useState(""); 
    const [rememberme, setRememberme] = useState(false); 
    const [error, setError] = useState(""); 

    const navigate = useNavigate();

    // Handles changes in the input fields
    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        if (name === "email") setEmail(value); 
        if (name === "password") setPassword(value); 
        if (name === "rememberme") setRememberme(type === "checkbox" ? checked : value); 

    // Redirects to the Register page when the button is clicked
    const handleRegisterClick = () => {
        navigate("/Registratie");
    };

    // Handles the form submission
    const handleSubmit = (e) => {
        e.preventDefault();

        if (!email || !password) {
            setError("Please fill in all fields."); 
        } else {
            setError("");

            const loginurl = rememberme ? "https://localhost:7065/api/gebruikers/login?useCookies=true"
                : "https://localhost:7065/api/gebruikers/login?useSessionCookies=true";

            fetch(loginurl, {
                method: "POST", 
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ email, password }), 
            })
                .then((response) => {
                    console.log(response); 
                    if (response.ok) {
                        setError("Successful Login."); 
                        window.location.href = '/'; 
                    } else {
                        setError("Error Logging In."); 
                    }
                })
                .catch((error) => {
                    console.error(error);
                    setError("Error Logging in."); 
                });
        }
    };

    return (
        <div className="containerbox">
            <h3>Login</h3>
            <form onSubmit={handleSubmit}>
                <div>
                    <label className="forminput" htmlFor="email">Email:</label>
                </div>
                <div>
                    <input
                        type="email" 
                        id="email"
                        name="email" 
                        value={email} 
                        onChange={handleChange}
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
                    />
                </div>
                <div>
                    <input
                        type="checkbox" 
                        id="rememberme"
                        name="rememberme"
                        checked={rememberme} 
                        onChange={handleChange}
                    />
                    <span>Remember Me</span>
                </div>
                <div>
                    <button type="submit">Login</button> {}
                </div>
                <div>
                    <button type="button" onClick={handleRegisterClick}>Register</button> {}
                </div>
            </form>
            {error && <p className="error">{error}</p>} {}
        </div>
    );
}

export default Login;