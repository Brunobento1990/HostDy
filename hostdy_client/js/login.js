var email = document.querySelector('#email');
var senha = document.querySelector('#senha');
var btnLogar = document.querySelector('#frm-btn-logar');
btnLogar.addEventListener('click',Logar);

async function Logar(event){
    event.preventDefault()
    if(!email.value){
        alert("Informe seu e-mail");
        return
    }
    if(!senha.value){
        alert("Informe sua senha!")
        return
    }

    const options = {
        method: 'GET',
        headers: { 'content-type': 'application/json' },
    };

    const req = fetch('https:/localhost:44347/Usuario/GetUsuario?email='+email.value+'&senha='+senha.value, options)
        .then(response => {  
            if (response.status == 200)  {
                window.location.href = 'principal.html';
            }else if(response.status == 404){
                alert('Usuário ou senha incorreta!');
            }else{
                alert('Parâmetros inválidos');
            }
        })
        .catch(erro => {
            console.log(erro);
            return erro;
        });
}