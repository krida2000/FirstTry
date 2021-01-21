function createChart() {    
    google.load('visualization', '1.0', { 'packages': ['corechart'] });

    // Set a callback to run when the Google Visualization API is loaded.
    google.setOnLoadCallback(drawChart);

    // Callback that creates and populates a data table,
    // instantiates the pie chart, passes in the data and
    // draws it.
    function drawChart() {

        // Create the data table.
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'FIO');
        data.addColumn('number', 'Count');
        i = 0;        

        names = document.querySelectorAll('.DName');
        values = document.querySelectorAll('.DCount');

        while (i + 1 <= names.length) {
            data.addRows([
                [names[i].innerText, +values[i].innerText]
            ]);
            i++;
        }

        // Set chart options
        var options = {
            'title': 'Кругова діаграма',
            'width': 500,
            'height': 500
        };

        // Instantiate and draw our chart, passing in some options.
        var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
        chart.draw(data, options);
    } 
}