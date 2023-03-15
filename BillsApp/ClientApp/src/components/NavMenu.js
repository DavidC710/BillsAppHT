import React, { Component } from 'react';
import {
    Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink
} from 'reactstrap';

import { Link } from 'react-router-dom';
import './NavMenu.css';
import SessionManager from "./Auth/SessionManager";

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        return (
            SessionManager.getIsAdmin() === "true" ?
                (
                    <header>
                        <div className="container">
                            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                                <Container>
                                    <NavbarBrand className="navTitle">BillsApp</NavbarBrand>
                                    <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                                        <ul className="navbar-nav flex-grow">
                                            <NavItem className="menu-links">
                                                <NavLink tag={Link} className="text navLink" to="/home">Home</NavLink>
                                            </NavItem>
                                            <NavItem className="menu-links">
                                                <NavLink tag={Link} className="navLink" to="/accounts">Account Manager</NavLink>
                                            </NavItem>
                                            <NavItem className="menu-links">
                                                <NavLink tag={Link} className="navLink" to="/configurations">Configurations</NavLink>
                                            </NavItem>
                                            <NavItem className="menu-links">
                                                <NavLink tag={Link} className="navLink navLink" to="/news">News</NavLink>
                                            </NavItem>
                                            <NavItem className="menu-links">
                                                <NavLink tag={Link} className='navLink' to="/logout">Logout</NavLink>
                                            </NavItem>
                                        </ul>
                                    </Collapse>
                                </Container>
                            </Navbar>
                        </div>
                    </header>
                )
                :
                (
                    <header>
                        <div className="container">
                            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                                <Container>
                                    <NavbarBrand className="navTitle">BillsApp</NavbarBrand>
                                    <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                                        <ul className="navbar-nav flex-grow">
                                            <NavItem className="menu-links">
                                                <NavLink tag={Link} className="text navLink" to="/home">Home</NavLink>
                                            </NavItem>
                                            <NavItem className="menu-links">
                                                <NavLink tag={Link} className="navLink" to="/profile">Profile</NavLink>
                                            </NavItem>
                                            <NavItem className="menu-links">
                                                <NavLink tag={Link} className='navLink' to="/logout">Logout</NavLink>
                                            </NavItem>
                                        </ul>
                                    </Collapse>
                                </Container>
                            </Navbar>
                        </div>
                    </header>
                )
        );
    }
}
