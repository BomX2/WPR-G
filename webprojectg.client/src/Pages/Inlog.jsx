import React from 'react';
import "./Inlog.css"
import { useNavigate } from 'react-router-dom';

const Inlog = () => {

    const navigate = useNavigate();

    const goToRegister = () => {
        navigate('/registratie');
    };

    return (
        <>
            <div className='container'>
                <div className="header">
                    <div className="text">Sign Up</div>
                </div>
                <div className="inputs">
                    <div className="input">
                        <input type="naam" placeholder="naam" />
                    </div>
                    <div className="input">
                        <input type="wachtwoord" placeholder="wachtwoord" />
                    </div>
                </div>
                <div className="submit-container">
                    <div className="submit">
                        <button className="buttons" onClick={goToRegister}>registreer</button>
                    </div>
                    <div className="submit">
                        <button className="buttons">log in</button>
                    </div>
                </div>
            </div>
        </>
    );
};
export default Inlog