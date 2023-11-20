import { MainPage } from "../mainpage/mainpage.js";

export class SettingsPage extends MainPage {
    constructor() {
        super({"title": "Settings", "heading": "Settings"});
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await super.initAsync();
    }
}

customElements.define("settings-page", SettingsPage);