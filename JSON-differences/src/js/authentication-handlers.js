import { Authentication } from "./authentication.js";
import { UI } from "./update-ui.js";
import { elements } from "./dom-elements.js";

const updateAuthUI = () => {
  const { isAuthenticated, username } = Authentication.getAuthState();
  if (isAuthenticated) {
    UI.showLoggedInState(username);
  } else {
    UI.showLoggedOutState();
  }
};

export function configureAuthHandlers() {
  updateAuthUI();
  elements.loginButton.addEventListener(`click`, UI.showLoginSection);

  elements.loginForm.addEventListener(`submit`, (event) => {
    event.preventDefault();
    UI.hideLoginError();
    const username = elements.usernameInput.value;

    if (Authentication.login(username)) {
      UI.hideLoginSection();
      UI.showLoggedInState(username);
      UI.clearLoginInput();
    } else {
      UI.showLoginError();
    }
  });

  elements.logoutButton.addEventListener(`click`, (event) => {
    event.preventDefault();
    Authentication.logout();
    UI.showLoggedOutState();
    UI.clearMainFormAndHideResult();
  });

  elements.promoSectionLink.addEventListener(`click`, (event) => {
    event.preventDefault();
    UI.showMainForm();
  });

  elements.headerLogo.addEventListener(`click`, (event) => {
    event.preventDefault();

    const username = localStorage.getItem(`username`);
    const isLoggedIn = username !== null;

    UI.clearLoginInput();
    UI.clearMainFormAndHideResult();
    UI.showPromoSection(isLoggedIn);
  });
}