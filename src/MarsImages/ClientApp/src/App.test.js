import React from 'react';
import {render, fireEvent, screen} from '@testing-library/react'
import ReactDOM from 'react-dom';
import App from './App';
import ImageResult from './components/Images/ImageResult';
import ImageResults from './components/Images/ImageResults';
import ImageSearch from './components/Images/ImageSearch';
import NoResultsFound from './components/Images/NoResultsFound';

let container = null;
beforeEach(() => {
    container = document.createElement("div");
    document.body.appendChild(container);
});
afterEach(() => {
    ReactDOM.unmountComponentAtNode(container);
    container.remove();
    container = null;
});

it('renders without crashing', async () => {
    ReactDOM.render(<App />, container);
    await new Promise(resolve => setTimeout(resolve, 1000));
});

it('ImageResults Should return null I', async () => {
    const component = ReactDOM.render(<ImageResults />, container);
    expect(component).toBe(null);
});
it('ImageResults Should Render NoResultsFound', async () => {
    const images = [];
    const component = ReactDOM.render(<ImageResults images={images} />, container);
    expect(container.textContent).toEqual('No Results Found');
});
it('ImageResults Should return null II', async () => {
    const images = {};
    const component = ReactDOM.render(<ImageResults images={images} />, container);
    expect(component).toBe(null);
});
it('ImageResult should return null', async () => {
    const component = ReactDOM.render(<ImageResult />, container);
    expect(component).toBe(null);
});
it('ImageResult should Contain formated Name', async () => {
    const metaData = {
        id: 89292,
        rover: {
            name: 'ABC'
        },
        camera: {
            full_name:''
        },
        earth_date: '123'
    };
    const component = ReactDOM.render(<ImageResult metaData={metaData} />, container);
    expect(container.textContent).toContain('ABC - 123');
});