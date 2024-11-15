mergeInto(LibraryManager.library, {
    SaveFile: function(array, length, fileName) {
        var bytes = new Uint8Array(Module.HEAPU8.buffer, array, length);
        var blob = new Blob([bytes], { type: 'image/png' });
        var link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = UTF8ToString(fileName);
        link.click();
        URL.revokeObjectURL(link.href);
    }
});
