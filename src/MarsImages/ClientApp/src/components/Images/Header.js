import React from 'react';
import { Navbar, NavbarBrand }  from 'reactstrap';

const Header = (props) => {
    return (
        <Navbar color="dark" dark expand="md" fixed="top">
            <NavbarBrand>Mars Images</NavbarBrand>
        </Navbar>
    );
};

export default Header;