const swal_toast = swal.mixin({
    toast: true,
    position: "top-end",
    showConfirmButton: false,
    timer: 1500,
    width: 320,
    padding: "1.5em"
});

function showMessageBox(msg) {
    swal.fire({
        title: "Error!",
        type: "error",
        text: msg
    });
}

function AjaxErrorHandler(rs) {
    if (rs.status === 0) {
        rs.statusText = "Network connection failed.";
    }
    showMessageBox(rs.statusText + " (" + rs.status + ")");
}

function SetupAjaxForm(formId, funcDone = null) {
    var submitBtnId = formId + " .btn-submit";
    $(submitBtnId).unbind("click");
    $(submitBtnId).click(function () {
        clickAjaxFormSaveBtn(formId, funcDone, false);
    });

    var submitRedirectBtnId = formId + " .btn-submit_redirect";
    if ($(submitRedirectBtnId).length) {
        $(submitRedirectBtnId).unbind("click");
        $(submitRedirectBtnId).click(function () {
            clickAjaxFormSaveBtn(formId, funcDone, true);
        });
    }
}

function clickAjaxFormSaveBtn(formId, funcDone = null, redirect = false) {
    var formObj = $(formId);
    var validator = formObj.validate();
    if (validator.form()) {

        var ajaxSetting = {
            type: "POST",
            url: formObj.attr("action"),
            data: formObj.serialize()
        };
        if (formObj.has("input:file").length > 0) {
            ajaxSetting = {
                type: "POST",
                url: formObj.attr("action"),
                data: new FormData(document.getElementById(formId.replace("#", ""))),
                enctype: "multipart/form-data",
                contentType: false,
                cache: false,
                processData: false
            };
        }
        $.ajax(ajaxSetting).done(function (data) {
            swal.fire({
                title: data.title,
                text: data.message,
                type: data.success ? "success" : "warning"
            }).then(function () {
                if (funcDone !== null) {
                    funcDone();
                }
                if (data.enforce || redirect) {
                    if (data.url !== null) {
                        $(location).attr('href', data.url);
                    }
                }
            });
        })
            .fail(function (rs) {
                AjaxErrorHandler(rs);
            })
            .always(function () {
                //mApp.unblock(formId, {});
            });
    }
}

function CallAjaxGet(url, funcDone = null, redirect = false) {
    var ajaxSetting = {
        type: "GET",
        url: url
    };

    $.ajax(ajaxSetting).done(function (data) {
        swal.fire({
            title: data.title,
            text: data.message,
            type: data.success ? "success" : "error"
        }).then(function () {
            if (funcDone !== null) {
                funcDone();
            }
            if (data.enforce || redirect) {
                if (data.url !== null) {
                    $(location).attr("href", data.url);
                }
            }
        });
    })
        .fail(function (rs) { AjaxErrorHandler(rs); });
}
