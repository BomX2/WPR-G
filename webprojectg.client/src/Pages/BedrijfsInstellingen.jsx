import React, { useState } from 'react'

const BedrijfsSettings = () => {
    const [emailg, setEmailg] = useState("");
    const [bDomein, setBDomein] = useState("");
    const [kvknummer, setKvknummer] = useState("")
        const VoegMedewerkersToe = async () => {
        try {
            const toevoegen = await fetch(`https://localhost:7065/api/gebruikers/AddGebruikerToBedrijf/${kvknummer}`, {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    kvknummer:  kvknummer,
                    email: emailg,
                    domeinNaam: bDomein,
                })
            })
            if (toevoegen.ok) {
                alert("Medewerker succesvoltoegevoegd");
            }
            else {
                console.log(kvknummer, emailg, bDomein);
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
                <div className="form-overlay">
                    <div className="form-content">
                        <form onSubmit={(e) => {
                            e.preventDefault();
                            if (bDomein == null || emailg == null || kvknummer == null) {
                                alert("Voer alle velden in");
                                return;
                            }
                            VoegMedewerkersToe();
                         
                        }}>
                            <input type="text" value={kvknummer} onChange={(e) => setKvknummer(e.target.value)} placeholder="Voer hier het kvknummer van uw Bedrijf in:">
                            </input>
                            <input type="text" value={bDomein} onChange={(e) => setBDomein(e.target.value)} placeholder="Voer hier het domeinnaam van uw Bedrijf in:">
                            </input>
                            <input type="text" value={emailg} onChange={(e) => setEmailg(e.target.value)} placeholder="Voer hier het emailadres van uw medewerker in:" >
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