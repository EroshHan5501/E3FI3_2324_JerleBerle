import { MainPage } from "../mainpage/mainpage.js";

export class DashboardPage extends MainPage {
    constructor() {
        super({ "title": "Dashboard", "heading": "Dashboard" });
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await super.initAsync();

        await this.appendTemplate("/app/dashboard/dashboard.html", this.getPageContentElement);


    }
}

customElements.define("dashboard-page", DashboardPage);