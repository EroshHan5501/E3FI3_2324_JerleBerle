import { AppConfig } from "../helper/AppConfig.js";
import { MainPage } from "../mainpage/mainpage.js";

export class RecipesPage extends MainPage {
    constructor() {
        super({"title": "Recipes", "heading": "Recipes"});
        this.setInitCallback = this.initAsync;
    }

    async initAsync() {
        await super.initAsync();

        await this.appendTemplate("/app/recipes/recipes.html", this.getPageContentElement);

        this.guiContent = {
            recipeList: this.querySelector(".recipe-list")
        }

        const url = AppConfig.buildApiPath("Recipe/?pageSize=10000&pageIndex=1");
        await this.getDataAsync(
            url,
            {
                400: this.#handle400Error.bind(this)
            },
            this.#handleSuccess.bind(this)
        );
    }

    #handle400Error(statusCode, data) {

    }

    async #handleSuccess(data) {
        console.log(data);
        // for (const elem in )
    }
}

customElements.define("recipes-page", RecipesPage);