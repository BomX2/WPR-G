import React, { useState } from 'react';


const AccSettings = () => {
    const [naam, setNaam] = useState('');
    const [adres, setAdres] = useState('');
    const [email, setEmail] = useState('');
    const [telefoonnummer, setTelefoonnummer] = useState('');

    const SaveOnSubmit = async () => {
        try {

            //    method: put;
         //   body: JSON.stringify({
           //     naam,
           //     adres,
       //         email,
                
         //   })
      }
        catch (error) {
            console.error("fout opgetreden bij opslaan gegevens", error)
            alert("Er is een fout opgetreden bij opslaan gegevens")
        }
    }
    return (
        <div>
            <h1>Account Settings</h1>
            <form onSubmit={(e) => {
                e.preventDefault();
                SaveOnSubmit()
            }} >
                <div>
                    <label>
                       naam
                    </label>
                    <input 
                        type="text"
                        value={naam}
                           onChange={(e) => setNaam(e.target.value)}
                        placeholder="verander uw naam"
                    >
                     
                    </input>
                </div>
                <div>
                    <label>
                        adres
                    </label>
                    <input
                       type="text"
                        value={adres}
                        onChange={(e) => setAdres(e.target.value)}
                        placeholder="verander uw adres"
                    >

                    </input>
                </div>
                <div>
                    <label>
                        email
                    </label>
                    <input
                        type="text"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        placeholder="verander uw email adres"
                    >

                    </input>
                </div>
                <div>
                    <label>
                        telefoonnummer
                    </label>
                    <input
                        type="tel"
                        value={telefoonnummer}
                        onChange={(e) => setTelefoonnummer(e.target.value)}
                        placeholder="verander uw telefoonnummer"
                    />

                   
                </div>
                <button type="submit">submit</button>
            </form>
        </div>
    );
};
export default AccSettings