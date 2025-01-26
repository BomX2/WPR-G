import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './buttons.css'
const Abonnementen = () => {
    const [keuze, setKeuze] = useState('');
    const [kvknummer, setKvknummer] = useState('');
    const [prijs, setPrijs] = useState('');
    const [betaalMethode, setBetaalMethode] = useState('');
    const [periode, setPeriode] = useState('');
    const [abonnementWijzigen, setAbonnementWijzigen] = useState(false);

    const navigeren = useNavigate();


    const PutAbonnement = async () => {

        try {
            const wijzig = await fetch(`https://localhost:7065/api/gebruikers/putBedrijfsAbonnement/${kvknummer}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    AbonnementType: keuze,
                    Prijs: prijs,
                    BetaalMethode: betaalMethode,
                    Periode: periode,
                }),
            });
            console.log(JSON.stringify({
                Kvknummer: kvknummer,
                AbonnementType: keuze,
                Prijs: prijs,
                Periode: periode,
                BetaalMethode: betaalMethode,
            }));
            if (wijzig.ok) {
                setAbonnementWijzigen(true);
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
    return (
        <div>
            <div className="buttons-overlay">
                <div className="buttons-content" >
                    <h2>kies een abonnement voor uw bedrijfsabonnement</h2>
                    <form type="submit" onSubmit={(e) => {
                        e.preventDefault();
                        if (!kvknummer) {
                            alert("voer het kvknummer veld in");
                            return;
                        }
                        PutAbonnement();
                    }}>
                        <input type="text" value={kvknummer} placeholder="Voer hier het kvknummer van uw bedrijf in:" onChange={(e) => setKvknummer(e.target.value)}></input>
                        <p value={prijs}>&euro; 400</p>
                        <button type="button" onClick={() => { setKeuze('Alles inclusief abonnement'); setPrijs(400); }}>
                            All-inclusive abonnement
                        </button>
                        <p value={prijs}>&euro; 250</p>
                        <button type="button" onClick={() => { setKeuze('standaard abonnement'); setPrijs(250); }}>
                            standaard abonnement
                        </button>
                        <h2>kies een betaalmethode</h2>
                        <button type="button" id="betaalMethode" onClick={() => { setBetaalMethode('pre-paid') }}>Pre-paid</button>
                        <button type="button" id="betaalMethode" onClick={() => { setBetaalMethode('Pay-as-you-go') }}>Pay-as-you-go</button>
                        <div className="buttons-input">

                            <select
                                placeholder="Kies de tijdsperiode"
                                value={periode}
                                onChange={(e) => setPeriode(e.target.value)}
                                required
                            >
                                <option value="een maand">een maand</option>
                                <option value="6 maanden">6 maanden</option>
                                <option value="een jaar">een jaar</option>
                            </select>
                        </div>
                        <p> U heeft gekozen voor een: {keuze} met {betaalMethode} als betaalmethode voor {periode}. </p>

                        <button id="submitButton" type="submit" disabled={!kvknummer, !keuze, !betaalMethode, !periode}>Maak abonnement aan</button>
                    </form>

                    <div>
                        <button onClick={() => navigeren('/Catalogus')} disabled={abonnementWijzigen == false} >
                            Ga naar de catalogus pagina
                        </button>
                        <button onClick={() => navigeren('/BedrijfsInstellingen')} disabled={abonnementWijzigen == false} >
                            Voeg medewerkers toe aan uw BedrijfsAccount
                        </button>
                    </div>
                </div>
            </div>

        </div>
    );
}
export default Abonnementen;