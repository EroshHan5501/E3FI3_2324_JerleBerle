import { BasePage } from "/app/page.js";
import { Navigator } from "../../helper/Navigator.js";
import { RouteNames } from "../../helper/RouteNames.js";

export class LoginPage extends BasePage {
    constructor() {
        super({"title": "Login", "heading": "Login"});
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await this.appendTemplate("app/auth/login/login.html", this);

        this.guiContent = {
            usernameTxt: this.querySelector("#username"),
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

    }
}

customElements.define("login-page", LoginPage);