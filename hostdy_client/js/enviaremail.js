var btn = document.querySelector('#enviar-email');

btn.addEventListener('click',EnviarEmail);

async function EnviarEmail(event){
    event.preventDefault()

    const options = {
        method: 'GET',
        headers: { 'content-type': 'application/json' },
    };

    const req = fetch('https:/localhost:44347/Email/EnviarEmail', options)
        .then(response => {  
            if (response.status == 200)  {
                alert('E-mail enviado com sucesso!');
            }else{
                alert('Falha ao enviar o e-mail, tente novamente mais tarde!')
            }
        })
        .catch(erro => {
            console.log(erro);
            return erro;
        });
}