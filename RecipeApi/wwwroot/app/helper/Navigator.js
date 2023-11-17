import { RouteNames } from "./RouteNames.js";

export class Navigator {

    static async goToAsync(path) {

        let page = null;

        switch(path) {
            case RouteNames.login: {
                const module = await import("/app/auth/login/login.js");
                page = new module.LoginPage();
            }
            break;
            case RouteNames.register: {
                const module = await import("/app/auth/register/register.js");
                page = new module.RegisterPage();
            }
            break;
            case RouteNames.logout: {
                const module = await import("/app/auth/logout/logout.js");
                page = new module.LogoutPage();
            }
            break;
            case RouteNames.dashboard: {
                const module = await import("/app/dashboard/dashboard.js");
                page = new module.DashboardPage();
            }
            default:
                // Error page
                break;
        }

        const main = document.querySelector("main");
        main.innerText = "";
        main.appendChild(page);
    }
}