import React, { useState } from 'react';
import './Registratie.css';
import { useUser } from '../componements/userContext';
import { QRCodeSVG } from 'qrcode.react';

const AccSettings = () => {
    const { user, setUser } = useUser();
    const [adres, setAdres] = useState(user?.adres || '');
    const [email, setEmail] = useState(user?.email || '');
    const [phonenumber, setPhonenumber] = useState(user?.phonenumber || '');

    const [show2FA, setShow2FA] = useState(false);
    const [otpAuthUrl, setOtpAuthUrl] = useState('');
    const [twoFaCode, setTwoFaCode] = useState('');
    const [isTwoFactorEnabled, setIsTwoFactorEnabled] = useState(user?.twoFactorEnabled || false);

    const enable2FA = async () => {
        try {
            const response = await fetch('https://localhost:7065/api/gebruikers/enable-2fa', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include'
            });
            const data = await response.json();
            if (response.ok) {
                setOtpAuthUrl(data.otpAuthUrl);
                setShow2FA(true);
            } else {
                alert(data.message || 'Failed to enable 2FA.');
            }
        } catch (error) {
            console.error("Enable 2FA failed", error);
            alert("Error enabling 2FA.");
        }
    };

    const disable2FA = async () => {
        try {
            const response = await fetch('https://localhost:7065/api/gebruikers/disable-2fa', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                credentials: 'include'
            });
            const data = await response.json();
            if (response.ok) {
                alert("2FA disabled!");
                setIsTwoFactorEnabled(false);
                setShow2FA(false);
                setTwoFaCode('');
                setOtpAuthUrl('');
            } else {
                alert(data.message || "Failed to disable 2FA.");
            }
        } catch (error) {
            console.error("Error disabling 2FA:", error);
            alert("Error disabling 2FA.");
        }
    };

    const verify2FA = async () => {
        try {
            const response = await fetch('https://localhost:7065/api/gebruikers/verify-2fa', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include',
                body: JSON.stringify({ token: twoFaCode })
            });
            const data = await response.json();
            if (response.ok) {
                alert("2FA is now enabled!");
                setShow2FA(false);
            } else {
                alert(data.message || "Ongelidge 2FA code.");
            }
        } catch (error) {
            console.error("Error verifying 2FA code:", error);
            alert("Error verifying 2FA code.");
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
                body: JSON.stringify({ id: user.id })
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
                setUser({ ...user, adres, email, phonenumber });
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
        <div className="container">
            <div className="header">
                <div className="text">Accountinstellingen</div>
            </div>
            <form className="inputs" onSubmit={SaveOnSubmit}>
                <div className="input">
                    <input
                        type="text"
                        value={adres}
                        onChange={(e) => setAdres(e.target.value)}
                        placeholder="Adres"
                    />
                </div>
                <div className="input">
                    <input
                        type="text"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        placeholder="E-mailadres"
                    />
                </div>
                <div className="input">
                    <input
                        type="text"
                        value={phonenumber}
                        onChange={(e) => setPhonenumber(e.target.value)}
                        placeholder="Telefoonnummer"
                    />
                </div>
                <div className="submit-container">
                    <button type="submit" className="buttons">
                        Opslaan
                    </button>
                    <button type="button" className="buttons" onClick={DeleteAccount}>
                        Verwijder Account
                    </button>
                </div>
            </form>
            <div className="submit-container">
                {!isTwoFactorEnabled && !show2FA && (
                    <button className="buttons" type="button" onClick={enable2FA}>
                        Schakel 2FA in
                    </button>
                )}
                {isTwoFactorEnabled && (
                    <button
                        className="buttons"
                        type="button"
                        onClick={disable2FA}
                        style={{ backgroundColor: '#e74c3c', color: '#ffffff' }}
                    >
                        Schakel 2FA uit
                    </button>
                )}
            </div>

            {show2FA && (
                <div className="qr-container">
                    <p>Scan de QR-code met uw authenticator-app:</p>
                    {otpAuthUrl && (
                        <div className="qr-code">
                            <QRCodeSVG value={otpAuthUrl} size={256} />
                        </div>
                    )}
                    <div className="inputs">
                        <div className="input">
                            <input
                                type="text"
                                value={twoFaCode}
                                onChange={(e) => setTwoFaCode(e.target.value)}
                                placeholder="Voer 2FA-code in"
                            />
                        </div>
                        <div className="submit-container">
                            <button className="buttons" onClick={verify2FA}>
                                Verifieer Code
                            </button>
                            <button
                                className="buttons cancel-button"
                                onClick={() => {
                                    setShow2FA(false);
                                    setOtpAuthUrl('');
                                    setTwoFaCode('');
                                }}
                            >
                                Annuleer
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default AccSettings;
