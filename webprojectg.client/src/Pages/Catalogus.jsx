import React, { useEffect, useState } from 'react';
import './Catalogus.css'
import Products from '../componements/Products/Products'
import SideBar from '../componements/SideBar/SideBar'
import { useLocation } from "react-router-dom"
import dayjs from "dayjs"
import SearchFilters from '../componements/filter-bar/FilterBar';


export default function Catalogus() {
    const location = useLocation();
    const [autos, setautos] = useState([]);
    const [errorMessage, setErrorMessage] = useState("");
    const [filters, setFilters] = useState({});
    const [soort, setSoort] = useState("");
    const [showFilters, setShowFilters] = useState(false);

    useEffect(() => {

        const queryparams = new URLSearchParams(location.search);
        const ophaalDatum = queryparams.get("ophaalDatum");
        const OphaalTijd = queryparams.get("OphaalTijd");
        const inleverDatum = queryparams.get("inleverDatum");

        const InleverTijd = queryparams.get("InleverTijd");
        const soort = queryparams.get("soort");
        setSoort(soort)

        if (!ophaalDatum || !inleverDatum || !OphaalTijd || !InleverTijd || !soort) {
            setShowFilters(true);
            return;
        }

        setShowFilters(false);

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

    const getNestedValue = (obj, path) => {
        return path.split('.').reduce((acc, part) => acc && acc[part], obj);
    };

    const filteredAutos = autos.filter((auto) => {
        return Object.entries(filters).every(([key, value]) => {
            if (!value) return true; // Sla lege filters over

            const autoValue = getNestedValue(auto, key);

            // String-vergelijkingen
            if (typeof autoValue === "string") {
                return autoValue.toLowerCase().includes(value.toLowerCase());
            }

            // Numerieke vergelijkingen
            if (typeof autoValue === "number") {
                if (key.startsWith("min")) {
                    const field = key.replace("min", "").toLowerCase();
                    const minValue = getNestedValue(auto, field);
                    return minValue >= Number(value);
                } else if (key.startsWith("max")) {
                    const field = key.replace("max", "").toLowerCase();
                    const maxValue = getNestedValue(auto, field);
                    return maxValue <= Number(value);
                }
                return autoValue === Number(value);
            }

            // Boolean-vergelijkingen
            if (typeof autoValue === "boolean") {
                return autoValue === (value === "true");
            }

            return true;
        });
    });

    return (
        <div className="catalogus-container">
            {showFilters ? (
                <SearchFilters />
            ) : (
                <>
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
                </>
            )}
        </div>
    );
};
