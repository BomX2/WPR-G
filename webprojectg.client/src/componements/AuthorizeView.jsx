import React, { useEffect, useState } from "react";
import PropTypes from "prop-types";
import { Navigate } from "react-router-dom";

const AuthorizeView = ({ children, allowedRoles }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchUser = async () => {
            try {
                const response = await fetch("https://localhost:7065/api/gebruikers/pingauth", {
                    credentials: "include",
                });
                if (response.ok) {
                    const data = await response.json();
                    console.log("Fetched user data:", data); // Debug log
                    setUser(data);
                } else {
                    console.error("Failed to fetch user:", response.status);
                    setUser(null);
                }
            } catch (err) {
                console.error("Error fetching user info:", err);
                setUser(null);
            } finally {
                setLoading(false);
            }
        };

        fetchUser();
    }, []);

    if (loading) {
        return <p>Loading...</p>;
    }

    const userRole = user?.role; // Since your API returns a single role, not an array

    if (!userRole || !allowedRoles.includes(userRole)) {
        console.log("Access Denied. User Role:", userRole, "Allowed Roles:", allowedRoles);
        return <Navigate to="/access-denied" />;
    }

    return <>{children({ user })}</>;
};

AuthorizeView.propTypes = {
    children: PropTypes.func.isRequired,
    allowedRoles: PropTypes.arrayOf(PropTypes.string).isRequired,
};

export default AuthorizeView;
