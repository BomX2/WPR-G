import React, { useState } from 'react'
const BedrijfsSettings = () => {
    const [emailg, setEmailg] = useState("");
    const bedrijfsId = sessionStorage.getItem('bedrijfsId')
        const VoegMedewerkersToe = async () => {
        try {
            const toevoegen = await fetch(`https://localhost:7065/api/Gebruiker/AddGebruikerTo/${bedrijfsId}`, {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Id:  bedrijfsId,
                    email: emailg,
                })
            })
            if (toevoegen.ok) {
                alert("Medewerker succesvol toegevoegd");
            }
            else {
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
                <h1>BedrijfsInstellingen</h1>
                <p>
                    voer hieronder het emailadres van uw medewerkers in die u wilt toevoegen aan uw bedrijfsAbonnement:
                </p>
                <form onSubmit={(e) => {
                    e.preventDefault
                    VoegMedewerkersToe();
                }}>
                    <input type="text" value={emailg} onChange={(e) => setEmailg(e.target.value)} placeholder="Voer hier het emailadres van uw medewerker in:" >  
                    </input>
                    <button type="submit" label="knop: Voeg medewerker toe">
                        Voeg medewerker toe
                    </button>
                </form>
            </div>
        );
    };

export default BedrijfsSettings;