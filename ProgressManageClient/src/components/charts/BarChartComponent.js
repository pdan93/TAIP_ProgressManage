'use strict';

import React from 'react';

class BarChartComponent extends React.Component {

  componentDidMount() {
      var svg = d3.select("svg"),
        margin = {top: 20, right: 20, bottom: 30, left: 40},
        width = +svg.attr("width") - margin.left - margin.right,
        height = +svg.attr("height") - margin.top - margin.bottom;

    var x = d3.scaleBand().rangeRound([0, width]).padding(0.1),
        y = d3.scaleLinear().rangeRound([height, 0]);

    var g = svg.append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    d3.tsv("./components/data/barChartData.tsv", function(d) {
    d.accuracy = +d.accuracy;
    return d;
    }, function(error, data) {
    if (error) throw error;

    x.domain(data.map(function(d) { return d.team; }));
    y.domain([0, d3.max(data, function(d) { return d.accuracy; })]);

    g.append("g")
        .attr("class", "axis axis--x")
        .attr("transform", "translate(0," + height + ")")
        .call(d3.axisBottom(x));

    g.append("g")
        .attr("class", "axis axis--y")
        .call(d3.axisLeft(y).ticks(10, "%"))
        .append("text")
        .attr("x", 50)
        .attr("y", 6)        
        .attr("dy", "0.71em")
        .attr("fill", "#000")
        .attr("text-anchor", "end")
        .text("Accuracy");

    g.selectAll(".bar")
        .data(data)
        .enter().append("rect")
        .attr("class", "bar")
        .attr("x", function(d) { return x(d.team); })
        .attr("y", function(d) { return y(d.accuracy); })
        .attr("width", x.bandwidth())
        .attr("height", function(d) { return height - y(d.accuracy); });
    });
  }

  render() {
    return (<div className="chart"><svg width="960" height="500"></svg></div>)
  }
}

BarChartComponent.displayName = 'BarChartComponent';
export default BarChartComponent;



