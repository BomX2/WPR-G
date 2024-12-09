import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
const Abonnementen = () => {
    const [keuze, setKeuze] = useState('');
    const navigeren = useNavigate();
        const verwerkKeuze = (optie) => {
            setKeuze(optie);
         }
    
    return (
        <div>
            <h1>Abonnement pagina</h1>
            <h2>kies een abonnement voor uw bedrijfsabonnement</h2>
            <button onClick={() => verwerkKeuze('pay-as-you-go')}>
               pay as you go
            </button>
            <button onClick={() => verwerkKeuze('pre-paid')}>
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