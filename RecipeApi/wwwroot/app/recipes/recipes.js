import { BasePage } from "../page.js";

export class RecipesPage extends BasePage {
    constructor() {
        super({"title": "Recipes", "heading": "Recipes"});
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {

    }
}

customElements.define("recipes-page", RecipesPage);