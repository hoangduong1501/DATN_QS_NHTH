
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgHocVien').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

function Refresh_ViewBag_Notify() {
    //location.reload();
    //alert("OK");
}

$("#fileLoad").change(function () {
    readURL(this);
});

function Delete_Event(areaName, actionName, controllerName, idName, idValue) {
    if (areaName != '') {
        $('#footer_Popup').html(
            '<a id="btn_Delete" class="btn btn-danger" href="/' + areaName + '/' + controllerName + '/' + actionName
            + '?' + idName + '=' + idValue + '">Xóa</a>' +
            '<button class="btn btn-secondary" data-dismiss="modal">Hủy</button>');
    } else {
        $('#footer_Popup').html(
            '<a id="btn_Delete" class="btn btn-danger" href="/Manager/HocVien_Ma_/Xoa_HoSo_HocVien_MA?id_TaiKhoan=3">Xóa</a>' +
            '<button class="btn btn-secondary" data-dismiss="modal">Hủy</button>');
    }

    $("#delete_Popup").modal();
}