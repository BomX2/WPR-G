import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './forms.css'
const RegistreerBedrijf = () => {
    const [BedrijfsNaam, setBedrijfsNaam] = useState("");
    const [adres, setAdres] = useState("");
    const [kvknummer, setKvknummer] = useState("");
    const [domeinNaam, setDomeinNaam] = useState("");
    const navigeren = useNavigate();

    const BedrijfToevoegen = async () => {
        try { 
            const Toevoegen = await fetch('https://localhost:7065/api/gebruikers/postbedrijf', {
                method: 'POST', headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    BedrijfsNaam,
                    adres,
                    kvknummer,
                    domeinNaam,
                }),

            });
            if (Toevoegen.ok) {
                alert("Bedrijfs account succesvol toegevoegd.");
                const data = await Toevoegen.json();
                const bedrijfsKvknummer = data.kvkNummer 
                sessionStorage.setItem('bedrijfsKvknummer', bedrijfsKvknummer);
                navigeren('/Abonnement');
            }
            else {
                alert("er is een fout opgetreden bij het aanmaken van een bedrijfs account");
            }
        }

        catch (error) {
            console.log("Houston we have a problem: ", error);
        }
    }
    return (
        <div>
            <div className="form-overlay">
                <div className="form-content">
                    <h1>Bedrijfsregistratiepagina</h1>
                    <form onSubmit={(e) => {
                        e.preventDefault();
                        if (!BedrijfsNaam || !kvknummer || !adres || !domeinNaam) {
                            alert("Alle velden dienen worden ingevuld");
                            return;
                        }
                        BedrijfToevoegen();
                    }} >
                        <div>
                            <input type="text" value={BedrijfsNaam}
                                onChange={(e) => setBedrijfsNaam(e.target.value)}
                                placeholder="Voer de naam van uw bedrijf in:" >
                            </input>
                            <div>
                                <input type="text" value={adres}
                                    onChange={(e) => setAdres(e.target.value)}
                                    placeholder="Voer het adres van uw bedrijf in:" >
                                </input>
                            </div>
                        </div>
                        <div>
                            <input type="text" value={kvknummer}
                                onChange={(e) => setKvknummer(e.target.value)}
                                placeholder="Voer het kvknummer van uw bedrijf in:" >
                            </input>
                        </div>
                        <div>
                            <input type="text" value={domeinNaam}
                                onChange={(e) => setDomeinNaam(e.target.value)}
                                placeholder="Voer het domeinnaam van uw bedrijf in:"></input>
                        </div>
                        <button type="submit">bedrijfsaccount aanmaken</button>
                    </form>
                </div>
            </div>
          
        </div>
    );
}
export default RegistreerBedrijf;