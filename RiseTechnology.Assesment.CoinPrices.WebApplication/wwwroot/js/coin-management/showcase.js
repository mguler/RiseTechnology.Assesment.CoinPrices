
$(async () => {

    let getData = async (url, data) => {
        return new Promise((resolve, reject) => {
            $.post("/login", data, function (resp) {

            })
                .done(function () {
                    resolve(resp);
                })
                .fail(function () {
                    reject();
                })
                .always(function () {
                    console.log("request finished...");
                })
        });
    };


    const xValues = [50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150];
    const yValues = [7, 8, 8, 9, 9, 9, 10, 11, 14, 14, 15];

    new Chart("chart", {
        type: "line",
        height: "100%",
        data: {
            labels: xValues,
            datasets: [{
                fill: false,
                lineTension: 0,
                backgroundColor: "rgba(0,0,255,1.0)",
                borderColor: "rgba(0,0,255,0.1)",
                data: yValues
            }]
        },
        options: {
            legend: { display: false },
            scales: {
                yAxes: [{ ticks: { min: 6, max: 16 } }],
            }
        }
    });

});