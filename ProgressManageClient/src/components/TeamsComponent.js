'use strict';

import React from 'react';
require('styles//Teams.css');

class TeamsComponent extends React.Component {

  componentDidMount() {
  }

  render() {
    return (
      <nav className="chartsNav">
        <a className="button" href="/BarChart">Bar Chart Comparison</a>
        <a className="button" href="/GroupedBarChart">Grouped Bar Chart Comparison</a>
        <a className="button" href="/BulletChart">Bullet Chart Comparison</a>
      </nav>
      )
  }
}

TeamsComponent.displayName = 'TeamsComponent';
export default TeamsComponent;



