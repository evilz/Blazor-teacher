// Prism.js initialization for Blazor
window.PrismHelper = {
    highlightAll: function () {
        if (window.Prism) {
            window.Prism.highlightAll();
        }
    },
    highlightElement: function (element) {
        if (window.Prism && element) {
            window.Prism.highlightElement(element);
        }
    }
};

