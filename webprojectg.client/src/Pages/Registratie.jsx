import React from 'react';
import "./Registratie.css"

const Registratie = () => {


    return (
        <>
            <div className='container'>
                <div className="header">
                    <div className="text">Sign Up</div>

                </div>
                <div className="inputs">
                    <div className="input">
                    <input type="naam"/>
                    </div>
                    <div className="input">
                        <input type="adress" />
                    </div>
                    <div className="input">
                        <input type="telefoon nummer" />
                    </div>
                    <div className="input">
                        <input type="email" />
                    </div>
                    <div className="input">
                        <input type="wachtwoord" />
                    </div>
                </div>
                </div>
            </div>
        </>
    );
};
export default Registratie