import React from 'react';
import { Link, useNavigate } from "react-router-dom";
import { useUser } from '../componements/userContext';
import './Navigation.css';

function Navigation() {
    const { user, setUser } = useUser();
    const navigate = useNavigate();

    const handleLogout = async () => {
        try {
            console.log('Attempting to log out.');

            const response = await fetch("https://localhost:7065/api/gebruikers/logout", {
                method: "POST",
                credentials: "include",
            });

            if (response.ok) {
                console.log('Logout successful.');
                setUser(null);
                navigate("/inlog");
            } else {
                console.error('Logout failed:', response.statusText);
            }
        } catch (err) {
            console.error('Unexpected error during logout:', err);
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

                        {['ZakelijkeHuurder', 'WagenparkBeheerder', 'Admin'].includes(user.role) && (
                            <li>
                                <Link to="/Bedrijf">Bedrijf</Link>
                            </li>
                        )}




                        {['WagenparkBeheerder', 'Admin'].includes(user.role) && (
                            <li>
                                <Link to="/BedrijfsRegistratie">Bedrijf Registratie</Link>
                            </li>
                        )}

                        {['WagenparkBeheerder', 'Admin'].includes(user.role) && (
                            <li>
                                <Link to="/BedrijfsInstellingen">bedrijfs-instellingen</Link>
                            </li>
                        )}

                        {['BackOffice', 'Admin'].includes(user.role) && (
                            <li>
                                <Link to="/AanvraagBackOffice">Aanvragen(B)</Link>
                            </li>,
                            <li>
                                <Link to="/VoertuigBeheren">Voertuigen Beheren</Link>
                            </li>
                        )}

                        {['BackOffice', 'Admin'].includes(user.role) && (
                            <li>
                                <Link to="/AanvraagBackOffice">Aanvragen(B)</Link>
                            </li>
                        )}


                        {['FrontOffice', 'Admin'].includes(user.role) && (
                            <li>
                                <Link to="/FrontAanvraag">Aanvragen(F)</Link>
                            </li>
                        )}

                        {['Admin'].includes(user.role) && (
                            <li>
                                <Link to="/MedewerkerRegistratie">MedewerkerRegistratie</Link>
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
            </ul>
        </nav>
    );
};

export default Navigation;
