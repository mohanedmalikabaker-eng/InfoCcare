(function () {
    // فتح/قفل الجروبات
    document.querySelectorAll(".nav-group").forEach(btn => {
        btn.addEventListener("click", () => {
            const key = btn.getAttribute("data-group");
            const subtree = document.querySelector(`.nav-subtree[data-subtree="${key}"]`);
            if (!subtree) return;

            const isOpen = subtree.classList.contains("is-open");
            subtree.classList.toggle("is-open", !isOpen);
            btn.setAttribute("aria-expanded", (!isOpen).toString());
        });
    });

    // يقفل الـ offcanvas لما تضغط على أي رابط داخل السايدبار (اختياري)
    const offcanvasEl = document.getElementById("sidebarOffcanvas");
    if (!offcanvasEl) return;

    offcanvasEl.addEventListener("click", (e) => {
        const a = e.target.closest("a");
        if (!a) return;

        const inst = bootstrap.Offcanvas.getInstance(offcanvasEl);
        if (inst) inst.hide();
    });
})();
