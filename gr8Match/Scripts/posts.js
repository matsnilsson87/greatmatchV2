


function getName() {
    const id = $('#user-id').val();
    var userId = parseInt(id, 10)

    var namn = $.get('/api/posts/writtenbyname/' + userId).responseText();
    return namn;
    
}


function updateMessageList() {
    // Hämta användarid från den dolda input-taggen:
    const id = $('#user-id').val();
    var userId = parseInt(id, 10)
    var name = "Mats";
    
    $.get('/api/posts/getpostslist/' + userId)
        .then((resp) => {
            if (resp && Array.isArray(resp)) {
                $('#message-list').html('');
                resp.forEach((post) => {
                    //const isMine = post.WrittenTo === userId;
                    $('#messagelist')
                        .append(
                            `<article class="message">
                                    <header class="header">
                                        <h6>${name}</h6>
                                    </header>
                                    <main class="body">${post.Text}</main>
                                </article>`
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
    if (newMessage) {
        const messageObj = {
            Text: text,
            Datum: datum,
            WrittenTo: writtenTo,
            WrittenBy: writtenBy
        };
        $.post('/api/chatmessageapi/send', messageObj)
            .then((resp) => {
                if (resp === "Ok") {
                    $('#new-message').val('');
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
