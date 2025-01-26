/* eslint-disable react/prop-types */
import "./Products.css";
import carImage from "../../image/kever.jpg";
import { useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom"
export default function Products({ auto }) {
    const location = useLocation();
    const queryparams = new URLSearchParams(location.search);
    const ophaalDatum = queryparams.get("ophaalDatum");
    const OphaalTijd = queryparams.get("OphaalTijd");
    const inleverDatum = queryparams.get("inleverDatum");
    const InleverTijd = queryparams.get("InleverTijd");
    const verhicleType = queryparams.get("soort");
    const navigate = useNavigate('');

    const vehicleImage = `../../images/${auto.voertuig.voertuigFoto}`;
    console.log("Afbeeldingspad:", vehicleImage);

    return (
        <section className="products-container">
            <section className="products">
                <h1 className="products-title">{auto.voertuig.merk} {auto.voertuig.type}</h1>
                <div className="products-content">
                    <img src={carImage} alt="auto" className="products-image" />
                    <div className="products-details">
                        <h3>prijs per dag :{auto.voertuig.prijsPerDag} euro</h3>
                        <h3>{ auto.huurStatus }</h3>
                        <button onClick={() => navigate(`/Product/${encodeURIComponent(auto.kenteken)}?ophaalDatum=${encodeURIComponent(ophaalDatum)}&ophaalTijd=${OphaalTijd}
                            &inleverDatum=${encodeURIComponent(inleverDatum)}&inleverTijd=${InleverTijd}
                            &soort=${encodeURIComponent(verhicleType)}`) }>Klik hier!</button>

                        <h3>{auto.kenteken}</h3>
                        <h3>{ auto.voertuig.kleur }</h3>

                    </div>
                </div>
            </section>
        </section>
    );
}