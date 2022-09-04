
function GetGroupId(option, id) {

    if ($("#" + option.id + " option").length == 1) {

        $("#"+option.id + "Label")[0].innerHTML = $("#"+option.id + "Label")[0].innerHTML + '<i class="fa fa-refresh fa-spin"></i>'
        $("#" + option.id).prop('disabled', true); 

        $.post("../../Admin/GetGroup?Id=" + id, function (data) {
            if (data.success) {
                data.data.forEach(element => {
                    $("#" + option.id).append($('<option>', { value: element.id, text: element.title }));
                });
            }
        });

        $("#" + option.id).prop('disabled', false);
        var labelValue = $("#" + option.id + "Label")[0].innerHTML;
        $("#" + option.id + "Label")[0].innerHTML = labelValue.replace('<i class="fa fa-refresh fa-spin"></i>', '');

    }

}