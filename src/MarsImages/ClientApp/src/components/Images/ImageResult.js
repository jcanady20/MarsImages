import React from 'react';
import { Card, CardBody, CardHeader, CardTitle } from 'reactstrap';

const ImageResult = (props) => {
    var metaData = props.metaData;
    if (!metaData) return null;
    const imageUrl = `api/marsimage/${metaData.id}`;
    return (
        <Card>
            <CardHeader>{metaData.rover.name} - {metaData.earth_date}</CardHeader>
            <CardBody>
                <CardTitle>{metaData.camera.full_name}</CardTitle>
                <img src={imageUrl} atl="" />
            </CardBody>
        </Card>
    );
};

export default ImageResult;