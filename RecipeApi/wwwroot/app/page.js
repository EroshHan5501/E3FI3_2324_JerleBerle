
// What does a page need to be able to do 
// - Load stuff from external resources 
// - Keep track of the page history 
// - Updating the UI 
// - Render stuff 
// - Handling errors 

import { AppConfig } from "/app/helper/AppConfig.js";
import { Navigator } from "/app/helper/Navigator.js";
import { RouteNames } from "/app/helper/RouteNames.js";

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
        const content = await this.getTemplateContentAsync(path);
        node.appendChild(content);
    }

    async getTemplateContentAsync(path) {
        const html = await this.#loadTemplateAsync(path);
        const template = html.querySelector("template");
        if (template === undefined) {
            throw new Error("Could not found template tag!");
        }
        const content = template.content.cloneNode(true);
        return content;
    }

    async #loadTemplateAsync(path) {
        const url = AppConfig.buildPath(path);
        const response = await fetch(url);
        const html = await response.text();
        const parser = new DOMParser();
        return parser.parseFromString(html, "text/html");
    }

    async getDataAsync(url, errorCallbacks, successCallback) {
        return this.#makeRequest(url, "GET", null, errorCallbacks, successCallback);
    }

    async sendDataAsync(url, payload, errorCallbacks, successCallback) {
        return this.#makeRequest(url, "POST", payload, errorCallbacks, successCallback);
    }

    async deleteDataAsync(url, errorCallbacks, successCallback) {
        return this.#makeRequest(url, 'DELETE', errorCallbacks, successCallback);
    }

    async #makeRequest(url, method, payload, errorCallbacks, successCallback) {
        const request = {
            method: method,
            headers: new Headers({ 'content-type': 'application/json' }),
        }

        if (payload !== null) {
            request["body"] = JSON.stringify(payload);
        }

        const response = await fetch(url, request);

        let data;
        try {
            data = await response.json();
        }
        catch {
            data = "";
        }

        if (response.status !== 200) {
            await this.#handleErrorInternal(response.status, data, errorCallbacks);
            return;
        }

        await successCallback(data);
    }

    async #handleErrorInternal(statusCode, data, errorCallbacks) {
        switch (statusCode) {
            case 400:
                errorCallbacks["400"](statusCode, data);
                break;
            case 401:
                await Navigator.goToAsync(RouteNames.login);
                break;
            case 403:
                errorCallbacks["403"](statusCode, data);
                break;
        }
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
