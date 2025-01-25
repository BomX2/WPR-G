import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Navigation from './componements/Navigation';
import Home from './Pages/Home';
import Registratie from './Pages/Registratie';
import Inlog from './Pages/Inlog';
import Catalogus from './Pages/Catalogus';
import AccSettings from './Pages/AccountSettings';
import Product from './Pages/Product';
import About from './Pages/About';
import RegistreerBedrijf from './Pages/BedrijfsRegistratie';
import Abonnementen from './Pages/Abonnement';
import BedrijfsSettings from './Pages/BedrijfsInstellingen';
import AanvraagBackOffice from './Pages/AanvraagBackOffice';
import FrontOfficeAanvraag from './Pages/FrontAanvraag';
import MedewerkerRegistratie from './Pages/MedewerkerRegistratie';
import SchadeRegistreren from './Pages/SchadePagina';
import { UserProvider } from './componements/UserContext';
const App = () => {

    return (
        <UserProvider>
            <div>
                <Navigation />
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/about" element={<About />} />\
                    <Route path="/catalogus" element={<Catalogus />} />
                    <Route path="/Product/:Kenteken" element={<Product />} />
                    <Route path="/inlog" element={<Inlog />} />
                    <Route path="/registratie" element={<Registratie />} />
                    <Route path="/AccountSettings" element={<AccSettings />} />
                    <Route path="/BedrijfsRegistratie" element={<RegistreerBedrijf />} />
                    <Route path="/Abonnement" element={<Abonnementen />} />
                    <Route path="/BedrijfsInstellingen" element={<BedrijfsSettings />} />
                    <Route path="/AanvraagBackOffice" element={<AanvraagBackOffice />} />
                    <Route path="/FrontAanvraag" element={<FrontOfficeAanvraag />} />
                    <Route path="/MedewerkerRegistratie" element={<MedewerkerRegistratie />} />
                    <Route path="/SchadePagina" element={<SchadeRegistreren /> } />
                 </Routes>
            </div>
        </UserProvider>
    );
}

export default App;