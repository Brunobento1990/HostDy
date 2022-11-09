var email = document.querySelector('#email');
var senha = document.querySelector('#senha');
var btnLogar = document.querySelector('#frm-btn-logar');
var error = document.querySelector('#error');
error.innerHTML = '';
localStorage.removeItem("EmailUsuario");

btnLogar.addEventListener('click',Logar);

async function Logar(event){
    event.preventDefault()
    btnLogar.innerHTML = 'Logando...';
    var validaEmail = validarEmail(email.value)
    if(!email.value || !validaEmail){
        alert("E-mail inválido!");
        btnLogar.innerHTML = 'Login';
        return
    }
    
    if(!senha.value){
        alert("Informe sua senha!")
        btnLogar.innerHTML = 'Login';
        return
    }

    const options = {
        method: 'GET',
        headers: { 'content-type': 'application/json' },
    };

    const req = fetch('https:/localhost:44347/Usuario/GetUsuario?email='+email.value+'&senha='+senha.value, options)
        .then(response => {  
            if (response.status == 200)  {
                localStorage.setItem("EmailUsuario",email.value);
                window.location.href = 'principal.html';
            }else{
                btnLogar.innerHTML = 'Login';
                return response.json()
            }
        })
        .then(resp =>{
            Error(resp)
        })
        .catch(erro => {
            console.log(erro);
            return erro;
        });
}
function Error(json){
    error.innerHTML = json.message;
}

function PreencherUsuraio(json){
    var Usuario = {
        IdUsuario : json.idUsuario,
        Email : json.email,
        Senha : json.senha, 
        Ativo : json.ativo
    }

    return Usuario
}

function validarEmail(email) {
    var result = /\S+@\S+\.\S+/;
    return result.test(email);
}

