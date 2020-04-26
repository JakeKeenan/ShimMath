
window.onload = function () {
    editorDict = {};

}

var editorDict;

function setTextEditor(elementID, exitOptionsID, editButtonID)
{
    //ensure that element is textarea?
    document.getElementById(elementID).contentEditable = "true";
    document.getElementById(elementID).focus();
    CKEDITOR.disableAutoInline = true;
    editorDict[elementID] = [CKEDITOR.inline(elementID), document.getElementById(elementID).innerHTML];

    document.getElementById(editButtonID).style.display = "none";
    document.getElementById(exitOptionsID).style.display = "block"
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

function saveChanges(elementID, exitOptionsID, editButtonID, WebAPIUrl) {
    newTextString = "";
    var jsonObject = {
        "EditedItem" : elementID, "NewText" : newTextString
    };
    console.log(WebAPIUrl);

}