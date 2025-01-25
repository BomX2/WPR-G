import './SideBar.css'
export default function SideBar({ filters, onFilterChange, soort }) {
    const commonFilters = (
        <>
            <input
                type="text"
                name="merk"
                placeholder="Merk"
                value={filters.merk || ""}
                onChange={onFilterChange}
            />
            <input
                type="text"
                name="kleur"
                placeholder="Kleur"
                value={filters.kleur || ""}
                onChange={onFilterChange}
            />
            <input
                type="number"
                name="kostenPerDag"
                placeholder="Max kosten per dag"
                value={filters.kostenPerDag || ""}
                onChange={onFilterChange}
            />
        </>
    );

    const autoFilters = (
        <>
            <input
                type="number"
                name="aantalDeuren"
                placeholder="Aantal deuren"
                value={filters.aantalDeuren || ""}
                onChange={onFilterChange}
            />
            <select
                name="brandstofType"
                value={filters.brandstofType || ""}
                onChange={onFilterChange}
            >
                <option value="">Alle brandstoffen</option>
                <option value="benzine">Benzine</option>
                <option value="diesel">Diesel</option>
                <option value="elektrisch">Elektrisch</option>
            </select>
            <input
                type="checkbox"
                name="heeftAirco"
                checked={filters.heeftAirco || false}
                onChange={(e) => onFilterChange({ target: { name: "heeftAirco", value: e.target.checked } })}
            />
            <label>Heeft airco</label>
            <input
                type="number"
                name="brandstofVerbruik"
                placeholder="Max brandstofverbruik (L/100km)"
                value={filters.brandstofVerbruik || ""}
                onChange={onFilterChange}
            />
            <select
                name="transmissieType"
                value={filters.transmissieType || ""}
                onChange={onFilterChange}
            >
                <option value="">Alle transmissies</option>
                <option value="Handgeschakeld">Handmatig</option>
                <option value="Automatisch">Automatisch</option>
            </select>
            <input
                type="number"
                name="bagageruimte"
                placeholder="Min bagageruimte (L)"
                value={filters.bagageruimte || ""}
                onChange={onFilterChange}
            />
        </>
    );

    const camperFilters = (
        <>
            <input
                type="number"
                name="lengte"
                placeholder="Max lengte (m)"
                value={filters.lengte || ""}
                onChange={onFilterChange}
            />
            <input
                type="number"
                name="hoogte"
                placeholder="Max hoogte (m)"
                value={filters.hoogte || ""}
                onChange={onFilterChange}
            />
            <input
                type="number"
                name="slaapplaatsen"
                placeholder="Min slaapplaatsen"
                value={filters.slaapplaatsen || ""}
                onChange={onFilterChange}
            />
            <input
                type="checkbox"
                name="heeftBadkamer"
                checked={filters.heeftBadkamer || false}
                onChange={(e) => onFilterChange({ target: { name: "heeftBadkamer", value: e.target.checked } })}
            />
            <label>Heeft badkamer</label>
            <input
                type="checkbox"
                name="heeftKeuken"
                checked={filters.heeftKeuken || false}
                onChange={(e) => onFilterChange({ target: { name: "heeftKeuken", value: e.target.checked } })}
            />
            <label>Heeft keuken</label>
            <input
                type="number"
                name="waterTankCapaciteit"
                placeholder="Min watertank capaciteit (L)"
                value={filters.waterTankCapaciteit || ""}
                onChange={onFilterChange}
            />
            <input
                type="number"
                name="afvalTankCapaciteit"
                placeholder="Min afvalwatertank capaciteit (L)"
                value={filters.afvalTankCapaciteit || ""}
                onChange={onFilterChange}
            />
            <input
                type="checkbox"
                name="heeftZonnepanelen"
                checked={filters.heeftZonnepanelen || false}
                onChange={(e) => onFilterChange({ target: { name: "heeftZonnepanelen", value: e.target.checked } })}
            />
            <label>Heeft zonnepanelen</label>
            <input
                type="number"
                name="fietsRekCapaciteit"
                placeholder="Min fietrek capaciteit"
                value={filters.fietsRekCapaciteit || ""}
                onChange={onFilterChange}
            />
            <input
                type="checkbox"
                name="heeftLuifel"
                checked={filters.heeftLuifel || false}
                onChange={(e) => onFilterChange({ target: { name: "heeftLuifel", value: e.target.checked } })}
            />
            <label>Heeft luifel</label>
        </>
    );

    const caravanFilters = (
        <>
            <input
                type="number"
                name="lengte"
                placeholder="Max lengte (m)"
                value={filters.lengte || ""}
                onChange={onFilterChange}
            />
            <input
                type="number"
                name="slaapplaatsen"
                placeholder="Min slaapplaatsen"
                value={filters.slaapplaatsen || ""}
                onChange={onFilterChange}
            />
            <input
                type="checkbox"
                name="heeftKeuken"
                checked={filters.heeftKeuken || false}
                onChange={(e) => onFilterChange({ target: { name: "heeftKeuken", value: e.target.checked } })}
            />
            <label>Heeft keuken</label>
            <input
                type="number"
                name="waterTankCapaciteit"
                placeholder="Min watertank capaciteit (L)"
                value={filters.waterTankCapaciteit || ""}
                onChange={onFilterChange}
            />
            <input
                type="number"
                name="afvalTankCapaciteit"
                placeholder="Min afvalwatertank capaciteit (L)"
                value={filters.afvalTankCapaciteit || ""}
                onChange={onFilterChange}
            />
            <input
                type="checkbox"
                name="heeftLuifel"
                checked={filters.heeftLuifel || false}
                onChange={(e) => onFilterChange({ target: { name: "heeftLuifel", value: e.target.checked } })}
            />
            <label>Heeft luifel</label>
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