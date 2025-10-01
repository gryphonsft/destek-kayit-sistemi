
const modal = document.getElementById('crud-modal');
const openBtn = document.getElementById('open-crud-modal');
const closeBtn = modal.querySelector('.modal-close');
const backdrop = modal.querySelector('.modal-backdrop');

openBtn.addEventListener('click', () => {
    modal.style.display = 'flex';
});

closeBtn.addEventListener('click', () => {
    modal.style.display = 'none';
});

backdrop.addEventListener('click', () => {
    modal.style.display = 'none';
});