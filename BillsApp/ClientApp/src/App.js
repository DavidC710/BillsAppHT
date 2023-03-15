import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Register from './components/Auth/Register';
import Login from './components/Auth/Login';
import Logout from './components/Auth/Logout';
import SessionManager from './components/Auth/SessionManager';
import Home from './components/Home/Home';
import Profile from './components/Profile';
import { Configurations } from './components/Configurations';
import RecoverPassword from './components/Auth/RecoverPassword';
import AccountManager from './components/AccountManager';
import { News } from './components/News';
import './custom.css';


export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            SessionManager.getToken() ? (
                SessionManager.getIsAdmin() === "true" ?
                    <Layout>
                        <Route exact path='/' component={Home} />
                        <Route exact path='/home' component={Home} />
                        <Route exact path='/accounts' component={AccountManager} />
                        <Route exact path='/configurations' component={Configurations} />
                        <Route exact path='/news' component={News} />
                        <Route path='/logout' component={Logout} />
                        <Route exact path={'/recoverpassword'} component={RecoverPassword} />
                    </Layout>
                    :
                    <Layout>
                        <Route exact path='/' component={Home} />
                        <Route exact path='/home' component={Home} />
                        <Route exact path='/profile' component={Profile} />
                        <Route path='/logout' component={Logout} />
                        <Route exact path={'/recoverpassword'} component={RecoverPassword} />
                    </Layout>
            ) : (
                <>
                    <Layout>
                        <Route exact path={'/'} component={Login} />
                        <Route exact path={'/login'} component={Login} />
                        <Route exact path={'/register'} component={Register} />
                        <Route exact path={'/recoverpassword'} component={RecoverPassword} />
                    </Layout>
                </>

            )

        );
    }
}
