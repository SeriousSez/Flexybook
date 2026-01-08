// Enables horizontal scrolling with the mouse wheel for any element passed to it.
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
