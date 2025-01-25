import React, { useState, useEffect } from "react";
import "./SideBar.css";

export default function SideBar({ filters, onFilterChange, soort }) {
    const [merken, setMerken] = useState([]);

    // Ophalen van merken vanuit de API
    useEffect(() => {
        const fetchMerken = async () => {
            try {
                const response = await fetch(`https://localhost:7065/api/voertuigen/merken?soort=${soort}`);
                if (!response.ok) {
                    throw new Error("Fout bij het ophalen van merken");
                }
                const data = await response.json();
                setMerken(data);
            } catch (error) {
                console.error("Error fetching merken:", error);
            }
        };

        fetchMerken();
    }, [soort]);

    // Algemene filters die altijd beschikbaar zijn
    const commonFilters = (
        <>
            {/* Filter: Prijs */}
            <label>Prijs</label>
            <select name="voertuig.prijsPerDag" onChange={onFilterChange}>
                <option value="">Kies een optie</option>
                <option value="laag-hoog">Laag naar Hoog</option>
                <option value="hoog-laag">Hoog naar Laag</option>
            </select>

            {/* Filter: Merk */}
            <label>Merk</label>
            <select name="voertuig.merk" value={filters.merk} onChange={onFilterChange}>
                <option value="">Alle merken</option>
                {merken.map((merk) => (
                    <option key={merk} value={merk}>
                        {merk}
                    </option>
                ))}
            </select>

            {/* Filter: Bouwjaar */}
            <label>Bouwjaar</label>
            <input
                type="number"
                name="voertuig.aanschafJaar"
                placeholder="Bijv. 2020"
                value={filters.bouwjaar}
                onChange={onFilterChange}
            />

            {/* Filter: Kleur */}
            <label>Kleur</label>
            <input
                type="text"
                name="voertuig.kleur"
                placeholder="Bijv. Rood"
                value={filters.kleur}
                onChange={onFilterChange}
            />
        </>
    );

    // Filters specifiek voor auto's
    const autoFilters = (
        <>
            <label>Aantal deuren</label>
            <input
                type="number"
                name="aantalDeuren"
                placeholder="Aantal deuren"
                value={filters.aantalDeuren || ""}
                onChange={onFilterChange}
            />

            <label>Brandstoftype</label>
            <select name="brandstofType" value={filters.brandstofType || ""} onChange={onFilterChange}>
                <option value="">Alle brandstoffen</option>
                <option value="benzine">Benzine</option>
                <option value="diesel">Diesel</option>
                <option value="elektrisch">Elektrisch</option>
            </select>

            <label>
                <input
                    type="checkbox"
                    name="heeftAirco"
                    checked={filters.heeftAirco || false}
                    onChange={(e) =>
                        onFilterChange({ target: { name: "heeftAirco", value: e.target.checked } })
                    }
                />
                Heeft airco
            </label>

            <label>Brandstofverbruik (L/100km)</label>
            <input
                type="number"
                name="brandstofVerbruik"
                placeholder="Max brandstofverbruik"
                value={filters.brandstofVerbruik || ""}
                onChange={onFilterChange}
            />

            <label>Transmissietype</label>
            <select
                name="transmissieType"
                value={filters.transmissieType || ""}
                onChange={onFilterChange}
            >
                <option value="">Alle transmissies</option>
                <option value="Handgeschakeld">Handmatig</option>
                <option value="Automatisch">Automatisch</option>
            </select>

            <label>Bagageruimte (L)</label>
            <input
                type="number"
                name="bagageruimte"
                placeholder="Min bagageruimte"
                value={filters.bagageruimte || ""}
                onChange={onFilterChange}
            />
        </>
    );

    // Filters specifiek voor campers
    const camperFilters = (
        <>
            <label>Lengte (m)</label>
            <input
                type="number"
                name="lengte"
                placeholder="Max lengte"
                value={filters.lengte || ""}
                onChange={onFilterChange}
            />

            <label>Hoogte (m)</label>
            <input
                type="number"
                name="hoogte"
                placeholder="Max hoogte"
                value={filters.hoogte || ""}
                onChange={onFilterChange}
            />

            <label>Slaapplaatsen</label>
            <input
                type="number"
                name="slaapplaatsen"
                placeholder="Min slaapplaatsen"
                value={filters.slaapplaatsen || ""}
                onChange={onFilterChange}
            />

            <label>
                <input
                    type="checkbox"
                    name="heeftBadkamer"
                    checked={filters.heeftBadkamer || false}
                    onChange={(e) =>
                        onFilterChange({ target: { name: "heeftBadkamer", value: e.target.checked } })
                    }
                />
                Heeft badkamer
            </label>

            <label>
                <input
                    type="checkbox"
                    name="heeftKeuken"
                    checked={filters.heeftKeuken || false}
                    onChange={(e) =>
                        onFilterChange({ target: { name: "heeftKeuken", value: e.target.checked } })
                    }
                />
                Heeft keuken
            </label>
        </>
    );

    // Filters specifiek voor caravans
    const caravanFilters = (
        <>
            <label>Lengte (m)</label>
            <input
                type="number"
                name="lengte"
                placeholder="Max lengte"
                value={filters.lengte || ""}
                onChange={onFilterChange}
            />

            <label>Slaapplaatsen</label>
            <input
                type="number"
                name="slaapplaatsen"
                placeholder="Min slaapplaatsen"
                value={filters.slaapplaatsen || ""}
                onChange={onFilterChange}
            />

            <label>
                <input
                    type="checkbox"
                    name="heeftKeuken"
                    checked={filters.heeftKeuken || false}
                    onChange={(e) =>
                        onFilterChange({ target: { name: "heeftKeuken", value: e.target.checked } })
                    }
                />
                Heeft keuken
            </label>
        </>
    );

    return (
        <div className="sidebar">
            <h2>Filters</h2>
            {commonFilters}
            {soort === "auto" && autoFilters}
            {soort === "camper" && camperFilters}
            {soort === "caravan" && caravanFilters}
        </div>
    );
}
