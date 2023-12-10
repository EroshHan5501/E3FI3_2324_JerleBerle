import { BasePage } from "/app/page.js";
import { Navigator } from "../../helper/Navigator.js";
import { RouteNames } from "../../helper/RouteNames.js";
import { AppConfig } from "../../helper/AppConfig.js";

export class LoginPage extends BasePage {
    constructor() {
        super({ "title": "Login", "heading": "Login" });
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await this.appendTemplate("app/auth/login/login.html", this);

        this.guiContent = {
            emailTxt: this.querySelector("#email"),
            passwordTxt: this.querySelector("#password"),
            loginBtn: this.querySelector("#login-btn"),
            goRegisterLink: this.querySelector("#go-register-link"),
            mainErrTxt: this.querySelector("#main-error")
        }

        this.handleClick(
            this.guiContent.loginBtn,
            this.#handleLoginBtnClick.bind(this));

        this.handleClick(
            this.guiContent.goRegisterLink,
            async (e) => {
                e.preventDefault(); // this prevent default could be handled in the registration
                await Navigator.goToAsync(RouteNames.register);
            }
        )
    }

    async #handleLoginBtnClick(event) {
        event.preventDefault();

        const payload = {
            "email": this.guiContent.emailTxt.value,
            "password": this.guiContent.passwordTxt.value
        }

        const url = AppConfig.buildApiPath("login/");
        await this.sendDataAsync(
            url,
            payload,
            {
                400: this.#handle400Errors.bind(this),
                403: this.#handle403Errors.bind(this)
            },
            this.#handleSuccessAsync.bind(this));
    }

    #handle400Errors(statusCode, data) {
        this.guiContent.mainErrTxt.innerText = "";
        for (const err of data.errors) {
            const errElem = document.createElement("span");
            errElem.classList.add("error");
            errElem.innerText = err;
            this.guiContent.mainErrTxt.appendChild(errElem);
        }
    }

    #handle403Errors(statusCode, data) {
        
    }

    async #handleSuccessAsync(data) {
        await Navigator.goToAsync(RouteNames.dashboard);
    }
}

customElements.define("login-page", LoginPage);