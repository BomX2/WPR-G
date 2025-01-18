import React, { createContext, useContext, useEffect, useState } from 'react';

const UserContext = createContext();

export const UserProvider = ({ children }) => {
    const [user, setUser] = useState(null);

    useEffect(() => {
        const fetchCurrentUser = async () => {
            try {
                const response = await fetch("https://localhost:7065/api/gebruikers/me", {
                    credentials: "include",
                });

                if (response.ok) {
                    const userData = await response.json();
                    setUser(userData);
                    console.log("User context updated:", userData);
                } else {
                    console.log("No authenticated user found.");
                    setUser(null);
                }
            } catch (error) {
                console.error("Error fetching current user:", error);
                setUser(null);
            }
        };

        fetchCurrentUser();
    }, []);

    return (
        <UserContext.Provider value={{ user, setUser }}>
            {children}
        </UserContext.Provider>
    );
};

// Custom hook for accessing the UserContext
export const useUser = () => {
    const context = useContext(UserContext);
    if (!context) {
        throw new Error("useUser must be used within a UserProvider");
    }
    return context;
};