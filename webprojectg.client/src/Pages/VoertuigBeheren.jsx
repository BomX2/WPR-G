import React, { useState } from 'react';
import AuthorizeView from "../componements/AuthorizeView";

const VoertuigBeheren = () => {
    const [soort, setSoort] = useState(""); // Tracks the type of vehicle
    const [formData, setFormData] = useState({}); // Tracks form inputs

    // Handles input changes dynamically
    const handleInputChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData({
            ...formData,
            [name]: type === "checkbox" ? checked : value,
        });
    };

    // Submits the form to the API
    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch("/api/voertuigen/createVoertuig", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(formData),
            });

            if (!response.ok) {
                const errorData = await response.json();
                alert(`Error: ${errorData.message}`);
                return;
            }

            alert("Vehicle successfully added!");
            setFormData({}); // Reset the form after submission
        } catch (error) {
            alert(`An error occurred: ${error.message}`);
        }
    };

    // Render specific fields based on the vehicle type
    const renderSpecificFields = () => {
        switch (soort) {
            case "auto":
                return (
                    <>
                        <div>
                            <label>Aantal Deuren</label>
                            <input
                                type="number"
                                name="AantalDeuren"
                                value={formData.AantalDeuren || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Brandstof Type</label>
                            <input
                                type="text"
                                name="BrandstofType"
                                value={formData.BrandstofType || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Heeft Airco</label>
                            <input
                                type="checkbox"
                                name="HeeftAirco"
                                checked={formData.HeeftAirco || false}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Brandstof Verbruik</label>
                            <input
                                type="text"
                                name="BrandstofVerbruik"
                                value={formData.BrandstofVerbruik || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Transmissie Type</label>
                            <input
                                type="text"
                                name="TransmissieType"
                                value={formData.TransmissieType || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Bagageruimte</label>
                            <input
                                type="number"
                                name="Bagageruimte"
                                value={formData.Bagageruimte || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                    </>
                );
            case "camper":
                return (
                    <>
                        <div>
                            <label>Lengte</label>
                            <input
                                type="number"
                                name="Lengte"
                                value={formData.Lengte || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Hoogte</label>
                            <input
                                type="number"
                                name="Hoogte"
                                value={formData.Hoogte || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Slaapplaatsen</label>
                            <input
                                type="number"
                                name="Slaapplaatsen"
                                value={formData.Slaapplaatsen || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Heeft Badkamer</label>
                            <input
                                type="checkbox"
                                name="HeeftBadkamer"
                                checked={formData.HeeftBadkamer || false}
                                onChange={handleInputChange}
                            />
                        </div>
                    </>
                );
            case "caravan":
                return (
                    <>
                        <div>
                            <label>Lengte</label>
                            <input
                                type="number"
                                name="Lengte"
                                value={formData.Lengte || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Slaapplaatsen</label>
                            <input
                                type="number"
                                name="Slaapplaatsen"
                                value={formData.Slaapplaatsen || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Heeft Keuken</label>
                            <input
                                type="checkbox"
                                name="HeeftKeuken"
                                checked={formData.HeeftKeuken || false}
                                onChange={handleInputChange}
                            />
                        </div>
                        {/* Add other caravan-specific fields here */}
                    </>
                );
            default:
                return null;
        }
    };

    return (
        <AuthorizeView allowedRoles={["BackOffice"]}>
            {({ user }) => (
                <div className="p-4">
                    <h1 className="text-xl font-bold">Voertuig Beheren</h1>
                    <form onSubmit={handleSubmit} className="space-y-4">
                        
                        <div>
                            <label>Kenteken</label>
                            <input
                                type="text"
                                name="Kenteken"
                                value={formData.Kenteken || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Merk</label>
                            <input
                                type="text"
                                name="Merk"
                                value={formData.Merk || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Kleur</label>
                            <input
                                type="text"
                                name="Kleur"
                                value={formData.Kleur || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Aanschafjaar</label>
                            <input
                                type="text"
                                name="Aanschafjaar"
                                value={formData.Aanschafjaar || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Prijs per Dag</label>
                            <input
                                type="text"
                                name="PrijsPerDag"
                                value={formData.PrijsPerDag || ""}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div>
                            <label>Soort</label>
                            <select
                                name="Soort"
                                value={soort}
                                onChange={(e) => {
                                    setSoort(e.target.value);
                                    handleInputChange(e);
                                }}
                            >
                                <option value="">Selecteer een soort</option>
                                <option value="auto">Auto</option>
                                <option value="camper">Camper</option>
                                <option value="caravan">Caravan</option>
                            </select>
                        </div>

                        {/* Dynamic fields */}
                        {renderSpecificFields()}

                        <button type="submit" className="bg-blue-500 text-white px-4 py-2 rounded">
                            Voeg Voertuig Toe
                        </button>
                    </form>
                </div>
            )}</AuthorizeView>
    );
};
export default VoertuigBeheren;