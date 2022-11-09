var email = document.querySelector('#email');
var senha = document.querySelector('#senha');
var btnLogar = document.querySelector('#frm-btn-cadastrar');
var error = document.querySelector('#error');
error.innerHTML = ''
btnLogar.addEventListener('click',Cadastrar);

async function Cadastrar(event){
    event.preventDefault()
    event.preventDefault()
    var validaEmail = validarEmail(email.value)
    if(!email.value || !validaEmail){
        error.innerHTML = 'E-mail inválido!';
        return
    }
    if(!senha.value){
        alert("Informe sua senha!")
        return
    }

    var UsuarioModel = Usuario();

    const options = {
        method: 'POST',
        headers: { 'content-type': 'application/json' },
        body: JSON.stringify(UsuarioModel)
    };

    const req = fetch('https:/localhost:44347/Usuario/CreateUsuario', options)
        .then(response => {    
            if(response.status == 201){
                localStorage.setItem("EmailUsuario",email.value);
                window.location.href = 'principal.html';
                return response.json()
            }else if(response.status == 400){
                return response.json();
            }
        })
        .then(resp => {
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

function Usuario(){
    var Usuario = {
        Email : email.value ,
        Senha : senha.value ,
        Ativo : true
    }
    return Usuario
}

function validarEmail(email) {
    var result = /\S+@\S+\.\S+/;
    return result.test(email);
}