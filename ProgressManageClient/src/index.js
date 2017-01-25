import 'core-js/fn/object/assign';
import React from 'react';
import ReactDOM from 'react-dom';
import App from './components/Main';
import TeamsComponent from './components/TeamsComponent';
import BarChartComponent from './components/charts/BarChartComponent';
import GroupedBarChartComponent from './components/charts/GroupedBarChartComponent';
import BulletChartComponent from './components/charts/BulletChartComponent';
import { Router, Route, browserHistory } from 'react-router'


// Render the main component into the dom
ReactDOM.render((
 <div>
    <nav>
        <a className="button" href="/">PetriNets </a>
        <a className="button" href="/teams">Teams Comparison</a>
    </nav>
    <Router history = {browserHistory}>
        <Route path="/" component={App} />
        <Route path="/teams" component={TeamsComponent} />
        <Route path="/BarChart" component={BarChartComponent} />
        <Route path="/GroupedBarChart" component={GroupedBarChartComponent} />
        <Route path="/BulletChart" component={BulletChartComponent} />
    </Router>
  </div>
), document.getElementById('app'));
