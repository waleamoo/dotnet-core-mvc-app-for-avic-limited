$(document).ready(function () {
    //alert('Welldone!'); 
    $('#loginForm').on('submit', function (event) {
        event.preventDefault();

        $.ajax({
            url: '/data/admin-scripts.php',
            type: 'POST',
            data: new FormData(this),
            dataType: 'json',
            contentType: false,
            cache: false,
            processData: false,
            success: function (response) {
                $(".formResult").html('');
                if (response.status == 1) {
                    window.location.href = "/home/dashboard"; // dashboard
                } else {
                    $(".formResult").html("<div class='alert alert-danger'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a><strong>" + response.message + "</strong>.</div>");
                }
            }
        });
    });

    // choose type of product to upload
    $("#variableProductUploadForm").hide();
    $("#productUploadForm").hide();
    $("#is-variable").change(function () {
        if ($(this).val() != "") {
            var val = $(this).val();
            if (val === "Yes") {
                $("#productUploadForm").hide();
                $("#variableProductUploadForm").show();

            } else {
                $("#variableProductUploadForm").hide();
                $("#productUploadForm").show();
            }
        }
    });

    // code to add a single product 
    $("#productUploadForm").on('submit', function (e) {
        e.preventDefault();
        //alert('hi');
        $.ajax({
            url: '/data/admin-scripts.php',
            method: 'POST',
            data: new FormData(this),
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.status == 1) {
                    $('.uploadResult').html("<div class='alert alert-success'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>" + response.message + "</div>");
                    $("#productUploadForm")[0].reset();
                } else {
                    $('.uploadResult').html("<div class='alert alert-danger'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>" + response.message + "</div>");

                }
            }
        });
    });

    // dynamic dropdown for product category and sub category
    $('.dynamic').change(function () {
        if ($(this).val() != '') {
            var category_id = $(this).val(); // 1
            var dependent = $(this).data('dependent'); // product-sub-category
            //alert(value + " " + dependent);
            $.ajax({
                url: "/data/admin-scripts.php",
                method: "POST",
                data: { changedDrop: 1, category_id: category_id },
                success: function (result) {
                    $('.' + dependent).html(result);
                    //console.log(category_id + " " + dependent);
                }
            });
        }
    });

    $('.dynamic-variable').change(function () {
        if ($(this).val() != '') {
            var category_id = $(this).val(); // 1
            var dependent = $(this).data('dependent'); // product-variable-sub-category
            $.ajax({
                url: "/data/admin-scripts.php",
                method: "POST",
                data: { changedDrop: 1, category_id: category_id },
                success: function (result) {
                    $('#' + dependent).html(result);
                    //console.log(category_id + " " + dependent);
                }
            });
        }
    });

    // for add multiple files in the input form 
    var i = 1;
    $(".add").click(function () {
        i++;
        $('.dynamic_field').append('<tr id="row' + i + '"><td><input type="file" name="files[]" class="form-control" required><small class="text-muted">Dimension must be 500x500.</small></td><td><button type="button" name="remove" id="' + i + '" class="btn btn-danger btn_remove"> X </button></td></tr>');
    });
    // onclick event 
    $(document).on('click', '.btn_remove', function () {
        var button_id = $(this).attr("id");
        $("#row" + button_id + "").remove();
    });

    // for dynamic field
    var x = 1;
    $("#add-dynamic").click(function () {
        x++;
        $('.dynamic_variable_field').append('<tr id="row' + x + '">' +
            '<td><input class="form-control" type="text" name="product_variable[]" placeholder="e.g Size M | Blue" required></td>' +
            '<td><input class="form-control" type="text" name="product_price[]" placeholder="Product Price" required onkeyup="numbersOnly(this)"></td>' +
            '<td><input class="form-control" type="text" name="discount_price[]" value="0" placeholder="Discount Price" required onkeyup="numbersOnly(this)"></td>' +
            '<td><input class="form-control" type="text" name="product_stock_level[]" placeholder="Stock Level" required onkeyup="numbersOnly(this)"></td> ' +
            '<td><button type="button" id="' + x + '" class="btn btn-danger btn-remove-dynamic"> X </button></td>' +
            '</tr>');
    });
    // onclick event 
    $(document).on('click', '.btn-remove-dynamic', function () {
        var id = $(this).attr("id");
        $("#row" + id + "").remove();
    });
    // code to add variable product 
    $("#variableProductUploadForm").on('submit', function (e) {
        e.preventDefault();
        //alert('hi');
        $.ajax({
            url: '/data/admin-scripts.php',
            method: 'POST',
            data: new FormData(this),
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.status == 1) {
                    $('.uploadResult').html("<div class='alert alert-success'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>" + response.message + "</div>");
                    $("#variableProductUploadForm")[0].reset();
                } else {
                    $('.uploadResult').html("<div class='alert alert-danger'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>" + response.message + "</div>");

                }
            }
        });
    });
    // code to add product categories
    $("#uploadCategoriesForm").on('submit', function (e) {
        e.preventDefault();

        var category_name = $("#category-name").val();
        var category_slug = $("#category-slug").val();
        //alert('Name: ' + category_name + ' Slug: ' + category_slug);

        $.ajax({
            url: '/data/admin-scripts.php',
            method: 'POST',
            data: { categoryFormData: 1, category_name: category_name, category_slug: category_slug },
            dataType: 'json',
            success: function (response) {
                if (response.status == 1) {
                    $('.uploadResult').html("<div class='alert alert-success'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>" + response.message + "</div>");
                } else {
                    $('.uploadResult').html("<div class='alert alert-danger'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>" + response.message + "</div>");

                }
            }
        });
    });

    // code to add product sub categories
    $("#uploadSubCategoriesForm").on('submit', function (e) {
        e.preventDefault();

        var category_id = $("#product-category").val();
        var sub_category_name = $("#sub-category-name").val();
        var sub_category_slug = $("#sub-category-slug").val();

        $.ajax({
            url: '/data/admin-scripts.php',
            method: 'POST',
            data: { subCategoryFormData: 1, category_id: category_id, sub_category_name: sub_category_name, sub_category_slug: sub_category_slug },
            dataType: 'json',
            success: function (response) {
                if (response.status == 1) {
                    $('.uploadSubResult').html("<div class='alert alert-success'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>" + response.message + "</div>");
                } else {
                    $('.uploadSubResult').html("<div class='alert alert-danger'><a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>" + response.message + "</div>");

                }
            }
        });
    });


    // ckeditor field
    //$('textarea.editor').ckeditor();

});