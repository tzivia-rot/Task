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
                "isAdmin": false  
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
                text=text.replace(/"/g,'')
                sessionStorage.setItem('token',text)
                alert(text);
               location.href ="user.html"                    
            })
            .catch(error => console.error('Unable to add user.', error));
}
