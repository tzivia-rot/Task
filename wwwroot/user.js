const uriUserLogin = '/User/Login';
var  token="";
let users = [];
const uriUser = '/User';
function toSend(){
    const username=document.getElementById('UserName');
    const password=document.getElementById('password');
    login(username.value,password.value);
}
function login(username,password){
        const user = {     
                "userId": 0,
                "userName": username,
                "password": password,
                "isAdmin": true  
        };
        fetch(uriUserLogin, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(user)
            })
            .then(response =>
                response.text())
            .then((text) => {
                token= text;
                sessionStorage.setItem('token', "Bearer "+text)
                alert(text)
               location.href ="admin.html"                    
            })
            .catch(error => console.error('Unable to add user.', error));
}
function getUser() {
    fetch(uriUser)
        .then(response => response.json())
        .then(data => _displayUser(data))
        .catch(error => console.error('Unable to get items.', error));
}
function _displayUser(data) {
    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';

    // _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isDoneCheckbox = document.createElement('input');
        isDoneCheckbox.type = 'checkbox';
        isDoneCheckbox.disabled = true;
        isDoneCheckbox.checked = item.done;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isDoneCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    tasks = data;
}
