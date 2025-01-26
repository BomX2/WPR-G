import React, { useState } from 'react';
import AuthorizeView from "../componements/AuthorizeView";

const VoertuigBeheren = () => {
    const [action, setAction] = useState(null); // "add" or "modify" or null
    const [formData, setFormData] = useState({
        soort: "",
        huurStatus: "",
        merk: "",
        type: "",
        kenteken: "",
        kleur: "",
        aanschafJaar: "",
        prijsPerDag: "",
        inclusiefVerzekering: false,
        extraFields: {},
    });

    const handleOptionSelect = (option) => {
        setAction(option);
    };

    const handleCancel = () => {
        setAction(null);
        setFormData({
            soort: "",
            huurStatus: "",
            merk: "",
            type: "",
            kenteken: "",
            kleur: "",
            aanschafJaar: "",
            prijsPerDag: "",
            inclusiefVerzekering: false,
            extraFields: {},
        });
    };

    const handleInputChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: type === "checkbox" ? checked : value,
        }));
    };

    const handleExtraFieldChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            extraFields: {
                ...prev.extraFields,
                [name]: value,
            },
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch("https://localhost:5173/api/voertuigen/createVoertuig", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(formData),
            });
            if (response.ok) {
                alert("Voertuig succesvol toegevoegd!");
                handleCancel();
            } else {
                alert("Fout bij het toevoegen van voertuig.");
            }
        } catch (error) {
            console.error("Error submitting form:", error);
            alert("Er is een fout opgetreden.");
        }
    };

    const renderForm = () => {
        return (
            <form onSubmit={handleSubmit} className="space-y-4 p-4 border rounded">
                <div>
                    <label htmlFor="soort" className="block font-medium">
                        Soort voertuig
                    </label>
                    <select
                        id="soort"
                        name="soort"
                        value={formData.soort}
                        onChange={handleInputChange}
                        className="border p-2 rounded w-full"
                    >
                        <option value="">Selecteer soort</option>
                        <option value="auto">Auto</option>
                        <option value="camper">Camper</option>
                        <option value="caravan">Caravan</option>
                    </select>
                </div>

                {/* Common Fields */}
                <div>
                    <label htmlFor="kenteken" className="block font-medium">
                        Kenteken
                    </label>
                    <input
                        type="text"
                        id="kenteken"
                        name="kenteken"
                        value={formData.kenteken}
                        onChange={handleInputChange}
                        className="border p-2 rounded w-full"
                    />
                </div>

                <div>
                    <label htmlFor="huurStatus" className="block font-medium">
                        Huurstatus
                    </label>
                    <input
                        type="text"
                        id="huurStatus"
                        name="huurStatus"
                        value={formData.huurStatus}
                        onChange={handleInputChange}
                        className="border p-2 rounded w-full"
                    />
                </div>

                <div>
                    <label htmlFor="merk" className="block font-medium">
                        Merk
                    </label>
                    <input
                        type="text"
                        id="merk"
                        name="merk"
                        value={formData.merk}
                        onChange={handleInputChange}
                        className="border p-2 rounded w-full"
                    />
                </div>

                <div>
                    <label htmlFor="type" className="block font-medium">
                        Type
                    </label>
                    <input
                        type="text"
                        id="type"
                        name="type"
                        value={formData.type}
                        onChange={handleInputChange}
                        className="border p-2 rounded w-full"
                    />
                </div>

                <div>
                    <label htmlFor="kleur" className="block font-medium">
                        Kleur
                    </label>
                    <input
                        type="text"
                        id="kleur"
                        name="kleur"
                        value={formData.kleur}
                        onChange={handleInputChange}
                        className="border p-2 rounded w-full"
                    />
                </div>

                <div>
                    <label htmlFor="aanschafJaar" className="block font-medium">
                        Aanschafjaar
                    </label>
                    <input
                        type="number"
                        id="aanschafJaar"
                        name="aanschafJaar"
                        value={formData.aanschafJaar}
                        onChange={handleInputChange}
                        className="border p-2 rounded w-full"
                    />
                </div>

                <div>
                    <label htmlFor="prijsPerDag" className="block font-medium">
                        Prijs per dag
                    </label>
                    <input
                        type="number"
                        id="prijsPerDag"
                        name="prijsPerDag"
                        value={formData.prijsPerDag}
                        onChange={handleInputChange}
                        className="border p-2 rounded w-full"
                    />
                </div>

                <div>
                    <label htmlFor="inclusiefVerzekering" className="block font-medium">
                        Inclusief verzekering
                    </label>
                    <input
                        type="checkbox"
                        id="inclusiefVerzekering"
                        name="inclusiefVerzekering"
                        checked={formData.inclusiefVerzekering}
                        onChange={handleInputChange}
                        className="mr-2"
                    />
                </div>

                {/* Conditional Fields Based on Soort */}
                {formData.soort === "auto" && (
                    <div>
                        <label htmlFor="aantalDeuren" className="block font-medium">
                            Aantal Deuren
                        </label>
                        <input
                            type="text"
                            id="aantalDeuren"
                            name="aantalDeuren"
                            value={formData.extraFields.aantalDeuren || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}
                {formData.soort === "auto" && (
                    <div>
                        <label htmlFor="brandstofType" className="block font-medium">
                            Brandstof type
                        </label>
                        <input
                            type="text"
                            id="brandstofType"
                            name="brandstofType"
                            value={formData.extraFields.brandstofType || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "auto" && (
                    <div>
                        <label htmlFor="heeftAirco" className="block font-medium">
                            Heeft Airco
                        </label>
                        <input
                            type="checkbox"
                            id="heeftAirco"
                            name="heeftAirco"
                            value={formData.extraFields.heeftAirco || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "auto" && (
                    <div>
                        <label htmlFor="brandstofVerbruik" className="block font-medium">
                            Brandstof verbruik
                        </label>
                        <input
                            type="text"
                            id="brandstofVerbruik"
                            name="brandstofVerbruik"
                            value={formData.extraFields.brandstofVerbruik || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "auto" && (
                    <div>
                        <label htmlFor="transmissieType" className="block font-medium">
                            Transmissietype
                        </label>
                        <input
                            type="text"
                            id="transmissieType"
                            name="transmissieType"
                            value={formData.extraFields.transmissieType || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "auto" && (
                    <div>
                        <label htmlFor="bagageRuimte" className="block font-medium">
                            Bagage ruimte
                        </label>
                        <input
                            type="text"
                            id="bagageRuimte"
                            name="bagageRuimte"
                            value={formData.extraFields.bagageRuimte || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "camper" && (
                    <div>
                        <label htmlFor="lengte" className="block font-medium">
                            Lengte
                        </label>
                        <input
                            type="text"
                            id="lengte"
                            name="lengte"
                            value={formData.extraFields.lengte || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "camper" && (
                    <div>
                        <label htmlFor="hoogte" className="block font-medium">
                            Hoogte
                        </label>
                        <input
                            type="text"
                            id="hoogte"
                            name="hoogte"
                            value={formData.extraFields.hoogte || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "camper" && (
                    <div>
                        <label htmlFor="slaapplaatsen" className="block font-medium">
                            Aantal Slaapplaatsen
                        </label>
                        <input
                            type="text"
                            id="slaapplaatsen"
                            name="slaapplaatsen"
                            value={formData.extraFields.slaapplaatsen || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "camper" && (
                    <div>
                        <label htmlFor="heeftBadkamer" className="block font-medium">
                            Heeft badkamer
                        </label>
                        <input
                            type="checkbox"
                            id="heeftBadkamer"
                            name="heeftBadkamer"
                            value={formData.extraFields.heeftBadkamer || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "camper" && (
                    <div>
                        <label htmlFor="heeftKeuken" className="block font-medium">
                            heeft Keuken
                        </label>
                        <input
                            type="checkbox"
                            id="heeftKeuken"
                            name="heeftKeuken"
                            value={formData.extraFields.heeftKeuken || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "camper" && (
                    <div>
                        <label htmlFor="waterTankCapaciteit" className="block font-medium">
                            Watertank Capaciteit
                        </label>
                        <input
                            type="text"
                            id="waterTankCapaciteit"
                            name="waterTankCapaciteit"
                            value={formData.extraFields.waterTankCapaciteit || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "camper" && (
                    <div>
                        <label htmlFor="afvalTankCapaciteit" className="block font-medium">
                            Afvaltank Capaciteit
                        </label>
                        <input
                            type="text"
                            id="afvalTankCapaciteit"
                            name="afvalTankCapaciteit"
                            value={formData.extraFields.afvalTankCapaciteit || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "camper" && (
                    <div>
                        <label htmlFor="brandstofVerbuikC" className="block font-medium">
                            Brandstof Verbruik
                        </label>
                        <input
                            type="text"
                            id="brandstofVerbuikC"
                            name="brandstofVerbuikC"
                            value={formData.extraFields.brandstofVerbuikC || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "camper" && (
                    <div>
                        <label htmlFor="heeftZonnepanelen" className="block font-medium">
                            Heeft Zonnepanelen
                        </label>
                        <input
                            type="checkbox"
                            id="heeftZonnepanelen"
                            name="heeftZonnepanelen"
                            value={formData.extraFields.heeftZonnepanelen || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "camper" && (
                    <div>
                        <label htmlFor="fietsRekCapaciteit" className="block font-medium">
                            Fietsrek Capaciteit
                        </label>
                        <input
                            type="text"
                            id="fietsRekCapaciteit"
                            name="fietsRekCapaciteit"
                            value={formData.extraFields.fietsRekCapaciteit || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "camper" && (
                    <div>
                        <label htmlFor="heeftLuifel" className="block font-medium">
                            Heeft Luifel
                        </label>
                        <input
                            type="checkbox"
                            id="heeftLuifel"
                            name="heeftLuifel"
                            value={formData.extraFields.heeftLuifel || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "caravan" && (
                    <div>
                        <label htmlFor="lengteVan" className="block font-medium">
                            Lengte
                        </label>
                        <input
                            type="text"
                            id="lengteVan"
                            name="lengteVan"
                            value={formData.extraFields.lengteVan || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "caravan" && (
                    <div>
                        <label htmlFor="slaapplaatsenVan" className="block font-medium">
                            Aantal Slaapplaatsen
                        </label>
                        <input
                            type="text"
                            id="slaapplaatsenVan"
                            name="slaapplaatsenVan"
                            value={formData.extraFields.slaapplaatsenVan || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "caravan" && (
                    <div>
                        <label htmlFor="heeftKeukenVan" className="block font-medium">
                            Heeft Keuken
                        </label>
                        <input
                            type="checkbox"
                            id="heeftKeukenVan"
                            name="heeftKeukenVan"
                            value={formData.extraFields.heeftKeukenVan || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "caravan" && (
                    <div>
                        <label htmlFor="watertankCapaciteitVan" className="block font-medium">
                            Watertank Capaciteit
                        </label>
                        <input
                            type="text"
                            id="watertankCapaciteitVan"
                            name="watertankCapaciteitVan"
                            value={formData.extraFields.watertankCapaciteitVan || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "caravan" && (
                    <div>
                        <label htmlFor="afvaltankCapaciteitVan" className="block font-medium">
                            Afvaltank Capaciteit
                        </label>
                        <input
                            type="text"
                            id="afvaltankCapaciteitVan"
                            name="afvaltankCapaciteitVan"
                            value={formData.extraFields.afvaltankCapaciteitVan || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}

                {formData.soort === "caravan" && (
                    <div>
                        <label htmlFor="heeftLuifelVan" className="block font-medium">
                            Heeft Luifel
                        </label>
                        <input
                            type="checkbox"
                            id="heeftLuifelVan"
                            name="heeftLuifelVan"
                            value={formData.extraFields.heeftLuifelVan || ""}
                            onChange={handleExtraFieldChange}
                            className="border p-2 rounded w-full"
                        />
                    </div>
                )}
                {/* Add more conditional fields for camper or caravan here */}

                <div className="flex gap-2">
                    <button
                        type="submit"
                        className="bg-blue-500 text-white p-2 rounded hover:bg-blue-600"
                    >
                        Maak
                    </button>
                    <button
                        type="button"
                        onClick={handleCancel}
                        className="bg-gray-500 text-white p-2 rounded hover:bg-gray-600"
                    >
                        Terug
                    </button>
                </div>
            </form>
        );
    };

    return (
        <AuthorizeView allowedRoles={["BackOffice"]}>
            {({ user }) => (
                <div className="p-4">
                    {!action && (
                        <div className="space-y-4">
                            <button
                                onClick={() => handleOptionSelect("add")}
                                className="bg-green-500 text-white p-2 rounded hover:bg-green-600"
                            >
                                Voertuig toevoegen
                            </button>
                            <button
                                onClick={() => handleOptionSelect("modify")}
                                className="bg-yellow-500 text-white p-2 rounded hover:bg-yellow-600"
                            >
                                Voertuig wijzigen
                            </button>
                        </div>
                    )}

                    {action === "add" && (
                        <div>
                            <h2 className="text-xl font-bold mb-4">Voertuig toevoegen</h2>
                            {renderForm()}
                        </div>
                    )}

                    {action === "modify" && (
                        <div>
                            <h2 className="text-xl font-bold mb-4">Voertuig wijzigen</h2>
                            {/* Modify form implementation here */}
                            <button
                                type="button"
                                onClick={handleCancel}
                                className="bg-gray-500 text-white p-2 rounded hover:bg-gray-600"
                            >
                                Cancel
                            </button>
                        </div>
                    )}
                </div>
            )}
        </AuthorizeView>
    );
}
export default VoertuigBeheren;

//const VoertuigBeheren = () => {
//    const [soort, setSoort] = useState(""); // Tracks the type of vehicle
//    const [formData, setFormData] = useState({}); // Tracks form inputs

//    // Handles input changes dynamically
//    const handleInputChange = (e) => {
//        const { name, value, type, checked } = e.target;
//        setFormData({
//            ...formData,
//            [name]: type === "checkbox" ? checked : value,
//        });
//    };

//    // Submits the form to the API
//    const handleSubmit = async (e) => {
//        e.preventDefault();

//        try {
//            const response = await fetch("/api/voertuigen/createVoertuig", {
//                method: "POST",
//                headers: {
//                    "Content-Type": "application/json",
//                },
//                body: JSON.stringify(formData),
//            });

//            if (!response.ok) {
//                const errorData = await response.json();
//                alert(`Error: ${errorData.message}`);
//                return;
//            }

//            alert("Vehicle successfully added!");
//            setFormData({}); // Reset the form after submission
//        } catch (error) {
//            alert(`An error occurred: ${error.message}`);
//        }
//    };

//    // Render specific fields based on the vehicle type
//    const renderSpecificFields = () => {
//        switch (soort) {
//            case "auto":
//                return (
//                    <>
//                        <div>
//                            <label>Aantal Deuren</label>
//                            <input
//                                type="number"
//                                name="AantalDeuren"
//                                value={formData.AantalDeuren || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Brandstof Type</label>
//                            <input
//                                type="text"
//                                name="BrandstofType"
//                                value={formData.BrandstofType || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Heeft Airco</label>
//                            <input
//                                type="checkbox"
//                                name="HeeftAirco"
//                                checked={formData.HeeftAirco || false}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Brandstof Verbruik</label>
//                            <input
//                                type="text"
//                                name="BrandstofVerbruik"
//                                value={formData.BrandstofVerbruik || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Transmissie Type</label>
//                            <input
//                                type="text"
//                                name="TransmissieType"
//                                value={formData.TransmissieType || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Bagageruimte</label>
//                            <input
//                                type="number"
//                                name="Bagageruimte"
//                                value={formData.Bagageruimte || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                    </>
//                );
//            case "camper":
//                return (
//                    <>
//                        <div>
//                            <label>Lengte</label>
//                            <input
//                                type="number"
//                                name="Lengte"
//                                value={formData.Lengte || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Hoogte</label>
//                            <input
//                                type="number"
//                                name="Hoogte"
//                                value={formData.Hoogte || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Slaapplaatsen</label>
//                            <input
//                                type="number"
//                                name="Slaapplaatsen"
//                                value={formData.Slaapplaatsen || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Heeft Badkamer</label>
//                            <input
//                                type="checkbox"
//                                name="HeeftBadkamer"
//                                checked={formData.HeeftBadkamer || false}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                    </>
//                );
//            case "caravan":
//                return (
//                    <>
//                        <div>
//                            <label>Lengte</label>
//                            <input
//                                type="number"
//                                name="Lengte"
//                                value={formData.Lengte || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Slaapplaatsen</label>
//                            <input
//                                type="number"
//                                name="Slaapplaatsen"
//                                value={formData.Slaapplaatsen || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Heeft Keuken</label>
//                            <input
//                                type="checkbox"
//                                name="HeeftKeuken"
//                                checked={formData.HeeftKeuken || false}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        {/* Add other caravan-specific fields here */}
//                    </>
//                );
//            default:
//                return null;
//        }
//    };

//    return (
//        <AuthorizeView allowedRoles={["BackOffice"]}>
//            {({ user }) => (
//                <div className="p-4">
//                    <h1 className="text-xl font-bold">Voertuig Beheren</h1>
//                    <form onSubmit={handleSubmit} className="space-y-4">

//                        <div>
//                            <label>Kenteken</label>
//                            <input
//                                type="text"
//                                name="Kenteken"
//                                value={formData.Kenteken || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Merk</label>
//                            <input
//                                type="text"
//                                name="Merk"
//                                value={formData.Merk || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Kleur</label>
//                            <input
//                                type="text"
//                                name="Kleur"
//                                value={formData.Kleur || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Aanschafjaar</label>
//                            <input
//                                type="text"
//                                name="Aanschafjaar"
//                                value={formData.Aanschafjaar || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Prijs per Dag</label>
//                            <input
//                                type="text"
//                                name="PrijsPerDag"
//                                value={formData.PrijsPerDag || ""}
//                                onChange={handleInputChange}
//                            />
//                        </div>
//                        <div>
//                            <label>Soort</label>
//                            <select
//                                name="Soort"
//                                value={soort}
//                                onChange={(e) => {
//                                    setSoort(e.target.value);
//                                    handleInputChange(e);
//                                }}
//                            >
//                                <option value="">Selecteer een soort</option>
//                                <option value="auto">Auto</option>
//                                <option value="camper">Camper</option>
//                                <option value="caravan">Caravan</option>
//                            </select>
//                        </div>

//                        {/* Dynamic fields */}
//                        {renderSpecificFields()}

//                        <button type="submit" className="bg-blue-500 text-white px-4 py-2 rounded">
//                            Voeg Voertuig Toe
//                        </button>
//                    </form>
//                </div>
//            )}</AuthorizeView>
//    );
//};
//export default VoertuigBeheren;