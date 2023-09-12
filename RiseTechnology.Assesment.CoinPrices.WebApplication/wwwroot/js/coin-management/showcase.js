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
                        toastr.warning("You are not authorized to access this resource", 'Attention!', {
                            "positionClass": "toast-bottom-right",
                        });
                    } else
                    {
                        toastr.error("An error occured. Please contact to system administrator", 'Attention!', {
                            "positionClass": "toast-bottom-right",
                        });
                    }
                }
            });
        });
    };

    let renderData = (data) => {

        let prices = data?.prices?.map(item => item?.price);
        let labels = data?.labels;
        
        if (!chart) {
            chart = new Chart("chart", {
                type: "line",
                height: "100%",
                options: {
                    legend: { display: false },
                    scales: { }
                }
            });
        }

        chart.data = {
            labels : labels,
            datasets: [{
                fill: false,
                lineTension: 0,
                backgroundColor: "rgba(0,0,255,1.0)",
                borderColor: "rgba(0,0,255,0.1)",
                data: prices
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