import React, { useState } from 'react';
import './AanvraagItems.css'
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { cache } from 'react';
const FrontOfficeAanvraag = () => {
        const [modalWindow, setModalWindow] = useState(false);
    const [item, setItem] = useState([]);
    const [activeItem, setActiveItem] = useState(null);
    const navigeren = useNavigate('');
    useEffect(() => {
        const fetchAanvragen = async () => {
            try {
                const response = await fetch(`https://localhost:7065/api/gebruikers/getAanvragenFront`);
                const data = await response.json();
                const geupdateData = data.map((item) => ({
                    ...item,
                }));
                setItem(geupdateData);
                console.log(data);
                console.log(geupdateData);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchAanvragen();
    }, []);
    const SetUitgaveStatus = async (Status) => {
        try {
            const keurGoed = await fetch(`https://localhost:7065/api/gebruikers/KeurAanvraagGoed/${activeItem.id}`, {
                method: 'PUT',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify({

                    id: activeItem.id,
                    goedgekeurd: true,
                    status: Status,
                })
            })
            if (keurGoed.ok) {
                alert("Voertuig uitgegeven!");
                CloseWindow();
                setItem(prevItems => prevItems.filter(a => a.id !== activeItem.id));
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
            const verwijder = await fetch(`https://localhost:7065/api/gebruikers/verwijderAanvraag/${activeItem.id}`, {
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
    const CreeerSchadeFormulier = async () => {
        try {
            const MaakFormulier = await fetch(`https://localhost:7065/api/gebruikers/MaakSchadeFormulier`, {
                method: 'POST',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify({
                    AanvraagId: activeItem.id,
                    Kenteken: activeItem.kenteken,
                    Email: activeItem.email,
                    Telefoonnummer: activeItem.telefoonnummer,

                })
            })
            if (MaakFormulier.ok) {
                const data = await MaakFormulier.json();
                const SchadeId = data.id;
                console.log(data.id);
                console.log(SchadeId);
                sessionStorage.setItem("SchadeId", SchadeId);
                navigeren('/SchadePagina');
            }
            else {
                alert("Er is iets fout gegaan");
                console.log(activeItem.Kenteken);
            }
        }
        catch (error) {
            console.log("error bij schadeformulier aanmaken:", error);
        }
    }
    return (
        <div>
            <h1>Onbehandelde huuraanvragen</h1>
            <h2>Uitgaves en innames voor vandaag</h2>
            <div className="scherm-layout">
                <div className="huidigeDatums-balk">
                    <div className="aanvraagItems-layout">

                        {item.filter(
                            item => 
                                new Date(item.startDatum).toDateString() === new Date().toDateString() || new Date(item.eindDatum).toDateString() === new Date().toDateString()
                    ).map(gefilterdeItem => (
                        <div key={gefilterdeItem.id} className="aanvraagItems-box">
                            <h3>Aanvraag: {gefilterdeItem.id}</h3>
                            <p>De klant: {gefilterdeItem.email}</p>
                            <button onClick={() => OnButtonClick(gefilterdeItem)}>Bekijk aanvraag</button>
                        </div>
                        
                    ))}
                    </div>
                </div>     
                <h2>nieuwe aanvragen:</h2>
                <div className="middel-sectie">
                    <div className="linker-balk">
                        <div className="aanvraagItems-layout">
                            {item.filter(
                                gefilterdeItem => gefilterdeItem.status === null
                            ).map(gefilterdeItem => (
                                <div key={gefilterdeItem.id} className="aanvraagItems-box">
                                    <h3>Aanvraag: {gefilterdeItem.id}</h3>
                                    <p>De klant: {gefilterdeItem.email}</p>
                                    <button onClick={() => OnButtonClick(gefilterdeItem)}>Bekijk aanvraag</button>
                                </div>

                            ))}
                        </div>
                    </div>
                    <div className="rechter-balk">
                        <div className="aanvraagItems-layout">
                            {item.filter(
                                item => item.status === "uitgegeven" 
                            ).map(gefilterdeItem => (
                                <div key={gefilterdeItem.id} className="aanvraagItems-box">
                                    <h3>Aanvraag: {gefilterdeItem.id}</h3>
                                    <p>De klant: {gefilterdeItem.email}</p>
                                    <button onClick={() => OnButtonClick(gefilterdeItem)}>Bekijk aanvraag</button>
                                </div>
                            ))}
                        </div>
                    </div>  
                </div>
               
                            {modalWindow && (
                                <div className="modal-overlay">
                                    <div className="modal-content">
                                        <h2>Huuraanvraag</h2>
                                        <p>De klant: {activeItem.email} </p>
                                        <p>wil een {activeItem.autoMerk}  {activeItem.autoType} huren in de periode van: {activeItem.startDatum} tot {activeItem.eindDatum}  </p>
                                          <p>De klant heeft de volgende persoonsgegevens voor identificatie:</p>
                                         
                            <p> email: {activeItem.email}, telefoonnummer: {activeItem.telefoonnummer}, </p>
                            {activeItem.status !== 'uitgegeven' && activeItem.status !== 'beschadigd' && (
                                <button onClick={() => SetUitgaveStatus("uitgegeven")} >markeer als Uitgegeven.</button>

                            )}
                            {activeItem.status == 'uitgegeven' && (
                                <button onClick={() => SetUitgaveStatus("beschadigd"), () => CreeerSchadeFormulier() }>Registreer schade</button>
                            )}
                                        <button onClick={() => HandelInNameAf()}>Neem voertuig in.</button>
                                        <button onClick={CloseWindow}>Sluiten</button>
                                    </div>
                                </div>
                            )}
            </div>
            
            </div>

    );

}
export default FrontOfficeAanvraag;