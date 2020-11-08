

//sepete ekleme
$('.add-to-cart').click(function () {

    var productID = $(this).data("productid")

    $.getJSON("/Home/AddToChart/" + productID, function (data) {
        if (data.Quantity == 1) {
            $("table tbody").append('<tr id="Chart_tr-' + data.ID + '">'
                + ' <td class= "cart_product">'
                + ' <img style="width:110px; height:110px" src="' + data.ProductImgURL + '" />'
                + ' </td>'
                + ' <td class="cart_description">'
                + ' <h4>' + data.ProductName + '</h4>'
                + ' </td>'
                + ' <td class="cart_price">'
                + ' <p id="TotalPrc-' + data.ID + '" >' + data.ProductPrice + '  </p>'
                + ' </td>'
                + ' <td class="cart_quantity">'
                + ' <div class="cart_quantity_button">'
                + ' <button data-chartid="' + data.ID + '" id="btn_u-' + data.ID + '" class="cart_quantity_up">+</button>'
                + ' <input readonly id="inpt-' + data.ID + '" class="cart_quantity_input" value=' + data.Quantity + ' type="text" name="quantity" style="width:28px">'
                + ' <button data-chartid="' + data.ID + '" id="btn_d-' + data.ID + '" class="cart_quantity_down" >-</button>'
                + ' </div>'
                + ' </td>'
                + ' <td class="cart_delete">'
                + ' <a data-chartid="' + data.ID + '" id="Trsh-' + data.ID + '" class="cart_quantity_delete" href="javascript: void (0)">'
                + ' <i class="fas fa-trash - alt"></i></a></td></tr>')

            //sepetteki ürünün miktarı tekrar arttırıldığında azaltma butonu aktif ediliyor
            if (data.Quantity == 1) {
                var element = "btn_d-" + data.ID
                document.getElementById(element).disabled = true;
                document.getElementById(element).style.background = "#ffffff";
            }

            //toplam ücreti yazdırmak için kullandığım alan
            let txt = 0
            $("td.cart_price").each(function () {
                txt += parseFloat($(this).text());
            });


            $('#Chart_prc').text('TOTAL PRICE: ' + txt + '  TL')
        }
        else {

            $('#inpt-' + data.ID).val('' + data.Quantity)

            $('#TotalPrc-' + data.ID).html('' + data.TotalPrice)

            //sepetteki ürünün miktarı tekrar arttırıldığında azaltma butonu aktif ediliyor
            if (data.Quantity != 1) {
                var element = "btn_d-" + data.ID
                document.getElementById(element).disabled = false;
                document.getElementById(element).style.background = "#FE980F";
            }

            //toplam ücreti yazdırmak için kullandığım alan
            let txt = 0
            $("td.cart_price").each(function () {
                txt += parseFloat($(this).text());
            });

            $('#Chart_prc').text('TOTAL PRICE: ' + txt + '  TL')

        }
    })

});

//miktar arttırma
$('table tbody').on('click', '.cart_quantity_up', function () {
    var chartID = $(this).data("chartid")

    $.getJSON("/Home/Increase/" + chartID, function (data) {
        $('#inpt-' + data.ID).val('' + data.Quantity)
        $('#TotalPrc-' + data.ID).html('' + data.TotalPrice)

        //sepetteki ürünün miktarı tekrar arttırıldığında azaltma butonu aktif ediliyor
        if (data.Quantity != 1) {
            var element = "btn_d-" + data.ID
            document.getElementById(element).disabled = false;
            document.getElementById(element).style.background = "#FE980F";
        }

        //toplam ücreti yazdırmak için kullandığım alan
        let txt = 0
        $("td.cart_price").each(function () {
            txt += parseFloat($(this).text());
        });
        $('#Chart_prc').text('TOTAL PRICE: ' + txt + '  TL')
    })
})


//miktar azaltma
$('table tbody').on('click', '.cart_quantity_down', function () {
    var chartID = $(this).data("chartid")

    $.getJSON("/Home/Decrease/" + chartID, function (data) {
        $('#inpt-' + data.ID).val('' + data.Quantity)
        $('#TotalPrc-' + data.ID).html('' + data.TotalPrice)

        //sepetteki miktarı sıfır olan ürünün azalt butonu kilitleniyor
        if (data.Quantity == 1) {
            var element = "btn_d-" + data.ID
            document.getElementById(element).disabled = true;
            document.getElementById(element).style.background = "#ffffff";
        }

        //toplam ücreti yazdırmak için kullandığım alan
        let txt = 0
        $("td.cart_price").each(function () {
            txt += parseFloat($(this).text());
        });
        $('#Chart_prc').text('TOTAL PRICE: ' + txt + '  TL')
    })
})

//silme işlemi
$('table tbody').on('click', '.cart_quantity_delete', function () {
    var chartID = $(this).data("chartid")

    swal({
        title: "Ürünü silmek istediğinizden eminmisiniz",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {

                $.getJSON("/Home/Delete/" + chartID, function (response) {
                    $("#Chart_tr-" + chartID).fadeOut("normal", function () {

                        $(this).remove();
                        //toplam ücreti yazdırmak için kullandığım alan
                        let txt = 0
                        $("td.cart_price").each(function () {
                            txt += parseFloat($(this).text());
                        });

                        if (txt == 0) {
                            $('#Chart_prc').text('')
                        }
                        else {
                            $('#Chart_prc').text('TOTAL PRICE: ' + txt + '  TL')
                        }
                    });
                })

                swal("Ürün başarılı bir şekilde silindi", {
                    icon: "success",
                });
            } else {
                //swal("Ürün sepette");
            }
        });


})