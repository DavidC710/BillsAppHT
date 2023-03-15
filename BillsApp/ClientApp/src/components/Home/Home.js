import { Component } from "react";
import SessionManager from "../Auth/SessionManager";

export default class Home extends Component {

    static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state = { displayName: Home.name, userName: SessionManager.getFirstName() };
    }

    render() {
        return (
            SessionManager.getIsAdmin() === "true" ?
                (
                    <div>
                        <div className="module-title">
                            {this.state.displayName}
                        </div>
                        <div className="regular-text">
                            <span>
                                ¡Bienvenido Admin {this.state.userName}!
                            </span>
                            <div className="spaced-text">
                                ¿Qué quieres hacer hoy?
                            </div>
                            <div className="links-list">
                                <ul>
                                    <li><a href="/accounts">Account Manager</a></li>
                                    <li><a href="/configurations">Configurations</a></li>
                                    <li><a href="/news">News</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                )
                :
                (
                    <div>
                        <div className="module-title">
                            {this.state.displayName}
                        </div>
                        <div className="regular-text">
                            <span>
                                ¡Bienvenido {this.state.userName}!
                            </span>
                            <div className="spaced-text">
                                ¿Qué quieres hacer hoy?
                            </div>
                            <div className="links-list">
                                <ul>
                                    <li><a href="/profile">Profile</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                )
        );
    }
}