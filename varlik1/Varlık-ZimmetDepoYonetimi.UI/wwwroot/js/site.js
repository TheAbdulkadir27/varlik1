// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // DataTables initialization
    if ($.fn.DataTable) {
        $('.datatable').DataTable({
            language: {
                url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
            },
            responsive: true,
            pageLength: 10,
            order: [[0, 'asc']]
        });
    }

    // Form validation için
    if ($.validator) {
        $.validator.setDefaults({
            errorElement: 'span',
            errorClass: 'text-danger',
            highlight: function (element, errorClass, validClass) {
                $(element).addClass('is-invalid');
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).removeClass('is-invalid');
            }
        });
    }
});
