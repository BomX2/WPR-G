import React, { createContext, useContext, useEffect, useState } from 'react';

const UserContext = createContext();

export const UserProvider = ({ children }) => {
    const [user, setUser] = useState(() => {
        const savedUser = localStorage.getItem('user');
        return savedUser ? JSON.parse(savedUser) : null;
    });

    useEffect(() => {
        if (user) {
            console.log('User state updated:', user);
            localStorage.setItem('user', JSON.stringify(user));
        } else {
            console.log('User logged out, clearing localStorage.');
            localStorage.removeItem('user');
        }
    }, [user]);

    
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