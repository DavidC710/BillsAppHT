const SessionManager = {

    getToken() {
        const token = sessionStorage.getItem('token');
        if (token) return token;
        else return null;
    },

    getFirstName() {
        const firstName = sessionStorage.getItem('firstName');
        if (firstName) return firstName;
        else return null;
    },

    getLastName() {
        const lastName = sessionStorage.getItem('lastName');
        if (lastName) return lastName;
        else return null;
    },

    getEmail() {
        const email = sessionStorage.getItem('email');
        if (email) return email;
        else return null;
    },

    getCellphone() {
        const cellphone = sessionStorage.getItem('cellphone');
        if (cellphone) return cellphone;
        else return null;
    },

    getCreatedDate() {
        const created = sessionStorage.getItem('created');
        if (created) return created;
        else return null;
    },

    getId() {
        const id = sessionStorage.getItem('id');
        if (id) return id;
        else return null;
    },

    getIsAdmin() {
        const id = sessionStorage.getItem('isAdmin');
        if (id) return id;
        else return null;
    },

    setUserSession(userName, token, id, firstName, lastName, cellphone, created, isAdmin) {
        sessionStorage.setItem('email', userName);
        sessionStorage.setItem('token', token);
        sessionStorage.setItem('id', id);
        sessionStorage.setItem('firstName', firstName);
        sessionStorage.setItem('lastName', lastName);
        sessionStorage.setItem('cellphone', cellphone);
        sessionStorage.setItem('created', created);
        sessionStorage.setItem('isAdmin', isAdmin);
    },

    setUpdateUserSession(email, firstName, lastName, cellphone) {
        sessionStorage.setItem('email', email);
        sessionStorage.setItem('firstName', firstName);
        sessionStorage.setItem('lastName', lastName);
        sessionStorage.setItem('cellphone', cellphone);
    },

    removeUserSession(){
        sessionStorage.removeItem('email');
        sessionStorage.removeItem('token');
        sessionStorage.removeItem('id');
        sessionStorage.removeItem('firstName');
        sessionStorage.removeItem('lastName');
        sessionStorage.removeItem('cellphone');
        sessionStorage.removeItem('created');
    }
}

export default SessionManager;