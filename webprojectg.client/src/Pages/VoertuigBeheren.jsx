import React, { useState } from 'react';
import AuthorizeView from "../componements/AuthorizeView";

const VoertuigBeheren = () => {
    return (
        <AuthorizeView allowedRoles={["BackOffice"]}>
            {({ user }) => (
                <div>
                    <h1>Looks like you are a {user?.role}!</h1>
                </div>
            )}
        </AuthorizeView>
    );
};
export default VoertuigBeheren;
