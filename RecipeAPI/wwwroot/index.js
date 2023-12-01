import { Navigator } from "./app/helper/Navigator.js";
import { RouteNames } from "./app/helper/RouteNames.js";

document.addEventListener("DOMContentLoaded", async () => {
    const path = document.location.pathname;
    if (path === "/")
        await Navigator.goToAsync(RouteNames.dashboard);
    else {
        await Navigator.goToAsync(path.substring(1));
    }
})