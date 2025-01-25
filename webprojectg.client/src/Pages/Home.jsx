import React, { useState } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import TimePicker from 'react-time-picker';
import 'react-time-picker/dist/TimePicker.css';
import './Home.css';
import heroImage from '../image/CARNAL_LOGO.png';
import { useNavigate } from 'react-router-dom';

const Home = () => {
    const [vehicleType, setVehicleType] = useState("");
    const [ophaalDatum, setOphaalDatum] = useState(null);
    const [inleverDatum, setInleverDatum] = useState(null);
    const [ophaalTime, setOphaalTime] = useState("");
    const [inleverTime, setInleverTime] = useState("");

    const navigate = useNavigate();

    const handelsubmit = (e) => {
        e.preventDefault();

        if (!ophaalDatum || !inleverDatum) {
            alert("Vul alle velden in");
            return;
        }
        console.log("OphaalTijd:", ophaalTime);  
        console.log("InleverTijd:", inleverTime);

        navigate(`/Catalogus?ophaalDatum=${encodeURIComponent(ophaalDatum)}&OphaalTijd=${ophaalTime}
                            &inleverDatum=${encodeURIComponent(inleverDatum)}&InleverTijd=${inleverTime}
                            &soort=${encodeURIComponent(vehicleType)}`);
    }

    return (

        <div className="home">
            {/* Hero Section */}
            <section className="hero">
                <img src={heroImage} alt="Hero" className="hero-image" />
                <div className="hero-text">
                    <h1>Jouw Perfecte Rit Wacht</h1>
                    <p>Vind en huur de beste voertuigen tegen onverslaanbare prijzen.</p>
                    <button className="cta-button">Huur Nu</button>
                </div>
            </section>

            {/* Search and Filter Options */}
            <form className="search-filter" onSubmit={handelsubmit}>
                <div className="filters">
                    <select value={vehicleType}
                        onChange={(e) => setVehicleType(e.target.value.trim())} >
                        <option value="" disabled>Voertuigtype</option>
                        <option value="auto">Auto</option>
                        <option value="camper">Camper</option>
                        <option value="caravan">Caravan</option>
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
                    <div className="react-time-picker">
                        <label htmlFor="ophaal-time-picker">Kies ophaaltijd:</label>
                        <select value={ophaalTime}
                            onChange={(e) => setOphaalTime(e.target.value.trim())} >
                            <option value="" disabled>ophaaltijd</option>
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
                            disabled={!ophaalDatum} // Disable until Ophaaldatum is selected
                        />
                    </div>
                    <div className="react-time-picker">
                        <label >Kies inlevertijd:</label>
                        <select value={inleverTime}
                            onChange={(e) => setInleverTime(e.target.value)} >
                            <option value="" disabled>inlevertijd</option>
                            <option value="ochtend">Ochtend</option>
                            <option value="middag">Middag</option>
                            <option value="avond">Avond</option>
                        </select>
                    </div>
                    <button className="cta-button" type="submit">Huur Nu</button>
                </div>
            </form>

            {/* Customer Testimonials */}
            <section className="testimonials">
                <h2>Klantbeoordelingen</h2>
                <div className="testimonial-list">
                    <div className="testimonial-item">
                        <p>Geweldige service en fantastische voertuigen!</p>
                        <h4>- Klantnaam</h4>
                    </div>
                    <div className="testimonial-item">
                        <p>Ik had een geweldige ervaring met huren hier.</p>
                        <h4>- Klantnaam</h4>
                    </div>
                    {/* Voeg meer beoordelingen toe indien nodig */}
                </div>
            </section>

            {/* Special Offers */}
            <section className="special-offers">
                <h2>Speciale Aanbiedingen</h2>
                <div className="offer-item">
                    <p>Krijg 20% korting op je eerste huur!</p>
                    <button className="cta-button">Claim Aanbieding</button>
                </div>
            </section>

            {/* Benefits of Renting with Us */}
            <section className="benefits">
                <h2>Waarom Huren Bij Ons?</h2>
                <ul>
                    <li>Grote selectie voertuigen</li>
                    <li>Concurrerende prijzen</li>
                    <li>Eenvoudig boekingsproces</li>
                </ul>
            </section>

            {/* Newsletter Signup */}
            <section className="newsletter-signup">
                <h2>Blijf Op De Hoogte</h2>
                <p>Schrijf je in voor onze nieuwsbrief en ontvang de nieuwste aanbiedingen en updates.</p>
                <input type="email" placeholder="Voer je e-mailadres in" />
                <button className="cta-button">Abonneer</button>
            </section>

            {/* Blog/News Section */}
            <section className="blog-news">
                <h2>Laatste Nieuws</h2>
                <div className="blog-post">
                    <h3>Blogtitel</h3>
                    <p>Samenvatting van het blogbericht...</p>
                    <button className="cta-button">Lees Meer</button>
                </div>
                {/* Voeg meer blogberichten toe indien nodig */}
            </section>

            {/* Contact Information */}
            <section className="contact-info">
                <h2>Neem Contact Op</h2>
                <p>Email: contact@yourwebsite.com</p>
                <p>Telefoon: +123 456 7890</p>
                <p>Adres: Hoofdstraat 123, Stad, Land</p>
            </section>
        </div>
    );
};
export default Home;
