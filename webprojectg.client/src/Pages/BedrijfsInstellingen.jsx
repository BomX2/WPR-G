import React, { useState } from 'react'
import './forms.css'
const BedrijfsSettings = () => {
    const [emailg, setEmailg] = useState("");
    const [kvknummer, setKvknummer] = useState("");
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
        return (
            <div>
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
                            <button type="submit" label="knop: Voeg medewerker toe">
                                Voeg medewerker toe
                            </button>
                        </form>
                    </div>
                </div>
                
            </div>
        );
    };

export default BedrijfsSettings;