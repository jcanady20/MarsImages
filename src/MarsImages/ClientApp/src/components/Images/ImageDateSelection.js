import React from 'react';
import { Col, FormGroup, Input } from 'reactstrap';

const ImageDateSelection = (props) => {
    return (
        <FormGroup row size="lg">
            <Col sm={12}>
                <Input  type="select" bsSize="lg" onChange={e => props.onHandleDateChange(e)}>
                    <option value="">Select Date</option>
                    {props.results && props.results.map((idt, idx) => {
                        return (<option key={idx} value={idt.date}>{idt.name} - {idt.status}</option>)
                    })}
                </Input>
            </Col>
        </FormGroup>
    );
};
export default ImageDateSelection;