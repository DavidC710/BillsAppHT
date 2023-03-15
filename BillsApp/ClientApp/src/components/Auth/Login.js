import { Component } from "react";
import { toast, ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { postDataForLogin } from "../services/AccessAPI";
import SessionManager from "./SessionManager";
const jssha = require('js-sha256')

export default class Login extends Component {
    constructor() {
        super();
        this.state = {
            email: "",
            password: "",
        };

        this.login = this.login.bind(this);
        this.onChange = this.onChange.bind(this);
    }

    onChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }

    onKeyDown = (e) => {
        if (e.key === 'Enter') {
            this.login();
        }
    }

    login() {
        var hashed = this.state.password === '' ? '' : hash(this.state.password);
        let userInfo = this.state;
        userInfo.password = hashed;
        function hash(input) {
            const hashString = "0x" + jssha.sha256(input)
            return hashString;
        }

        this.setState({
            loading: true
        });

        postDataForLogin('api/Login/Login', userInfo).then((result) => {
            if (result?.token) {
                SessionManager.setUserSession(result.email, result.token, result.id, result.firstName, result.lastName,
                    result.cellphone, result.created, result.isAdmin)
                if (SessionManager.getToken()) {
                    this.setState({
                        loading: false
                    });

                    toast.success("Login Successfull!", {
                        position: "top-right",
                        autoClose: 1000,
                        closeOnClick: true,
                        hideProgressBar: true,
                        pauseOnHover: true,
                        draggable: true
                    });
                    setTimeout(() => {
                        window.location.href = "/home";
                    }, 1000);                    
                }
            }
            else {
                let errors = '';

                result.forEach((key) => {
                    errors += ' ' + key['propertyName'] + ': ' + key['errorMessage'];
                });

                if (result[0] === 'Permission Denied.') errors = 'Wrong password!'
                else if (result[0] === 'Not Found.') errors = 'User not registered!'

                errors = errors === '' ? 'Login is unsuccessfull!' : errors;
                toast.error(errors, {
                    position: "top-right",
                    autoClose: 5000,
                    hideProgressBar: true,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true
                });

                this.setState({
                    errors: "Login failed!",
                    loading: false
                });
            }

        });
    }

    registration() {
        window.location.href = "/register";
    }

    recoverPassword() {
        window.location.href = "/recoverPassword";
    }

    render() {
        let content;
        if (this.state.loading) {
            content = <div>Loading...</div>;
        }

        return (
            <div className="login-outer-div">
                <div className="login-inner-div">
                    <div className="login-title">
                        <a href="/"><b>BillsApp</b></a>
                    </div>
                    <div className="login-body">
                        <p className="login-text">Sign in to access the application</p>
                        <input
                            type="text"
                            className="form-control login-txt-box"
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
                        <div style={{ textAlign: "center", marginTop: "10px" }}>
                            <a style={{ color: "#FFF" }} href="/recoverpassword">
                                Recover Password
                            </a>
                        </div>
                        <div className="login-btns-container">
                            <button className="btn login-button" onClick={this.login}>
                                Sign In
                            </button>
                            <br />
                            <button className="btn login-button" onClick={this.registration}>
                                Create an account
                            </button>
                        </div>
                        <div>
                            {content}
                        </div>

                            <div className="col-md-4">
                                <ToastContainer></ToastContainer>
                            </div>

                    </div>
                </div>
            </div>
        );
    }
}