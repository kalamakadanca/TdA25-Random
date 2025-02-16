window.authHelper = {
    login: async function (email, password) {
        const response = await fetch('/api/auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password }),
            credentials: 'include'
        });

        return response.ok;
    },

    logout: async function () {
        const response = await fetch('api/auth/logout', {
            method: 'POST',
        });
        
        return response.ok;
    },

    register: async function (email, password) {
        const response = await fetch("api/auth/register", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password }),
            credentials: 'include'
        });
        
        return response.ok
    },
};