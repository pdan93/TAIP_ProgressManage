'use strict';
require('normalize.css/normalize.css');
require('styles/App.css');

import React from 'react';
import RevisionsComponent from './RevisionsComponent'
import StatesComponent from './StatesComponent'
import axios from 'axios';
import Rx from 'rx';

class AppComponent extends React.Component {

  constructor(props) {
      super(props)
      this.state = {
          tasks: [],
          petriNets: ''
      };
  }

  componentDidMount() {
    var self = this;
    var petriNetsPromise = axios.get('http://localhost:59981/api/PetriNets');
    Rx.Observable
      .fromPromise(petriNetsPromise)
      .subscribe(function (response) {
          self.setState({petriNets: response['data'] });
      });

    var tasksPromise = axios.get('http://localhost:59981/api/TfsTasks');
    Rx.Observable
      .fromPromise(tasksPromise)
      .subscribe(function (response) {
        self.setState({tasks: response['data'] });
      });
  }

  render() {
    return (
      <section className="index">
          <article>
            <select>
              <option key='1' value="defaultValue"> -- Select task  -- </option>
              {this.state.tasks.map(function(task, index){
                return <option key={index} value={index}>{task}</option>
              })}
            </select>
          </article>
          <article>
            <RevisionsComponent/>
            <StatesComponent/>
            <label> Petri nets evaluation message: </label>
            <p id="PetriNets"> { this.state.petriNets} </p>
          </article>
      </section>
    );
  }
}

AppComponent.defaultProps = {};
export default AppComponent;