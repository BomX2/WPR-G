import React, { useEffect, useState } from 'react';
import './Catalogus.css'
import Products from '../componements/Products/Products'
import SideBar from '../componements/SideBar/SideBar'
import { useLocation } from "react-router-dom"
import dayjs from "dayjs"

export default function Catalogus() {
    const location = useLocation();
    const [autos, setautos] = useState([]);
    const [errorMessage, setErrorMessage] = useState("");

    useEffect(() => {

        const queryparams = new URLSearchParams(location.search);
        const ophaalDatum = queryparams.get("ophaalDatum");
        const ophaalTijd = queryparams.get("ophaalTijd");
        const inleverDatum = queryparams.get("inleverDatum");
        const inleverTijd = queryparams.get("inleverTijd");
        const soort = queryparams.get("soort")

        if (!ophaalDatum || !inleverDatum) {
            setErrorMessage("Ophaaldatum en/of inleverdatum ontbreken.");
            return;
        }

        const formattedOphaalDatum = dayjs(ophaalDatum).format("YYYY-MM-DD");
        const formattedInleverDatum = dayjs(inleverDatum).format("YYYY-MM-DD");

        const fetchAutos = async () => {

            const baseUrl = "https://localhost:7065/api/voertuigen/autos";
            const Url = `${baseUrl}?StartDatum=${encodeURIComponent(formattedOphaalDatum)}&OphaalTijd=${encodeURIComponent(ophaalTijd)}
                                   &EindDatum=${encodeURIComponent(formattedInleverDatum)}&InleverTijd=${encodeURIComponent(inleverTijd)}
                                   &soort=${encodeURIComponent(soort)}`;

            try {
                const response = await fetch(Url);
                if (!response.ok) {
                    throw new Error(`Error: ${response.status} ${response.statusText}`);
                }
                const data = await response.json();
                setautos(data);
            } catch (error) {
                console.error('Error fetching autos', error);
                setErrorMessage("Er is een fout opgetreden bij het ophalen van de voertuigen.");
            }
        };
        fetchAutos();
    }, [location.search]);

    // vergeet niet in een useEffect te zetten
    // const queryParams = URLSearchParams(this.props.location.search);
    // let url = new URL("https://localhost:7065/api/voertuigen/autos");

    // voor elke filter optie doe je dit maar dan met de key in de query param voor de filteroptie dus "minPrijs" vervangen
    // if (queryParams.get("minPrijs")) url.searchParams.append("minPrijs", queryParams.get("minPrijs"));
    // if (queryParams.get("maxPrijs")) url.searchParams.append("maxPrijs", queryParams.get("maxPrijs"));



    return (
        <div className="catalogus-container">
            <SideBar />
            <div className="content">
                {errorMessage ? (
                    <div className="error-message">{errorMessage}</div>
                ) : (
                    autos.map(auto => (
                        <Products key={auto.kenteken} auto={auto} />
                    ))
                )}
            </div>
        </div>
    );
};
