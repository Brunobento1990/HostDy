var table = document.querySelector('#table-principal');
const options = {
    method: 'GET',
    headers: { 'content-type': 'application/json' },
};

const req = fetch('https://localhost:44347/DadosIBGE/GetDadosIBGE', options)
    .then(response => {   
        if (response.status == 200){
            return response.json()
        }
        if(response.status == 404){
            alert("Não há dados a serem apresentados!")
            return 
        }
    })
    .then(resp => {
        InserirTabela(resp)
    })
    .catch(erro => {
        console.log(erro);
        return erro;
    });

function InserirTabela(data){
    data.forEach(function (d) {

        var linha = document.createElement('tr');
        var tdUf = document.createElement('td');
        var tdReg = document.createElement('td');
        var tdCid = document.createElement('td');
        var tdNomeReg = document.createElement('td');
        var tdNomeForm = document.createElement('td');

        tdUf.innerHTML = d.siglaEstado;
        tdReg.innerHTML = d.regiaoNome;
        tdCid.innerHTML = d.nomeCidade;
        tdNomeReg.innerHTML = d.nomeMesorregiao;
        tdNomeForm.innerHTML = d.nomeFormatado;

        linha.appendChild(tdUf);
        linha.appendChild(tdReg);
        linha.appendChild(tdCid);
        linha.appendChild(tdNomeReg);
        linha.appendChild(tdNomeForm);

        table.appendChild(linha);
    })  
}

