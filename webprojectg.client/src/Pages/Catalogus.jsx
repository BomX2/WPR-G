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
    const [filters, setFilters] = useState({});
    const [soort, setSoort] = useState("")

    useEffect(() => {

        const queryparams = new URLSearchParams(location.search);
        const ophaalDatum = queryparams.get("ophaalDatum");
        const OphaalTijd = queryparams.get("OphaalTijd");
        const inleverDatum = queryparams.get("inleverDatum");

        const InleverTijd = queryparams.get("inleverTijd");
        const soort = queryparams.get("soort");
        setSoort(soort)

        console.log("Soort:", soort);

        if (!ophaalDatum || !inleverDatum) {
            setErrorMessage("Ophaaldatum en/of inleverdatum ontbreken.");
            return;
        }

      

        const formattedOphaalDatum = dayjs(ophaalDatum).format("YYYY-MM-DD");
        const formattedInleverDatum = dayjs(inleverDatum).format("YYYY-MM-DD");

        const fetchAutos = async () => {

            const baseUrl = "https://localhost:7065/api/voertuigen/autos";
            const Url = `${baseUrl}?StartDatum=${encodeURIComponent(formattedOphaalDatum)}&OphaalTijd=${encodeURIComponent(OphaalTijd)}
                                   &EindDatum=${encodeURIComponent(formattedInleverDatum)}&InleverTijd=${encodeURIComponent(InleverTijd)}
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

    const handleFilterChange = (event) => {
        const { name, value } = event.target; // Zorg ervoor dat je de 'name' en 'value' van het inputveld oppakt.
        setFilters((prevFilters) => ({
            ...prevFilters,
            [name]: value, // Update het specifieke filter op basis van de naam van het veld.
        }));
    };

    const filteredAutos = autos.filter((auto) => {
        return Object.entries(filters).every(([key, value]) => {
            if (!value) return true; // Geen filterwaarde
            if (typeof auto[key] === "string") {
                return auto[key].toLowerCase().includes(value.toLowerCase()); // Tekst
            }
            if (typeof auto[key] === "number") {
                return auto[key] === Number(value); // Numeriek
            }
            return true;
        });
    });

    return (
        <div className="catalogus-container">
            <SideBar filters={filters} onFilterChange={handleFilterChange} soort={soort} />
            <div className="content">
                {errorMessage ? (
                    <div className="error-message">{errorMessage}</div>
                ) : (
                    filteredAutos.map(auto => (
                        <Products key={auto.kenteken} auto={auto} />
                    ))
                )}
            </div>
        </div>
    );
};
