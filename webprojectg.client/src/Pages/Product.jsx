import React, { useEffect, useState } from 'react';
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
    const [geboektedatums, setGeBoekteDatums] = useState([]);
    const { id } = useParams()

    useEffect(() => {
        const fetchGeboekteDatums = async () => {
            try {
                const Calldatums = await fetch(`https://localhost:7065/api/gebruiker/GetgeboekteDatums/${id}`, {

                })
                if (Calldatums.ok) {
                    const data = await Calldatums.json();
                    const geboektedatums = [];
                    data.forEach(({ beginDatum, eindDatum }) => {
                        const current = new Date(beginDatum);
                        while (current <= new Date(eindDatum)) {
                            geboektedatums.push(new Date(current));
                            current.setDate(current.getDate() + 1);
                        }
                    });
                    setGeBoekteDatums(geboektedatums);
                }
                else {
                    alert("Pagina incorrect geladen");
                }
            }

            catch (error) {
                console.log("error:", error)
            }
        }
        fetchGeboekteDatums();
    }, [id]);
    
    const HandleAanvraag = async () => {
        try {
            const formatDateToUTC = (date) => {
                if (!date) return null;

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
                setGeBoekteDatums((prev) => [
                    ...prev,
                    ...getDatesInRange(new Date(beginDatum), new Date(eindDatum))
                ]);
            }
            else {
                alert("Er is iets fout gegaan");
            }
        }
        
        catch (error) {
            console.log("er was een fout: ", error)
        }

    }
    const getDatesInRange = (start, end) => {
        const dates = [];
        const current = new Date(start);
        while (current <= end) {
            dates.push(new Date(current));
            current.setDate(current.getDate() + 1);
        }
        return dates;
    };
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
                excludeDates={geboektedatums}
            />
            <DatePicker
                selected={eindDatum}
                onChange={(date) => setEindDatum(date)}
                selectsEnd
                startDate={beginDatum}
                endDate={eindDatum}
                minDate={beginDatum}
                dateFormat="dd/MM/yyyy"
                disabled={!beginDatum}
                excludeDates={geboektedatums}
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