const token = localStorage.getItem("token");

// Guard: redirect to login if no token
if (!token) {
    window.location.href = "/templates/Login.html";
}

const logOutBtn = document.getElementById("LogoutButton") as HTMLButtonElement | null;

logOutBtn?.addEventListener("click", () => {
    localStorage.removeItem("token");       // ← add this!
    localStorage.removeItem("username");    // ← and this
    window.location.href = "/templates/Login.html";
});


//  Task type (match your API response fields)
type Task = {
    id: number;
    inputTask: string;
    createdAt: string;
};

// Update your api() helper to attach the token
async function api<T>(url: string, options: RequestInit = {}): Promise<T> {
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

    list.innerHTML = "";

    for (const t of tasks) {
        const li = document.createElement("li");

        const span = document.createElement("span");
        span.className = "task-text";
        span.textContent = t.inputTask;

        const date = document.createElement("span");
        date.className = "task-date";
        date.textContent = new Date(t.createdAt).toLocaleDateString("en-US", {
            month: "short", day: "numeric", year: "numeric"
        });

        const actions = document.createElement("div");
        actions.className = "task-actions";

        const completeBtn = document.createElement("button");
        completeBtn.className = "task-btn complete-btn";
        completeBtn.textContent = "✓";
        completeBtn.title = "Mark complete";

        const deleteBtn = document.createElement("button");
        deleteBtn.className = "task-btn delete-btn";
        deleteBtn.textContent = "✕";
        deleteBtn.title = "Delete task";

        actions.appendChild(completeBtn);
        actions.appendChild(deleteBtn);

        li.appendChild(span);
        li.appendChild(date);
        li.appendChild(actions);
        list.appendChild(li);
    }
}

// Load + render
async function loadAndRender() {
    const tasks = await getTasks();

    renderTasks(tasks);
}


