
// What does a page need to be able to do 
// - Load stuff from external resources 
// - Keep track of the page history 
// - Updating the UI 
// - Render stuff 
// - Handling errors 

import { AppConfig } from "/app/helper/AppConfig.js";

/*
 pageConfig 
    - title 
    - heading 

*/

export class BasePage extends HTMLElement {
    #pageConfig = null;
    #initCallback = null;

    constructor(pageConfig, initCallback) {
        super();
        this.#pageConfig = pageConfig;
        this.#initCallback = initCallback;
    } 

    async appendTemplate(path, node) {
        const html = await this.#loadTemplate(path);
        const template = html.querySelector("template");
        if (template === undefined) {
            throw new Error("Could not found template tag!");
        }
        const content = template.content.cloneNode(true);
        node.appendChild(content);
    }

    async #loadTemplate(path) {
        const url = AppConfig.buildPath(path);
        console.log(url);
        const response = await fetch(url);
        const html = await response.text();
        const parser = new DOMParser();
        return parser.parseFromString(html, "text/html");
    }

    async connectedCallback() {
        document.title = `${this.#pageConfig["title"]} - Recipedia`;
        
        await this.#initCallback();
    }

    async disconnectedCallback() {
        // display a loading animation 
    }

    set setInitCallback(callback) {
        this.#initCallback = callback;
    }

    handleClick(element, callback) {
        element.addEventListener("click", callback);
    }
}
