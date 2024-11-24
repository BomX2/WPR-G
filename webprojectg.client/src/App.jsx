    import logo from './assets/logo.png';
    import ReactDOM from 'react-dom/client';
    import './App.css';
    import { useState } from 'react';

function App() {
    const [code, setCode] = useState("");
    const secretLogIn = (waarde) => {
        setCode(waarde);
        if (waarde === "mib") {
            console.log("Geheime code is ingevoerd");
        }
    };
    return (
        <>
            <img
                src={logo}
                alt="Logo van CarAndAll"
                style={{ width: "200px", height: "auto" }}

            />
            <h2>CarAndAll</h2>
            <p> CarAndAll is een bedrijf dat specialiseert in het verhuren van verschillende soorten voertuigen</p>
            <p>{code }</p>
            <input onInput={(e) => secretLogIn(e.target.value)}
                type="text"
            >
            </input>
        </>
    );
}
const root = ReactDOM.createRoot(document.getElementById('root'));
    root.render(<App />);

    export default App;