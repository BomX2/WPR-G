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
                    <Link to="/Inlog">log in</Link>
                </li>
                <li>
                    <Link to="/AccountSettings">account settings</Link>
                </li>
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
    )
}
export default Navigation;