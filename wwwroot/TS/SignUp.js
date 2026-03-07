"use strict";
const SIGNUP_URL = "/api/user/signup";
async function signUp(data) {
    const res = await fetch(SIGNUP_URL, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    });
    if (!res.ok) {
        alert(await res.text());
        return;
    }
    alert("Account created successfully!");
    window.location.href = "/templates/Login.html";
}
function initSignUp() {
    const form = document.getElementById("SignUpForm");
    const usernameInput = document.getElementById("InputUsername");
    const emailInput = document.getElementById("InputEmail");
    const passwordInput = document.getElementById("InputPassword");
    const confirmPasswordInput = document.getElementById("InputCMPassword");
    if (!form || !usernameInput || !emailInput || !passwordInput || !confirmPasswordInput)
        return;
    form.addEventListener("submit", async (e) => {
        e.preventDefault();
        const username = usernameInput.value.trim();
        const email = emailInput.value.trim();
        const password = passwordInput.value.trim();
        const confirmPassword = confirmPasswordInput.value.trim();
        if (!username || !email || !password || !confirmPassword) {
            alert("Please fill all fields");
            return;
        }
        if (password !== confirmPassword) {
            alert("Passwords do not match");
            return;
        }
        await signUp({
            username: username,
            email: email,
            password: password
        });
    });
}
document.addEventListener("DOMContentLoaded", initSignUp);
