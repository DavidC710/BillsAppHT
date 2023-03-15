import React, { Component } from 'react';
import { getData } from "../../src/components/services/AccessAPI";

export default class AccountManager extends Component {

    static displayName = AccountManager.name;

    constructor(props) {
        super(props);
        this.state = { accounts: [], loading: true };
    }

    componentDidMount() {
        this.populateUsersData();
    }

    static renderAccountManager(accounts) {
        return (
            <div>
                <div className="module-title">
                    {this.displayName}
                </div>
                <table className='table table-striped table-list' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email</th>
                            <th>Created Date</th>
                            <th>Active Profile</th>
                        </tr>
                    </thead>
                    <tbody>
                        {accounts.map(account =>
                            <tr key={account.id}>
                                <td>{account.firstName}</td>
                                <td>{account.lastName}</td>
                                <td>{account.created}</td>
                                <td>{account.created}</td>
                                <td><input type="checkbox" />{account.isDeleted}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : AccountManager.renderAccountManager(this.state.accounts);

        return (
            <div>
                {contents}
            </div>
        );
    }

    async populateUsersData() {
        getData('api/Login/GetAll').then((result) => {
            this.setState({ accounts: result, loading: false });
        });        
    }
}
