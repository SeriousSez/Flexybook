// Enables horizontal scrolling with the mouse wheel for elements with the class 'image-gallery-inner'.
window.enableGalleryDragScroll = function () {
    window.enableHorizontalWheelScroll(document.querySelector('.image-gallery-inner'));
};

window.enableHorizontalWheelScroll = function (element) {
    if (!element) return;
    // Remove previous listeners if any
    element.onwheel = null;
    element.addEventListener('wheel', function (e) {
        if (e.deltaY === 0) return;
        e.preventDefault();
        element.scrollLeft += e.deltaY;
    }, { passive: false });
};
