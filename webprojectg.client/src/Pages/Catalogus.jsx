import React, { useEffect, useState } from 'react';
import './Catalogus.css'
import Products from '../componements/Products/Products'
import SideBar from '../componements/SideBar/SideBar'
export default function Catalogus() {
    const [autos, setautos] = useState([]);

    useEffect(() => {
        const fetchAutos = async () => {
            try {
                const response = await fetch("https://localhost:7065/api/gebruikers/autos");
                const data = await response.json();
                setautos(data);
            } catch (error) {
                console.error('Error fetching autos', error);
            }
        };
        fetchAutos();
    }, []);

    return (
        <div className="catalogus-container">
        <SideBar/>
            <div className="content">
                <h1>catalogus nr </h1>
                {autos.map(auto => (
                    <Products auto={auto} />
                ))}
            </div>
        </div>
    );
};
