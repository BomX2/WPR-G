import React, { useState } from 'react';
import  './AanvraagItems.css'
import { useEffect } from 'react';
const AanvraagBackOffice = () => {
    const [modalWindow, setModalWindow] = useState(false);
    const [item, setItem] = useState([]);
    const [activeItem, setActiveItem] = useState(null);
    useEffect(() => {
        const fetchAanvragen = async () => {
            try {
                const response = await fetch(`https://localhost:7065/api/gebruikers/getAanvragen`);
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
    const OnButtonClick = (item) => {
        setActiveItem(item);
        setModalWindow(true);
    }
    const CloseWindow = () => {
        setModalWindow(false);
    }
    const PasAanvraagAan = async () => {
        try {
            const keurGoed = await fetch(`https://localhost:7065/api/gebruikers/KeurAanvraagGoed/${activeItem.id}`, {
                method: 'PUT',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify({
                  
                    id: activeItem.id,               
                    goedgekeurd: true, 
                })
            })
            if (keurGoed.ok) {
                alert("Aanvraag goedgekeurd");
                setItem(prevItems => prevItems.filter(a => a.id !== activeItem.id));
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
    const VerwijderAanvraag = async () => {
        try {
            const verwijder = await fetch(`https://localhost:7065/api/gebruikers/verwijderAanvraag/${activeItem.id}`, {
                method: 'Delete',
                headers: { 'content-type': 'application/json' },
             
            })
            if (verwijder.ok) {
                alert("Aanvraag verwijderd.");
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
            <h1>Onbehandelde huur aanvragen BackOffice:</h1>
            <div className="aanvraagItems-layout">
                {item.map(item => (
                    <div key={item.id} className="aanvraagItems-box">
                        <h3>Aanvraag: {item.id}</h3>
                        <p>De klant: {item.persoonsGegevens}</p>
                        <button
                            onClick={() => OnButtonClick(item)}>
                            bekijk deze huuraanvraag
                        </button>
                        {modalWindow &&  (
                            <div className="modal-overlay">
                                <div className="modal-content">
                                    <h2>Huuraanvraag</h2>
                                    <p>De klant: {activeItem.persoonsGegevens} </p>
                                    <p>Wil een {activeItem.autoMerk} {activeItem.autoType} huren in de periode van: {activeItem.startDatum} tot {activeItem.eindDatum}  </p>
                                    <button onClick={() => PasAanvraagAan()}>accepteer aanvraag</button> <button onClick={() => VerwijderAanvraag()}> verwijder aanvraag</button>
                                    <button onClick={CloseWindow}>Sluiten</button>
                                </div>
                            </div>
                        )}
                    </div>
                ))}
            </div>
        </div>
    );
};

export default AanvraagBackOffice;