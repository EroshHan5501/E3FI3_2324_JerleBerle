import { BasePage } from "../../page.js";
import { AppConfig } from "../../helper/AppConfig.js";
import { Navigator } from "../../helper/Navigator.js";
import { RouteNames } from "../../helper/RouteNames.js";

export class LogoutPage extends BasePage {
    constructor() {
        super({"title": "Logout", "heading": "Logout"});
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await this.appendTemplate("/app/auth/logout/logout.html", this);

        const url = AppConfig.buildApiPath("logout");
        await this.getDataAsync(
            url, 
            {
                400: this.#handle400Error.bind(this),
                403: this.#handle403Error.bind(this)
            },
            this.#handleSuccessAsync.bind(this));

        await Navigator.goToAsync(RouteNames.login);
    }

    #handle400Error(statusCode, data) {

    }

    #handle403Error(statusCode, data) {

    }

    async #handleSuccessAsync(data) {
        await Navigator.goToAsync(RouteNames.login);
    }
}

customElements.define("logout-page", LogoutPage);