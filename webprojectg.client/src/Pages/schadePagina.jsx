import React, { useState } from "react";
import { useEffect } from 'react';
import './forms.css'
const SchadeRegistreren = () => {
    const schadeId = sessionStorage.getItem('SchadeId');
    const [item, setItem] = useState([]);
    useEffect(() => {
        const RoepSchadeFormulierOp = async () => {
            try {
                const RoepOp = await fetch(`https://localhost:7065/api/gebruikers/SchadeFormulier/${schadeId}`)
                const data = await RoepOp.json();
                console.log(data);

                setItem(data);
                console.log(item);
                if (RoepOp.ok) {
                    console.log("succes");
                }
                else {
                    alert("Er is een fout opgetreden")
                }
            }
            catch (error) {
                console.log("error:", error);
            }
        }
        RoepSchadeFormulierOp();

    }, [schadeId])
    const PutSchadeFormulier = async () => {
        try {
            const PutFormulier = await fetch(`https://localhost:7065/api/gebruikers/PutSchadeForm/${schadeId}`, {
                method: 'PUT',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify({
                    SchadeType: item.schadeType,
                    ErnstVDSchade: item.ErnstVDSchade   ,
                    Kenteken: item.kenteken,
                    Email: item.email,
                    Telefoonnummer: item.telefoonnummer,
                    AanvraagId: item.aanvraagId,
                })
        
            })
            console.log(item);
            if (PutFormulier.ok) {
                alert("Schadeformulier succesvol opgeslagen!")
            }
            else {
                alert("Er is iets misgegaan bij het opslaan van het schadeformulier")
            }
        }
        catch (error) {
            console.log(error);
        }
    }
    return (
        <div>
            <h1>Schadeformulier pagina</h1>

            <div className="form-overlay">
                <div className="form-content">
                    <form onSubmit={(e) => {
                        e.preventDefault();
                        PutSchadeFormulier();

                    }}>
                        <div>
                            <h1>SchadeFormulier</h1>
                            <h2>klant: {item.email}</h2>
                            <h2>auto-kenteken: {item.kenteken}</h2>
                            <select
                                placeholder="Kies het type schade"
                                value={item.schadeType}
                                onChange={(e) => setItem({ ...item, schadeType: e.target.value})}
                                required
                            >
                                <option value="" disabled>
                                kies het type schade
                                </option>
                                <option value="beschadigde ruit(en)">beschadigde ruit(en)</option>
                                <option value="kras(sen)">kras(sen)</option>
                                <option value="deuk(en)">deuk(en)</option>
                                <option value="lek-wiel">lek-wiel</option>
                                <option value="interieur beschadiging(en)">interieur beschadiging(en)</option>
                                <option value="interne onderdelen beschadigd">interne onderdelen beschadiging(en)</option>
                                <option value="motor-schade">motor-schade</option>
                                <option value="alle types schade">alle types schade</option>
                            </select>
                        </div>
                        <div>
                            <select
                                value={item.ErnstVDSchade}
                                onChange={(e) => setItem({ ...item, ErnstVDSchade: e.target.value })}
                                required
                            >
                                <option value="" disabled>
                                    Selecteer de ernst van de schade
                                </option>
                                <option value="Type: 1, zeer lichte schade">Type: 1, zeer lichte schade</option>
                                <option value="Type: 2, lichte schade">Type: 2, lichte schade</option>
                                <option value="Type: 3, gemiddelde schade">Type: 3, gemiddelde schade</option>
                                <option value="Type: 4, zware schade">Type: 4, zware schade</option>
                                <option value="Type: 5, total loss">Type: 5, total loss</option>
                            </select>
                        </div>
                        <button disabled={!item.schadeType, !item.ErnstVDSchade } type="submit">Klik hier om het formulier op te slaan.</button>
                    </form>
                </div>
            </div>

        </div>

    )
}
export default SchadeRegistreren;