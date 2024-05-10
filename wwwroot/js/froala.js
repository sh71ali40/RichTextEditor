var DotnetHelper;
function initializeFroalaEditor(elementId, dotnetHelper) {
    debugger;

    DotnetHelper = dotnetHelper;
    const editor = new FroalaEditor(`#${elementId}`, {
        fileUpload: true,
        fileUploadURL: '/api/froala/file',
        imageUpload: true,
        imageUploadURL: '/api/froala/image',
        imageUploadMethod: 'POST',
        videoUpload: true,
        videoUploadURL: '/api/froala/video',
        videoUploadMethod: 'POST',
        imageManagerLoadURL: '/api/froala/folder',  
        imageManagerPreloader: true, 
        imageManagerPageSize: 20,
        imageManagerDeleteURL: '/api/froala/delete',
        imageManagerDeleteMethod: "DELETE",
        events: {
            'blur': function () {

                DotnetHelper.invokeMethodAsync('UpdateContent', editor.html.get());

            },

        }
    });
}

function getFroalaContent(elementId) {
    const editor = FroalaEditor.INSTANCES.find(inst => inst.el.id === elementId);
    return editor ? editor.html.get() : '';
}

function setFroalaContent(elementId, content) {
    const editor = FroalaEditor.INSTANCES.find(inst => inst.el.id === elementId);
    if (editor) {
        editor.html.set(content);
    }
}