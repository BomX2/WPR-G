import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Navigation from './componements/Navigation';
import Home from './Pages/Home';
import About from './pages/About';
import Registratie from './Pages/Registratie';
import Inlog from './Pages/Inlog';
import Catalogus from './Pages/Catalogus';
import AccSettings from './Pages/AccountSettings';
import Product from './Pages/Product';
import RegistreerBedrijf from './Pages/BedrijfsRegistratie';
import Abonnementen from './Pages/Abonnement';

const App = () => {
     
    return (
        
        <div>
            <Navigation />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/about" element={<About />} />\
                <Route path="/catalogus" element={<Catalogus />} />
                <Route path="/Product/:id" element={<Product />} />
                <Route path="/inlog" element={<Inlog/> } />
                <Route path="/registratie" element={<Registratie />} />
                <Route path="/AccountSettings" element={<AccSettings />} />
                  <Route path="/BedrijfsRegistratie" element={<RegistreerBedrijf />} />
                  <Route path="/Abonnement" element={<Abonnementen />} />
            </Routes>
        </div>
    );
}

export default App;