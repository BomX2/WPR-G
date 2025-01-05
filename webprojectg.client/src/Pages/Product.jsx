import React, { useState } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { useParams } from "react-router-dom";
import './modal.css';
export default function Product() {
    const [modalWindow, setModalWindow] = useState(false);
    const [beginDatum, setBeginDatum] = useState(null);
    const [eindDatum, setEindDatum] = useState(null);
    const [naam, setNaam] = useState("");
    const [email, setEmail] = useState("");
    const [telnummer, setTelNummer] = useState("")
    const { id } = useParams()
    const HandleAanvraag = async () => {
        try {
            const formatDateToUTC = (date) => {
                const utcDate = new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate()));
                return utcDate.toISOString().split('T')[0];
            };
            const PostAanvraag = await fetch(`https://localhost:7065/api/gebruiker/postAanvraag`, {
                method: 'POST',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify({
                    startDatum: formatDateToUTC(beginDatum),
                    eindDatum: formatDateToUTC(eindDatum),
                    persoonsGegevens: naam,
                    email: email,
                    telefoonnummer: telnummer,
                    autoId: id,
                })
            })
            if (PostAanvraag.ok) {
                alert("Aanvraag succesvol aangemaakt");
                CloseWindow();
            }
            else {
                alert("Er is iets fout gegaan");
            }
        }
        
        catch (error) {
            console.log("er was een fout: ", error)
        }

    }
    const OnButtonClick = () => {   
        setModalWindow(true);
    }
    const  CloseWindow = () => {
        setModalWindow(false);
    }
    return (
        <div>
            <h1>catalogus nr {id}</h1>
            <DatePicker 
                selected={beginDatum}
                onChange={(date) => setBeginDatum(date)}
                selectsStart
                startDate={beginDatum}
                endDate={eindDatum}
                dateFormat="dd/MM/yyyy"
            />
            <DatePicker
                selected={eindDatum}
                onChange={(date) => setEindDatum(date)}
                selectsEnd
                startDate={beginDatum}
                endDate={eindDatum}
                minDate={beginDatum}
                dateFormat="dd/MM/yyyy"
                disabled={!beginDatum }
            />
            <button onClick={OnButtonClick} disabled={!beginDatum || !eindDatum} >klik hier om een huuraanvraag te maken voor deze auto</button>
            {modalWindow && (
                <div className="modal-overlay">
                <div className="modal-content" >
                        <h2>Huuraanvraag</h2>
                        <p>Vul de gegevens in voor de huuraanvraag van deze auto.</p>
                        <form onSubmit={(e) => {
                            e.preventDefault(); if (!naam || !email || !telnummer) {
                                alert("Voer alle gegevens in.");
                                return;
                            }
                            HandleAanvraag()
                        }}>
                            <input type="text" value={naam} placeholder="Voer hier uw voornaam en achternaam in." onChange={(e) => setNaam(e.target.value)}>
                            </input>
                            <input type="email" value={email} placeholder="Voer hier uw emailadres in." onChange={(e) => setEmail(e.target.value)}>
                            </input>
                            <input type="tel" value={telnummer} placeholder="Voer hier uw telefoonnummer in." onChange={(e) => setTelNummer(e.target.value)}>
                            </input>
                            <button type="submit">verstuur huuraanvraag</button>

                        </form>
                   
                    <button onClick={CloseWindow}>Sluiten</button>
                    </div>
                </div>
            )}
        </div>
    );
}