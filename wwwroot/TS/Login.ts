const signUpBtn = document.getElementById("SignUpButton") as HTMLButtonElement | null;

signUpBtn?.addEventListener("click", () => {
    window.location.href = "/templates/SignUp.html";
});


const LoginBtn = document.getElementById("LoginButton") as HTMLButtonElement | null;

LoginBtn?.addEventListener("click", async () => {
    const username = (document.getElementById("InputUsername") as HTMLInputElement).value.trim();
    const password = (document.getElementById("InputPassword") as HTMLInputElement).value.trim();

    if (!username || !password) {
        alert("Please fill all fields");
        return;
    }

    const res = await fetch("/api/user/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password })
    });

    if (!res.ok) {
        alert("Invalid username or password");
        return;
    }

    const data = await res.json();

    localStorage.setItem("token", data.token);
    localStorage.setItem("username", data.username);

    window.location.href = "/templates/ToDoList.html";
});
