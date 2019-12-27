import React from 'react';
import ImageSearch from './components/Images/ImageSearch';
import './custom.css'
import Header from './components/Images/Header';

const App = (props) => {
    return (
        <React.Fragment>
            <Header />
            <ImageSearch />
        </React.Fragment>
    );
}

export default App;