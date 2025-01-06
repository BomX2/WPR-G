import React, { useState } from 'react';
import './AanvraagItems.css'
import { useEffect } from 'react';
const FrontOfficeAanvraag = () => {
        const [modalWindow, setModalWindow] = useState(false);
    const [item, setItem] = useState([]);
    const [activeItem, setActiveItem] = useState(null);
    useEffect(() => {
        const fetchAanvragen = async () => {
            try {
                const response = await fetch(`https://localhost:7065/api/gebruiker/getAanvragenFront`);
                const data = await response.json();
                const geupdateData = data.map((item) => ({
                    ...item,
                }));
                setItem(geupdateData);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchAanvragen();
    }, []);
    const SetUitgaveStatus = async () => {
        try {
            const keurGoed = await fetch(`https://localhost:7065/api/gebruiker/KeurAanvraagGoed/${activeItem.id}`, {
                method: 'PUT',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify({

                    id: activeItem.id,
                    goedgekeurd: true,
                    status: "uitgegeven",
                })
            })
            if (keurGoed.ok) {
                alert("Voertuig uitgegeven!");
                CloseWindow();

            }
            else {
                alert("Er is iets fout gegaan");
            }
        }
        catch (error) {
            console.log(error);
        }
    }
    const OnButtonClick = (item) => {
        setActiveItem(item);
        setModalWindow(true);
    }
    const CloseWindow = () => {
        setModalWindow(false);
    }

    const HandelInNameAf = async () => {
        try {
            const verwijder = await fetch(`https://localhost:7065/api/gebruiker/verwijderAanvraag/${activeItem.id}`, {
                method: 'Delete',
                headers: { 'content-type': 'application/json' },

            })
            if (verwijder.ok) {
                alert("Voertuig ingenomen en transactie is gelogd.");
                setItem(prevItems => prevItems.filter(a => a.id !== activeItem.id));
                CloseWindow();
            }
            else {
                alert("Iets is fout gegaan bij het verwijderen van de aanvraag");
            }
        }
        catch (error) {
            console.log("error: ", error)
        }
    }
    return (
        <div>
            <h1>Onbehandelde huur aanvragen FrontOffice: </h1>
            <div className="aanvraagItems-layout">
                {item.map(item => (
                    <div key={item.id} className="aanvraagItems-box">
                        <h3>Aanvraag: {item.id}</h3>
                        <p>De klant: {item.persoonsGegevens}</p>
                        <button
                            onClick={() => OnButtonClick(item)}>
                            bekijk deze huuraanvraag
                        </button>
                        {modalWindow && (
                            <div className="modal-overlay">
                                <div className="modal-content">
                                    <h2>Huuraanvraag</h2>
                                    <p>De klant: {activeItem.persoonsGegevens} </p>
                                    <p>wil een {activeItem.merk}  {activeItem.type} huren in de periode van: {activeItem.startDatum} tot {activeItem.eindDatum}  </p>
                                    <p>De klant heeft de volgende persoonsgegevens voor identificatie:</p>
                                    <p> email: {activeItem.email}, telefoonnummer: {activeItem.telefoonnummer}, </p>
                                    <button onClick={() => SetUitgaveStatus()} >markeer als Uitgegeven.</button> <button onClick={() => HandelInNameAf()}>Neem voertuig in.</button>
                                    <button onClick={CloseWindow}>Sluiten</button>
                                </div>
                            </div>
                        )}
                    </div>
                ))}
            </div>
        </div>
    );
}
export default FrontOfficeAanvraag;