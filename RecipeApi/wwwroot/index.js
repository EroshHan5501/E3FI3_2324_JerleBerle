import { LoginPage } from "./app/auth/login/login.js";

document.addEventListener("DOMContentLoaded", () => {
    const main = document.querySelector("main");

    const loginPage = new LoginPage();

    main.appendChild(loginPage);
})