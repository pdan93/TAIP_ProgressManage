'use strict';

import React from 'react';
require('components/charts/bullets.js');
require('styles/Bullets.css');

class BulletChartComponent extends React.Component {

  componentDidMount() {
    var margin = {top: 5, right: 40, bottom: 20, left: 120},
        width = 960 - margin.left - margin.right,
        height = 50 - margin.top - margin.bottom;

    var chart = d3.bullet()
        .width(width)
        .height(height);

    d3.json("./components/data/bullets.json", function(error, data) {
    if (error) throw error;

    var svg = d3.select("body").selectAll("svg")
        .data(data)
        .enter().append("svg")
        .attr("class", "bullet")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")")
        .call(chart);

    var title = svg.append("g")
        .style("text-anchor", "end")
        .attr("transform", "translate(-6," + height / 2 + ")");

    title.append("text")
        .attr("class", "title")
        .text(function(d) { return d.title; });

    title.append("text")
        .attr("class", "subtitle")
        .attr("dy", "1em")
        .text(function(d) { return d.subtitle; });

    d3.selectAll("button").on("click", function() {
        svg.datum(nextIteration).call(chart.duration(1000));
    });
    });

    function nextIteration(d) {
        d.ranges = [150,225,300];
        d.measures = [220,270];
        d.markers = [0];
        d.subtitle = "Iteration 2";
        return d;
    }
  }

  render() {
    return (<div className="chart"><button>Next Iteration</button></div>)
  }
}

BulletChartComponent.displayName = 'BulletChartComponent';
export default BulletChartComponent;



