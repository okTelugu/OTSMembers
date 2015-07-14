$(document).ready(function () {
    $('#directoryTable').DataTable({
        dom: 'T<"clear">lfrtip',
        tableTools: {
            "sSwfPath": "../swf/copy_csv_xls_pdf.swf"
        }
    });

    var tableTools = new $.fn.dataTable.TableTools('#directoryTable', {
        "buttons": [
            "copy",
            "csv",
            "xls",
            "pdf",
            { "type": "print", "buttonText": "Print me!" }
        ]
    });

    $(tableTools.fnContainer()).insertAfter('#directoryTable');
});