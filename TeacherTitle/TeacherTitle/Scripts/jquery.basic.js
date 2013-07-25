function isIE() {
    if ($.browser.msie)
        return true
    else
        return false;
}

function onlyNum(target) {
    $(target).keydown(function (e) {
        return checkKeyCode(e);
    });
    $(target).keyup(function () {
        checkValue($(target));
    });
}

//function NoChinese(target) {
//    $(target).keyup(function () {
//        $(this).val($(this).val().replace(/[\W]/g, ''));
//    });
//}

function checkKeyCode(e) {
    if ((e.keyCode < 48 && e.keyCode != 8) || e.keyCode > 57) {
        if (isIE())
            window.event.returnValue = false;
        else
            return false;
    }
}

function checkValue(target) {
    $(target).val($(target).val().replace(/\D/g, ''));
}

