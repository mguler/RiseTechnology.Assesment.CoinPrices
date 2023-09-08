
$(async () => {

    let postData = async (url, data) => {
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

    let form = document.querySelector("form");
    form.addEventListener("submit", async e => {

        e.preventDefault();
        let formData = new FormData(e.target);
        let data = Object.fromEntries(formData.entries());

        let response = await postData("/register", data);


    });
});