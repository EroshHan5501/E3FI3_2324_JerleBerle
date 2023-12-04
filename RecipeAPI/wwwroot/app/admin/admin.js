import { AppConfig } from "../helper/AppConfig.js";
import { MainPage } from "../mainpage/mainpage.js";

export class AdminPage extends MainPage {
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
            tableBody: this.querySelector("tbody")
        }

        const url = AppConfig.buildApiPath("admin/User/?pageIndex=1&pageSize=1");
        // TODO: Parameter building 

        this.getDataAsync(
            url,
            {
                400: (statusCode, data) => console.log(data),
                404: (statusCode, data) => console.log("Could not found!")
            },
            this.#initTableAsync.bind(this));
    }

    async #initTableAsync(json) {

        const template = await this.loadTemplateAsync(
            "/app/admin/snippets/row.html");

        for (const user in json.entities) {
            const tmp = template.cloneNode(true);
            const id = tmp.querySelector(".id-cell"),
                username = tmp.querySelector(".username-cell"),
                email = tmp.querySelector(".email-cell"),
                role = tmp.querySelector(".role-cell");

            id.innerText = user.id;
            username.innerText = user.username;
            email.innerText = user.email;
            role.innerText = user.role;

            console.log(tmp);

            this.guiContent.tableBody.appendChild(tmp);
        }

    }
}

customElements.define("admin-page", AdminPage);