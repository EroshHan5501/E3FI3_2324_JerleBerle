import { Navigator } from "../helper/Navigator.js";
import { BasePage } from "../page.js";
import { AppConfig } from "../helper/AppConfig.js";

export class MainPage extends BasePage {
    #pageContentElement = null;
    currentUser = null;
    constructor(pageConfig) {
        super(pageConfig);
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        // INFO: Call this method first in all derived classes before doing anything
        await this.appendTemplate("app/mainpage/mainpage.html", this);

        if (this.currentUser === null) {

            const url = AppConfig.buildApiPath("User/current");

            await this.getDataAsync(
                url,
                {
                    400: (statusCode, data) => console.log(data),
                    404: (statusCode, data) => console.log("Could not find API route to get current user")
                },
                (json) => this.currentUser = json);
        }

        this.#setPageContentElement = this.querySelector("#page-content");

        const navElements = this.querySelectorAll(".nav-link");

        navElements.forEach(navElem => {
            this.handleClick(navElem, async (event) => {
                event.preventDefault();
                const arr = event.target.href.split("/");
                const path = arr[arr.length - 1];
                await Navigator.goToAsync(path);
            });
        });

        if (this.currentUser.role === 1) {
            const adminLink = this.querySelector("#admin-link");
            adminLink.classList.remove("invisible");
        }
    }

    set #setPageContentElement(element) {
        this.#pageContentElement = element;
    }

    get getPageContentElement() {
        if (this.#pageContentElement === null) {
            throw new TypeError("Call the init function of the main page!!!");
        }

        return this.#pageContentElement;
    }
}