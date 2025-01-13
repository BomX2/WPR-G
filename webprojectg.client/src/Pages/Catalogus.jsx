import React, { useEffect, useState } from 'react';
import './Catalogus.css'
import Products from '../componements/Products/Products'
import SideBar from '../componements/SideBar/SideBar'
export default function Catalogus() {
    const [autos, setautos] = useState([]);

    useEffect(() => {
        const fetchAutos = async () => {
            try {
                const response = await fetch("https://localhost:7065/api/voertuigen/autos");
                const data = await response.json();
                setautos(data);
            } catch (error) {
                console.error('Error fetching autos', error);
            }
        };
        fetchAutos();
    }, []);

    // vergeet niet in een useEffect te zetten
    // const queryParams = URLSearchParams(this.props.location.search);
    // let url = new URL("https://localhost:7065/api/voertuigen/autos");

    // voor elke filter optie doe je dit maar dan met de key in de query param voor de filteroptie dus "minPrijs" vervangen
    // if (queryParams.get("minPrijs")) url.searchParams.append("minPrijs", queryParams.get("minPrijs"));
    // if (queryParams.get("maxPrijs")) url.searchParams.append("maxPrijs", queryParams.get("maxPrijs"));



    return (
        <div className="catalogus-container">
        <SideBar/>
            <div className="content">
                {autos.map(auto => (
                    <Products key={auto.kenteken} auto={auto} />
                ))}
            </div>
        </div>
    );
};
