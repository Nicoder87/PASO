function BuscarMesa(control, evento) {

    //var id = control.getAttribute('id');
    var mesa = control.value;
    var tecla = (document.all) ? evento.keyCode : evento.which;
    if (tecla == 13) {
        if (mesa != '') {
            CargoDatosMesa(mesa);
            $("#Votos_emitidos").focus();
            $("#Votos_emitidos").select();
        }
    }
}

function Pasar(control, evento) {

    var id = control.getAttribute('id');

    var tecla = (document.all) ? evento.keyCode : evento.which;

    if (tecla == 13 || tecla == 109) { // tecla Enter o Menos

        if (id == "Votos_emitidos") {

            if (tecla == 109) { // si es la tecla Menos
                $("#Mesa").focus();
                $("#Mesa").select();
            }
            else {
                //$('#PostulacionesDiv').find('.tag1').each(function () {
                $('.tag1').each(function () {
                    var control = $(this);
                    control.focus();
                    control.select();
                });
            }
        }

        else {
            var clase = control.getAttribute('class');
            var nt = 0;

            var a = clase.substr(4, 0);

            if (a == " ") { // si no tiene 2 decenas
                nt = parseInt(clase.substr(3, 1));
            }
            else {
                nt = parseInt(clase.substr(3, 2));
            }

            if (tecla == 13) // si es la tecla Enter
            {
                nt = nt + 1;
            } else
            {
                nt = nt - 1;
            }

            var tag = '.tag' + nt;

            $(tag).each(function () {

                var control = $(this);
                control.focus();
                control.select();
            });

        }

    }
}

function SoloNumeros(e) {

    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) { // Tecla Retroceso
        return true;
    }

    patron = /[0-9]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
}

function CalcularValidos() {

    var car1 = 0;
    var car2 = 0;
    var car3 = 0;
    var car4 = 0;
    var car5 = 0;

    $('.CAR1').each(function () {
        var a = $(this).val();
        if (a != "") { car1 = car1 + parseInt(a); }
    });

    $('.CAR2').each(function () {
        var a = $(this).val();
        if (a != "") { car2 = car2 + parseInt(a); }
    });

    $('.CAR3').each(function () {
        var a = $(this).val();
        if (a != "") { car3 = car3 + parseInt(a); }
    });

    $('.CAR4').each(function () {
        var a = $(this).val();
        if (a != "") { car4 = car4 + parseInt(a); }
    });

    $('.CAR5').each(function () {
        var a = $(this).val();
        if (a != "") { car5 = car5 + parseInt(a); }
    });

    $("#CARGO0001").val(car1);
    $("#CARGO0002").val(car2);
    $("#CARGO0003").val(car3);
    $("#CARGO0004").val(car4);
    $("#CARGO0005").val(car5);

    ///////////////////////////////////////////////

    var tot1 = 0;
    var tot2 = 0;
    var tot3 = 0;
    var tot4 = 0;
    var tot5 = 0;

    $('.TOT1').each(function () {
        var a = $(this).val();
        if (a != "") { tot1 = tot1 + parseInt(a); }
    });

    $('.TOT2').each(function () {
        var a = $(this).val();
        if (a != "") { tot2 = tot2 + parseInt(a); }
    });

    $('.TOT3').each(function () {
        var a = $(this).val();
        if (a != "") { tot3 = tot3 + parseInt(a); }
    });

    $('.TOT4').each(function () {
        var a = $(this).val();
        if (a != "") { tot4 = tot4 + parseInt(a); }
    });

    $('.TOT5').each(function () {
        var a = $(this).val();
        if (a != "") { tot5 = tot5 + parseInt(a); }
    });

    $("#TOTAL0001").val(tot1);
    $("#TOTAL0002").val(tot2);
    $("#TOTAL0003").val(tot3);
    $("#TOTAL0004").val(tot4);
    $("#TOTAL0005").val(tot5);

}

function cartelitoOk(msg) {

    alertify.set({ delay: 5000 });
    alertify.success(msg);
    return false;
}

function cartelitoError(msg) {

    alertify.set({ delay: 5000 });
    alertify.error(msg);
    return false;
}

function cartelitoInfo(msg) {
    alertify.alert(msg, function () {
        //alertify.message('OK');
    });
}
