import { MainPage } from "../../mainpage/mainpage.js";

export class CreateRecipePage extends MainPage {
    constructor() {
        super({"title": "Create recipe", "heading": "Create recipe"});
        this.setInitCallback = this.initAsync; 
    }

    async initAsync() {
        await super.initAsync();

        await this.appendTemplate(
            "/app/recipes/create/createRecipe.html", 
            this.getPageContentElement);
    }
}

customElements.define("create-recipe-page", CreateRecipePage);