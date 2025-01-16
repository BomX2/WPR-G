import React from 'react';
import { Link, useNavigate } from "react-router-dom";
import { useUser } from '../componements/userContext';
import './Navigation.css';

function Navigation() {
    const { user, setUser } = useUser();
    const navigate = useNavigate();

    const handleLogout = async () => {
        try {
            console.log('Attempting to log out.'); // Debug log for logout attempt

            const response = await fetch("https://localhost:7065/api/gebruikers/logout", {
                method: "POST",
                credentials: "include",
            });

            if (response.ok) {
                console.log('Logout successful.'); // Debug log for successful logout
                setUser(null);
                navigate("/inlog");
            } else {
                console.error('Logout failed:', response.statusText); // Debug log for failed logout
            }
        } catch (err) {
            console.error('Unexpected error during logout:', err); // Debug log for unexpected errors
        }
    };

    return (
        <nav>
            <Link to="/" className="title">CarAndAll</Link>
            <ul>
                <li>
                    <Link to="/Catalogus">Catalogus</Link>
                </li>
                <li>
                    <Link to="/About">About</Link>
                </li>

                {!user ? (
                    <li>
                        <Link to="/Inlog">Log In</Link>
                    </li>
                ) : (
                    <>
                        <li>
                            <Link to="/AccountSettings">Account Settings</Link>
                        </li>

                        {['ZakelijkeHuurder', 'WagenparkBeheerder'].includes(user.role) && (
                            <li>
                                <Link to="/Bedrijf">Bedrijf</Link>
                            </li>
                        )}

                        {user.role === 'WagenparkBeheerder' && (
                            <li>
                                <Link to="/BedrijfsRegistratie">Bedrijf Registratie</Link>
                            </li>
                        )}

                        <li>
                                <button
                                    onClick={handleLogout}
                                    style={{
                                        all: 'unset',
                                        display: 'block',
                                        textDecoration: 'none',
                                        color: 'white',
                                        padding: '0.5rem',
                                        margin: '0 0.5rem',
                                        borderRadius: '0.5rem',
                                        cursor: 'pointer',
                                    }}
                                >
                                    Uitloggen
                                </button>
                        </li>
                    </>
                )}
                
                <li>
                    <Link to="/BedrijfsRegistratie">log in voor bedrijven</Link>
                </li>
                <li>
                    <Link to="/AanvraagBackOffice">bekijk aanvragen</Link>
                </li>
                <li>
                    <Link to="/FrontAanvraag">Front Aanvragen</Link>
                </li>
            </ul>
        </nav>
    );
};

export default Navigation;
