window.addEventListener("DOMContentLoaded", function () {
    var consent = document.querySelector(".cookie-consent");
    var acceptButton = consent.querySelector("button");
    acceptButton.addEventListener("click", function () {
        document.cookie = "consent=true; expires=Thu, 01 Jan 2099 00:00:00 UTC; path=/";
        consent.style.display = "none";
    });
});
