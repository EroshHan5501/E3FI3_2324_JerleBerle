import { AppConfig } from "../../helper/AppConfig.js";
import { BasePage } from "../../page.js";

export class ChangeRolePage extends BasePage {
    #user = null;
    #notifyCallback = null;
    constructor(user, notifyCallback) {
        super({ "title": "Change role" });
        this.setInitCallback = this.initAsync;
        this.#user = user;
        this.#notifyCallback = notifyCallback;
    }

    async initAsync() {
        await this.appendTemplate("/app/subpages/admin/changeRolePage.html", this);

        this.guiContent = {
            heading: this.querySelector("h2"),
            label: this.querySelector("label"),
            check: this.querySelector("input"),
            button: this.querySelector("button")
        }

        this.guiContent.heading.innerText = `Change role of ${this.#user.username}`;
        this.guiContent.label.innerText = this.#user.role === "1" ? "Admin" : "User";
        this.guiContent.check.checked = this.#user.role === "1";

        this.handleChange(
            this.guiContent.check,
            this.#toggleCheckbox.bind(this)
        );

        this.handleClick(
            this.guiContent.button,
            this.#handleRoleChange.bind(this));
    }

    async #handleRoleChange(event) {
        event.preventDefault();

        const url = AppConfig.buildApiPath("admin/User/update-role/");

        const payload = {
            email: this.#user.email,
            role: Number(this.guiContent.check.checked)
        }

        await this.sendDataAsync(url, payload, {
            404: (status, data) => console.log("not found"),
            400: (status, data) => console.log(data)
        }, async () => {
            await this.#notifyCallback();
            document.body.removeChild(this);
        });
    }

    #toggleCheckbox(event) {
        event.preventDefault();

        const { label, check } = this.guiContent;

        if (check.checked) {
            label.innerText = "Admin";
        }
        else {
            label.innerText = "User";
        }
    }
}

customElements.define("change-role-page", ChangeRolePage);