import { Component } from "react";
import { toast, ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { postData } from "../services/AccessAPI";
const jssha = require('js-sha256')

export default class Register extends Component {

    constructor() {
        super();
        this.state = {
            firstName: "",
            lastName: "",
            cellphone: "",
            email: "",
            password: "",
            isRunning: false,
        };

        this.register = this.register.bind(this);
        this.onChange = this.onChange.bind(this);
    }


    onChange(d) {
        this.setState({ [d.target.name]: d.target.value });
    }

    onKeyDown = (e) => {

        if (e.key === 'Enter') {
            this.register();
        }
    }


    register() {
        this.setState({
            isRunning: true
        });
        var hashed = this.state.password === '' ? '' : hash(this.state.password);
        let userInfo = this.state;
        userInfo.password = hashed;
        function hash(input) {
            const hashString = "0x" + jssha.sha256(input)
            return hashString;
        }

        postData('api/Login/Create', userInfo).then((result) => {
            if (result?.token != null || result?.data?.token != null || result?.data?.token != null) {
                this.setState({
                    loading: false
                });

                toast.success("Registered successfully!", {
                    position: "top-right",
                    autoClose: 3500,
                    closeOnClick: true,
                    hideProgressBar: true,
                    pauseOnHover: true,
                    draggable: true
                });
                setTimeout(() => {
                    window.location.href = "/login";
                }, 3500);
            }
            else {
                this.setState({
                    isRunning: false
                });
                let errors = '';
                result.forEach((key) => {
                    errors += ' ' + key['propertyName'] + ': ' + key['errorMessage'];
                });

                errors = errors === '' ? 'Register is unsuccessfull!' : errors;
                toast.error(errors, {
                    position: "top-right",
                    autoClose: 5000,
                    hideProgressBar: true,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    
                });
                this.setState({
                    errors: "Register failed!",
                    loading: false
                });
            }

        });
    }

    login() {
        window.location.href = "/login";
    }

    render() {
        if (this.state.loading) {
            return <div>Loading...</div>;
        }
        else
            return (
                <div className="login-outer-div">
                    <div className="login-inner-div">
                        <div className="login-title">
                            <a href="/"><b>BillsApp</b></a>
                        </div>
                        <div className="login-body">
                            <p className="login-text">Register into the application</p>
                            <input
                                type="text"
                                className="form-control login-txt-box"
                                placeholder="Firstname"
                                name="firstName"
                                onChange={this.onChange}
                                onKeyDown={this.onKeyDown}
                            />
                            <input
                                type="text"
                                className="form-control login-txt-box"
                                style={{ marginTop: 5 }}
                                placeholder="Lastname"
                                name="lastName"
                                onChange={this.onChange}
                                onKeyDown={this.onKeyDown}
                            />
                            <input
                                type="text"
                                className="form-control login-txt-box"
                                style={{ marginTop: 5 }}
                                placeholder="Cellphone"
                                name="cellphone"
                                onChange={this.onChange}
                                onKeyDown={this.onKeyDown}
                            />
                            <input
                                type="text"
                                className="form-control login-txt-box"
                                style={{ marginTop: 5 }}
                                placeholder="Email"
                                name="email"
                                onChange={this.onChange}
                                onKeyDown={this.onKeyDown}
                            />
                            <input
                                type="password"
                                className="form-control login-txt-box"
                                style={{ marginTop: 5 }}
                                placeholder="Password"
                                name="password"
                                onChange={this.onChange}
                                onKeyDown={this.onKeyDown}
                            />
                            <div className="login-btns-container">
                                <button className="btn login-button" disabled={this.state.isRunning} onClick={this.register}>
                                    Register
                                </button>
                                <button className="btn login-button" disabled={this.state.isRunning} onClick={this.login}>
                                    Login
                                </button>

                            </div>
                            <div className="row">
                                <div className="col-md-8" style={{ textAlign: "center", paddingTop: "16px" }}>
                                    <strong className="has-error" style={{ color: "red" }}>{this.state.errorMsg}</strong>
                                </div>
                                <div className="col-md-4">
                                    <ToastContainer></ToastContainer>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            );
    }
}