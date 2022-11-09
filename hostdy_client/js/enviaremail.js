var btn = document.querySelector('#enviar-email');

btn.addEventListener('click',EnviarEmail);

async function EnviarEmail(event){
    event.preventDefault()
    var enviandoEmail= document.getElementById('enviandoEmail');
    enviandoEmail.innerHTML = 'Enviando E-mail...';
    const options = {
        method: 'GET',
        headers: { 'content-type': 'application/json' },
    };
    if(!localStorage.getItem("EmailUsuario")){
        alert("É necessário logar novamente!")
        return
    }else{
        var email = localStorage.getItem("EmailUsuario");
    }
    const req = fetch('https:/localhost:44347/Email/EnviarEmail?email='+email, options)
        .then(response => {  
            if (response.status == 200)  {
                alert('E-mail enviado com sucesso!');
                enviandoEmail.innerHTML = '';
            }else{
                alert('Falha ao enviar o e-mail, tente novamente mais tarde!')
                enviandoEmail.innerHTML = '';
            }
        })
        .catch(erro => {
            console.log(erro);
            return erro;
        });
        
        
}
