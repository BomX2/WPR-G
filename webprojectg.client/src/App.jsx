import logo from './assets/logo.png';
import ReactDOM from 'react-dom/client';
import './App.css';

function App() {

    const frontpage = (
        <>
            <img
                src={logo} 
                alt="Logo van CarAndAll"
                style={{ width: "200px", height: "auto" }}

            />
            <h2>CarAndAll</h2>
            <p> CarAndAll is een bedrijf dat specialiseert in het verhuren van verschillende soorten voertuigen</p>
            <a
                href="https://www.google.nl/maps/place/De+Haagse+Hogeschool/@52.0670747,4.3213991,17z/data=!3m1!4b1!4m6!3m5!1s0x47c5b6e175fe3619:0x9d1994a880751d7a!8m2!3d52.0670747!4d4.323974!16s%2Fm%2F027_cbq?entry=ttu&g_ep=EgoyMDI0MTExOC4wIKXMDSoASAFQAw%3D%3D"
                target="_blank"
                rel ="noopener noreferrer"
            >
               De haagse hogeschool locatie Den Haag op google maps
            </a>
        </>
    );
    const root = ReactDOM.createRoot(document.getElementById('root'));
    root.render(frontpage);
    }

export default App;