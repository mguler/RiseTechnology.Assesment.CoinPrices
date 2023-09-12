$(() => {

    $.ajaxSetup({
        headers: {
            'Content-Type': 'application/json',
        }
    });

    document.querySelector("#logout").addEventListener("click", e => {
        localStorage.removeItem("Token");
        window.location.href = "/logout";

    });

})