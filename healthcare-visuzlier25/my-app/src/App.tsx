import React from 'react';
// import logo from './logo.svg';
import './App.css';
import Welcome, { } from './components/hello';

function App() {
  return (
    <div className="App">
      <Welcome name='Ahmed' age={23}> </Welcome>
    </div>
  );
}

export default App;
