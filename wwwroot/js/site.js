let activeImage = 0;
const slider = document.getElementById('image-slider');

function changeImage(index) {
    activeImage = index;
    updateSlider();
    console.log("yuju");
}

function updateSlider() {
    const newPosition = activeImage * slider.clientWidth;
    slider.scrollLeft = newPosition;
}