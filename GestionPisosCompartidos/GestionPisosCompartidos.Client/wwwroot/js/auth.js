window.authStorage = {
    setToken: function (token) {
        localStorage.setItem("jwt_token", token);
    },
    getToken: function () {
        return localStorage.getItem("jwt_token");
    },
    removeToken: function () {
        localStorage.removeItem("jwt_token");
    },
    setUser: function (user) {
        localStorage.setItem("user_data", JSON.stringify(user));
    },
    getUser: function () {
        const data = localStorage.getItem("user_data");
        return data ? JSON.parse(data) : null;
    },
    removeUser: function () {
        localStorage.removeItem("user_data");
    },
    logout: function () {
        localStorage.removeItem("jwt_token");
        localStorage.removeItem("user_data");
    }
};