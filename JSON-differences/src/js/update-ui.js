import { elements } from "./dom-elements.js";

const classHidden = `hidden`;

const showLoginSection = () => {
  elements.promoSection.classList.add(classHidden);
  elements.loginSection.classList.remove(classHidden);
};

const showLoggedInState = (username) => {
  elements.loginButton.classList.add(classHidden);
  elements.headerLoggedInContainer.classList.remove(classHidden);
  elements.promoSection.classList.remove(classHidden);
  elements.promoSectionLink.classList.remove(classHidden);
  elements.headerUserName.innerText = username.trim();
};

const showLoggedOutState = () => {
  elements.loginButton.classList.remove(classHidden);
  elements.promoSection.classList.remove(classHidden);
  elements.headerLoggedInContainer.classList.add(classHidden);
  elements.mainForm.classList.add(classHidden);
  elements.promoSectionLink.classList.add(classHidden);
  elements.headerUserName.innerText = ``;
};

const showPromoSection = (isLoggedIn) => {
  hideLoginSection();
  hideLoginError();
  hideJsonErrors();
  elements.promoSection.classList.remove(classHidden);
  elements.mainForm.classList.add(classHidden);

  if (isLoggedIn) {
    elements.promoSectionLink.classList.remove(classHidden);
  } else {
    elements.promoSectionLink.classList.add(classHidden);
  }
};

const showMainForm = () => {
  elements.promoSection.classList.add(classHidden);
  elements.mainForm.classList.remove(classHidden);
};

const showResult = (result) => {
  const jsonResult = JSON.stringify(result, undefined, 2);
  elements.resultBlock.innerHTML = `<pre>${jsonResult}</pre>`;
  elements.resultBlock.classList.remove(classHidden);
};

const showLoginError = () => {
  const errorSpan = elements.usernameInput.nextElementSibling;
  errorSpan.classList.remove(classHidden);
};

const showJsonError = (field, message) => {
  field.textContent = message;
  field.classList.remove(classHidden);
};

const clearLoginInput = () => {
  elements.usernameInput.value = ``;
};

const clearMainFormAndHideResult = () => {
  elements.textareaOld.value = ``;
  elements.textareaNew.value = ``;
  elements.resultBlock.classList.add(classHidden);
  hideJsonErrors();
};

const hideLoginSection = () => {
  elements.loginSection.classList.add(classHidden);
};

const hideLoginError = () => {
  const errorSpan = elements.usernameInput.nextElementSibling;
  errorSpan.classList.add(classHidden);
};

const hideJsonErrors = () => {
  elements.oldJsonError.classList.add(classHidden);
  elements.newJsonError.classList.add(classHidden);
};

export const UI = {
  showLoginSection,
  showLoggedInState,
  showLoggedOutState,
  showMainForm,
  showResult,
  showPromoSection,
  showLoginError,
  showJsonError,
  clearLoginInput,
  clearMainFormAndHideResult,
  hideLoginSection,
  hideLoginError,
  hideJsonErrors
};