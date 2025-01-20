import React, { useEffect, useState } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { useParams } from "react-router-dom";
import './modal.css';
import carImage from "../image/kever.jpg";
import './product.css'
export default function Product() {
    const [auto, setAuto] = useState("");
    const [autoBestaat, setAutoBestaat] = useState(true);
    const [modalWindow, setModalWindow] = useState(false);
    const [startDatum, setStartDatum] = useState(null);
    const [eindDatum, setEindDatum] = useState(null);
    const [naam, setNaam] = useState("");
    const [email, setEmail] = useState("");
    const [telnummer, setTelNummer] = useState("")
    const [geboektedatums, setGeBoekteDatums] = useState([]);
    const { id } = useParams()

    useEffect(() => {
        const fetchGeboekteDatums = async () => {
            try {
                const Calldatums = await fetch(`https://localhost:7065/api/gebruikers/GetgeboekteDatums/${id}`, {

                })
                if (Calldatums.ok) {
                    const data = await Calldatums.json();
                    const geboektedatums = [];
                    console.log("Data ontvangen van API:", data);

                    data.forEach(({ startDatum, eindDatum }) => {
                        console.log(`Oorspronkelijke data - Begin: ${startDatum}, Eind: ${eindDatum}`);

                        const current = new Date(startDatum);
                        const end = new Date(eindDatum);
                        console.log(`Verwerken: Startdatum=${current}, Einddatum=${end}`);
                        if (isNaN(current)) {
                            console.error(`Parsing error: Ongeldige datum voor startDatum - ${startDatum}`);
                        }
                        if (!isNaN(current)) {
                            while (current <= end) {
                                geboektedatums.push(new Date(current));
                                current.setDate(current.getDate() + 1);
                            }
                        }
                        while (current <= end) {
                            geboektedatums.push(new Date(current));
                            current.setDate(current.getDate() + 1);
                        }
                    });
                    console.log("Geformatteerde geboektedatums:", geboektedatums);
                    setGeBoekteDatums(geboektedatums);
                }
                else {
                    console.log("pagina incorrect geladen");
                }
            }

            catch (error) {
                console.log("error:", error)
            }
        }
        fetchGeboekteDatums();
    }, [id]);
        useEffect(() => {
        const fetchCar = async () => {
            try {
                const response = await fetch(`https://localhost:7065/api/voertuigen/getAutoById/${id}`);
                if (!response.ok) {
                    setAutoBestaat(false);
                    return;
                }
                const data = await response.json();

                if (!data || Object.keys(data).length === 0) {
                    setAutoBestaat(false);
                    return;
                }
                setAuto(data);
            } catch (error) {
                console.error('Error fetching auto', error);
                setAutoBestaat(false);
            }
        };
        fetchCar();
    }, [id]);
    

    const HandleAanvraag = async () => {
        try {
            const formatDateToUTC = (date) => {
                if (!date) return null;

                const utcDate = new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate()));
                return utcDate.toISOString().split('T')[0];
            };
            const PostAanvraag = await fetch(`https://localhost:7065/api/gebruikers/postAanvraag`, {
                method: 'POST',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify({
                    startDatum: formatDateToUTC(startDatum),
                    eindDatum: formatDateToUTC(eindDatum),
                    persoonsgegevens: naam,
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
                    ...getDatesInRange(new Date(startDatum), new Date(eindDatum))
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

    if (!autoBestaat) {
            return <h2>De auto bestaat niet.</h2>;
    }
    return (
        <div>
            <div className="productPage">
            <div className= "links">
                    <img src={carImage} alt="auto" className="product-image" />
                    <div className="auto-info"> 
                        <div><strong>Aantal deuren:</strong> {auto.aantalDeuren}</div>
                        <div><strong>Brandstoftype:</strong> {auto.brandstofType}</div>
                        <div><strong>Aanschaf jaar:</strong> {auto.aanschafJaar}</div>
                        <div><strong>Bagageruimte:</strong> {auto.bagageruimte}</div>
                    </div>
                </div>
            <div className = "rechts">
                    <h1>{auto.merk} {auto.type}</h1>
                    <h2>{auto.kleur}</h2>
                <h2> {auto.prijsPerDag} euro per dag</h2>
            
            <div className="date-picker-container">
            <DatePicker 
                selected={startDatum}
                onChange={(date) => setStartDatum(date)}
                selectsStart
                startDate={startDatum}
                endDate={eindDatum}
                dateFormat="dd/MM/yyyy"
                excludeDates={geboektedatums}
            />
            <DatePicker
                selected={eindDatum}
                onChange={(date) => setEindDatum(date)}
                selectsEnd
                startDate={startDatum}
                endDate={eindDatum}
                minDate={startDatum}
                dateFormat="dd/MM/yyyy"
                disabled={!startDatum}
                excludeDates={geboektedatums}
            />
            <button onClick={OnButtonClick} disabled={!startDatum || !eindDatum} >klik hier om een huuraanvraag te maken voor deze auto</button>
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
                </div>
            </div>
        </div>
    );
}