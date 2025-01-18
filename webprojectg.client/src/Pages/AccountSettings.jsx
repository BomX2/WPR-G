import React, { useEffect, useState } from 'react';
import './forms.css'
const AccSettings = () => {
    const [adres, setAdres] = useState('');
    const [email, setEmail] = useState('');
    const [telefoonnummer, setTelefoonnummer] = useState('');
    const userId = sessionStorage.getItem("UserId");

    useEffect => 
  const DeleteAccount = async () => {
          
        try {
            const verwijdering = await fetch(`https://localhost:7065/api/gebruikers/deleteUser/${userId}`,{
                
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    id: userId,
                }),
            });
            if (verwijdering.ok) {
                alert("Account verwijdering succesvol");
                window.location.href = '/';
            }
            else {
                alert("Er is een fout opgetreden bij het verwijderen van uw account");
            }
        }
        catch (error) {
            console.error("try is mislukt", error);
            alert("Er is een fout opgetreden");
        }
    };

    const SaveOnSubmit = async () => {
        try {
            const verwerking = await fetch(`https://localhost:7065/api/gebruikers/updateGebruiker/${userId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Adres: adres, 
                    Email: email, 
                    PhoneNumber: telefoonnummer,
                        
                }),
            });

            if (verwerking.ok) {
                alert('Account succesvol bijgewerkt');
                console.log(userId);    
            } else {
                console.log(userId);
                alert('Er is een fout opgetreden bij het bijwerken van uw account');
            }
        } catch (error) {
            console.error("try is mislukt", error);
            alert("Er is een fout opgetreden");
        }
    };
    return (
        <div>
            <div className="form-overlay">
                <div className="form-content">
                    <h1>Account Settings</h1>
                    <form onSubmit={(e) => {
                        e.preventDefault();
                        if (!adres || !email || !telefoonnummer) {
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
                                placeholder="verander uw adres"
                            >

                            </input>
                        </div>
                        <div>

                            <input
                                type="text"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                placeholder="verander uw email adres"
                            >

                            </input>
                        </div>
                        <div>

                            <input
                                type="text"
                                value={telefoonnummer}
                                onChange={(e) => setTelefoonnummer(e.target.value)}
                                placeholder="verander uw telefoonnummer"
                            />


                        </div>
                        <button type="submit">submit</button>
                        <button type="button" onClick={DeleteAccount}>Verwijder account</button>
                    </form>
                </div>
            </div>
           
        </div>

        
    );
 };
      export default AccSettings;

