/* eslint-disable react/prop-types */
import "./Products.css";
import carImage from "../../image/kever.jpg";
import { Link } from "react-router-dom";
export default function Products({ auto }) {

    return (
        <section className="products-container">
            <section className="products">
                <h1 className="products-title">{auto.voertuig.merk} {auto.voertuig.type}</h1>
                <div className="products-content">
                    <img src={carImage} alt="auto" className="products-image" />
                    <div className="products-details">
                        <h3>prijs per dag: {auto.voertuig.prijsPerDag} euro per dag</h3>
                        <h3>{auto.kenteken}</h3>
                        <h3>{ auto.voertuig.kleur }</h3>
                        <Link to={`/Product/${auto.kenteken}`}>Klik hier</Link>
                    </div>
                </div>
            </section>
        </section>
    );
}