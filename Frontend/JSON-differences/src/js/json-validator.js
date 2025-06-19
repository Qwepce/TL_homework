const tryParse = (data) => {
  try {
    JSON.parse(data);
    return true;
  } catch (error) {
    return false;
  }
};

export const JsonValidator = { tryParse };