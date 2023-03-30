const revealElements = document.querySelectorAll("[data-reveal]");

const scrollReveal = function () {
    for (let i = 0; i < revealElements.length; i++) {
        const isElementsOnScreen = revealElements[i].getBoundingClientRect().top < window.innerHeight;

        if (isElementsOnScreen) {
            revealElements[i].classList.add("revealed")
        } else {
            revealElements[i].classList.remove("revealed")
        }
    }
}

window.addEventListener("scroll", scrollReveal);
window.addEventListener("load", scrollReveal);

//const priceFilter = document.getElementById('price-filter');
//const priceOutput = document.getElementById('price-output');

var lowerSlider = document.querySelector('#lower');
var upperSlider = document.querySelector('#upper');

document.querySelector('#two').value = upperSlider.value;
document.querySelector('#one').value = lowerSlider.value;

var lowerVal = parseInt(lowerSlider.value);
var upperVal = parseInt(upperSlider.value);

upperSlider.oninput = function () {
    lowerVal = parseInt(lowerSlider.value);
    upperVal = parseInt(upperSlider.value);

    if (upperVal < lowerVal + 4) {
        lowerSlider.value = upperVal - 4;
        if (lowerVal == lowerSlider.min) {
            upperSlider.value = 4;
        }
    }
    document.querySelector('#two').value = this.value
};

