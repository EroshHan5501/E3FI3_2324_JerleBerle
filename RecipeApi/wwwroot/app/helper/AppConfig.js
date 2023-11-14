
export class AppConfig {
    static baseUrl = "https://localhost:7295";

    static buildPath(path) {
        console.log(path);
        return `${AppConfig.baseUrl}/${path}`;
    }
}