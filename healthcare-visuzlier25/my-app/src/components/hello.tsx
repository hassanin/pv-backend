import React from 'react';
export interface HelloProps {
    name: string,
    age: number,
}
export default class Welcome extends React.Component<HelloProps, {}> {
    render() {
        return <h1>Hello, {this.props.name}</h1>;
    }
}