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

//监听textbox值的变化
var easychange = function (foochange) {
    //debugger;
    if (!foochange || foochange.constructor != Function) {
        return;
    }
    try {
        //debugger;
        this.watch("value", function (id, oldval, newval) {

            foochange(newval);
            return newval;
        });
    }
    catch (e) {
        //debugger;
        //alert(e.ToString());
    }
    this.setAttribute('oninput', '(' + foochange.toString() + ')(this.value)');
    this.onpropertychange = function () {

        foochange(this.value);
    };
//    this.onkeyup = function () {

//        foochange(this.value);
//    };
};


var valuelistener = function (foochange) {
    if (!foochange || foochange.constructor != Function) {
        return;
    }
    try {
        $(this).keyup(function () {
            var oldValue = $(this).val();
            changevalue(this, oldValue, foochange);
        })

        function changevalue(tag, oldValue, foochange) {
            var value = $(tag).val();
            foochange(value);
            if (oldValue == value) {
                setTimeout(function () {
                    changevalue(tag, oldValue, foochange);
                }, 1000);
            }
        }
    }
    catch (e) { }
}

