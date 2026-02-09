const logOutBtn = document.getElementById("LogoutButton") as HTMLButtonElement | null;

logOutBtn?.addEventListener("click", () => {
    window.location.href = "/templates/Login.html";
});


//  Task type (match your API response fields)
type Task = {
    id: number;
    inputTask: string;
};

async function api<T>(url: string, options: RequestInit = {}): Promise<T> {
    const res = await fetch(url, {
        headers: { "Content-Type": "application/json", ...(options.headers ?? {}) },
        ...options
    });

    if (!res.ok) throw new Error(await res.text());
    return (await res.json()) as T;
}

//  Your endpoints (Controller file name)
const TASKS_URL = "/api/todo";

function initTodoPage() {
    const taskInput = document.getElementById("InputTask") as HTMLInputElement | null;
    const addBtn = document.getElementById("AddButton") as HTMLButtonElement | null;

    if (!taskInput || !addBtn) return;

    loadAndRender().catch(console.error);

    //  Reusable add logic
    const submitTask = async () => {
        const text = taskInput.value.trim();
        if (!text) return;

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
        if (e.key !== "Enter") return;
        e.preventDefault();
        submitTask().catch(console.error);
    });
}


document.addEventListener("DOMContentLoaded", initTodoPage);

// GET
function getTasks() {
    return api<Task[]>(TASKS_URL);
}

// POST
function createTask(inputTask: string) {
    return api<Task>(TASKS_URL, {
        method: "POST",
        body: JSON.stringify({
            InputTask: inputTask

        })
    });
}

//  Render
function renderTasks(tasks: Task[]) {
    const list = document.getElementById("taskUl") as HTMLUListElement | null;
    if (!list) return;

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


