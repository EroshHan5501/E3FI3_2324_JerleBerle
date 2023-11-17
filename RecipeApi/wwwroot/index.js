import { Navigator } from "./app/helper/Navigator.js";
import { RouteNames } from "./app/helper/RouteNames.js";

document.addEventListener("DOMContentLoaded", () => {
    Navigator.goToAsync(RouteNames.dashboard);
})