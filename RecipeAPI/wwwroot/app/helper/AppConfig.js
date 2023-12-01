
export class AppConfig {
    static baseUrl = "https://localhost:7086";

    static apiBaseUrl = "https://localhost:7086/api";

    static buildPath(path) {
        return `${AppConfig.baseUrl}/${path}`;
    }

    static buildApiPath(path) {
        return `${AppConfig.apiBaseUrl}/${path}`;
    }
}