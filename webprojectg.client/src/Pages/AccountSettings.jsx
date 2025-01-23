import React, { useState } from 'react';
import './forms.css';
import { useUser } from '../componements/userContext';
import { QRCodeSVG }  from 'qrcode.react';  

const AccSettings = () => {
    const { user, setUser } = useUser(); // Access user from context
    const [adres, setAdres] = useState(user?.adres || '');
    const [email, setEmail] = useState(user?.email || '');
    const [phonenumber, setPhonenumber] = useState(user?.phonenumber || '');
    const [show2FA, setShow2FA] = useState(false); // For showing the 2FA setup
    const [token, setToken] = useState(''); // Store the 2FA token

    const enable2FA = async () => {
        try {
            const response = await fetch('https://localhost:7065/api/gebruikers/enable-2fa', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${user.token}`, // Assuming you manage tokens
                },
            });
            const data = await response.json();
            if (response.ok) {
                setToken(data.token);
                setShow2FA(true);
            } else {
                alert(data.message || 'Failed to enable 2FA.');
            }
        } catch (error) {
            console.error("Enable 2FA failed", error);
            alert("Error enabling 2FA.");
        }
    };


    const DeleteAccount = async () => {
        if (!user) return;

        try {
            const verwijdering = await fetch(`https://localhost:7065/api/gebruikers/deleteUser/${user.id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    id: user.id,  // Use user.id directly
                }),
            });

            if (verwijdering.ok) {
                alert("Account verwijdering succesvol");
                window.location.href = '/';
            } else {
                alert("Er is een fout opgetreden bij het verwijderen van uw account");
            }
        } catch (error) {
            console.error("try is mislukt", error);
            alert("Er is een fout opgetreden");
        }
    };

    const SaveOnSubmit = async () => {
        if (!user) return;

        try {
            const verwerking = await fetch(`https://localhost:7065/api/gebruikers/updateGebruiker/${user.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Adres: adres,
                    Email: email,
                    PhoneNumber: phonenumber,
                }),
            });

            if (verwerking.ok) {
                alert('Account succesvol bijgewerkt');
                setUser({ ...user, adres, email, phonenumber }); // Update context
            } else {
                alert('Er is een fout opgetreden bij het bijwerken van uw account');
            }
        } catch (error) {
            console.error("try is mislukt", error);
            alert("Er is een fout opgetreden");
        }
    };

    if (!user) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <div className="form-overlay">
                <div className="form-content">
                    <h1>Account Settings</h1>
                    <form onSubmit={(e) => {
                        e.preventDefault();
                        if (!adres || !email || !phonenumber) {
                            alert("Voer  alle velden in");
                            return;
                        }
                        SaveOnSubmit();
                    }} >
                        <div>

                            <input
                                type="text"
                                value={adres}
                                onChange={(e) => setAdres(e.target.value)}
                                placeholder={adres}
                            >

                            </input>
                        </div>
                        <div>

                            <input
                                type="text"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                placeholder={email}
                            >

                            </input>
                        </div>
                        <div>

                            <input
                                type="text"
                                value={phonenumber}
                                onChange={(e) => setPhonenumber(e.target.value)}
                                placeholder={phonenumber}
                            >
                            </input>

                        </div>
                        <button type="submit">submit</button>
                        <button type="button" onClick={DeleteAccount}>Verwijder account</button>
                    </form>
                    <button type="button" onClick={enable2FA}>Gebruik 2FA</button>
                    {show2FA && (
                        <div>
                            <p>Scan de QR Code met jouw autheticator app:</p>
                            <QRCodeSVG value={token} size={256} level={"H"} includeMargin={true} />
                        </div>
                    )}
                </div>
            </div>
           
        </div>

        
    );
 };
      export default AccSettings;

