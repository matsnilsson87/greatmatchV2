function updateVisitorList() {
    // Hämta användarid från den dolda input-taggen:
    const id = $('#myId').val();
    var userId = parseInt(id, 10);

    $.get('/api/visitor/getlatestvisitors/' + userId)
        .then((resp) => {
            if (resp && Array.isArray(resp)) {
                $('.visitor').html('');
                resp.forEach((user) => {
       
                        $('.visitor')
                        .append(
                            `<div class="message">
                                <h4>${user.FirstName}  ${user.LastName}</h4>
                                </div>`
                           
                        );
                   
                });
            }
        });
}

window.addEventListener('load', () => {
    updateVisitorList();
    
});