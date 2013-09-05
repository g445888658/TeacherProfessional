
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

//绑定上传插件
function bindUploadify(target) {
    for (var i = 0; i < target.length; i++) {
        var ASU_Code = $(target[i]).children("input").val();
        var fileQueue = $($(target[i]).children().children()[1]).attr("id");
        //alert(fileQueue);
        var tag = $(target[i]).children().children("input");
        $(tag).uploadify({
            //以下参数均是可选
            'scriptData': {
                'U_Code': $("#U_Code").val(),
                'ASU_Code': ASU_Code
            },
            'buttonImg': '../../Plug-in/jquery.uploadify-v2.1.4/xuanze.png',
            'uploader': '../../Plug-in/jquery.uploadify-v2.1.4/uploadify.swf', //指定上传控件的主体文件，默认‘uploader.swf’
            //            'folder': 'Attachment', //要上传到的服务器路径，默认‘/’
            'script': $(tag).attr('url'), //上传文件的后台
            'cancelImg': '../../Plug-in/jquery.uploadify-v2.1.4/cancel.png', //指定取消上传的图片，默认‘cancel.png’
            'width': 83,
            'height': 28,
            'auto': false, //选定文件后是否自动上传，默认false
            'method': 'POST',
            'queueID': fileQueue,
            'multi': true, //是否允许同时上传多文件，默认false
            'fileDesc': 'doc文件,docx文件,xls文件,xlsx文件,jpg文件,bmp文件,png文件,gif文件,rmvb文件,rm文件,avi文件,mkv文件,mp4文件,wma文件', //出现在上传对话框中的文件类型描述
            'fileExt': '*.doc;*.docx;*.xls;*.xlsx;*.jpg;*.bmp;*.png;*.gif;*.rmvb;*.rm;*.avi;*.mkv;*.mp4;*.wma;', //控制可上传文件的扩展名，文件启用本项时需同时声明fileDesc
            'sizeLimit': 1024 * 1024 * 1024, //控制上传文件的大小，单位byte(1G)
            'simUploadLimit': 5, //多文件上传时，同时上传文件数目限制
            'queueSizeLimit': 5,
            'onComplete': function (event, queueID, fileObj, response, data) {
                if (response != "") {
                    //alert(uploadifyCallBack);
                    if (uploadifyCallBack) {
                        uploadifyCallBack(response);
                    }
                    var saveName = $("<input type='hidden' id='SaveName' name='SaveName' value='' />");
                    var fileName = $("<input type='hidden' id='FileName' name='FileName' value='' />");
                    $(saveName).val(response);
                    $(fileName).val(fileObj.name);
                    $(tag).parent().append(saveName);
                    $(tag).parent().append(fileName);
                    showInfo("成功上传" + fileObj.name, true); //showInfo方法设置上传结果   
                }
                else {
                    showInfo("文件" + fileObj.name + "上传出错", false);
                }
            },
            'onError': function (event, queueID, fileObj) {
                $("#ui_dialog").html("文件:" + fileObj.name + " 上传失败");
                $('#ui_dialog').dialog({
                    modal: true,
                    show: 'blind',
                    hide: 'explode',
                    resizable: false,
                    title: "提示"
                });
            }
        });
    }

}
