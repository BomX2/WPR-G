import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const Abonnementen = () => {
    const [keuze, setKeuze] = useState('');
    const navigeren = useNavigate();
    const bedrijfsKvknummer = sessionStorage.getItem('bedrijfsKvknummer');
  
    const PutAbonnement = async (choice) => {
    
        try {
        const wijzig = await fetch(`https://localhost:7065/api/gebruikers/putBedrijfsAbonnement/${bedrijfsKvknummer}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Kvknummer: bedrijfsKvknummer,
                    AbonnementType: choice,
               
            }),
        });
            console.log(JSON.stringify({
                Kvknummer: bedrijfsKvknummer,
                AbonnementType: choice,
            }));
        if (wijzig.ok) {
            alert("abonnementkeuze succesvol opgeslagen");
        }
        else {
            alert("iets ging fout")
        }
    }
        catch (error) {
            console.log("dit ging er fout:", error);
        }
    }
    const Update = (choice) => {
        setKeuze(choice);
        PutAbonnement(choice);
    }
    return (
        <div>
            <h1>Abonnement pagina</h1>
            <h2>kies een abonnement voor uw bedrijfsabonnement</h2>
            <button onClick={() => Update('pay-as-you-go')}>
               pay as you go
            </button>
            <button onClick={() => Update('pre-paid')}>
                    pre-paid
                </button>
                <p> U heeft gekozen voor: {keuze}</p>
                <div>
                <button onClick={() => navigeren('/Catalogus')} disabled={!keuze } >
                  Ga naar de catalogus pagina
                </button>
                <button onClick={() => navigeren('/BedrijfsInstellingen')} disabled={!keuze} >
                 Voeg medewerkers toe aan uw BedrijfsAccount
                </button>
            </div>
        </div>  
    );
}
export default Abonnementen;