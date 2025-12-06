//  signup form validation 

function validateSignupForm(event) {
    event.preventDefault();

    const nameInput = document.querySelector("#fullName");
    const emailInput = document.querySelector("#signupEmail");
    const passInput = document.querySelector("#signupPassword");
    const confirmInput = document.querySelector("#confirmPassword");
    const msg = document.querySelector("#signupMsg");

    [nameInput, emailInput, passInput, confirmInput].forEach(input => {
        input.classList.remove("error-input");
    });
    msg.textContent = "";
    msg.className = "form-message";

    let hasError = false;

    if (nameInput.value.trim().length < 3) {
        nameInput.classList.add("error-input");
        hasError = true;
    }

    const emailValue = emailInput.value.trim();
    const emailPattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;
    if (!emailPattern.test(emailValue)) {
        emailInput.classList.add("error-input");
        hasError = true;
    }

    if (passInput.value.length < 8) {
        passInput.classList.add("error-input");
        hasError = true;
    }

    if (passInput.value !== confirmInput.value) {
        confirmInput.classList.add("error-input");
        hasError = true;
    }

    if (hasError) {
        msg.textContent = "Please fix the highlighted fields.";
        msg.classList.add("error");
        return;
    }

    msg.textContent = "Sign up successful!";
    msg.classList.add("success");
}

//  Login form validation 
function validateLoginForm(event) {
    event.preventDefault();

    const emailInput = document.querySelector("#loginEmail");
    const passInput = document.querySelector("#loginPassword");
    const msg = document.querySelector("#loginMsg");

    [emailInput, passInput].forEach(input => {
        input.classList.remove("error-input");
    });
    msg.textContent = "";
    msg.className = "form-message";

    let hasError = false;

    const emailValue = emailInput.value.trim();
    const emailPattern = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;
    if (!emailPattern.test(emailValue)) {
        emailInput.classList.add("error-input");
        hasError = true;
    }

    if (passInput.value.length < 8) {
        passInput.classList.add("error-input");
        hasError = true;
    }

    if (hasError) {
        msg.textContent = "Incorrect email or password format.";
        msg.classList.add("error");
        return;
    }

    msg.textContent = "Login successful!";
    msg.classList.add("success");
}
function setupAuthForms() {
    const signupForm = document.querySelector("#signupForm");
    const loginForm = document.querySelector("#loginForm");

    if (signupForm) {
        signupForm.addEventListener("submit", validateSignupForm);
    }
    if (loginForm) {
        loginForm.addEventListener("submit", validateLoginForm);
    }
}
document.addEventListener("DOMContentLoaded", () => {
    if (typeof setupToolsPage === "function") {
        setupToolsPage();
    }
    setupAuthForms();
});
function changeSize() {
    document.getElementById("Siz").height = "300";
    document.getElementById("Siz").width = "300";
}
function OriginalSize() {
    document.getElementById("Siz").height = "100";
    document.getElementById("Siz").width = "100";

}
