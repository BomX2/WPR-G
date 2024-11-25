import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Navigation from './componements/Navigation';
import Home from './Pages/Home';
import About from './pages/About';

const App = () => {
     
    return (
        
        <div>
            <Navigation />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/about" element={<About />} />
            </Routes>

        </div>
    );
}

export default App;