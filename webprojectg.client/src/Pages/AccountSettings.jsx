import React, { useState } from 'react';


const AccSettings = () => {
    const [naam, setNaam] = useState('');
    const [adres, setAdres] = useState('');
    const [email, setEmail] = useState('');
    const [telefoonnummer, setTelefoonnummer] = useState('');

    const DeleteAccount = async () => {

        try {
            const verwijdering = await fetch('https://localhost:7065/api/klant/delete', {
                
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    naam,
                    adres,
                    telefoonnummer,
                    email,
                }),
            });
            if (verwijdering.ok) {
                alert("Account succesvol verwijdering");
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
            const verwerking = await fetch('https://localhost:7065/api/klant/update', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    naam,
                    adres,
                    telefoonnummer,
                    email,
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
    };
    return (
        <div>
            <h1>Account Settings</h1>
            <form onSubmit={(e) => {
                e.preventDefault();
                SaveOnSubmit()
            }} >
                <div>
                    <input
                        type="text"
                        value={naam}
                           onChange={(e) => setNaam(e.target.value)}
                        placeholder="verander uw naam"
                    >
                     
                    </input>
                </div>
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
                        type="tel"
                        value={telefoonnummer}
                        onChange={(e) => setTelefoonnummer(e.target.value)}
                        placeholder="verander uw telefoonnummer"
                    />

                   
                </div>
                <button type="submit">submit</button>
                <button type="button" onClick={DeleteAccount}>Verwijder account</button>
            </form>
        </div>

        
    );
 };
      export default AccSettings;

