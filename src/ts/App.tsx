import React, { Component } from 'react';

interface AppState {
  message: string;
}

class App extends Component<{}, AppState> {
  constructor(props: any) {
    super(props);
    this.state = {
      message: 'Hello, World!',
    };
  }

  render() {
    return (
      <div>
        <h1>{this.state.message}</h1>
      </div>
    );
  }
}

export default App;
