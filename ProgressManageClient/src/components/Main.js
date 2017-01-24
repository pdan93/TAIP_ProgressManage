require('normalize.css/normalize.css');
require('styles/App.css');

import React from 'react';
import Rx from 'rx-react';
import jQuery from 'jquery';

class AppComponent extends React.Component {
  render() {
    return (
      <section className="index">
          <article>
            <select>
              <option value="task1">task1</option>
            </select>
          </article>
          <article>
            <label id="demo">All states that went task:</label>
            <ul id="revisionList"> </ul>

            <label id="demo">All states in the petri nets:</label>
            <ul id="stateList"> </ul>

            <label> Petri nets evaluation message: </label>
            <p id="PetriNets"> </p>
          </article>
      </section>
    );
  }
}

AppComponent.defaultProps = {
};

export default AppComponent;
/*
  componentDidMount() {
    var responseStream = Rx.Observable.fromPromise($.getJSON('localhost:59981/api/Revisions'));
    console.log(responseStream);
  }
*/
