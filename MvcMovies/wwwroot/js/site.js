// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    //re-set all client validation given a jQuery selected form or child
    //complemento que permite resetear un formulario con validacion de jquery.validate
    $.fn.resetValidation = function () {
        var $form = this.closest('form');
        //reset jQuery Validate's internals
        $form.validate().resetForm();
        //reset unobtrusive validation summary, if it exists
        $form.find("[data-valmsg-summary=true]")
            .removeClass("validation-summary-errors")
            .addClass("validation-summary-valid")
            .find("ul").empty();

        //reset unobtrusive field level, if it exists
        $form.find("[data-valmsg-replace]")
            .removeClass("field-validation-error")
            .addClass("field-validation-valid")
            .empty();
        if ($form.hasClass('editForm')) {
            $form.addClass('readonly');
            $form.find(':input').prop('readonly', true);
            $form.find('button.btn-modal-edit').hide();
            $form.find('button.btn-modal-edit-edit').show();
        }
        $form[0].reset();
        return $form;
    };
    $('#editForm.readonly :input').prop('readonly', true);

    $('#editForm.readonly :button.btn-modal-edit',).hide();

    $('#editForm :button.btn-modal-edit-edit').on('click', function () {
        $(this).hide();
        const form = $(this).closest('form');
        form.removeClass('readonly');
        form.find(':input').prop('readonly', false);
        form.find('button.btn-modal-edit').show();
    });
});
function showLoading() {
    $("#loading,#loadingIcon").show();
}
function hideLoading() {
    $("#loading,#loadingIcon").hide();
}
