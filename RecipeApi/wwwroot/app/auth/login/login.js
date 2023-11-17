import { BasePage } from "/app/page.js";
import { Navigator } from "../../helper/Navigator.js";
import { RouteNames } from "../../helper/RouteNames.js";
import { AppConfig } from "../../helper/AppConfig.js";

export class LoginPage extends BasePage {
    constructor() {
        super({"title": "Login", "heading": "Login"});
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await this.appendTemplate("app/auth/login/login.html", this);

        this.guiContent = {
            emailTxt: this.querySelector("#email"),
            passwordTxt: this.querySelector("#password"),
            loginBtn: this.querySelector("#login-btn"),
            goRegisterLink: this.querySelector("#go-register-link")
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

        const url = AppConfig.buildApiPath("login");
        await this.sendDataAsync(url, payload, this.#handleErrorResponse);

        await Navigator.goToAsync(RouteNames.dashboard);
    }

    async #handleErrorResponse(statusCode, json) {
        switch(statusCode) {
            case 401:

                break;
        }
    }
}

customElements.define("login-page", LoginPage);