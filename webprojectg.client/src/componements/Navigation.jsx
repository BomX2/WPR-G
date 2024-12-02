import React from 'react';
import { Link } from "react-router-dom";
import './Navigation.css';

const Navigation = () => {
    return (
        <nav>
            <ul>
                <li>
                    <Link to="/">Home</Link>
                </li>
                <li>
                    <Link to="/about">about</Link>
                </li>
                <li>
                    <Link to="/registratie">log in</Link>
                </li>
            </ul>
        </nav>
    )
}
export default Navigation;