import React from 'react';
import logo from './logo.png';
    import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import AccSettings from '/AccountSettings.Jsx';
import { Link } from 'react-router-dom';
const Home = () => (
    <div style={{ position: "relative", minHeight: "100vh", padding: "20px" }}>
        <Link to="/account-settings" style={{position: "absolute", top: "20px", right: "20px"} } >
            <button style={{
                padding: "10px 20px",
                backgroundColor: "aliceblue",
                color: "black",
                border: "none",
                borderRadius: "5px",
                cursor: "pointer",
                fontSize: "16px"
            }}>
               AccountInstellingen
            </button>
        </Link>
        <h2>CarAndAll</h2>
        <p> CarAndAll is een bedrijf dat specialiseert in het verhuren van verschillende soorten voertuigen</p>
    </div>
     
);
function App() {
    return (
        
        <>
            <Router>
            <img
                src={logo}
                alt="Logo van CarAndAll"
                style={{ width: "200px", height: "auto" }}

                />
                <main>
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/account-settings" element={<AccSettings />} />
                    </Routes>
                </main>
            </Router>
        </>
    );
}

export default App;