$(document).ready(function () {
    //alert('Welldone!'); 
    $("#CategoryId").change(function () {
        var id = $(this).val(); // select id
        $("#SubCategoryId").empty();
        //$("#SubCategoryId").append("<option value='0'>Please choose subcategory</option>");
        $.ajax({
            url: "/home/getsubcategorybycategoryid?id=" + id,
            success: function (result) {
                $.each(result, function (i, data) {
                    //console.log(data);
                    $("#SubCategoryId").append('<option value=' + data.id + '>' + data.subCategoryName + '</option>');
                });
            }
        });
    });
}); 