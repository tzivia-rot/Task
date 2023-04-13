
var token=sessionStorage.getItem('token');
alert(token);
let users = [];
const uriUser = '/User';

function addUsers() {
    fetch(uriUser,{method:'GET', headers:{'Authorization': `Bearer ${token}`}})
        .then(response =>
            response.json())
        .then(data => _displayUser(data))
        .catch(error => console.error('Unable to get items.', error));
}
 
function deleteItem(id) {
    fetch(`${uriUser}/${id}`, {
            method: 'DELETE'
        })
        .then(() => getUser())
        .catch(error => console.error('Unable to delete item.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addPassword=document.getElementById('add-password');
    const addIsAdmin=document.getElementById('add-isAdmin');

    const item = {
        UserName: addNameTextbox.value.trim(),
        Password:addPassword.value.trim(),
        IsAdmin:addIsAdmin.value.trim()
    };

    fetch(uriUser, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
            addUsers();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}
function _displayUser(data) {
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    // _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isAdminCheckbox = document.createElement('input');
        isAdminCheckbox.type = 'checkbox';
        isAdminCheckbox.disabled = true;
        isAdminCheckbox.checked = item.IsAdmin;

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick',`deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isAdminCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.UserName);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        let textNode2 = document.createTextNode(item.Password);
        td3.appendChild(textNode2);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    users = data;
}
