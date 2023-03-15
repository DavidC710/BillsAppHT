import React, { Component } from 'react';

export class Configurations extends Component {
    static displayName = Configurations.name;

  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
    this.incrementCounter = this.incrementCounter.bind(this);
  }

  incrementCounter() {
    this.setState({
      currentCount: this.state.currentCount + 1
    });
  }

  render() {
    return (
        <div className="navTitle">
            <h1>Bienvenido <h1 className="bold">@Name!</h1></h1>
            <p>Welcome to Bills application. You can access:</p>
            <ul>
               MI INFOOOOOOOOOOOOOOOOOOOO
            </ul>
        </div>
    );
  }
}
