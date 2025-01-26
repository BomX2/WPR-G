import React, { useEffect, useState } from 'react';
import './Catalogus.css';
import Products from '../componements/Products/Products';
import SideBar from '../componements/SideBar/SideBar';
import { useLocation } from 'react-router-dom';
import dayjs from 'dayjs';
import SearchFilters from '../componements/filter-bar/FilterBar';

export default function Catalogus() {
    const location = useLocation();
    const [autos, setAutos] = useState([]);
    const [errorMessage, setErrorMessage] = useState('');
    const [filters, setFilters] = useState({});
    const [soort, setSoort] = useState('');
    const [showFilters, setShowFilters] = useState(false);

    useEffect(() => {
        const queryparams = new URLSearchParams(location.search);
        const ophaalDatum = queryparams.get('ophaalDatum');
        const OphaalTijd = queryparams.get('OphaalTijd');
        const inleverDatum = queryparams.get('inleverDatum');
        const InleverTijd = queryparams.get('InleverTijd');
        const soort = queryparams.get('soort');
        setSoort(soort);

        if (!ophaalDatum || !inleverDatum || !OphaalTijd || !InleverTijd || !soort) {
            setShowFilters(true);
            return;
        }

        setShowFilters(false);

        if (!ophaalDatum || !inleverDatum) {
            setErrorMessage('Ophaaldatum en/of inleverdatum ontbreken.');
            return;
        }

        const formattedOphaalDatum = dayjs(ophaalDatum).format('YYYY-MM-DD');
        const formattedInleverDatum = dayjs(inleverDatum).format('YYYY-MM-DD');

        const fetchAutos = async () => {
            const baseUrl = 'https://localhost:7065/api/voertuigen/autos';
            const Url = `${baseUrl}?StartDatum=${encodeURIComponent(formattedOphaalDatum)}&OphaalTijd=${encodeURIComponent(OphaalTijd)}
                                   &EindDatum=${encodeURIComponent(formattedInleverDatum)}&InleverTijd=${encodeURIComponent(InleverTijd)}
                                   &soort=${encodeURIComponent(soort)}`;

            try {
                const response = await fetch(Url);
                if (!response.ok) {
                    throw new Error(`Error: ${response.status} ${response.statusText}`);
                }
                const data = await response.json();
                setAutos(data);
            } catch (error) {
                console.error('Error fetching autos', error);
                setErrorMessage('Er is een fout opgetreden bij het ophalen van de voertuigen.');
            }
        };
        fetchAutos();
    }, [location.search]);

    const handleFilterChange = (event) => {
        const { name, value } = event.target;
        setFilters((prevFilters) => ({
            ...prevFilters,
            [name]: value,
        }));
    };

    const getNestedValue = (obj, path) => {
        return path.split('.').reduce((acc, part) => acc && acc[part], obj);
    };

    const filteredAutos = autos.filter((auto) => {
        return Object.entries(filters).every(([key, value]) => {
            if (!value) return true;

            const autoValue = getNestedValue(auto, key);

            if (typeof autoValue === 'string') {
                return autoValue.toLowerCase().includes(value.toLowerCase());
            }

            if (typeof autoValue === 'number') {
                // Filteren op minimale waarde
                if (key.includes('Min') && value !== "") {
                    return autoValue >= value;  // Alleen voertuigen waarvan de waarde groter dan of gelijk is aan de minwaarde
                }
                // Filteren op maximale waarde
                if (key.includes('Max') && value !== "") {
                    return autoValue <= value;  // Alleen voertuigen waarvan de waarde kleiner dan of gelijk is aan de maxwaarde
                }
            }

            if (typeof autoValue === 'boolean') {
                return autoValue === (value === 'true');
            }

            return true;
        });
    });

  
    const sortedAutos = filteredAutos.sort((a, b) => {
        const prijsA = getNestedValue(a, 'voertuig.prijsPerDag') || 0; // Fallback naar 0 als er geen waarde is
        const prijsB = getNestedValue(b, 'voertuig.prijsPerDag') || 0

        if (filters.prijs === 'laag-hoog') {
            return prijsA - prijsB; // Sorteer oplopend
        }
        if (filters.prijs === 'hoog-laag') {
            return prijsB - prijsA; // Sorteer aflopend
        }
        return 0; // Geen sortering als filters.prijs niet is ingesteld
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
                            sortedAutos.map((auto) => <Products key={auto.kenteken} auto={auto} />)
                        )}
                    </div>
                </>
            )}
        </div>
    );
}
