import React from "react";
import AuthorizeView from "../componements/AuthorizeView";

const About = () => {
    return (
        <AuthorizeView allowedRoles={["ZakelijkeHuurder", "Particulier", "WagenparkBeheerder"]}>
            {({ user }) => (
                <div>
                    <h1>Looks like you are a {user?.role} user!</h1>
                </div>
            )}
        </AuthorizeView>
    );
};

export default About;
