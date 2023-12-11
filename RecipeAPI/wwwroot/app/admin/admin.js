import { AppConfig } from "../helper/AppConfig.js";
import { MainPage } from "../mainpage/mainpage.js";

export class AdminPage extends MainPage {
    static #entityNumber = 2;
    #currentIndex = 1;
    constructor() {
        super({ "title": "Admin panel", "heading": "Admin panel" });
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await super.initAsync();

        await this.appendTemplate(
            "/app/admin/admin.html",
            this.getPageContentElement);

        this.guiContent = {
            tableBody: this.querySelector("tbody"),
            loadBtn: this.querySelector(".load-button")
        }

        await this.#requestDataAsync(1, AdminPage.#entityNumber);

        this.handleClick(
            this.guiContent.loadBtn,
            this.#handleLoadMoreAsync.bind(this));
    }

    async #handleLoadMoreAsync(event) {
        event.preventDefault();
        this.#currentIndex += 1;
        await this.#requestDataAsync(this.#currentIndex, AdminPage.#entityNumber);
    }

    async #handleDeleteAsync(event) {
        event.preventDefault();
        const url = AppConfig.buildApiPath(
            `admin/User/delete?email=${event.target.getAttribute("data")}`);

        await this.deleteDataAsync(
            url,
            {
                400: (status, data) => console.log(data)
            },
            () => console.log("delete was successfull"));

        this.guiContent.tableBody.innerText = "";

        const pageSize = AdminPage.#entityNumber * this.#currentIndex;
        await this.#requestDataAsync(1, pageSize);
    }

    async #requestDataAsync(currentIndex, pageSize) {
        const url = AppConfig.buildApiPath(
            `admin/User/?pageIndex=${currentIndex}&pageSize=${pageSize}`);

        await this.getDataAsync(
            url,
            {
                400: (statusCode, data) => console.log(data),
                404: (statusCode, data) => console.log("Could not found!")
            },
            this.#initTableAsync.bind(this));
    }

    async #initTableAsync(json) {
        const template = await this.getTemplateContentAsync(
            "/app/admin/snippets/row.html");

        for (const user of json.content) {
            const tmp = template.cloneNode(true);
            const id = tmp.querySelector(".id-cell"),
                username = tmp.querySelector(".username-cell"),
                email = tmp.querySelector(".email-cell"),
                role = tmp.querySelector(".role-cell"),
                delBtn = tmp.querySelector(".del-btn");

            id.innerText = user.id;
            username.innerText = user.username;
            email.innerText = user.email;
            role.innerText = user.role;
            delBtn.setAttribute("data", user.email);

            this.handleClick(
                delBtn,
                this.#handleDeleteAsync.bind(this));

            this.guiContent.tableBody.appendChild(tmp);
        }

    }
}

customElements.define("admin-page", AdminPage);