import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import './Pages/index.css';
import App from './App.jsx';
import { UserProvider } from './componements/userContext'; // Ensure correct path

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <UserProvider>
            <BrowserRouter>
                <App />
            </BrowserRouter>
        </UserProvider>
    </StrictMode>
);
