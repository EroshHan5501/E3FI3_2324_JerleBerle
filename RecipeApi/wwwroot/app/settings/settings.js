import { MainPage } from "../mainpage/mainpage.js";
import { AppConfig } from "../helper/AppConfig.js";
import { Navigator } from "../helper/Navigator.js";
import { RouteNames } from "../helper/RouteNames.js";

export class SettingsPage extends MainPage {
    static roleMapping = {
        0: "User",
        1: "Admin"
    }

    constructor() {
        super({ "title": "Settings", "heading": "Settings" });
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await super.initAsync();

        await this.appendTemplate(
            "/app/settings/settings.html",
            this.getPageContentElement);

        this.guiContent = {
            usernameTxt: this.querySelector("#username"),
            emailTxt: this.querySelector("#email"),
            roleTxt: this.querySelector("#role"),
            saveBtn: this.querySelector("#save"),
            oldPasswordTxt: this.querySelector("#old"),
            newPasswordTxt: this.querySelector("#new"),
            confirmPasswordTxt: this.querySelector("#confirm"),
            changeBtn: this.querySelector("#change"),
            deleteBtn: this.querySelector("#delete-button")
        }

        const url = AppConfig.buildApiPath("User/current/");
        await this.getDataAsync(
            url,
            {
                400: this.#handle400Error.bind(this)
            },
            this.#handleSuccessInit.bind(this));

        this.handleClick(
            this.guiContent.saveBtn,
            this.#onSaveButtonClick.bind(this));

        this.handleClick(
            this.guiContent.changeBtn,
            this.#onChangeButtonClick.bind(this));

        this.handleClick(
            this.guiContent.deleteBtn,
            this.#onDeleteButtonClick.bind(this));
    }

    #handle400Error(statusCode, json) {

    }

    #handleSuccessInit(json) {
        this.guiContent.usernameTxt.value = json.username;
        this.guiContent.emailTxt.value = json.email;
        this.guiContent.roleTxt.value = SettingsPage.roleMapping[json.role];
    }

    async #onSaveButtonClick(event) {
        event.preventDefault();

        const payload = {
            username: this.guiContent.usernameTxt.value,
            email: this.guiContent.emailTxt.value
        }

        const url = AppConfig.buildApiPath("User/update/");
        await this.sendDataAsync(url, payload, {
            400: (statusCode, data) => console.log(data)
        }, () => { });
    }

    async #onDeleteButtonClick(event) {
        event.preventDefault();

        const url = AppConfig.buildApiPath("User/delete");
        await this.deleteDataAsync(
            url,
            null,
            {
                400: (statusCode, data) => console.log(data)
            },
            async () => await Navigator.goToAsync(RouteNames.login));
    }

    async #onChangeButtonClick(event) {
        event.preventDefault();

        const payload = {
            oldpassword: this.guiContent.oldPasswordTxt.value,
            newpassword: this.guiContent.newPasswordTxt.value,
            confirm: this.guiContent.confirmPasswordTxt.value
        };

        const url = AppConfig.buildApiPath("User/change-password/");
        await this.sendDataAsync(url, payload, {}, () => { });
    }

}

customElements.define("settings-page", SettingsPage);