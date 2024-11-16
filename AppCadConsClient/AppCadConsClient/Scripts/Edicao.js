$(document).ready(function () {
    // Aplica a máscara de CPF usando o plugin jQuery Mask
    $('.cpf').mask('000.000.000-00');
    $('.nascimento').mask('00/00/0000');
    $('.cep').mask('00000-000');
    $('.estado').select2({
        placeholder: 'Selecione um Estado',
        allowClear: true
    });
    $('.cidade').select2({
        placeholder: 'Selecione uma Cidade',
        allowClear: true
    });
    // Inicializa o Datepicker para o campo com a classe 'nascimento'
    $(".nascimento").datepicker({
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: '<',
        prevText: '>',
        dateFormat: "dd/mm/yy",       // Formato de data (ex.: 15/11/2024)
        changeMonth: true,            // Permite selecionar o mês
        changeYear: true,             // Permite selecionar o ano
        yearRange: "1900:2100",       // Define o intervalo de anos
        showAnim: "slideDown",        // Animação ao abrir o Datepicker
        minDate: new Date(1900, 0, 1), // Data mínima permitida
        maxDate: new Date(2100, 11, 31) // Data máxima permitida
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
