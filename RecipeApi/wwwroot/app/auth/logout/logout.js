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

        const url = AppConfig.apiBaseUrl("logout");
        await this.getData(url, this.#onReceivingError);

        await Navigator.goToAsync(RouteNames.login);
    }

    async #onReceivingError(statusCode, json) {

    }
}

customElements.define("logout-page", LogoutPage);