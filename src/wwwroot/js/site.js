// Initialize AOS
AOS.init({
    duration: 1000,
    easing: 'ease-in-out',
    once: true,
    mirror: false
});

// Initialize Isotope
document.addEventListener('DOMContentLoaded', function() {
    let portfolioContainer = document.querySelector('.portfolio .isotope-container');
    if (portfolioContainer) {
        let portfolioIsotope = new Isotope(portfolioContainer, {
            itemSelector: '.portfolio-item',
            layoutMode: 'masonry'
        });

        let portfolioFilters = document.querySelectorAll('.portfolio .isotope-filters li');
        portfolioFilters.forEach(function(filter) {
            filter.addEventListener('click', function(e) {
                e.preventDefault();
                portfolioFilters.forEach(function(el) {
                    el.classList.remove('filter-active');
                });
                this.classList.add('filter-active');

                portfolioIsotope.arrange({
                    filter: this.getAttribute('data-filter')
                });
            });
        });
    }
});

// Initialize Swiper for portfolio details
document.addEventListener('DOMContentLoaded', function() {
    new Swiper('.portfolio-details-slider', {
        speed: 400,
        loop: true,
        autoplay: {
            delay: 5000,
            disableOnInteraction: false
        },
        pagination: {
            el: '.swiper-pagination',
            type: 'bullets',
            clickable: true
        }
    });
});

// Initialize GLightbox
document.addEventListener('DOMContentLoaded', function() {
    GLightbox({
        selector: '.glightbox'
    });
});
