import { BasePage } from "../../page.js";
import { Navigator } from "../../helper/Navigator.js";
import { RouteNames } from "../../helper/RouteNames.js";
import { AppConfig } from "../../helper/AppConfig.js";

export class RegisterPage extends BasePage {
    constructor() {
        super({ "title": "Register", "heading": "Register" });
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await this.appendTemplate("/app/auth/register/register.html", this);

        this.guiContent = {
            emailTxt: this.querySelector("#email"),
            usernameTxt: this.querySelector("#username"),
            passwordTxt: this.querySelector("#password"),
            registerBtn: this.querySelector("#register-btn"),
            goLoginLink: this.querySelector("#go-login-link")
        }

        this.handleClick(
            this.guiContent.registerBtn,
            this.#handleRegisterBtnClick.bind(this))

        this.handleClick(
            this.guiContent.goLoginLink,
            async (event) => {
                event.preventDefault();
                await Navigator.goToAsync(RouteNames.login);
            }
        )
    }

    async #handleRegisterBtnClick(event) {
        event.preventDefault();

        const payload = {
            username: this.guiContent.usernameTxt.value,
            email: this.guiContent.emailTxt.value,
            password: this.guiContent.passwordTxt.value
        }

        const url = AppConfig.buildApiPath("User/register/");
        this.sendDataAsync(url, payload, this.#handleResponseError.bind(this))
    }

    async #handleResponseError(statusCode, json) {
        switch(statusCode) {
            case 400:
                for (let errorKeys of Object.keys(json.errors)) {
                    let message = "";
                    for (let msg of json.errors[errorKeys]) {
                        message += msg;
                    }

                    const elem = this.querySelector(`#${errorKeys}`);
                    elem.innerText = message;
                }
                break;
        }
    }
}

customElements.define("register-page", RegisterPage);