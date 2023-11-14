import { BasePage } from "/app/page.js";

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
            loginBtn: this.querySelector("#login-btn")
        }

        this.handleClick(this.guiContent.loginBtn, () => console.log("Login"));
    }
}

customElements.define("login-page", LoginPage);