import React, { useState } from 'react';
import "./Registratie.css";
import { useNavigate } from 'react-router-dom';

const Registratie = () => {

    const [naam, setNaam] = useState('');
    const [adres, setAdress] = useState('');
    const [tel, setTel] = useState('');
    const [email, setEmail] = useState('');
    const [wachtwoord, setWachtwoord] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();


    const goBack = () => {
        navigate('/Inlog')
    }

    const registratieBasisDetails = async () => {
        try {
            const verwerking = await fetch('/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    email: email,
                    wachtwoord: wachtwoord
                }),
            });

            if (verwerking.ok) {
                alert('Account succesvol bijgewerkt');
            } else {
                alert('Er is een fout opgetreden bij het bijwerken van uw account');
            }
            return true;
        } catch (error) {
            console.error("try is mislukt", error);
            alert("Er is een fout opgetreden");
            return false;
        }
    }

    const registerUserDetails = async () => {
        try {
            const verwerking = await fetch('http://localhost:7065/api/klant/post', {
            method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
            body: JSON.stringify({
                naam: naam,
                adres: adres,
                telephone: tel,
                email: email,
            }),
        });
        if (verwerking.ok) {
            alert('Account succesvol bijgewerkt');
        } else {
            alert('Er is een fout opgetreden bij het bijwerken van uw account');
        }
        return true;
    } catch (error) {
        console.error("try is mislukt", error);
        alert("Er is een fout opgetreden");
        return false;
    }
    }
    
    const onSubmit = async () => {
        setError('');

        if (!naam || !adres || !tel || !email || !wachtwoord) {
            setError('Alle velden zijn verplicht.');
            return;
        }

        const basicDetailsSuccess = await registratieBasisDetails();
        if (!basicDetailsSuccess) return;

       
        

        goBack();
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