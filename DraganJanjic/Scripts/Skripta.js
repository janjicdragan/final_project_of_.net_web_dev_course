$(document).ready(function () {
    var host = "https://" + window.location.host;
    var zaposleniEndpoint = "/api/zaposleni/";
    var token;
    var headers = {};

    loadZaposleni();


    $("body").on("click", "#btnDelete", deleteZaposleni);


    function loadZaposleni() {
        var requestUrl = host + zaposleniEndpoint;
        $.getJSON(requestUrl, setZaposleni);
    }

    function setZaposleni(data, status) {
        var container = $("#data");
        container.empty();

        if (status === "success") {
            var div = $("<div class='text-center'></div>");
            var h1 = $("<h2>Zaposleni</h2><br />");
            div.append(h1);

            var table = $("<table border='1' style='margin: auto;width: 80%'></table>");
            var header;
            if (token) {
                header = $("<tr style='background-color: yellow;height: 40px'><td>Ime i prezime</td><td>Rola</td><td>Godina zaposlenja</td><td>Godina rodjenja</td><td>Jedinica</td><td>Plata</td><td>Akcija</td></tr>");
            } else {
                header = $("<tr style='background-color: yellow;height: 40px'><td>Ime i prezime</td><td>Rola</td><td>Godina zaposlenja</td><td>Jedinica</td></tr>");
            }
            table.append(header);

            for (i = 0; i < data.length; i++) {
                var row = "<tr style='height: 40px'>";
                var stringId = data[i].Id.toString();
                var displayDataNotSigned = "<td>" + data[i].ImeIPrezime + "</td><td>" + data[i].Rola + "</td><td>" + data[i].GodinaZaposlenja + "</td><td>" + data[i].Jedinica.Ime + "</td>";
                var displayDataSigned = "<td>" + data[i].ImeIPrezime + "</td><td>" + data[i].Rola + "</td><td>" + data[i].GodinaZaposlenja + "</td><td>" + data[i].GodinaRodjenja + "</td><td>" + data[i].Jedinica.Ime + "</td><td>" + data[i].Plata + "</td>";
                var displayDelete = "<td><button id=btnDelete name=" + stringId + ">Obrisi</button></td>";

                if (token) {
                    row += displayDataSigned + displayDelete + "</tr>";
                } else {
                    row += displayDataNotSigned + "</tr>";
                }

                table.append(row);
            }


            div.append(table);
            container.append(div);
        } else {
            var div = $("<div class='text-center'></div>");
            var h1 = $("<h1>Desila se greska prilikom ucitavanja zaposlenih</h1>");
            div.append(h1);
            container.append(div);
        }
    }


    //pozvati u uspesnoj prijavi
    function loadDropDown() {
        var requestUrl = host + "/api/jedinice";

        $.getJSON(requestUrl, setDropDown);
    }

    function setDropDown(data, status) {
        if (status === "success") {
            var select = $("#addJedinica");
            select.empty();

            for (i = 0; i < data.length; i++) {
                var option = "<option value=" + data[i].Id + ">" + data[i].Ime + "</option>";
                select.append(option);
            }
        } else {
            alert("Desila se greska prilikom ucitavanja dropdown menija!");
        }
    }


    $("#btnShowRegDiv").click(function () {
        $("#registracijaDiv").css("display", "block");
        $("#prijavaDiv").css("display", "none");
        $("#priRegBtns").css("display", "none");
    })

    $("#btnRegOdustani").click(function (e) {
        e.preventDefault();
        $("#registracijaDiv").css("display", "none");
        $("#priRegBtns").css("display", "block");
    })

    $("#btnPrijavaOdustani").click(function (e) {
        e.preventDefault();
        $("#prijavaDiv").css("display", "none");
        $("#priRegBtns").css("display", "block");
    })

    $("#btnShowPrijavaDiv").click(function () {
        $("#registracijaDiv").css("display", "none");
        $("#prijavaDiv").css("display", "block");
        $("#priRegBtns").css("display", "none");
    })

    $("#btnPrijaviSe").click(function (e) {
        e.preventDefault();

        var korIme = $("#korImePrijava").val();
        var loz = $("#lozPrijava").val();

        $.ajax({
            url: host + "/Token",
            type: "POST",
            data: {
                "grant_type": "password",
                "username": korIme,
                "password": loz
            }
        }).done(function (data, status) {
            $("#korImePrijava").val("");
            $("#lozPrijava").val("");
            $("#priRegBtns").css("display", "none");
            $("#prijavaDiv").css("display", "none");
            $("#prijavljen").css("display", "block");
            $("#addFormDiv").css("display", "block");
            $("#info").empty().append("Prijavljeni korisnik: <b>" + data.userName + "</b>");
            $("#pretragaDiv").css("display", "block");
            loadDropDown();
            token = data.access_token;
            refreshTable();
        }).fail(function (data, status) {
            alert("Desila se greska prilikom prijavljivanja!");
            $("#lozPrijava").val("");
        })
    });


    $("#btnOdjaviSe").click(function () {
        $("#prijavljen").css("display", "none");
        $("#info").empty();
        $("#addFormDiv").css("display", "none");
        $("#priRegBtns").css("display", "block");
        $("#pretragaDiv").css("display", "none");
        token = null;
        refreshTable();
    });


    $("#btnRegistrujSe").click(function (e) {
        e.preventDefault();

        var korIme = $("#korImeReg").val();
        var loz = $("#lozReg").val();

        $.ajax({
            url: host + "/api/Account/Register",
            type: "POST",
            data: {
                "Email": korIme,
                "Password": loz,
                "ConfirmPassword": loz
            }
        }).done(function (data, status) {
            $("#korImeReg").val("");
            $("#lozReg").val("");
            $("#registracijaDiv").css("display", "none");
            $("#prijavaDiv").css("display", "block");
            $("#priRegBtns").css("display", "none");
        }).fail(function (data, status) {
            alert("Desila se greska prilikom registracije!");
        })
    });

    
    function deleteZaposleni() {
        var deleteId = this.name;
        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        $.ajax({
            url: host + "/api/zaposleni/" + deleteId.toString(),
            type: "DELETE",
            headers: headers
        }).done(function (data, status) {
            refreshTable();
        }).fail(function (data, status) {
            alert("Desila se greska prilikom brisanja!");
        })
    }


    $("#btnDodaj").click(function (e) {
        e.preventDefault();

        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        var sendData = {
            "JedinicaId": $("#addJedinica").val(),
            "Rola": $("#addRola").val(),
            "ImeIPrezime": $("#addImeIPrezime").val(),
            "GodinaRodjenja": $("#addGodinaRodjenja").val(),
            "GodinaZaposlenja": $("#addGodinaZaposlenja").val(),
            "Plata": $("#addPlata").val()
        }

        $.ajax({
            url: host + zaposleniEndpoint,
            type: "POST",
            headers: headers,
            data: sendData
        }).done(function (data, status) {
            refreshTable();
        }).fail(function (data, status) {
            alert("Desila se greska prilikom dodavanja zaposlenog!");
        })
    })

    $("#btnAddOdustani").click(function (e) {
        e.preventDefault();
        refreshTable();
    });

    
    $("#pretragaForm").submit(function (e) {
        e.preventDefault();

        if (token) {
            headers.Authorization = "Bearer " + token;
        }

        var pretOd = $("#pretragaOd").val();
        var pretDo = $("#pretragaDo").val();


        $.ajax({
            url: host + "/api/pretraga",
            type: "POST",
            data: {
                "Najmanje": pretOd,
                "Najvise": pretDo
            },
            headers: headers
        })
            .done(setZaposleni)
            .fail(function (data, status) {
                alert("Desila se greska prilikom pretrage!");
            })
    })

    function refreshTable() {
        $("#addJedinica").val("");
        $("#addRola").val("");
        $("#addImeIPrezime").val("");
        $("#addGodinaRodjenja").val("");
        $("#addGodinaZaposlenja").val("");
        $("#addPlata").val("");
        loadZaposleni();
    }
});