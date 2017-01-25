'use strict';

require('styles//States.css');

import React from 'react';
import axios from 'axios';
import Rx from 'rx';


class StatesComponent extends React.Component {

  constructor(props) {
      super(props)
      this.state = {
          states: []
      };
  }

   componentDidMount() {
    var self = this;
    var statesPromise = axios.get('http://localhost:59981/api/States');
    Rx.Observable
      .fromPromise(statesPromise)
      .subscribe(function (response) {
         self.setState({states: response['data'] });
      });
  }
  
  render() {
        return (
            <div>
              <label>All the states in the petri nets:</label>
              <ul>
                {this.state.states.map(function(state, index){
                  return <li key={index}>{state}</li>;
                })}
              </ul>
            </div>
          )
      }
}
StatesComponent.displayName = 'StatesComponent';
export default StatesComponent;