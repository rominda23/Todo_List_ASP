"use strict";
const token = localStorage.getItem("token");
// Guard: redirect to login if no token
if (!token) {
    window.location.href = "/templates/Login.html";
}
const logOutBtn = document.getElementById("LogoutButton");
logOutBtn?.addEventListener("click", () => {
    localStorage.removeItem("token"); // ← add this!
    localStorage.removeItem("username"); // ← and this
    window.location.href = "/templates/Login.html";
});
// Update your api() helper to attach the token
async function api(url, options = {}) {
    const token = localStorage.getItem("token");
    const res = await fetch(url, {
        headers: {
            "Content-Type": "application/json",
            ...(token ? { "Authorization": `Bearer ${token}` } : {}),
            ...(options.headers ?? {})
        },
        ...options
    });
    if (res.status === 401) {
        // Token expired or invalid — kick back to login
        localStorage.removeItem("token");
        window.location.href = "/templates/Login.html";
        throw new Error("Unauthorized");
    }
    if (!res.ok)
        throw new Error(await res.text());
    return (await res.json());
}
//  Your endpoints (Controller file name)
const TASKS_URL = "/api/todo";
function initTodoPage() {
    const taskInput = document.getElementById("InputTask");
    const addBtn = document.getElementById("AddButton");
    if (!taskInput || !addBtn)
        return;
    loadAndRender().catch(console.error);
    //  Reusable add logic
    const submitTask = async () => {
        const text = taskInput.value.trim();
        if (!text)
            return;
        await createTask(text);
        taskInput.value = "";
        await loadAndRender();
    };
    //  Add button click
    addBtn.addEventListener("click", () => {
        submitTask().catch(console.error);
    });
    //  Enter key still works
    taskInput.addEventListener("keydown", (e) => {
        if (e.key !== "Enter")
            return;
        e.preventDefault();
        submitTask().catch(console.error);
    });
}
document.addEventListener("DOMContentLoaded", initTodoPage);
// GET
function getTasks() {
    return api(TASKS_URL);
}
// POST
function createTask(inputTask) {
    return api(TASKS_URL, {
        method: "POST",
        body: JSON.stringify({
            InputTask: inputTask
        })
    });
}
//  Render
function renderTasks(tasks) {
    const list = document.getElementById("taskUl");
    if (!list)
        return;
    list.innerHTML = ""; // clear
    for (const t of tasks) {
        const li = document.createElement("li");
        li.textContent = t.inputTask;
        list.appendChild(li);
    }
}
// Load + render
async function loadAndRender() {
    const tasks = await getTasks();
    renderTasks(tasks);
}
