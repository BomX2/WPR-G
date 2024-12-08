import React, { useState } from 'react';
import "./Inlog.css"
import { useNavigate } from 'react-router-dom';

const Inlog = () => {

    const [email, setEmail] = useState('');
    const [wachtwoord, setWachtwoord] = useState('');

    const [error, setError] = useState<String>('')

    const navigate = useNavigate();

    const goToRegister = () => {
        navigate('/registratie');
    };

    const handelLogin = (e) => {
        if (!email || !wachtwoord) {
            setError("vul allen velden in")
        } else {
            setError("");
        }
        var loginurl = "/login?useSessionCookies=true";
        fetch(loginurl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                email: email,
                password: wachtwoord,
            }),
        })
            .then((response) => {
            console.log(response);
            if (response.ok) {
                setError("sucsesful login.");
                window.location.href = '/';
            } else {
                setError("login error");
            }
        })
            .catch((error) => {
            console.error(error);
            setError("Error Logging in.");
        });
    }

    return (
        <>
            <div className='container'>
                <div className="header">
                    <div className="text">login</div>
                </div>
                <form onSubmit={
                    handelLogin
                }>
                <div className="inputs">
                    <div className="input">
                            <input
                                type="email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                placeholder="email" />
                    </div>
                    <div className="input">
                            <input
                                type="wachtwoord"
                                value={wachtwoord}
                                onChange={(e) => setWachtwoord(e.target.value)}
                                placeholder="wachtwoord" />
                    </div>
                </div>
                <div className="submit-container">
                    <div className="submit">
                        <button className="buttons" onClick={goToRegister}>registreer</button>
                    </div>
                    <div className="submit">
                        <button type="submit"className="buttons">log in</button>
                    </div>
                    </div>
                </form>
            </div>
        </>
    );
};
export default Inlog