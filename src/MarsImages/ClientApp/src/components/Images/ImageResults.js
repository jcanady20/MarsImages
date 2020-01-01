import React from 'react';
import ImageResult from './ImageResult';
import NoResultsFound from './NoResultsFound';

const ImageResults = (props) => {
    const images = props.images;
    if (!images) return null;
    if (!Array.isArray(images)) return null;
    if (images.length === 0) return <NoResultsFound />
    return (
        <div style={{marginTop:'30px', display:'flex', flexWrap:'wrap'}}>
            {images.map((img, idx) => <ImageResult key={idx} metaData={img} />)}
        </div>
    );
};

export default ImageResults;