

$(document).ready(function () {

    editorDict = {};
    var uniqId = (function () {
        var i = 0;
        return function () {
            return i++;
        }
    })();

    CKEDITOR.config.toolbar = [
        ['Styles', 'Format', 'Font', 'FontSize'],
        '/',
        ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Cut', 'Copy', 'Paste', 'Find', 'Replace', '-', 'Outdent', 'Indent', '-', 'Print'],
        '/',
        ['NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
        ['Image', 'Table', '-', 'Link', 'Flash', 'Smiley', 'TextColor', 'BGColor', 'Source']
    ];

    $('.basic-editable-container').find('.edit-button').on('click', function () {
        $editButton = $(this);
        $editTarget = $editButton.siblings('.edit-target');
        $editTarget.attr('contenteditable', 'true');
        $editTarget.focus();
        CKEDITOR.disableAutoInline = true;
        elementID = uniqId();
        $editTarget.attr('id', elementID);
        console.log($editTarget.html());
        editorDict[elementID] = [CKEDITOR.inline(elementID.toString()), $editTarget.html()];

        $editButton.css({ "display": "none" });
        $editExitOptions = $editButton.siblings('.edit-exit-buttons');
        $editExitOptions.css({ "display": "block"});
    })

    $('.basic-editable-container').find('.edit-exit-buttons').find('.edit-discard').on('click', function () {
        $editDiscardButton = $(this);
        $editTarget = $editDiscardButton.parent().siblings('.edit-target');
        editor = editorDict[$editTarget.attr('id')][0];
        OGContent = editorDict[$editTarget.attr('id')][1];
        if (editor) {
            editor.destroy();
            $editTarget.html(OGContent);
        }
        $editTarget.attr('contentEditable', 'false');
        $editTarget.siblings('.edit-button').css({ 'display': 'block' });
        $editTarget.siblings('.edit-exit-buttons').css({'display': 'none'});
    })

    $('.basic-editable-container').find('.edit-exit-buttons').find('.edit-save').on('click', function () {
        $editSaveButton = $(this);
        $editTarget = $editSaveButton.parent().siblings('.edit-target');

        newTextString = "";

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            console.log(this.status);
            if (this.readyState == 4 && this.status == 201) {
                console.log(this.responseText);
                console.log("above is the response text");
            }
        };
        newText = $editTarget.html();
        var WebAPIUrl = window.location.href;
        editor = editorDict[elementID][0];
        OGContent = editorDict[elementID][1];
        console.log(WebAPIUrl + "/EditText?newText=" + encodeURIComponent(newText) + "&oldText=" + encodeURIComponent(OGContent) + "&fileToEdit=" + fileToEdit);
        console.log("above is the request");
        xhttp.open("POST", WebAPIUrl + "/EditText?newText=" + encodeURIComponent(newText) + "&oldText=" + OGContent.trim() + "&fileToEdit=" + fileToEdit, true);
        xhttp.send();
        
        if (editor) {
            editor.destroy();
        }
        $editTarget.attr('contentEditable', 'false');
        $editTarget.siblings('.edit-button').css({ 'display': 'block' });
        $editTarget.siblings('.edit-exit-buttons').css({ 'display': 'none' });
    })


    $('.update-card').find('.edit-update-option').on('click', function () {
        //console.log(this);
        //$(this).attr('id', 'hey');
        if (updateInEdit == false) {
            updateInEdit = true;
            $(this).attr('id', 'editUpdateButton');
            $(this).siblings('.card-header-text').attr('id', 'updateHeadUnderEdit');
            $(this).siblings('.edit-update-exit-options').attr('id', 'updateEditExitOptions');

            $(this).parent().siblings('.card-body').children('.card-title').attr('id', 'updateBodyTitleUnderEdit');
            $(this).parent().siblings('.card-body').children('.card-subtitle').attr('id', 'updateBodySubtitleUnderEdit');
            $(this).parent().siblings('.card-body').children('.card-text').attr('id', 'updateBodyTextUnderEdit');

            $(this).siblings('.edit-update-exit-options').children('.edit-update-save-option').attr('id', 'updateSaveOption');
            $(this).siblings('.edit-update-exit-options').children('.edit-update-discard-option').attr('id', 'updateDiscardOption');

            setTextEditor('updateHeadUnderEdit', 'updateEditExitOptions', 'editUpdateButton', 'inline-block');

            setTextEditor('updateBodyTitleUnderEdit', 'updateEditExitOptions', 'editUpdateButton', 'inline-block');

            setTextEditor('updateBodySubtitleUnderEdit', 'updateEditExitOptions', 'editUpdateButton', 'inline-block');

            setTextEditor('updateBodyTextUnderEdit', 'updateEditExitOptions', 'editUpdateButton', 'inline-block');

            document.getElementById('updateHeadUnderEdit').focus();
        }
        
    });

    //uses something call a dynamic event handler ¯\_(ツ)_/¯
    $(document).on('click', '#updateDiscardOption', function () {
        console.log("hey");
        discardChanges('updateHeadUnderEdit', 'updateEditExitOptions', 'editUpdateButton');
        discardChanges('updateBodyTitleUnderEdit', 'updateEditExitOptions', 'editUpdateButton');
        discardChanges('updateBodySubtitleUnderEdit', 'updateEditExitOptions', 'editUpdateButton');
        discardChanges('updateBodyTextUnderEdit', 'updateEditExitOptions', 'editUpdateButton');
        updateInEdit = false;
        $(this).removeAttr('id');
        $(this).siblings('.card-header-text').removeAttr('id');
        $(this).siblings('.edit-update-exit-options').removeAttr('id');

        $(this).parent().siblings('.card-body').children('.card-title').removeAttr('id');
        $(this).parent().siblings('.card-body').children('.card-subtitle').removeAttr('id');
        $(this).parent().siblings('.card-body').children('.card-text').removeAttr('id');

        $(this).siblings('.edit-update-exit-options').children('.edit-update-save-option').removeAttr('id');
        $(this).siblings('.edit-update-exit-options').children('.edit-update-discard-option').removeAttr('id');
    })

    $('.update-card').find('.delete-update-option').on('click', function () {

    })
})

window.onload = function () {
    editorDict = {};
}

var editorDict;

function setTextEditor(element, exitOptionsElement, editButtonElement, displaySettings = "block", toolbarSetting = "basic") {
    if (toolbarSetting == "basic") {
        
    }

    element.contentEditable = "true";
    element.focus();

    CKEDITOR.disableAutoInline = true;
    elementID = element.id;
    editorDict[elementID] = [CKEDITOR.inline(elementID), element.innerHTML];

    element.style.display = "none";
}

function setTextEditor(elementID, exitOptionsID, editButtonID, displaySetting = "block", toolbarSetting = "basic")
{
    if (toolbarSetting == "basic") {
        CKEDITOR.config.toolbar = [
            ['Styles', 'Format', 'Font', 'FontSize'],
            '/',
            ['Bold', 'Italic', 'Underline', 'StrikeThrough', '-', 'Undo', 'Redo', '-', 'Cut', 'Copy', 'Paste', 'Find', 'Replace', '-', 'Outdent', 'Indent', '-', 'Print'],
            '/',
            ['NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ['Image', 'Table', '-', 'Link', 'Flash', 'Smiley', 'TextColor', 'BGColor', 'Source']
        ];
    }
    //ensure that element is textarea?
    document.getElementById(elementID).contentEditable = "true";
    document.getElementById(elementID).focus();
    CKEDITOR.disableAutoInline = true;
    editorDict[elementID] = [CKEDITOR.inline(elementID), document.getElementById(elementID).innerHTML];

    document.getElementById(editButtonID).style.display = "none";
    document.getElementById(exitOptionsID).style.display = displaySetting;
}




function discardChanges(elementID, exitOptionsID, editButtonID)
{
    editor = editorDict[elementID][0];
    OGContent = editorDict[elementID][1];
    if (editor) {
        editor.destroy();
        document.getElementById(elementID).innerHTML = OGContent;
    }
    document.getElementById(elementID).contentEditable = "false";
    document.getElementById(editButtonID).style.display = "block";
    document.getElementById(exitOptionsID).style.display = "none"
}

function saveChanges(elementID, exitOptionsID, editButtonID, WebAPIUrl, fileToEdit) {
    newTextString = "";
    var jsonObject = {
        "editedItem" : elementID, "newText" : newTextString
    };
    
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        console.log(this.status);
        if (this.readyState == 4 && this.status == 201) {
            console.log(this.responseText);
            console.log("above is the response text");
        }
    };
    newText = document.getElementById(elementID).innerHTML;
    console.log(WebAPIUrl + "/EditText?newText=" + newText + "&elementID=" + elementID + "&fileToEdit=" + fileToEdit);
    console.log("above is the request");
    xhttp.open("POST", WebAPIUrl + "/EditText?newText=" + newText + "&elementID=" + elementID + "&fileToEdit=" + fileToEdit, true);
    xhttp.send();
    editor = editorDict[elementID][0];
    OGContent = editorDict[elementID][1];
    if (editor) {
        editor.destroy();
    }
    document.getElementById(elementID).contentEditable = "false";
    document.getElementById(editButtonID).style.display = "block";
    document.getElementById(exitOptionsID).style.display = "none"


}