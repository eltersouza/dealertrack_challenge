// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var Sales = function () {
    var _self = this;
    _self.load = function () {
        _self.bindHandlers();
    };

    _self.bindHandlers = function () {
        _self.loadTable();
        _self.bindBtnImport();
        _self.bindImportButton();
        _self.bindFileChange();
        _self.bindUploadButton();
        _self.bindBtnCloseUploadPanel();
    };

    _self.loadTable = function () {
        $.LoadingOverlay("show");
        $.ajax({
            url: '/sales/getlist',
            method: 'GET',
            success: function (data) {
                if (data.salesVm.length > 0) {
                    _self.showPanelNoData(false);
                    _self.showPanelImport(false);
                    _self.mountTableData(data);
                    _self.showTableData(true);
                } else {
                    _self.showPanelNoData(true);
                    _self.showPanelImport(true);
                    _self.showTableData(false);
                }
            },
            error: function (err) {
                _self.showPanelNoData(true);
                _self.showPanelImport(true);
                _self.showTableData(false);
            },
            complete: function () {
                $.LoadingOverlay("hide", true);
            }
        });
    };

    _self.showPanelNoData = function (show) {
        if (show) {
            $("#pnlNoSales").show();
        } else {
            $("#pnlNoSales").hide();
        }
    };

    _self.showPanelImport = function (show) {
        if (show) {
            $("#importPanel").show();
        } else {
            $("#importPanel").hide();
        }
    };

    _self.showTableData = function (show) {
        if (show) {
            $("#tblData").show();
        } else {
            $("#tblData").hide();
        }
    };

    _self.bindBtnImport = function () {
        console.log('Bind btnOpenImportPanel');
        $("#btnOpenImportPanel").bind("click", function (evt) {
            console.log('btnOpenImportPanel clicked!');
            if ($("#importPanel").is(':hidden')) {
                $("#importPanel").show('slow');
            }
        });
    };

    _self.bindImportButton = function () {
        console.log('Bind btnImportFile');
        $("#btnImportFile").bind('click', function () {
            console.log('btnImportFile clicked!');
            $("#hdnFile").click();
        });
    };

    _self.bindFileChange = function () {
        console.log('Bind hdnFile');
        $(document).on('change', '#hdnFile', function (evt) {
            var file = evt.target.files[0];
            $("#txtFileAddress").val(file.name).show();
            $("#btnUpload").show();
        });
    };

    _self.bindUploadButton = function () {
        console.log('Bind btnUpload');
        $("#btnUpload").bind('click', function (evt) {
            console.log('Button Upload was clicked');
            $("#importPanel .panel-body").LoadingOverlay("show", {
                background: "rgba(51,122,183, 0.5)"
            });

            _self.uploadFile();
        });
    };

    _self.uploadFile = function () {
        var file = $("#hdnFile")[0].files[0];

        var formData = new FormData();
        formData.append('files', file, file.name);

        $.ajax({
            url: '/sales/uploadfile',
            method: 'POST',
            data: formData,
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            success: function (data) {
                $("#spnFileImportFeedback").removeClass("text-danger").addClass("text-success").html("The file was imported with success.");
                _self.showPanelImport(false);
                _self.showPanelNoData(false);

                _self.mountTableData(data);
                _self.showTableData(true);
            },
            error: function (err) {
                console.error(err);
                $("#spnFileImportFeedback").removeClass("text-success").addClass("text-danger").html("Error");
            },
            complete: function () {
                $("#importPanel .panel-body").LoadingOverlay("hide", true);
            }
        });
    };

    _self.mountTableData = function (data) {
        console.log("Mount Table", data);
        var table = $("#tblData tbody");
        for (var i = 0, j = data.salesVm.length; i < j; i++) {
            var item = data.salesVm[i];
            table.append("<tr><td>" + item.dealNumber + "</td><td>" + item.customerName + "</td><td>" + item.dealershipName + "</td><td>" + item.vehicle + "</td><td>" + item.priceFormated + "</td><td>" + item.dateFormated + "</td></tr>");
        }
    };

    _self.bindBtnCloseUploadPanel = function () {
        console.log('Bind bindBtnCloseUploadPanel');
        $("#btnClosePanel").bind('click', function (evt) {
            console.log('btnClosePanel was clicked');
            var panel = $("#btnClosePanel").parents(".panel");
            console.log(panel);
            panel.hide("slow");
        });
    };

    return _self;
};

$(document).ready(function () {
    var sales = new Sales();
    sales.load();
});