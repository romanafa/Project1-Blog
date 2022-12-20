// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Loding spinner
$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

// Open modal for creating/editting comment
showInCommentPopUp = (url, title) => {
    var postId = $(this).data('post-id');
    $.ajax({
        type: 'GET',
        url: url,
        success: function(res) {
            $('#form-modal .modal-body').val(postId);
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    });
}

// POST: new comment
jQueryAjaxPost = form => {
    try {
        var postName = document.getElementById('postTitle').innerHTML;
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function(res) {
                if (res.isValid) {
                    $("#view-all").html(res.html);
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-title").html('');
                    $("#form-modal").modal('hide');
                    Swal.fire({
                        title: 'Kommentar lagret',
                        icon: 'success',
                        timer: 1500
                    });

                    // message for SignalR notification
                    var message = "Ny kommentar i '" + postName + "'.";
                    connection.invoke("SendMessage", message).catch(function(err) {
                        return console.error(err.toString());
                    });
                } else
                    Swal.fire({
                        title: 'Noe gikk galt',
                        text: 'Kommentar er ikke lagret. Prøv igjen',
                        icon: 'error',
                        timer: 3000
                    });
                    $("#form-modal").modal('hide');;
            },
            error: function(err) {
                console.log(err);
            }
        });

    } catch (e) {
        console.log(e);
    }

    // to prevent default form submit event
    return false;
}
