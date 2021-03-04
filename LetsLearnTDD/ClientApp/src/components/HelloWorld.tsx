import React, { Component } from 'react';

type IProps = {};

type nameModel = {
  name: string
}

interface IState {
  helloname: nameModel
  loading: boolean
}


export class HelloWorld extends Component<IProps, IState> {
  static displayName = HelloWorld.name;

  constructor(props: IProps) {
    super(props);
    this.state = { helloname: { name: "unknown" }, loading: true };
  }

  componentDidMount() {
    this.populateName();
  }

  static renderHello(data: nameModel) {
    return (
      <div>
        <h2>
          Hello {data.name}
        </h2>
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : HelloWorld.renderHello(this.state.helloname);

    return (
      <div>
        <h1 id="tabelLabel" >Welcome</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateName() {
    const response = await fetch('helloworld');
    const data: nameModel = await response.json();
    this.setState({ helloname: data, loading: false });
  }
}
