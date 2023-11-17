import { Navigator } from "../helper/Navigator.js";
import { BasePage } from "../page.js";

export class MainPage extends BasePage {
    #pageContentElement = null;
    constructor(pageConfig) {
        super(pageConfig);
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        // INFO: Call this method first in all derived classes before doing anything
        await this.appendTemplate("/app/mainpage/mainpage.html", this);
        
        this.#setPageContentElement = this.querySelector("#page-content");
        
        const navElements = this.querySelectorAll(".nav-link");

        navElements.forEach(navElem => {
            this.handleClick(navElem, async (event) => {
                event.preventDefault();
                const path = event.target.href;
                await Navigator.goToAsync(path);
            });
        });
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
