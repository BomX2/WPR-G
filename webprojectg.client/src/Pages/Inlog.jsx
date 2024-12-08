import React, { useState } from 'react'; // Import React and useState hook
import { useNavigate } from 'react-router-dom'; // Import useNavigate for navigation

function Login() {
    // State variables to store user input
    const [email, setEmail] = useState(""); // Stores the email entered by the user
    const [password, setPassword] = useState(""); // Stores the password entered by the user
    const [rememberme, setRememberme] = useState(false); // Tracks if "Remember Me" is checked
    const [error, setError] = useState(""); // Stores any error messages to display

    const navigate = useNavigate(); // Allows navigation to different routes

    // Handles changes in the input fields
    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        if (name === "email") setEmail(value); // Update email state
        if (name === "password") setPassword(value); // Update password state
        if (name === "rememberme") setRememberme(type === "checkbox" ? checked : value); // Update "Remember Me" state
    };

    // Redirects to the Register page when the button is clicked
    const handleRegisterClick = () => {
        navigate("/Registratie"); // Navigate to the register page
    };

    // Handles the form submission
    const handleSubmit = (e) => {
        e.preventDefault(); // Prevent the default form submission behavior

        // Validate that both email and password fields are filled
        if (!email || !password) {
            setError("Please fill in all fields."); // Set an error message if fields are empty
        } else {
            setError(""); // Clear any previous error messages

            // Determine the login URL based on "Remember Me" status
            const loginurl = rememberme ? "/login?useCookies=true" : "/login?useSessionCookies=true";

            // Send login details to the server
            fetch(loginurl, {
                method: "POST", // HTTP POST request
                headers: {
                    "Content-Type": "application/json", // Indicate JSON content
                },
                body: JSON.stringify({ email, password }), // Send email and password as JSON
            })
                .then((response) => {
                    console.log(response); // Log the response for debugging
                    if (response.ok) {
                        setError("Successful Login."); // Notify the user of successful login
                        window.location.href = '/'; // Redirect to the homepage
                    } else {
                        setError("Error Logging In."); // Notify the user of a login error
                    }
                })
                .catch((error) => {
                    console.error(error); // Log any network errors
                    setError("Error Logging in."); // Notify the user of a network error
                });
        }
    };

    // JSX for rendering the login form
    return (
        <div className="containerbox">
            <h3>Login</h3>
            <form onSubmit={handleSubmit}>
                <div>
                    <label className="forminput" htmlFor="email">Email:</label>
                </div>
                <div>
                    <input
                        type="email" // Input type for email
                        id="email"
                        name="email" // Field name
                        value={email} // Bind state to input value
                        onChange={handleChange} // Call handleChange when the input changes
                    />
                </div>
                <div>
                    <label htmlFor="password">Password:</label>
                </div>
                <div>
                    <input
                        type="password" // Input type for password
                        id="password"
                        name="password"
                        value={password}
                        onChange={handleChange}
                    />
                </div>
                <div>
                    <input
                        type="checkbox" // Checkbox for "Remember Me"
                        id="rememberme"
                        name="rememberme"
                        checked={rememberme} // Bind state to checkbox
                        onChange={handleChange}
                    />
                    <span>Remember Me</span>
                </div>
                <div>
                    <button type="submit">Login</button> {/* Submit the form */}
                </div>
                <div>
                    <button type="button" onClick={handleRegisterClick}>Register</button> {/* Go to register page */}
                </div>
            </form>
            {error && <p className="error">{error}</p>} {/* Display error message if any */}
        </div>
    );
}

export default Login;
