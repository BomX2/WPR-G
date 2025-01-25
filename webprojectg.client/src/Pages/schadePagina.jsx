import React, { useState } from "react";
import { useEffect } from 'react';
const SchadeRegistreren = () => {
    useEffect(() => {
        const RoepSchadeFormulierOp = async () => {
            try {
                const RoepOp = await fetch(`https://localhost:7065/api/gebruikers/SchadeFormulier/`)

                if (RoepOp.ok) {
                    alert("")
                }
                else {
                   alert("Er is een fout opgetreden")
                }
            }
            catch (error) {
                console.log("error:", error);
            }
        }
        RoepSchadeFormulierOp();
    },[])
    return (
        <p>kay</p>
    )
}
export default SchadeRegistreren;