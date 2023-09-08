﻿
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

    let showErrorMessages = messages => {
        document.querySelector("form").classList.add("was-validated");

        document.querySelectorAll("form input").forEach(item => {
            item.classList.add("is-valid");
            item.reportValidity();
        });

        for (var key in messages) {
            let input = document.querySelector(`#${key.toLowerCase()}`);
            let feedback = document.querySelector(`#${key.toLowerCase()}-feedback`);
            input.classList.remove("is-valid");
            input.classList.add("is-invalid");
            input.setCustomValidity("Invalid field.");
            feedback.classList.remove("valid-feedback");
            feedback.classList.add("invalid-feedback");
            feedback.innerHTML = messages[key];
        }

        if (messages["Register"]) {
            toastr.error(messages["Register"], 'Dikkat!', {
                "positionClass": "toast-bottom-right",
            });
        }

        if (message["Error"]) {
            toastr.error(message["Error"], 'Dikkat!', {
                "positionClass": "toast-bottom-right",
            });
        }
    };

    let hideErrorMessages = () => {
        document.querySelectorAll(`.is-valid, .is-invalid, .valid-feedback, .valid-feedback, form`)
            .forEach(item => {
                item.classList.remove("is-valid");
                item.classList.remove("is-invalid");
                item.classList.remove("valid-feedback");
                item.classList.remove("invalid-feedback");
                item.classList.remove("was-validated");
                item.innerHTML = "";
                if (item.setCustomValidity) {
                    item.setCustomValidity("");
                };
            });
    };


    let button = document.querySelector("#submitbutton");
    button.addEventListener("click", async e => {
        e.preventDefault();
        hideErrorMessages();

        let form = document.querySelector("form");
        let formData = new FormData(form);
        let data = Object.fromEntries(formData.entries());

        let response = await postData("/register", data);
        if (response.isSuccessful == false) {
            showErrorMessages(response.message);
        } else {

            toastr.success('Giris Ekranina Yonlendiriliyorsunuz', 'Kayit Basarili!', {
                "positionClass": "toast-bottom-right",
            });
            setTimeout(() => window.location.href = "/login", 3000);
        }
    });
});