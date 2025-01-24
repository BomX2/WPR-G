import React, { useState } from 'react';
import './forms.css';
import { useUser } from '../componements/userContext';
import { QRCodeSVG } from 'qrcode.react';

const AccSettings = () => {
    const { user, setUser } = useUser();
    const [adres, setAdres] = useState(user?.adres || '');
    const [email, setEmail] = useState(user?.email || '');
    const [phonenumber, setPhonenumber] = useState(user?.phonenumber || '');

    const [show2FA, setShow2FA] = useState(false);
    const [otpAuthUrl, setOtpAuthUrl] = useState(''); // We'll store the otpauth url here
    const [twoFaCode, setTwoFaCode] = useState('');   // Input field for verifying 2FA
    const [isTwoFactorEnabled, setIsTwoFactorEnabled] = useState(user?.twoFactorEnabled || false);

    // Enable 2FA
    const enable2FA = async () => {
        try {
            const response = await fetch('https://localhost:7065/api/gebruikers/enable-2fa', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                    // No Authorization header since we're using cookies
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

    // Disabled 2FA
    const disable2FA = async () => {
        try {
            // This assumes you have an endpoint like:
            // POST /api/gebruikers/disable-2fa
            const response = await fetch('https://localhost:7065/api/gebruikers/disable-2fa', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                credentials: 'include'
            });
            const data = await response.json();
            if (response.ok) {
                alert("2FA disabled!");
                setIsTwoFactorEnabled(false);
                // Optionally clear any leftover 2FA UI
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

    // Verify 2FA
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
                // Optionally, you can hide the QR code after success
                setShow2FA(false);
            } else {
                alert(data.message || "Invalid 2FA code.");
            }
        } catch (error) {
            console.error("Error verifying 2FA code:", error);
            alert("Error verifying 2FA code.");
        }
    };

    // Delete account
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

    // Save updated info
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
        <div
            className="form-overlay"
            style={{
                maxHeight: '90vh',
                overflowY: 'auto',
                padding: '20px'
            }}
        >
            <div className="form-content">
                <h1>Account Settings</h1>
                <form onSubmit={(e) => {
                    e.preventDefault();
                    if (!adres || !email || !phonenumber) {
                        alert("Voer alle velden in");
                        return;
                    }
                    SaveOnSubmit();
                }}>
                    <div>
                        <input
                            type="text"
                            value={adres}
                            onChange={(e) => setAdres(e.target.value)}
                            placeholder="Adres"
                        />
                    </div>
                    <div>
                        <input
                            type="text"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            placeholder="Email"
                        />
                    </div>
                    <div>
                        <input
                            type="text"
                            value={phonenumber}
                            onChange={(e) => setPhonenumber(e.target.value)}
                            placeholder="Telefoonnummer"
                        />
                    </div>
                    <button type="submit">submit</button>
                    <button type="button" onClick={DeleteAccount}>Verwijder account</button>
                </form>

                {/* Conditionally render "Enable 2FA" or "Disable 2FA" */}
                {!isTwoFactorEnabled ? (
                    <button type="button" onClick={enable2FA}>
                        Enable 2FA
                    </button>
                ) : (
                    <button type="button" onClick={disable2FA}>
                        Disable 2FA
                    </button>
                )}

                {/* Show the QR code and a prompt to verify if user is enabling 2FA */}
                {show2FA && !isTwoFactorEnabled && (
                    <div style={{ marginTop: '20px' }}>
                        <p>Scan de QR Code met jouw authenticator app:</p>
                        {otpAuthUrl && (
                            <QRCodeSVG value={otpAuthUrl} size={256} level="H" includeMargin={true} />
                        )}
                        <div style={{ marginTop: '10px' }}>
                            <label>Voer de 6-cijferige code in:</label>
                            <input
                                type="text"
                                value={twoFaCode}
                                onChange={(e) => setTwoFaCode(e.target.value)}
                                placeholder="123456"
                            />
                            <button onClick={verify2FA}>Verifieer Code</button>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
};

export default AccSettings;
