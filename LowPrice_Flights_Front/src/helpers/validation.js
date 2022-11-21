/* eslint-disable */
$(function () {
    jQuery.validator.addMethod(
        "datePassed",
        function (value, element) {
            // Date Format: yyyy/mm/dd
            const today = FormatDate(new Date());
            return value >= today;
        },
        "Selected date has passed!"
    );
    $("form[name='flightSearch']").validate({
        rules: {
            originAirport: "required",
            destinationAirport: "required",
            departureDate: {
                required: true,
                datePassed: true,
            },
            returnDate: {
                required: true,
                datePassed: true,
            }
        },
        messages: {
            originAirport: "Please enter origin airport!",
            destinationAirport: "Please enter destination airport!",
            departureDate: {
                required: "Please select date!",
            },
            returnDate: {
                required: "Please select date!",
            }
        }
    });
});

function FormatDate(date) {
    let year = new Intl.DateTimeFormat("en", { year: "numeric" }).format(date);
    let month = new Intl.DateTimeFormat("en", { month: "2-digit" }).format(date);
    let day = new Intl.DateTimeFormat("en", { day: "2-digit" }).format(date);
    return `${year}-${month}-${day}`;
}
