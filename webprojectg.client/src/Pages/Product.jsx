import React from 'react';
import { useParams } from "react-router-dom";
export default function Product() {
    const { id } = useParams()
    return (
        <div>
            <h1>catalogus nr {id}</h1>

        </div>
    );
};