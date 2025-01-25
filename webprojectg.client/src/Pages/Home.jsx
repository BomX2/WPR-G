import React, { useState } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import TimePicker from 'react-time-picker';
import 'react-time-picker/dist/TimePicker.css';
import './Home.css';
import heroImage from '../image/CARNAL_LOGO.png';
import { useNavigate } from 'react-router-dom';
import SearchFilters from '../componements/filter-bar/FilterBar';

const Home = () => {
    const navigate = useNavigate();

    const handleSearchSubmit = (filters) => {
        const { vehicleType, ophaalDatum, inleverDatum, ophaalTime, inleverTime } = filters;

        navigate(`/Catalogus?ophaalDatum=${encodeURIComponent(ophaalDatum)}&OphaalTijd=${ophaalTime}
            &inleverDatum=${encodeURIComponent(inleverDatum)}&InleverTijd=${inleverTime}
            &soort=${encodeURIComponent(vehicleType)}`);
    };

    return (

        <div className="home">
           
            <section className="hero">
                <img src={heroImage} alt="Hero" className="hero-image" />
                <div className="hero-filter">
                    <SearchFilters onSubmit={handleSearchSubmit} />
                </div>
            </section>

            
            

            
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
                   
                </div>
            </section>

            
            <section className="special-offers">
                <h2>Speciale Aanbiedingen</h2>
                <div className="offer-item">
                    <p>Krijg 20% korting op je eerste huur!</p>
                    <button className="cta-button">Claim Aanbieding</button>
                </div>
            </section>

           
            <section className="benefits">
                <h2>Waarom Huren Bij Ons?</h2>
                <ul>
                    <li>Grote selectie voertuigen</li>
                    <li>Concurrerende prijzen</li>
                    <li>Eenvoudig boekingsproces</li>
                </ul>
            </section>

           
            <section className="newsletter-signup">
                <h2>Blijf Op De Hoogte</h2>
                <p>Schrijf je in voor onze nieuwsbrief en ontvang de nieuwste aanbiedingen en updates.</p>
                <input type="email" placeholder="Voer je e-mailadres in" />
                <button className="cta-button">Abonneer</button>
            </section>

       
            <section className="blog-news">
                <h2>Laatste Nieuws</h2>
                <div className="blog-post">
                    <h3>Blogtitel</h3>
                    <p>Samenvatting van het blogbericht...</p>
                    <button className="cta-button">Lees Meer</button>
                </div>

            </section>

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
