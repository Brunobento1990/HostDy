var email = document.querySelector('#email');
var senha = document.querySelector('#senha');
var btnLogar = document.querySelector('#frm-btn-cadastrar');
btnLogar.addEventListener('click',Cadastrar);

async function Cadastrar(event){
    event.preventDefault()
    event.preventDefault()
    if(!email.value){
        alert("Informe seu e-mail");
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
                window.location.href = 'principal.html';
                return response.json()
            }
            if(response.status == 404){
                alert('Ocorreu uma falha ao cadastrar o usuario, tente novamente mais tarde!')
            }
            
        })
        .catch(erro => {
            console.log(erro);
            return erro;
        });

    console.log(req)
}


function Usuario(){
    var Usuario = {
        Email : email.value ,
        Senha : senha.value ,
        Ativo : true
    }
    return Usuario
}