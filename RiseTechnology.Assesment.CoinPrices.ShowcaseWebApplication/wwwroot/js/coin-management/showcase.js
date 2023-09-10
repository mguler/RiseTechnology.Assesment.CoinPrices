
$(async () => {

    let filter = "today";
    let chart = null;
    let timeoutHandle = 0;

    document.querySelectorAll(".dropdown-item").forEach(item => item.addEventListener("click", e => {
            document.querySelector("#filterText").innerText = e.target.innerText;
            filter = e.target.dataset.key;
            clearTimeout(timeoutHandle);
            refresh();
        })
    );

    let getData = async (url) => {
        return new Promise((resolve, reject) => {

            let token = localStorage.getItem("Token");
            $.ajax({
                url: url,
                type: "GET",
                beforeSend: function (req) { req.setRequestHeader('Authorization', `Bearer ${token}`); },
                success: function (data) {
                    resolve(data);
                },
                error: function (req, status) {
                    if (req.status == 401) {
                        toastr.warning("You are not authorized to access this resource", 'Warning!', {
                            "positionClass": "toast-bottom-right",
                        });
                    } else
                    {
                        toastr.error("An error occured", 'Error!', {
                            "positionClass": "toast-bottom-right",
                        });
                    }
                }
            });
        });
    };

    let renderData = (data) => {

        if (!chart) {
            chart = new Chart("chart", {
                type: "line",
                height: "100%",
                options: {
                    legend: { display: false },
                    scales: {
                        yAxes: [{ ticks: { min: 6, max: 16 } }],
                    }
                }
            });
        }

        chart.data = {
            labels : [50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150],
            datasets: [{
                fill: false,
                lineTension: 0,
                backgroundColor: "rgba(0,0,255,1.0)",
                borderColor: "rgba(0,0,255,0.1)",
                data: data
            }]
        };
        chart.update();  
    };

    refresh = async () => {
        let data = await getData(`${DATA_URL}${filter}`);
        renderData(data);
        timeoutHandle = setTimeout(refresh, 60000);
    };

    await refresh();

});