//$(document).ready(function () {
//    var table = $('#directoryTable').DataTable({
//        dom: 'T<"clear">lfrtip'
//    });

//    var tableTools = new $.fn.dataTable.TableTools(table,
//        {
//    sSwfPath: "swf/copy_csv_xls_pdf.swf"
//        });

//    $(tableTools.fnContainer()).insertAfter('#directoryTable');
//});

$(document).ready(function () {
    $('#directoryTable').DataTable({
        dom: 'T<"clear">lfrtip',
        tableTools: {
            "sSwfPath": "../swf/copy_csv_xls_pdf.swf"
        }
    });
    $(tableTools.fnContainer()).insertAfter('#directoryTable');
});