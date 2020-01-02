import React from 'react';
import {render, fireEvent, screen} from '@testing-library/react'
import ReactDOM from 'react-dom';
import App from './App';
import Header from './components/Images/Header';
import ImageResult from './components/Images/ImageResult';
import ImageResults from './components/Images/ImageResults';
import ImageDateSelection from './components/Images/ImageDateSelection';
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

//  ImageResults tests
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

//  ImageResult tests
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

//  Header Tests
it('Header Should contain Brand', async () => {
    const component = ReactDOM.render(<Header />, container);
    expect(container.textContent).toContain('Mars Images');
});

//  ImageDateSelection
it('ImageDateSelection should render I', async () => {
    //  Stateless components are not returned by render
    const component = ReactDOM.render(<ImageDateSelection />, container);
    expect(component).toBe(null);
});

it('ImageDateSelection should render II', async () => {
    //  Stateless components are not returned by render
    ReactDOM.render(<ImageDateSelection />, container);
    expect(container.getElementsByTagName('option').length).toBe(1);
});

it('ImageDateSelection should render III', async () => {
    //  Stateless components are not returned by render
    var results = [
        {
            date:'2017-02-222',
            name: 'Bob',
            status: 'Pending'
        }
    ];
    ReactDOM.render(<ImageDateSelection results={results} />, container);
    expect(container.getElementsByTagName('option').length).toBe(2);
});

it('ImageDateSelection should render III', async () => {
    //  Stateless components are not returned by render
    var results = [
        {
            date:'2017-02-21',
            name: 'Bob I',
            status: 'Pending'
        },
        {
            date:'2017-02-22',
            name: 'Bob II',
            status: 'Pending'
        }
    ];
    ReactDOM.render(<ImageDateSelection results={results} />, container);
    expect(container.getElementsByTagName('option').length).toBe(3);
});