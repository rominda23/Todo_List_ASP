type SignUpRequest = {
    username: string;
    email: string;
    password: string;
};

const SIGNUP_URL = "/api/user/signup";

async function signUp(data: SignUpRequest) {

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

    const form = document.getElementById("SignUpForm") as HTMLFormElement | null;

    const usernameInput = document.getElementById("InputUsername") as HTMLInputElement | null;
    const emailInput = document.getElementById("InputEmail") as HTMLInputElement | null;
    const passwordInput = document.getElementById("InputPassword") as HTMLInputElement | null;
    const confirmPasswordInput = document.getElementById("InputCMPassword") as HTMLInputElement | null;

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