import { MainPage } from "../mainpage/mainpage.js";
import { AppConfig } from "../helper/AppConfig.js";
import { Navigator } from "../helper/Navigator.js";
import { RouteNames } from "../helper/RouteNames.js";

export class DashboardPage extends MainPage {
    constructor() {
        super({ "title": "Dashboard", "heading": "Dashboard" });
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await super.initAsync();

        await this.appendTemplate("/app/dashboard/dashboard.html", this.getPageContentElement);

        this.guiContent = {
            nameTxt: this.querySelector("#name-element"),
            searchBox: this.querySelector("#search-bar"),
            createRecipeLink: this.querySelector("#create-recipe-link")
        }

        const url = AppConfig.buildApiPath("User/current/");
        await this.getDataAsync(
            url, 
            {
                400: this.#handle400Error.bind(this)
            },
            this.#handleSuccessAsync.bind(this));
        
        this.handleClick(
            this.guiContent.createRecipeLink,
            async (event) => {
                event.preventDefault();
                await Navigator.goToAsync(RouteNames.createRecipe);
            });
    }

    #handle400Error(statusCode, data) {
        
    }

    async #handleSuccessAsync(data) {
        this.guiContent.nameTxt.innerText = data.username;
    }
}

customElements.define("dashboard-page", DashboardPage);