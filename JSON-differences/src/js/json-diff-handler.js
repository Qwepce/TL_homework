import { JsonDiff } from "./json-diff.js";
import { UI } from "./UI.js";
import { elements } from "./dom-elements.js";
import { JsonValidator } from "./json-validator.js";

export function configureDiffHandler() {
  elements.mainForm.addEventListener(`submit`, async (event) => {
    event.preventDefault();
    UI.hideJsonErrors();

    let invalidInputs = false;
    const oldJsonInput = elements.textareaOld.value.trim();
    const newJsonInput = elements.textareaNew.value.trim();

    const inputs = [
      { value: oldJsonInput, errorElement: elements.oldJsonError },
      { value: newJsonInput, errorElement: elements.newJsonError },
    ];

    inputs.forEach((input) => {
      if (input.value === ``) {
        UI.showJsonError(input.errorElement, `Обязательное поле`);
        invalidInputs = true;
      }
      if (input.value !== `` && !JsonValidator.tryParse(input.value)) {
        UI.showJsonError(input.errorElement, `Некорректный JSON`);
        invalidInputs = true;
      }
    });

    if (invalidInputs) {
      return;
    }

    const defaultButtonHtml = elements.mainFormButton.innerHTML;
    elements.mainFormButton.innerHTML = `Loading...`;
    elements.mainFormButton.disabled = true;

    const oldValue = JSON.parse(elements.textareaOld.value);
    const newValue = JSON.parse(elements.textareaNew.value);
    const result = await JsonDiff.create(oldValue, newValue);

    elements.mainFormButton.innerHTML = defaultButtonHtml;
    elements.mainFormButton.disabled = false;

    UI.showResult(result);
  });
}
