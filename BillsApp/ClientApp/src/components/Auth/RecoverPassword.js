import { Component } from "react";
import { toast, ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { putData } from "../services/AccessAPI";
import SessionManager from "./SessionManager";
const jssha = require('js-sha256')

export default class RecoverPassword extends Component {
    constructor() {
        super();
        this.state = {
            token: SessionManager.getToken(),
            email: "",
            cellphone: "",
            newPassword: "",
        };

        this.recoverPassword = this.recoverPassword.bind(this);
        this.onChange = this.onChange.bind(this);
    }


    onChange(e) {
        this.setState({ [e.target.name]: e.target.value });
    }

    onKeyDown = (e) => {
        if (e.key === 'Enter') {
            this.recoverPassword();
        }
    }

    recoverPassword() {
        var hashed = hash(this.state.newPassword);
        let userInfo = this.state;
        userInfo.newPassword = hashed;
        function hash(input) {
            const hashString = "0x" + jssha.sha256(input)
            return hashString;
        }

        this.setState({
            loading: true,
            token: SessionManager.getToken()
        });

        putData('api/Login/RecoverPassword', userInfo).then((result) => {
            if (result?.token) {
                this.setState({
                    loading: false
                });

                toast.success("Password recovered successfully!", {
                    position: "top-right",
                    autoClose: 3000,
                    closeOnClick: true,
                    hideProgressBar: true,
                    pauseOnHover: true,
                    draggable: true
                });
                setTimeout(() => {
                    window.location.href = "/login";
                }, 3000);
            }
            else {
                let errors = '';
                for (const key in result?.errors) {
                    if (Object.hasOwnProperty.call(result.errors, key)) {
                        errors += result.errors[key];

                    }
                }
                errors = errors === '' ? 'Recover password was unsuccessfull!' : errors;
                toast.error(errors, {
                    position: "top-right",
                    autoClose: 5000,
                    hideProgressBar: true,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true
                });

                this.setState({
                    errors: "Recover password failed!",
                    loading: false
                });
            }

        });
    }

    registration() {
        window.location.href = "/register";
    }
    login() {
        window.location.href = "/login";
    }

    render() {
        let content;
        if (this.state.loading) {
            content = <div>Loading...</div>;
        }

        return (
            <div className="login-outer-div">
                <div className="login-inner-div">
                    {
                        this.state.token == null ?
                            <div className="login-title">
                                <a href="/"><b>BillsApp</b></a>
                            </div> :
                            null
                    }

                    <div className="login-body">
                        <p className="login-text">Put your cellphone and email to recover the password</p>
                        <input
                            type="text"
                            className="form-control login-txt-box"
                            placeholder="Email"
                            name="email"
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
                            type="password"
                            className="form-control login-txt-box"
                            style={{ marginTop: 5 }}
                            placeholder="New Password"
                            name="newPassword"
                            onChange={this.onChange}
                            onKeyDown={this.onKeyDown}
                        />
                        <div className="login-btns-container">
                            <button className="btn login-button" onClick={this.recoverPassword}>
                                Recover
                            </button>
                        </div>
                        <div>
                            {content}
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