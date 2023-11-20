import { BasePage } from "../page.js";

export class SettingsPage extends BasePage {
    constructor() {
        super({"title": "Settings", "heading": "Settings"});
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {

    }
}