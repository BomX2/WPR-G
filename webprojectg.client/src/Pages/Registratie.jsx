
import "./Registratie.css";
import { useNavigate } from 'react-router-dom';

const Registratie = () => {

    const navigate = useNavigate();

    const goBack = () => {
        navigate('/Inlog')
    }

    return (
        <>
            <div className='container'>
                <div className="header">
                    <div className="text">Sign Up</div>

                </div>
                <div className="inputs">
                    <div className="input">
                    <input type="naam" placeholder = "naam"/>
                    </div>
                    <div className="input">
                        <input type="adress" placeholder="adress"/>
                    </div>
                    <div className="input">
                        <input type="telefoon nummer" placeholder= "telefoon nummer" />
                    </div>
                    <div className="input">
                        <input type="email" placeholder="email"/>
                    </div>
                    <div className="input">
                        <input type="wachtwoord" placeholder= "wachtwoord"/>
                    </div>
                </div>
                <div className="submit-container">
                    <div className="submit">
                        <button className="buttons" onClick={goBack}>ga terug</button>
                    </div>
                    <div className="submit">
                        <button className = "buttons">registreer</button>
                    </div>
                </div>
            </div>
        </>
    );
};
export default Registratie