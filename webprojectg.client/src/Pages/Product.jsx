import React, {useState } from 'react';
import { useParams } from "react-router-dom";

export default function Product() {
    const [modalWindow, setModalWindow] = useState("");
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
            <button onClick={OnButtonClick} >klik hier om een huuraanvraag te maken voor deze auto</button>
            {modalWindow && (
                <div>
                        <h2>Huuraanvraag</h2>
                        <p>Vul de gegevens in voor de huuraanvraag van deze auto.</p>
                        {/* Hier kun je een formulier plaatsen */}
                    <button onClick={CloseWindow}>Sluiten</button>
             </div>
            )}
        </div>
    );
}