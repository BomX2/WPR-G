import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Navigation from './componements/Navigation';
import Home from './Pages/Home';
import About from './pages/About';
import Registratie from './Pages/Registratie'
import Inlog from './Pages/Inlog'
import Catalogus from './Pages/Catalogus'

const App = () => {
     
    return (
        
        <div>
            <Navigation />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/about" element={<About />} />\
                <Route path="/catalogus/:id" element={<Catalogus/>}/>
                <Route path="/inlog" element={<Inlog/> } />
                <Route path="/registratie" element={<Registratie />} />
            </Routes>

        </div>
    );
}

export default App;