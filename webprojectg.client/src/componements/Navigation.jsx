import React from 'react';
import { Link } from "react-router-dom";
import './Navigation.css';

const Navigation = () => {
    return (
        <nav>
            <Link to="/" className="title">CarAndAll</Link>
            <ul>
                <li>
                    <Link to="/Catalogus">Catalogus</Link>
                </li>
                <li>
                    <Link to="/About">about</Link>
                </li>
                <li>
                    <Link to="/Inlog">log in</Link>
                </li>
                <li>
                    <Link to="/AccountSettings">account settings</Link>
                </li>
                <li>
                    <Link to="/BedrijfsRegistratie">log in voor bedrijven</Link>
                </li>
            </ul>
        </nav>
    )
}
export default Navigation;