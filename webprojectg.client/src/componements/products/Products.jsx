/* eslint-disable react/prop-types */
import "./Products.css";
import carImage from "../../image/kever.jpg";
import { Link } from "react-router-dom";
export default function Products({ auto }) {

    return (
        <section className="products-container">
            <section className="products">
                <h3 className="products-title">{auto.merk} {auto.type}</h3>
                <div className="products-content">
                    <img src={carImage} alt="auto" className="products-image" />
                    <div className="products-details">
                        <h3>prijs per dag :{auto.prijsPerDag} euro</h3>
                        <h3>{ auto.huurStatus }</h3>
                        <Link to={`/Product/${auto.kenteken}`}>Klik hier</Link>
                    </div>
                </div>
            </section>
        </section>
    );
}