export const elements = {
  headerLogo: document.querySelector(`.header-logo-link`),

  mainForm: document.querySelector(`.main-form-section`),
  textareaOld: document.getElementById(`oldJson`),
  textareaNew: document.getElementById(`newJson`),
  mainFormButton: document.querySelector(`.main-form button`),

  loginForm: document.querySelector(`.login-form`),
  loginSection: document.querySelector(`.login-section`),
  usernameInput: document.getElementById(`login`),

  loginButton: document.querySelector(`.header-login-button`),
  logoutButton: document.querySelector(`.logout-button`),
  headerLoggedInContainer: document.querySelector(`.header-logged-in`),
  headerUserName: document.getElementById(`username`),

  promoSection: document.querySelector(`.promo-section`),
  promoSectionLink: document.querySelector(`.promo-link`),

  resultBlock: document.querySelector(`.result`),

  oldJsonError: document.getElementById(`oldJson`).nextElementSibling,
  newJsonError: document.getElementById(`newJson`).nextElementSibling,
};