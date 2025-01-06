import React, { useState } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import TimePicker from 'react-time-picker';
import 'react-time-picker/dist/TimePicker.css';
import './Home.css';
import heroImage from '../image/CARNAL_LOGO.png';

const Home = () => {
    const [vehicleType, setVehicleType] = useState("");
    const [ophaalDatum, setOphaalDatum] = useState(null);
    const [inleverDatum, setInleverDatum] = useState(null);
    const [ophaalTime, setOphaalTime] = useState('10:00');
    const [inleverTime, setInleverTime] = useState('10:00');

    return (

        <div className="home">
            {/* Hero Section */}
            <section className="hero">
                <img src={heroImage} alt="Hero" className="hero-image" />
                <div className="hero-text">
                    <h1>Your Perfect Ride Awaits</h1>
                    <p>Find and rent the best vehicles at unbeatable prices.</p>
                    <button className="cta-button">Rent Now</button>
                </div>
            </section>

            {/* Search and Filter Options */}
            <section className="search-filter">
                <input type="text" placeholder="Search for vehicles..." className="search-bar" />
                <div className="filters">
                    <select value={vehicleType}
                        onChange={(e) => setVehicleType(e.target.value)} >
                        <option value="" disabled>Vehicle Type</option>
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
                        <TimePicker
                            id="ophaal-time-picker"
                            value={ophaalTime}
                            onChange={setOphaalTime}
                            disableClock={true} // Disable the clock mode for a simpler dropdown-style picker
                        />
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
                        <label htmlFor="inlever-time-picker">Kies inlevertijd:</label>
                        <TimePicker
                            id="inlever-time-picker"
                            value={inleverTime}
                            onChange={setInleverTime}
                            disableClock={true} // Disable the clock mode for a simpler dropdown-style picker
                        />
                    </div>
                </div>
            </section>

            {/* Customer Testimonials */}
            <section className="testimonials">
                <h2>Customer Testimonials</h2>
                <div className="testimonial-list">
                    <div className="testimonial-item">
                        <p>Great service and amazing vehicles!</p>
                        <h4>- Customer Name</h4>
                    </div>
                    <div className="testimonial-item">
                        <p>I had a fantastic experience renting from here.</p>
                        <h4>- Customer Name</h4>
                    </div>
                    {/* Add more testimonials as needed */}
                </div>
            </section>

            {/* Special Offers */}
            <section className="special-offers">
                <h2>Special Offers</h2>
                <div className="offer-item">
                    <p>Get 20% off on your first rental!</p>
                    <button className="cta-button">Claim Offer</button>
                </div>
            </section>

            {/* Benefits of Renting with Us */}
            <section className="benefits">
                <h2>Why Rent with Us?</h2>
                <ul>
                    <li>Wide selection of vehicles</li>
                    <li>Competitive prices</li>
                    <li>Easy booking process</li>
                </ul>
            </section>

            {/* Newsletter Signup */}
            <section className="newsletter-signup">
                <h2>Stay Updated</h2>
                <p>Sign up for our newsletter to receive the latest offers and updates.</p>
                <input type="email" placeholder="Enter your email" />
                <button className="cta-button">Subscribe</button>
            </section>

            {/* Blog/News Section */}
            <section className="blog-news">
                <h2>Latest News</h2>
                <div className="blog-post">
                    <h3>Blog Post Title</h3>
                    <p>Summary of the blog post...</p>
                    <button className="cta-button">Read More</button>
                </div>
                {/* Add more blog posts as needed */}
            </section>

            {/* Contact Information */}
            <section className="contact-info">
                <h2>Contact Us</h2>
                <p>Email: contact@yourwebsite.com</p>
                <p>Phone: +123 456 7890</p>
                <p>Address: 123 Main Street, City, Country</p>
            </section>
        </div>
    );
};
export default Home;