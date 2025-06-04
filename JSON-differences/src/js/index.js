import { configureAuthHandlers } from "./authentication-handlers.js";
import { configureDiffHandler } from "./json-diff-handler.js";

document.addEventListener(`DOMContentLoaded`, () => {
  configureAuthHandlers();
  configureDiffHandler();
});