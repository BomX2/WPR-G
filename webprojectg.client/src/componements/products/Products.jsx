/* eslint-disable react/prop-types */
import "./Products.css";
import carImage from "../../image/kever.jpg";
import { useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom"
export default function Products({ auto }) {
    const location = useLocation();
    const queryparams = new URLSearchParams(location.search);
    const ophaalDatum = queryparams.get("ophaalDatum");
    const ophaalTime = queryparams.get("ophaalTijd");
    const inleverDatum = queryparams.get("inleverDatum");
    const inleverTime = queryparams.get("inleverTijd");
    const verhicleType = queryparams.get("soort");
    const navigate = useNavigate('');
    return (
        <section className="products-container">
            <section className="products">
                <h3 className="products-title">{auto.merk} {auto.type}</h3>
                <div className="products-content">
                    <img src={carImage} alt="auto" className="products-image" />
                    <div className="products-details">
                        <h3>prijs per dag :{auto.prijsPerDag} euro</h3>
                        <h3>{ auto.huurStatus }</h3>
                        <button onClick={() => navigate(`/Product/${encodeURIComponent(auto.kenteken)}?ophaalDatum=${encodeURIComponent(ophaalDatum)}&ophaalTijd=${ophaalTime}
                            &inleverDatum=${encodeURIComponent(inleverDatum)}&inleverTijd=${inleverTime}
                            &soort=${encodeURIComponent(verhicleType)}`) }>Klik hier!</button>
                    </div>
                </div>
            </section>
        </section>
    );
}