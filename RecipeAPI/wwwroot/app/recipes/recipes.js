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
        //const urlRel = AppConfig.buildApiPath("recipe/rels/1/?pageSize=10000&pageIndex=1");
        //await this.getDataAsync(
        //    urlRel,
        //    {
        //        400: this.#handle400Error.bind(this)
        //    },
        //    this.#handleSuccessRel.bind(this)
        //);
    }

    #handle400Error(statusCode, data) {

    }

    async #handleSuccess(data) {
	    this.querySelector(".recipe-list").style.display = "flex";
	    //Recipe List
	    const list = this.querySelector(".recipe-list").appendChild(document.createElement("ul"));
	    list.style.overflow = "scroll";
	    list.style.height = "80vh";
	    list.style.border = "2px solid #333";
	    list.style.width = "max-content";
	    list.style.padding = "5px 20px";
	    //Quantity List
	    const listQuantity = this.querySelector(".recipe-list").appendChild(document.createElement("ul"));
	    listQuantity.style.overflow = "scroll";
	    listQuantity.style.height = "80vh";
	    listQuantity.style.border = "2px solid #333";
	    listQuantity.style.width = "100px";
	    listQuantity.style.padding = "5px 20px";
	    listQuantity.classList.add("quantity-list");
	    //UOM List
	    const listUOM = this.querySelector(".recipe-list").appendChild(document.createElement("ul"));
	    listUOM.style.overflow = "scroll";
	    listUOM.style.height = "80vh";
	    listUOM.style.border = "2px solid #333";
	    listUOM.style.width = "100px";
	    listUOM.style.padding = "5px 20px";
	    listUOM.classList.add("uom-list");
	    //Ingredient List
	    const listIngredient = this.querySelector(".recipe-list").appendChild(document.createElement("ul"));
	    listIngredient.style.overflow = "scroll";
	    listIngredient.style.height = "80vh";
	    listIngredient.style.border = "2px solid #333";
	    listIngredient.style.width = "100px";
	    listIngredient.style.padding = "5px 20px";
	    listIngredient.classList.add("ingredient-list");
	    //console.log(data);
	    for (const element of data) {
		    //const node = document.createTextNode(element.name);
		    //this.querySelector(".recipe-list").appendChild(node);

		    const listElement = document.createElement("li");
		    listElement.innerText = element.name;
		    list.appendChild(listElement);
		    listElement.addEventListener("click", (e) =>{
			    if (e.target.classList.contains("active")) {
				    e.target.classList.remove("active");
				    listQuantity.innerHTML = "";
				    listUOM.innerHTML = "";
				    listIngredient.innerHTML = "";
			    }
			    else {
				    for (const element of document.querySelectorAll(".active")) {
					    element.classList.remove("active");
				    }
				    e.target.classList.add("active");
				    console.log("asdsadads" + element.id);

				    console.log("|||");
				    this.#GetRelatedQuantity(element.id);
				    console.log("|||");
			    }
		    });
	    }
    }

	async #GetRelatedQuantity(id) {
	    const urlRel = AppConfig.buildApiPath("recipe/rels/"+id+"/?pageSize=10000&pageIndex=1");
	    await this.getDataAsync(
		    urlRel,
		    {
			400: this.#handle400Error.bind(this)
		    },
		    this.#handleSuccessRel.bind(this)
	    );
	}

    async #handleSuccessRel(data) {
	    console.log(data);
		const listQuantity = this.querySelector(".quantity-list");
		const listUOM = this.querySelector(".uom-list");
		const listIngredient = this.querySelector(".ingredient-list");
	    	listQuantity.innerHTML = "";
	    	listUOM.innerHTML = "";
	    	listIngredient.innerHTML = "";
	    for (const element of data) {
		    const listElement = document.createElement("li");
		    listElement.innerText = element.quantity;
		    listQuantity.appendChild(listElement);
		    this.#GetRelatedUOM(element.unitOfMeasurementId);
		    this.#GetRelatedIngredient(element.ingredientId);
	    }
    }

	async #GetRelatedUOM(id) {
	    const urlRel = AppConfig.buildApiPath("unitofmeasurement/"+id+"/?pageSize=10000&pageIndex=1");
	    await this.getDataAsync(
		    urlRel,
		    {
			400: this.#handle400Error.bind(this)
		    },
		    this.#handleSuccessUOM.bind(this)
	    );
	}

    async #handleSuccessUOM(data) {
		const listUOM = this.querySelector(".uom-list");
		    const listElement = document.createElement("li");
		    listElement.innerText = data.name;
		    listUOM.appendChild(listElement);
    }

	async #GetRelatedIngredient(id) {
	    const urlRel = AppConfig.buildApiPath("ingredient/"+id+"/?pageSize=10000&pageIndex=1");
	    await this.getDataAsync(
		    urlRel,
		    {
			400: this.#handle400Error.bind(this)
		    },
		    this.#handleSuccessIngredient.bind(this)
	    );
	}

    async #handleSuccessIngredient(data) {
		const listIngredient = this.querySelector(".ingredient-list");
		    const listElement = document.createElement("li");
		    listElement.innerText = data.name;
		    listIngredient.appendChild(listElement);
    }
}

customElements.define("recipes-page", RecipesPage);
