import React, { useState } from 'react';
import  './AanvraagItems.css'
import { useEffect } from 'react';
const AanvraagBackOffice = () => {
    const [modalWindow, setModalWindow] = useState(false);
    const [modalType, setModalType] = useState(null);
    const [item, setItem] = useState([]);
    const [sItem, SetsItem] = useState([]);
    const [activeItem, setActiveItem] = useState(null);
    useEffect(() => {
        const fetchAanvragen = async () => {
            try {
                const response = await fetch(`https://localhost:7065/api/gebruikers/getAanvragen`);
                const data = await response.json();
                console.log(data); 
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
    const OnButtonClick = (item, type) => {
        setActiveItem(item);
        setModalType(type);
        setModalWindow(true);
    }
   
    const CloseWindow = () => {
        setModalWindow(false);
        setModalType(null);
        setActiveItem(null);
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
    useEffect(() => {
        const GetSchadeForms = async () => {
            try {
                const GetSchade = await fetch(`https://localhost:7065/api/gebruikers/GetSFormulieren`)
                const data = await GetSchade.json();
                console.log("Schadeformulieren: ", data);
                SetsItem(data);
                if (GetSchade.ok) {
                    console.log("Schadeformulieren zijn correct geladen")
                }
                else {
                    alert("Schadeformulieren zijn incorrect geladen")
                }
            }
            catch (error) {
                console.log(error);
            }
        };
    GetSchadeForms();
    }, []);
    const VerwijderSchadeformulier = async () => {
        try {
            const verwijderS = await fetch(`https://localhost:7065/api/gebruikers/SchadeformVerwijder/${activeItem.id}`, {
                method: 'Delete',
                headers: { 'content-type': 'application/json' },
            });

            if (verwijderS.ok) {
                alert("Schadeformulier succesvol verwijderd!");
                SetsItem(prevItems => prevItems.filter(a => a.id !== activeItem.id));

            }
            else {
                alert("Iets ging fout bij het verwijderen van het schadeformulieren");
               
            }
        }
        catch (error) {
            console.log("error bij verwijdering:", error)
        }
        
    }
    const ZetOpGerepareerd = async () => {
        try {
            const RepareerAuto = await fetch(`https://localhost:7065/api/voertuigen/RepareerVoertuig/${activeItem.kenteken}`, {
                method: 'PUT',
                headers: { 'content-type': 'application/json' },
                
            })
            console.log(activeItem.kenteken);
            if (RepareerAuto.ok) {
                alert("Auto succesvol geregistreerd als Gerepareerd");
                VerwijderSchadeformulier();
            }
            else {
                alert("Er is iets fout gegaan bij de repareerstatus");
            }
        }
        catch (error){
            console.log("Error bij repareerstatus: ", error)
        }
    }

    return (
        <div>
            <h1>Onbehandelde huur aanvragen BackOffice:</h1>
            <div className="aanvraagItems-layout">
            <div className="bovenste-rij">
                {item.map(item => (
                    <div key={item.id} className="aanvraagItems-box">
                        <h3>Aanvraag: {item.id}</h3>
                        <p>De klant: {item.email}</p>
                        <button
                            onClick={() => OnButtonClick(item, 'aanvraag')}>
                            bekijk deze huuraanvraag
                        </button>
                        {modalType === 'aanvraag' && (
                            <div className="modal-overlay">
                                <div className="modal-content">
                                    <h2>Huuraanvraag</h2>
                                    <p>De klant: {activeItem.email} </p>
                                    <p>Wil een {activeItem.autoMerk} {activeItem.autoType} huren in de periode van: {activeItem.startDatum} tot {activeItem.eindDatum}  </p>
                                    <button onClick={() => PasAanvraagAan()}>accepteer aanvraag</button> <button onClick={() => VerwijderAanvraag()}> verwijder aanvraag</button>
                                    <button onClick={() => CloseWindow()}>Sluiten</button>
                                </div>
                            </div>
                        )}

                    </div>
                ))}
             </div>
                <div className="onderste-rij">
                    {sItem.map(sItem =>
                        <div key={sItem.id} className="aanvraagItems-box">
                            <h3>Schadeformulier: {sItem.id}</h3>
                            <p>voor klant: {sItem.email}</p>
                            <button
                                onClick={() => OnButtonClick(sItem, 'schade')}>
                                bekijk dit schadeformulier
                            </button>
                            {modalType === 'schade'  && (
                                <div className="modal-overlay">
                                    <div className="modal-content">
                                        <h2>Huuraanvraag</h2>
                                        <p>De klant: {activeItem.email} </p>
                                        <p>Heeft schade gereden op auto: {activeItem.kenteken}</p>
                                        <p>Het gaat om de volgende schade: {activeItem.schadeType}</p>
                                        <p>En het schadetype is een: {activeItem.ernstVDSchade}</p>
                                        <button onClick={() => VerwijderSchadeformulier()}>verwijder schadeformulier</button> <button onClick={() => ZetOpGerepareerd()}>Registreer auto als: gerepareerd</button>
                                        <button onClick={() => CloseWindow()}>Sluiten</button>
                                    </div>
                                </div>
                            )}
                        </div>
                        
                    )}
                </div>
            </div>
        </div>
    );
};

export default AanvraagBackOffice;