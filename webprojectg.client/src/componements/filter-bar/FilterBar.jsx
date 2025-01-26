import React, { useState } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { useUser } from '../userContext';


const SearchFilters = ({ onSubmit }) => {
    const [vehicleType, setVehicleType] = useState("auto");
    const [ophaalDatum, setOphaalDatum] = useState(null);
    const [inleverDatum, setInleverDatum] = useState(null);
    const [ophaalTime, setOphaalTime] = useState("");
    const [inleverTime, setInleverTime] = useState("");
    const { user } = useUser() || {};

    if (!user) {
        return <div>Loading...</div>; // Handle null user gracefully
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        if (!ophaalDatum || !inleverDatum) {
            alert("Vul alle velden in");
            return;
        }

        const filters = {
            vehicleType,
            ophaalDatum,
            inleverDatum,
            ophaalTime,
            inleverTime,
        };
        onSubmit(filters);
    };

    return (
        <form className="search-filter" onSubmit={handleSubmit}>
            <div className="filters">
                <select
                    onChange={(e) => setVehicleType(e.target.value.trim())}
                    disabled={user.role === "ZakelijkeHuurder"}
                >
                    <option value="" disabled>Voertuigtype</option>
                    <option value="auto">Auto</option>
                    {user.role !== "ZakelijkeHuurder" && (
                        <>
                            <option value="camper">Camper</option>
                            <option value="caravan">Caravan</option>
                        </>
                    )}
                </select>

                <div className="date-picker">
                    <label htmlFor="ophaaldatum">Ophaaldatum:</label>
                    <DatePicker
                        id="ophaalDatum"
                        selected={ophaalDatum}
                        onChange={(date) => setOphaalDatum(date)}
                        selectsStart
                        startDate={ophaalDatum}
                        endDate={inleverDatum}
                        dateFormat="dd/MM/yyyy"
                        placeholderText="Selecteer een ophaaldatum"
                    />
                </div>

                <div className="time-picker">
                    <label>Kies ophaaltijd:</label>
                    <select value={ophaalTime} onChange={(e) => setOphaalTime(e.target.value.trim())}>
                        <option value="" disabled>Ophaaltijd</option>
                        <option value="ochtend">Ochtend</option>
                        <option value="middag">Middag</option>
                        <option value="avond">Avond</option>
                    </select>
                </div>

                <div className="date-picker">
                    <label htmlFor="inleverdatum">Inleverdatum:</label>
                    <DatePicker
                        id="inleverDatum"
                        selected={inleverDatum}
                        onChange={(date) => setInleverDatum(date)}
                        selectsEnd
                        startDate={ophaalDatum}
                        endDate={inleverDatum}
                        minDate={ophaalDatum}
                        dateFormat="dd/MM/yyyy"
                        placeholderText="Selecteer een inleverdatum"
                        disabled={!ophaalDatum}
                    />
                </div>

                <div className="time-picker">
                    <label>Kies inlevertijd:</label>
                    <select value={inleverTime} onChange={(e) => setInleverTime(e.target.value)}>
                        <option value="" disabled>Inlevertijd</option>
                        <option value="ochtend">Ochtend</option>
                        <option value="middag">Middag</option>
                        <option value="avond">Avond</option>
                    </select>
                </div>

                <button className="cta-button" type="submit">Huur Nu</button>
            </div>
        </form>
    );
};

export default SearchFilters;