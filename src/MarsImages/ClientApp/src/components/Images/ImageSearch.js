import React, { useEffect, useState } from 'react';
import { Container, Col, Row, Form, FormGroup, Input } from 'reactstrap';
import ImageResults from './ImageResults';
import ImageDateSelection from './ImageDateSelection';

const ImageSearch = (props) => {
    const [results, setResults] = useState([]);
    const [selectedDate, setSelectedDate] = useState(null);
    useEffect(() => {
        async function getImages () {
            var resp = await fetch('api/marsdate');
            if (!resp.ok) throw new Error();
            var json = await resp.json();
            setResults(json);
        }
        getImages();
    }, []);
    const handleDateChange = (e) => {
        if (!e) return;
        var target = e.target;
        var value = target.value;
        if (!value)
        {
            setSelectedDate(null);
            return;
        }
        var idt = results.find(x => {
            return x.date === value;
        });
        if (!idt) return;
        setSelectedDate(idt);
    };
    return (
        <Container>
            <Row>
                <Col sm={12}>
                <Form>
                    <ImageDateSelection onHandleDateChange={handleDateChange} results={results} />
                </Form>
                </Col>
            </Row>
            {selectedDate &&
            <Row>
                <Col sm={12}>
                    <ImageResults images={selectedDate.photos} />
                </Col>
            </Row>
            }
        </Container>
    );
};

export default ImageSearch;