import "./Products.css";
import carImage from "../../image/kever.jpg";
import { Link } from "react-router-dom";
export default function Products({ props }) {

    return (
        <section className="product-container">
            <section className="product">
                <div className="product-content">
                    <img src={carImage} alt="auto" className="product-image" />
                    <div className="product-details">
                    <h3>auto</h3>
                        <Link to={`/Product/${props}`}>Klik hier</Link>
                    </div>
                </div>
            </section>
        </section>
    );
}