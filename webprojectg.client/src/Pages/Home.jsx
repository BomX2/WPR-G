import React from 'react';
import MainContent from '../componements/MainContent/MainContent'
import Header from '../componements/Header/Header'
import { useNavigate } from 'react-router-dom';
import Footer from '../componements/Footer/Footer';

const Home = () => {
    return (
        <div>
            <header>
                <Header />
            </header>
                <MainContent />
            <footer>
                <Footer />
            </footer>
        </div>
    );
};
export default Home;