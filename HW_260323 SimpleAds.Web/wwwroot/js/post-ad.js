$(() => {
    $("input, textarea").on("input", function() {
        const isFormValid = $("#phone-number").val().trim().length > 0 && $("#description").val().trim().length > 0;
        $(".ad-submit-btn").prop("disabled", !isFormValid);
    })
})