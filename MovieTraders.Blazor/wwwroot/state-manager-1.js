window.statemanager = {
    set: function (key, value) {
        if (value == null) {
            localStorage.removeItem(key);
        }
        else {
            localStorage.setItem(key, value);
        }
    },
    get: function (key) {
        return localStorage.getItem(key);
    },
    setVisibility: function (id, visible) {
        var elm = document.getElementById(id);
        if (elm) {
            elm.hidden = !visible;
        }
    }
}