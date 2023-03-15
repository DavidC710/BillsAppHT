import { Component } from 'react';
import SessionManager from "./Auth/SessionManager";
import { toast, ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { putData } from './services/AccessAPI';

export default class Profile extends Component {

    constructor(props) {
        super(props);
        this.onChange = this.onChange.bind(this);
        this.updateUser = this.updateUser.bind(this);
        this.state = {
            displayName: Profile.name,
            firstName: SessionManager.getFirstName(),
            lastName: SessionManager.getLastName(),
            cellphone: SessionManager.getCellphone(),
            email: SessionManager.getEmail(),
            loading: true
        };

    }

    onChange(e) {
        this.setState({ [e.target.name]: e.target.value });

    }

    updateUser() {
        let userInfo = this.state;
        this.setState({
            loading: true
        });

        putData('api/Login/Update', userInfo).then((result) => {
            if (result?.token) {
                SessionManager.setUpdateUserSession(result.email, result.firstName, result.lastName,
                    result.cellphone)
                if (SessionManager.getToken()) {
                    this.setState({
                        loading: false
                    });

                    toast.success("Update is successfull!", {
                        position: "top-right",
                        autoClose: 1000,
                        closeOnClick: true,
                        hideProgressBar: true,
                        pauseOnHover: true,
                        draggable: true
                    });
                }
            }
            else {
                let errors = '';

                result.forEach((key) => {
                    errors += ' ' + key['propertyName'] + ': ' + key['errorMessage'];
                });

                if (result[0] === 'Not Found.') errors = 'Internal server error!'

                errors = errors === '' ? 'UpdateUser is unsuccessfull!' : errors;
                toast.error(errors, {
                    position: "top-right",
                    autoClose: 5000,
                    hideProgressBar: true,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true
                });

                this.setState({
                    errors: "UpdateUser failed!",
                    loading: false
                });
            }

        });
    }

    render() {      
        return (
            <div>
                <div className="module-title">
                    {this.state.displayName}
                </div>
                <div className="form-container">
                    <label className="label-text">First Name</label>
                    <input
                        type="text"
                        className="txt-form"
                        name="firstName"
                        defaultValue={this.state.firstName}
                        onChange={this.onChange}
                    />
                    <br />
                    <label className="label-text">Last Name</label>
                    <input
                        type="text"
                        className="txt-form"
                        name="lastName"
                        defaultValue={this.state.lastName}
                        onChange={this.onChange}
                    />
                    <br />
                    <label className="label-text">Email</label>
                    <input
                        type="text"
                        className="txt-form"
                        value={this.state.email}
                        onChange={this.onChange}
                        disabled
                    />
                    <br />
                    <label className="label-text">Cellphone</label>
                    <input
                        type="text"
                        className="txt-form"
                        name="cellphone"
                        defaultValue={this.state.cellphone}
                        onChange={this.onChange}
                    />
                    <div style={{ textAlign: "center", marginTop: "10px" }}>
                        <a style={{ color: "#FFF" }} href="/recoverpassword">
                            Recover Password
                        </a>
                    </div>
                    <div className="login-btns-container">
                        <button className="btn login-button" onClick={this.updateUser}>
                            Update
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
        );
    }
}
