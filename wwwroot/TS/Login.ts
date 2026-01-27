const signUpBtn = document.getElementById("SignUpButton") as HTMLButtonElement | null;

signUpBtn?.addEventListener("click", () => {
    window.location.href = "/templates/SignUp.html";
});


const LoginBtn = document.getElementById("LoginButton") as HTMLButtonElement | null;

LoginBtn?.addEventListener("click", () => {
    window.location.href = "/templates/ToDoList.html";
});
