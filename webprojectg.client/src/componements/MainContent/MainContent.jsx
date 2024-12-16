import React from 'react';
import '../file.css'
import { useNavigate } from 'react-router-dom';

const MainContent = () => {
    const navigate = useNavigate(); // Verkrijgen van de navigate functie

    const goToAboutPage = () => {
        navigate('/about');  // Navigeren naar de About-pagina
    }; // <-- Add the missing closing brace here

    return (
        <div>
            <h1>Welkom op de Homepagina!</h1>
            <button onClick={goToAboutPage}>Ga naar About</button> {/* Knop om naar de About-pagina te navigeren */}
        </div>
    );
};

export default MainContent;
