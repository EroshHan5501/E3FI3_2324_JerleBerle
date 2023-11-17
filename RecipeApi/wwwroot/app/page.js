
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

    constructor(pageConfig) {
        super();
        this.#pageConfig = pageConfig;
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

    async getDataAsync(url, errorCallback) {
        return this.#makeRequest(url, "GET", null, errorCallback);
    }

    async sendDataAsync(url, payload, errorCallback) {
        return this.#makeRequest(url, "POST", payload, errorCallback);
    }

    async #makeRequest(url, method, payload, errorCallback) {

        const response = await fetch(url, {
            method: method,
            headers: new Headers({'content-type': 'application/json'}),
        })

        if (payload !== null) {
            response["body"] = JSON.stringify(payload);
        }

        let data;
        try{
            data = await response.json(); 
        }
        catch {
            return "";
        }

        if (response.status !== 200) {
            errorCallback(response.status, data);
            return;
        }

        return data;
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
