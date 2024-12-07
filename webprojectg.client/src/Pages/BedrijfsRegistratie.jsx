import React, { useState } from "react";

const RegistreerBedrijf = () => {
    const [BedrijfsNaam, setBedrijfsNaam] = useState("");
    const [Adres, setAdres] = useState("");
    const [Kvknummer, setKvknummer] = useState("");

    const BedrijfToevoegen = async () => {
        try {

            const Toevoegen = await fetch('https://localhost:7065/api/klant/postbedrijf', {
                method: 'POST', headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    BedrijfsNaam,
                    Adres,
                    Kvknummer,

                }),


            });
            if (Toevoegen.ok) {
                alert("Bedrijfs account succesvol toegevoegd.");
            }
            else {
                alert("er is een fout opgetreden bij het aanmaken van een bedrijfs account");
            }
        }

        catch (error) {
            console.log("Houston we have a problem: ", error)
        }
    }
    return (
        <div>
            <h1>Bedrijfsregistratiepagina</h1>
            <form onSubmit={(e) => {
                e.preventDefault();
                BedrijfToevoegen();
            }} >
                <div>
                    <input type="text" value={BedrijfsNaam}
                        onChange={(e) => setBedrijfsNaam(e.target.value)}
                     placeholder="voer de naam van uw bedrijf in:" >
                    </input>
                    <div>
                        <input type="text" value={Adres}
                            onChange={(e) => setAdres(e.target.value)}
                            placeholder="voer het adres van uw bedrijf in:" >
                        </input>
                    </div>
                </div>
                <div>
                    <input type="text" value={Kvknummer}
                        onChange={(e) => setKvknummer(e.target.value)}
                        placeholder="voer het kvknummer van uw bedrijf in:" >
                    </input>
                </div>
                <button type="submit">bedrijfsaccount aanmaken</button>
            </form>
        </div>
    );
}
export default RegistreerBedrijf;