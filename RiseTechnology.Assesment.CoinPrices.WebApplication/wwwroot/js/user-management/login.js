
$(async () => {

    let postData = async (url, data) => {
        return new Promise((resolve, reject) => {
            $.post(url, JSON.stringify(data), function (resp) {
                resolve(resp);
            }).fail(function (e) {
                reject();
            })
        });
    };

    let showErrorMessages = message => {

        if (message["Login"]) {
            document.querySelector("form").classList.add("was-validated");
            let username = document.querySelector("#username");
            let password = document.querySelector("#password");
            let feedback = document.querySelector(".feedback");

            username.classList.add("is-invalid");
            username.setCustomValidity("Invalid field.");
            password.classList.add("is-invalid");
            password.setCustomValidity("Invalid field.");
            feedback.classList.add("invalid-feedback");
            feedback.innerHTML = message["Login"];
        }

        if (message["Error"]) {
            toastr.error(message["Error"], 'Dikkat!', {
                "positionClass": "toast-bottom-right",
            });
        }
    };

    let hideErrorMessages = () => {
        document.querySelector("form").classList.remove("was-validated");
        let username = document.querySelector("#username");
        let password = document.querySelector("#password");
        let feedback = document.querySelector(".feedback");

        username.classList.remove("is-invalid");
        username.setCustomValidity("");
        password.classList.remove("is-invalid");
        password.setCustomValidity("");
        feedback.classList.remove("invalid-feedback");
        feedback.innerHTML = "";
    };


    let button = document.querySelector("#submitbutton");
    button.addEventListener("click", async e => {
        e.preventDefault();
        hideErrorMessages();

        let form = document.querySelector("form");
        let formData = new FormData(form);
        let data = Object.fromEntries(formData.entries());

        let response = await postData("/login", data);
        if (response.isSuccessful == false) {
            showErrorMessages(response.message);
        } else {

            localStorage.setItem("Token", response.data.token);
            toastr.success('Giris Yatiniz. Yonlendiriliyorsunuz...', 'Giris Basarili!', {
                "positionClass": "toast-bottom-right",
            });
            setTimeout(() => window.location.href = "/showcase", 3000);
        }
    });
});