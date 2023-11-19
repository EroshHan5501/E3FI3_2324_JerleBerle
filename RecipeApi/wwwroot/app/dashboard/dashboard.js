import { MainPage } from "../mainpage/mainpage.js";
import { AppConfig } from "../helper/AppConfig.js";

export class DashboardPage extends MainPage {
    constructor() {
        super({ "title": "Dashboard", "heading": "Dashboard" });
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await super.initAsync();

        await this.appendTemplate("/app/dashboard/dashboard.html", this.getPageContentElement);

        const url = AppConfig.buildApiPath("Recipe/")
        await this.getDataAsync(
            url, 
            {
                400: this.#handle400Error.bind(this)
            },
            this.#handleSuccessAsync.bind(this));
    }

    #handle400Error(statusCode, data) {

    }

    async #handleSuccessAsync(data) {

    }
}

customElements.define("dashboard-page", DashboardPage);