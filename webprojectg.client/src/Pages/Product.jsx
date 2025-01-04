import React, { useState } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { useParams } from "react-router-dom";
import './modal.css';
export default function Product() {
    const [modalWindow, setModalWindow] = useState(false);
    const [begindatum, setBeginDatum] = useState(null);
     const [eindDatum, setEindDatum] = useState(null);
    const [naam, setNaam] = useState("");
    const [email, setEmail] = useState("");
    const [telnummer, setTelNummer] = useState("")
    const { id } = useParams()
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
                selected={begindatum}
                onChange={(date) => setBeginDatum(date)}
                selectsStart
                startDate={begindatum}
                endDate={eindDatum}
                dateFormat="dd/MM/yyyy"
            />
            <DatePicker
                selected={eindDatum}
                onChange={(date) => setEindDatum(date)}
                selectsEnd
                startDate={begindatum}
                endDate={eindDatum}
                minDate={begindatum}
                dateFormat="dd/MM/yyyy"
                disabled={!begindatum }
            />
            <button onClick={OnButtonClick} disabled={!begindatum || !eindDatum} >klik hier om een huuraanvraag te maken voor deze auto</button>
            {modalWindow && (
                <div className="modal-overlay">
                <div className="modal-content" >
                        <h2>Huuraanvraag</h2>
                        <p>Vul de gegevens in voor de huuraanvraag van deze auto.</p>
                        <form>
                            <input type="text" value={naam} placeholder="Voer hier uw voornaam en achternaam in." onChange={(e) => setNaam(e.target.value)}>
                            </input>
                            <input type="email" value={email} placeholder="Voer hier uw emailadres in." onChange={(e) => setEmail(e.target.value)}>
                            </input>
                            <input type="tel" value={telnummer} placeholder="Voer hier uw telefoonnummer in." onChange={(e) => setTelNummer(e.target.value)}>
                            </input>
                           
                        </form>
                   
                    <button type="submit">verstuur huuraanvraag</button>
                    <button onClick={CloseWindow}>Sluiten</button>
                    </div>
                </div>
            )}
        </div>
    );
}