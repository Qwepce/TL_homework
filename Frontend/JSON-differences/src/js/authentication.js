const login = (username) => {
  if (!username || username.trim() === ``) {
    return false;
  }

  localStorage.setItem(`username`, username.trim());
  return true;
};

const logout = () => {
  localStorage.removeItem(`username`);
};

const getAuthState = () => {
  const username = localStorage.getItem(`username`);
  return { isAuthenticated: !!username, username };
};

export const Authentication = { login, logout, getAuthState };