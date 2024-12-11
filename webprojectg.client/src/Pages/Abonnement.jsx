import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const Abonnementen = () => {
    const [keuze, setKeuze] = useState('');
    const navigeren = useNavigate();
const PutAbonnement = async () => {

        try {
            const wijzig = await fetch('https://7065/api/gebruiker/putbedrijf', {
                method: 'PUT',
                headers: { 'Content-Type': 'application.Json' },
                body: JSON.stringify({
                })
            }
            )

            if (wijzig.ok) {
                alert("abonnementkeuze succesvol opgeslagen");
                sessionStorage.getItem.id;
            }
            else {
                alert("er is iets foutgegaan bij het opslaan van uw abonnementskeuze");
            }
        }
        
        catch (error) {
            console.log("dit ging er fout:", error);
        }
    }
    return (
        <div>
            <h1>Abonnement pagina</h1>
            <h2>kies een abonnement voor uw bedrijfsabonnement</h2>
            <button onClick={() => PutAbonnement('pay-as-you-go'), setKeuze(keuze)}>
               pay as you go
            </button>
            <button onClick={() => PutAbonnement('pre-paid'), setKeuze(keuze)}>
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