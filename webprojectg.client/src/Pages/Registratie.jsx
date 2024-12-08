import React, { useState } from 'react';
import "./Registratie.css";
import { useNavigate } from 'react-router-dom';

const Registratie = () => {

    const [naam, setNaam] = useState('');
    const [adres, setAdress] = useState('');
    const [tel, setTel] = useState('');
    const [email, setEmail] = useState('');
    const [wachtwoord, setWachtwoord] = useState('');
    const navigate = useNavigate();

    const goBack = () => {
        navigate('/Inlog')
    }

    const onSubmit = async () => {
        try {
            const verwerking = await fetch('http://localhost:7065/api/klant/post', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    naam,
                    adres,
                    email,
                    tel,
                    wachtwoord,
                }),
            });

            if (verwerking.ok) {
                alert('Account succesvol bijgewerkt');
            } else {
                alert('Er is een fout opgetreden bij het bijwerken van uw account');
            }
        } catch (error) {
            console.error("try is mislukt", error);
            alert("Er is een fout opgetreden");
        }
    }

    return (
        <>
            <div className='container'>
                <div className="header">
                    <div className="text">Sign Up</div>

                </div>

                <form onSubmit={(e) => {
                    e.preventDefault();
                    onSubmit();
                    goBack()
                }} >
                <div className="inputs">
                    <div className="input">
                        <input
                            type="naam"
                            value={naam}
                            onChange={(e) => setNaam(e.target.value)}
                            placeholder="naam" />
                    </div>
                    <div className="input">
                            <input
                                type="adres"
                                value={adres}
                                onChange={(e) => setAdress(e.target.value) }
                                placeholder="adres" />
                    </div>
                    <div className="input">
                            <input
                                type="telefoon nummer"
                                value={tel}
                                onChange={(e) => setTel(e.target.value) }
                                placeholder="telefoon nummer" />
                    </div>
                    <div className="input">
                            <input
                                type="email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value) }
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
                        <button className="buttons" onClick={goBack}>ga terug</button>
                    </div>
                    <div className="submit">
                        <button type="submit" className = "buttons">registreer</button>
                    </div>
                </div>
              </form>
            </div>
        </>
    );
};
export default Registratie