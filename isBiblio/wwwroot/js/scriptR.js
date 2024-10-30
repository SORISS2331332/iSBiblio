const container = document.getElementById('contain');
const loginButton = document.getElementById('login');
const signUpButton = document.getElementById('signUp');

signUpButton.addEventListener('click', () => {
	container.classList.add('panel-active');
})

loginButton.addEventListener('click', () => {
	container.classList.remove('panel-active');
})