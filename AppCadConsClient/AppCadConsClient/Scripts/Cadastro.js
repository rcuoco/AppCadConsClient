$(document).ready(function () {
    // Aplica a máscara de CPF usando o plugin jQuery Mask
    $('.cpf').mask('000.000.000-00');
    $('.nascimento').mask('00/00/0000');
    $('.cep').mask('00000-000');

    $('.validarCPF').c(function () {
        const cpf = $('#cpf').val();

    });

});


function bloquearBotao(idbotao) {
    // Bloqueia o botão
    var idCompleto = idbotao.split('|');

    for (var i = 1; i < idCompleto.length; i++) {
        // Bloqueia o botão
        var bt = document.getElementById('Content_' + idCompleto[i]);

        if (bt) {
            bt.disabled = true;
        } else {
            console.error('Elemento de botão não encontrado com o ID: ' + idCompleto);
        }
    }
    var clicado = 'ctl00$Content$' + idbotao.split('|')[0];
    __doPostBack(clicado, '');
}
