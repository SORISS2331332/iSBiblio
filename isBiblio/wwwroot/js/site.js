// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.querySelector('.navbar-toggler').addEventListener('click', function () {
    this.classList.toggle('collapsed'); // Ajoute ou enlève la classe 'collapsed'
});


// Obtenez l'URL de la page actuelle
const currentLocation = window.location.pathname;

// Sélectionnez tous les liens de la navbar
const navLinks = document.querySelectorAll('.navbar-nav .nav-link');

// Parcourez les liens et ajoutez la classe 'active' au lien correspondant
navLinks.forEach(link => {
    if (link.getAttribute('href') === currentLocation) {
        link.classList.add('active');
    }
});


$(document).ready(function () {
    $('#togglePassword').on('click', function () {
        const passwordField = $('#motdepasse');
        const type = passwordField.attr('type') === 'password' ? 'text' : 'password';
        passwordField.attr('type', type);
        $(this).find('i').toggleClass('fa-eye fa-eye-slash');
    });
});