import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const Abonnementen = () => {
    const [keuze, setKeuze] = useState('');
    const navigeren = useNavigate();
    const bedrijfsId = sessionStorage.getItem('bedrijfsId');
    if (isNaN(bedrijfsId)) {
        console.error("Invalid bedrijfsId retrieved from sessionStorage.");
    } else {
        console.log("Sending bedrijfsId to the backend:", bedrijfsId);
    }

    const PutAbonnement = async (choice) => {
    
        try {
        const wijzig = await fetch(`https://localhost:7065/api/gebruiker/putBedrijfsAbonnement/${bedrijfsId}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                Id: bedrijfsId,
                AbonnementType: choice,      
            }),
        });
            
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
                <p> u heeft gekozen voor: {keuze}</p>
                <div>
                <button onClick={() => navigeren('/Catalogus')} disabled={!keuze } >
                  ga naar de catalogus pagina
                </button>
            </div>
        </div>  
    );
}
export default Abonnementen;