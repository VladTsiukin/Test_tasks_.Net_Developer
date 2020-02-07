/* product.js */

$(document).ready(function () {
    var $ProductForm = $('#formProduct');
    var $alertModal = $('#alertModal');
    var $modalProduct = $('#modalProduct');
    var productId = null;

    // validate
    jQuery.validator.setDefaults({
        debug: true,
        success: "valid"
    });
    $ProductForm.validate({
        rules: {
            productDescription: {
                required: true,
                maxlength: 256
            },
            productName: {
                required: true,
                maxlength: 256
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });

    // edit show modal
    $(document).on('click', '.editProduct',
        function (e) {
            e.preventDefault();
            var $parent = $(this).parent('.parentItem');
            productId = $parent.attr('data-product-id');
            var $trElement = $('#tr-' + productId);
            var description = $trElement.find('.tr-description').get(0).innerText;
            var name = $trElement.find('.tr-name').get(0).innerText;

            $('#ProductName').val(name);
            $('#ProductDescription').val(description);
            $modalProduct.addClass('isEdit');
            $modalProduct.modal('show');
        });

    // delete show modal
    $(document).on('click', '.deleteProduct',
        function (e) {
            e.preventDefault();
            var $parent = $(this).parent('.parentItem');
            productId = $parent.attr('data-product-id');

            $alertModal.find('h5').text('Are you sure?');
            $alertModal.find('.modal-footer')
                .append('<button type="button" class="btn btn-danger btnRemove" data-dismiss="modal">Remove</button>');
            $alertModal.modal('show'); 
        });

    // delete
    $(document).on('click', '.btnRemove',
        function (e) {
            e.preventDefault();
            var $trElement = $('#tr-' + productId);
            $.post({
                data: {
                    __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val(),
                    storeId: $('#StoreId').val(),
                    productId: productId
                },
                url: '/management/deleteproduct',
                success: function (response) {
                    if (response.error === false) {
                        $alertModal.find('h5').text('Product removed successfully.');
                        $alertModal.modal('show');
                        $trElement.remove();
                    } else {
                        console.log(response.message);
                        ShowError();
                    }
                },
                error: function (xhr, status, error) {
                    console.log(error);
                    ShowError();
                },
                complete: function () {
                }
            });
        });

    // edit or add product
    $('.btnModalProduct').on('click',
        function (e) {
            e.preventDefault();
            if (!$ProductForm.valid()) {
                return;
            }

            var $inputs = $ProductForm.find("input, button");
            $inputs.prop("disabled", true);  

            if ($modalProduct.hasClass('isEdit')) {
                var prName = $('#ProductName').val().trim();
                var prDescription = $('#ProductDescription').val().trim();
                // edit
                $.post({
                    data: {
                        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val(),
                        ProductName: prName,
                        ProductDescription: prDescription,
                        storeId: $('#StoreId').val(),
                        productId: productId
                    },
                    url: '/management/editproduct',
                    success: function (response) {
                        if (response.error === false) {
                            $('#modalProduct').modal('hide');
                            var $tr = $('#tr-' + productId);
                            $tr.find('.tr-description').get(0).innerText = prDescription;
                            $tr.find('.tr-name').get(0).innerText = prName;
                            $alertModal.find('h5').text('Product edit successfully.');
                            $alertModal.modal('show');
                        } else {
                            console.log(response.message);
                            ShowError();
                        }
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                        ShowError();
                    },
                    complete: function () {
                        $inputs.prop("disabled", false);
                        $modalProduct.removeClass('isEdit');
                    }
                });
            } else {
                // add
                $.post({
                    data: {
                        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val(),
                        ProductName: $('#ProductName').val(),
                        ProductDescription: $('#ProductDescription').val(),
                        storeId: $('#StoreId').val()
                    },
                    url: '/management/createproduct',
                    success: function (response) {
                        if (response.error === false) {
                            $('tbody').append(createProductHtml(response));
                            $('#modalProduct').modal('hide');
                            $alertModal.find('h5').text('Product "' + response.name + '" created successfully.');
                            $alertModal.modal('show');
                        } else {
                            console.log(response.message);
                            ShowError();
                        }
                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                        ShowError();
                    },
                    complete: function () {
                        $inputs.prop("disabled", false);
                    }
                });
            }

        });

    function ClearModalInfo() {
        $alertModal.find('h5').text('');
        var rem = $alertModal.find('.btnRemove');
        if (rem.length) {
            rem.remove();
        }
    }

    function ClearModalProduct() {
        productId = null;
        $('#ProductName').val(null);
        $('#ProductDescription').val(null);
    }

    $('#modalProduct').on('hidden.bs.modal', function () {
        ClearModalProduct();
    });

    $alertModal.on('hidden.bs.modal', function () {
        ClearModalInfo();
    });

    // show error
    function ShowError() {
        $('#modalProduct').modal('hide');
        $alertModal.find('h5').text('Unexpected error occurred. Enter data again.');
        $alertModal.modal('show');
    }

    // create new tr
    function createProductHtml(json) {
        var tr = document.createElement('tr');
        tr.id = 'tr-' + json.productId;
        tr.innerHTML = '<td class="tr-name">' + json.name + '</td>' +
            '<td class="tr-description">' + json.description + '</td>' +
            '<td class="parentItem" data-product-id="'+ json.productId +'">' +
                '<a href="#" class="btn btn-primary editProduct" >' +
                '<i class="fa fa-pencil-square-o" aria-hidden="true"></i>' +
                '</a> | ' +
                '<a href="#" class="btn btn-primary deleteProduct">' +
                '<i class="fa fa-trash-o" aria-hidden="true"></i>' +
                '</a>' +
            '</td>';
        return tr;
    }
});