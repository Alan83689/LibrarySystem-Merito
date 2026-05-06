async function fetchJson(url) {
    const response = await fetch(url);
    if (!response.ok) throw new Error(`Błąd ${response.status}`);
    return await response.json();
}
function fillTable(tbodyId, rowsHtml, emptyMessage) {
    const tbody = document.getElementById(tbodyId);
    if (!rowsHtml.length) {
        tbody.innerHTML = `<tr><td colspan="10" class="empty">${emptyMessage}</td></tr>`;
        return;
    }
    tbody.innerHTML = rowsHtml.join('');
}
function formatDate(value) {
    if (!value) return '-';
    return new Date(value).toLocaleString('pl-PL');
}
async function loadBooks(search = '') {
    const data = await fetchJson(`/api/books?page=1&pageSize=10&search=${encodeURIComponent(search)}`);
    document.getElementById('booksCount').textContent = data.totalItems;
    fillTable('booksTable', data.items.map(x => `
        <tr><td>${x.id}</td><td>${x.tytul}</td><td>${x.autor}</td><td>${x.dostepneEgzemplarze}</td></tr>
    `), 'Brak książek do wyświetlenia.');
}
async function loadReaders() {
    const data = await fetchJson('/api/readers?page=1&pageSize=10');
    document.getElementById('readersCount').textContent = data.totalItems;
    fillTable('readersTable', data.items.map(x => `
        <tr><td>${x.id}</td><td>${x.imieNazwisko}</td><td>${x.numerKarty}</td></tr>
    `), 'Brak czytelników do wyświetlenia.');
}
async function loadLoans() {
    const data = await fetchJson('/api/loans?page=1&pageSize=10');
    document.getElementById('loansCount').textContent = data.totalItems;
    fillTable('loansTable', data.items.map(x => `
        <tr><td>${x.id}</td><td>${x.tytulKsiazki}</td><td>${x.czytelnik}</td><td>${formatDate(x.dataWypozyczenia)}</td><td><span class="status ${x.czyZwrocono ? 'returned' : 'active'}">${x.czyZwrocono ? 'Zwrócone' : 'Aktywne'}</span></td></tr>
    `), 'Brak wypożyczeń do wyświetlenia.');
}
async function init() { try { await Promise.all([loadBooks(), loadReaders(), loadLoans()]); } catch (error) { console.error(error); } }
document.getElementById('searchButton').addEventListener('click', () => loadBooks(document.getElementById('searchInput').value));
document.getElementById('resetButton').addEventListener('click', () => { document.getElementById('searchInput').value = ''; loadBooks(); });
document.getElementById('refreshBooks').addEventListener('click', () => loadBooks(document.getElementById('searchInput').value));
document.getElementById('refreshReaders').addEventListener('click', loadReaders);
document.getElementById('refreshLoans').addEventListener('click', loadLoans);
init();
