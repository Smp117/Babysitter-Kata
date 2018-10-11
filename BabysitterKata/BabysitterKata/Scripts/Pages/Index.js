$(document).ready(function () {
    $('.hourddl').on("change", function () {
        console.log("change");
        UpdateDropdowns();
    });

    function UpdateDropdowns() {
        var start = $('#StartHour').val();
        var bed = $('#BedTimeHour').val();
        var end = $('#EndHour').val();

        //console.log(start);
        //console.log(bed);
        //console.log(end);

        $.ajax({
            method: "POST",
            url: "Home/UpdateDropDowns",
            data: { start: start, bed: bed, end: end}
        })
            .done(function (data) {
                var result = JSON.parse(data);
                UpdateDropdown($('#StartHour'), result.StartOptions, start);
                UpdateDropdown($('#BedTimeHour'), result.BedTimeOptions, bed);
                UpdateDropdown($('#EndHour'), result.EndOptions, end);
            });
    }

    function UpdateDropdown($selector, list, selectedValue) {
        $selector.empty();
        $.each(list, function (index, item) {
            $selector.append(
                $('<option>', {
                    value: item.Value,
                    text: item.Display
                }, '</option>'))
            $selector.val(selectedValue);
            if ($selector.val() == null) 
                $selector.prop("selectedIndex", 0);
        })
    }

});