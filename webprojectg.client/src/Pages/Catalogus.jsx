import React from 'react';
import './Catalogus.css'
import Products from '../componements/Products/Products'
import SideBar from '../componements/SideBar/SideBar'
export default function Catalogus() {
    return (
        <div className="catalogus-container">
        <SideBar/>
            <div className="content">
                <h1>catalogus nr </h1>

            <Products props='1' />
            <Products props='2' />
            <Products props='3' />
            <Products props='4' />
            <Products props='5' />
            <Products props='6' />
            <Products props='7' />
            <Products props='8' />
            </div>
        </div>
    );
};
