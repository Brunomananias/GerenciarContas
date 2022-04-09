$(document).ready(function(){
  $('#valorConta').mask('000.000.000.000.000,00', {reverse: true});
})


/*Tabela de contas */

$.ajax({
    method: "GET",
    url: "https://localhost:44331/v1/Contas",
    dataType: "json"
  }).done(function (Contas) {

    var linhas = "";

    $(Contas).each(function (index, conta) {
      let id = conta.id
      let nomeConta = conta.nomeConta;
      let valorConta = conta.valorConta;

      const trs = `<tr>

        <td> ${nomeConta} </td>
        <td>R$ ${valorConta.toFixed(2)}</td>
        <td>
          <button 
            type='button' 
            class='button'
            onClick='deletarConta(${id})'>Deletar
          </button></td>
          
        </tr>`;
      

      linhas += trs

    });
    $("#tabela").html(linhas);
    $(document).ready(function () {
      $('#tabelaconta').DataTable();
    });

    


  }).fail(function (erro1, erro2, erro3) {
    console.log(erro1);
    console.log(erro2);
    console.log(erro3);
  });


  /*Somar Valores */

 
const minhaUrl = "https://localhost:44331/v2/Contas"
fetch(minhaUrl).then(function(response) {
  return response.json();
}).then(function(data) {
  
  console.log(data[0].valorTotal);
  let divValorTotal = document.getElementById('valorTotal');
  divValorTotal.innerHTML = "Total: " + data[0].valorTotal + "R$"

}).catch(function() {
  console.log("Houve algum problema!");
});



/* Inserir clientes */

  function fazPost(url, body){
    console.log("body= ", body)
    let request = new XMLHttpRequest()
    request.open("POST", url, true)
    request.setRequestHeader("content-type", "application/json")
    request.send(JSON.stringify(body))
 
    request.onload = function() {
         console.log(this.responseText) 
         location.reload()       
    }
 
    return request.responseText
 }
 
  function CadastrarConta(){
       event.preventDefault()
       let url= "https://localhost:44331/v1/Contas"
       let nomeConta = document.getElementById("nomeConta").value
       let valorConta = document.getElementById("valorConta").value
       
       console.log(nomeConta)
       console.log(valorConta)
       valorConta = valorConta.replace(',','.');

       body = {
         "nomeConta": nomeConta,
         "valorConta": valorConta,
         
       }
 
       fazPost(url, body)
       window.alert("Conta inserida!")
     }


      //Deletar clientes da lista//
    

    function fazDelete(url, body){
      console.log("body= ", body)
      let request = new XMLHttpRequest()
      request.open("DELETE", url, true)
      request.setRequestHeader("content-type", "application/json")
      request.send(JSON.stringify(body))
   
      request.onload = function() {
           console.log(this.responseText) 
           location.reload()     
      }
   
      return request.responseText
      
   }
   
    function deletarConta(id){
      
         event.preventDefault()
         let url= "https://localhost:44331/v1/Contas"
         
         
        console.log(nomeConta)
        body = {
           "id": id,
           
          }
   
         fazDelete(url, body)
         window.alert("Conta deletada com sucesso!")
       }

       

      
  
  