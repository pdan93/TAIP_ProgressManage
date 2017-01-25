'use strict';
require('normalize.css/normalize.css');
require('styles//Revisions.css');

import React from 'react';
import axios from 'axios';
import Rx from 'rx';

class RevisionsComponent extends React.Component {

  constructor(props) {
      super(props)
      this.state = {
          revisions: []
      };
  }

   componentDidMount() {
    var self = this;
    var revisionsPromise = axios.get('http://localhost:59981/api/Revisions');
    Rx.Observable
      .fromPromise(revisionsPromise)
      .subscribe(function (response) {
         self.setState({revisions: response['data'].map(x => x.Fields.State) });
      });
  }

  render() {
        return (
          <div>
            <label>All states that went task:</label>
            <ul>
              {this.state.revisions.map(function(revision, index){
                return <li key={index}>{revision}</li>;
              })}
            </ul>
          </div>
        )
    }
}
RevisionsComponent.displayName = 'RevisionsComponent';
export default RevisionsComponent;
