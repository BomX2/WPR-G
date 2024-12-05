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
                    <Link to="/about">about</Link>
                </li>
                <li>
                    <Link to="/Inlog">log in</Link>
                </li>
            </ul>
        </nav>
    )
}
export default Navigation;