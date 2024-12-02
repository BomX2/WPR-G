import React from 'react';
import './file.css'
import { useNavigate } from 'react-router-dom';

const Home = () => {
    const navigate = useNavigate(); // Verkrijgen van de navigate functie

    const goToAboutPage = () => {
        navigate('/about');  // Navigeren naar de About-pagina
    };
    const goToAccSettingsPage = () => {
        navigate('/AccouuntSettinggs')
    }
    return (
        <div>
            <h1>Welkom op de Homepagina!</h1>
            <button onClick={goToAboutPage}>Ga naar About</button> {/* Knop om naar de About-pagina te navigeren */}
            <button onClick={goToAccSettingsPage}>account settigs</button>
        </div>
    );
};
export default Home;
