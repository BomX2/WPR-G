import React, { useEffect, useState } from 'react';
import 'react-datepicker/dist/react-datepicker.css';
import { useParams, useLocation } from "react-router-dom";
import './modal.css';
import carImage from "../image/kever.jpg";
import './product.css'
import dayjs from "dayjs"
export default function Product() {
    const [auto, setAuto] = useState("");
    const [autoBestaat, setAutoBestaat] = useState(true);
    const { Kenteken } = useParams();
    const [item, setItem] = useState([]);
    const [lock, setLock] = useState(false);
    const location = useLocation();
    const queryparams = new URLSearchParams(location.search);
    const ophaalDatum = queryparams.get("ophaalDatum");
    const inleverDatum = queryparams.get("inleverDatum");
    const ophaalTijd = queryparams.get("ophaalTijd");
    const inleverTijd = queryparams.get("inleverTijd");
    const fOphaalDatum = dayjs(ophaalDatum).format("YYYY-MM-DD");
    const fInleverDatum = dayjs(inleverDatum).format("YYYY-MM-DD");

    useEffect(() => {
        const RoepUserGegevens = async () => {
            try {
                const RoepUser = await fetch(`https://localhost:7065/api/gebruikers/me`, {
                    credentials: "include",

                });
                const data = await RoepUser.json();
                setItem(data);
            
                if (RoepUser.ok) {
                    console.log(" usergegevens succesvol aangeroepen!")
                }
                else {
                    alert("pagina is incorrect geladen");
                }
            }
            catch (error) {
                alert("De volgende error is opgetreden:", error)
            }
        }
        RoepUserGegevens();
    }, [Kenteken]);
  
        useEffect(() => {
        const fetchCar = async () => {
            try {
                const response = await fetch(`https://localhost:7065/api/voertuigen/getByKenteken/${Kenteken}`);
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
    }, [Kenteken]);
    

    const HandleAanvraag = async () => {
        try {
          
            const PostAanvraag = await fetch(`https://localhost:7065/api/gebruikers/postAanvraag`, {
                method: 'POST',
                headers: { 'content-type': 'application/json' },
                body: JSON.stringify({
                    startDatum: fOphaalDatum,
                    eindDatum: fInleverDatum,
                    Kenteken: Kenteken,
                    Ophaaltijd: ophaalTijd,
                    Inlevertijd: inleverTijd,
                    email: item.email,
                    adres: item.adres,
                    telefoonnummer: item.phonenumber,
                })
            })
           
            if (PostAanvraag.ok) {
                setLock(true);
                alert("Aanvraag succesvol aangemaakt");
               
            }
            else {
                alert("Er is iets fout gegaan");
            }
        }
        
        catch (error) {
            console.log("er was een fout: ", error)
        }

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
                        {auto && auto.voertuig ? (
                            auto.voertuig.soort === "Auto" ? (
                                <>
                                    <div><strong>Aantal deuren:</strong> {auto.aantalDeuren}</div>
                                    <div><strong>Brandstoftype:</strong> {auto.brandstofType}</div>
                                    <div><strong>Transmissietype:</strong> {auto.transmissieType}</div>
                                    <div><strong>Heeft airco:</strong> {auto.heeftAirco ? "Ja" : "Nee"}</div>
                                    <div><strong>Brandstofverbruik:</strong> {auto.brandstofVerbruik} liter/100km</div>
                                    <div><strong>Bagageruimte:</strong> {auto.bagageruimte} liter</div>
                                    <div><strong>Aanschaf jaar:</strong> {auto.voertuig.aanschafJaar}</div>
                                </>
                            ) : auto.voertuig.soort === "Camper" ? (
                                <>
                                        <div><strong>Slaapplaatsen:</strong> {auto.slaapplaatsen}</div>
                                        <div><strong>Lengte:</strong> {auto.lengte} meter</div>
                                        <div><strong>Hoogte:</strong> {auto.hoogte} meter</div>
                                        <div><strong>Heeft keuken:</strong> {auto.heeftKeuken ? "Ja" : "Nee"}</div>
                                        <div><strong>Heeft badkamer:</strong> {auto.heeftBadkamer ? "Ja" : "Nee"}</div>
                                        <div><strong>Watertank capaciteit:</strong> {auto.waterTankCapaciteit} liter</div>
                                        <div><strong>Afvalwatertank capaciteit:</strong> {auto.afvalTankCapaciteit} liter</div>
                                        <div><strong>Brandstofverbruik:</strong> {auto.brandstofVerbruik} liter/100km</div>
                                        <div><strong>Heeft zonnepanelen:</strong> {auto.heeftZonnepanelen ? "Ja" : "Nee"}</div>
                                        <div><strong>Fietsrek capaciteit:</strong> {auto.fietsRekCapaciteit} fietsen</div>
                                        <div><strong>Heeft luifel:</strong> {auto.heeftLuifel ? "Ja" : "Nee"}</div>
                                </>
                            ) : auto.voertuig.soort === "Caravan" ? (
                                <>
                                            <div><strong>Lengte:</strong> {auto.lengte} meter</div>
                                            <div><strong>Slaapplaatsen:</strong> {auto.slaapplaatsen}</div>
                                            <div><strong>Heeft keuken:</strong> {auto.heeftKeuken ? "Ja" : "Nee"}</div>
                                            <div><strong>Watertank capaciteit:</strong> {auto.waterTankCapaciteit} liter</div>
                                            <div><strong>Afvalwatertank capaciteit:</strong> {auto.afvalTankCapaciteit} liter</div>
                                            <div><strong>Heeft luifel:</strong> {auto.heeftLuifel ? "Ja" : "Nee"}</div>
                                </>
                            ) : (
                                <div>geen verdere info.</div>
                            )
                        ) : (
                            <div>Auto data is niet beschikbaar</div>
                        )}
                    </div>
                </div>
            <div className = "rechts">
                    <h1>{auto?.voertuig?.merk || "Onbekend merk"} {auto?.voertuig?.type || "Onbekend type"}</h1>
                    <h2>{auto?.voertuig?.kleur || "Onbekende kleur"}</h2>
                <h2> {auto?.voertuig?.prijsPerDag || "prijs niet bekend"} euro per dag</h2>
            
            <div className="date-picker-container">
          
                        <button disabled={lock} onClick={() => HandleAanvraag()}  >klik hier om een huuraanvraag te maken voor deze auto</button>
   
                    </div>
                </div>
            </div>
        </div>
    );
}