(function () {
    const shell = document.getElementById("shell");
    const btn = document.getElementById("btnSidebarToggle");

    if (!shell || !btn) return;

    btn.addEventListener("click", () => {
        const isOpen = shell.classList.toggle("sidebar-open");
        btn.setAttribute("aria-expanded", isOpen ? "true" : "false");
    });

    // collapse groups داخل السايد بار
    document.querySelectorAll(".nav-group").forEach(btnGroup => {
        btnGroup.addEventListener("click", () => {
            const key = btnGroup.getAttribute("data-group");
            const subtree = document.querySelector(`.nav-subtree[data-subtree="${key}"]`);
            if (!subtree) return;

            const isOpen = subtree.classList.contains("is-open");
            subtree.classList.toggle("is-open", !isOpen);
            btnGroup.setAttribute("aria-expanded", (!isOpen).toString());
        });
    });
})();
