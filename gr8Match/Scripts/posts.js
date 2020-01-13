


function getName() {
    const id = $('#user-id').val();
    var userId = parseInt(id, 10)

    var namn = $.get('/api/posts/writtenbyname/' + userId).responseText();
    return namn;
    
}


function updateMessageList() {

    const id = $('#user-id').val();
    var userId = parseInt(id, 10);

    $.get('/api/posts/getpostslist/' + userId)
        .then((resp) => {
            if (resp && Array.isArray(resp)) {
                $('#message-list').html('');
                resp.forEach((post) => {

                    $('#messagelist')
                        .append(
                            `<div class="toRemove">
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <h6>Skrivet av användare:  ${post.WrittenBy}, ${post.Datum} </h6>                                                      
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">                                          
                                    <p>${post.Text}</p>
                                </div>
                            </div>
                            </div>`
                        );
                });
            }
        });
}

function sendMessage() {
    const text = $('#new-message').val();
    const datum = new Date().toISOString();
    const writtenTo = $('#user-id').val();
    const writtenBy = $('#user-myid').val();
    if (text) {

        const messageObj = {
            Text: text,
            Datum: datum,
            WrittenTo: writtenTo,
            WrittenBy: writtenBy
        };
        $.post('/api/posts/send', messageObj)
            .then((resp) => {
                if (resp === "Meddelandet skickat") {
                    $('#new-message').val('');
                    $('.toRemove').remove();
                    updateMessageList();
                } else {
                    alert('Något gick fel!');
                }
            });
    }
}

window.addEventListener('load', () => {
    updateMessageList();

    if ($('#user-id').length > 0) {
        $('#send-button').click(sendMessage);
    }
});
