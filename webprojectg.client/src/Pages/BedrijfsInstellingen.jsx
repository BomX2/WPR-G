import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import './forms.css'
import './GebruikersLijst.css'
const BedrijfsSettings = () => {
    const [emailg, setEmailg] = useState("");
    const [item, setItem] = useState([]);
    const [kvknummer, setKvknummer] = useState("");
    const navigeren = useNavigate();
        const VoegMedewerkersToe = async () => {
        try {
            const toevoegen = await fetch(`https://localhost:7065/api/gebruikers/AddGebruikerToBedrijf/${kvknummer}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    kvkNummer: kvknummer,
                    email: emailg,
                })
            })
            if (toevoegen.ok) {
                alert("Medewerker succesvol toegevoegd!");
            }
            else {
                console.log(kvknummer, emailg);
                alert("er is een fout opgetreden");
                console.log("lichte fout geen catch");
            }

        }
        catch (error) {
            console.log("catch: Dit ging fout: ", error);
        }
    }
    const GeefMedewerkersWeer = async () => {
        try {
            const GeefWeer = await fetch(`https://localhost:7065/api/gebruikers/LaatGebruikersZien/${kvknummer}`)
            const data = await GeefWeer.json();
           
            setItem(data);
            
            if (GeefWeer.ok) {
                console.log("succesvol geladen!")
            }
            else {
               alert("Iets ging fout")
            }
        }
        catch (error) {
            console.log("error: ", error)
        }   
    }
    const VerwijderMedewerker = async (email) => {
        try {
            const verwijder = await fetch(`https://localhost:7065/api/gebruikers/VerwijderMedewerker/${email}`, {
                method: 'DELETE',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify({
                    kvknummer: kvknummer,
                }),
            })

            if (verwijder.ok) {
                alert("medewerker succesvol verwijderd");
                setItem(items => items.filter(item => item !== email));
            }
            else {
                alert("Er is iets fout gegaan")
            }
        
        }
        catch (error) {
            console.log(error);
        }
    }
        return (
            <div>
                <div className="lijst-overlay">
                    <div className="lijst-content">
                        <h2>Geregistreerde medewerkers</h2>
                        
                        {item.map((email, index) => (
                            <div key={index} className="lijst-item">
                                {email}
                                <button onClick={() => VerwijderMedewerker(email) }>verwijder</button>
                            </div>

                        ))}
   
                    </div>
                        
                </div>
                <div className="form-overlay">
                    <div className="form-content">
                        <h1>BedrijfsInstellingen</h1>
                        <p>
                            voer hieronder het emailadres van uw medewerkers in die u wilt toevoegen aan uw bedrijfsAbonnement:
                        </p>
                       
                        <form onSubmit={(e) => {
                            e.preventDefault();
                            if (!kvknummer || !emailg) {
                                alert("voer alle velden in") 
                                return;
                            }
                            VoegMedewerkersToe();
                        }}>
                            <input type="text" value={kvknummer} onChange={(e) => setKvknummer(e.target.value)} placeholder="Voer hier het kvknummer van uw bedrijf in:">
                            </input>
                            <input type="email" value={emailg} onChange={(e) => setEmailg(e.target.value)} placeholder="Voer hier het emailadres van uw medewerker in:" >
                            </input>
                            <button type="button" onClick={() => {
                                if (!kvknummer) {
                                    alert("Voer een kvknummer in!");
                                    return;
                                }
                                GeefMedewerkersWeer()
                            }}>Geef medewerkers weer</button>
                            <button type="submit" label="knop: Voeg medewerker toe">
                                Voeg medewerker toe
                            </button>
                        </form>
                        <button onClick={() => navigeren('/Abonnement') }>Ga naar abonnement pagina</button>
                    </div>
                </div>
                
            </div>
        );
    };

export default BedrijfsSettings;