document.addEventListener('DOMContentLoaded', function() {
    // Feature handling
    const iconFeature = document.getElementById('iconfeature');
    const featuresContainer = document.getElementById('features-container');
    
    if (iconFeature) {
        iconFeature.addEventListener('click', function() {
            const featureItem = document.createElement('div');
            featureItem.className = 'feature-item mt-2 d-flex';
            featureItem.innerHTML = `
                <input type="text" name="Features" class="form-control" placeholder="Enter feature" />
                <button type="button" class="btn btn-danger ms-2 remove-item"><i class="bi bi-trash"></i></button>
            `;
            featuresContainer.appendChild(featureItem);
        });
    }

    // Model handling
    const iconModel = document.getElementById('iconmodel');
    const modelsContainer = document.getElementById('models-container');
    
    if (iconModel) {
        iconModel.addEventListener('click', function() {
            const modelItem = document.createElement('div');
            modelItem.className = 'model-item mt-2 d-flex';
            modelItem.innerHTML = `
                <input type="text" name="Models" class="form-control" placeholder="Enter model" />
                <button type="button" class="btn btn-danger ms-2 remove-item"><i class="bi bi-trash"></i></button>
            `;
            modelsContainer.appendChild(modelItem);
        });
    }

    // Photo handling
    const iconPhoto = document.getElementById('iconphoto');
    const photosContainer = document.getElementById('photos-container');
    
    if (iconPhoto) {
        iconPhoto.addEventListener('click', function() {
            const fileInput = photosContainer.querySelector('input[type="file"]');
            if (fileInput) {
                fileInput.style.display = 'block';
            }
        });
    }

    // Video handling
    const iconVideo = document.getElementById('iconvideo');
    const videosContainer = document.getElementById('videos-container');
    
    if (iconVideo) {
        iconVideo.addEventListener('click', function() {
            const videoItem = document.createElement('div');
            videoItem.className = 'video-item mt-2 d-flex';
            videoItem.innerHTML = `
                <input type="text" name="Videos" class="form-control" placeholder="Enter video URL" />
                <button type="button" class="btn btn-danger ms-2 remove-item"><i class="bi bi-trash"></i></button>
            `;
            videosContainer.appendChild(videoItem);
        });
    }

    // Remove item functionality
    document.addEventListener('click', function(e) {
        if (e.target.closest('.remove-item')) {
            const item = e.target.closest('.feature-item, .model-item, .video-item');
            if (item) {
                item.remove();
            }
        }
    });

    // Form submission handling
    const form = document.querySelector('form');
    if (form) {
        form.addEventListener('submit', function(e) {
            const features = Array.from(document.querySelectorAll('input[name="Features"]'))
                .map(input => input.value.trim())
                .filter(value => value !== '');
            
            const models = Array.from(document.querySelectorAll('input[name="Models"]'))
                .map(input => input.value.trim())
                .filter(value => value !== '');

            const videos = Array.from(document.querySelectorAll('input[name="Videos"]'))
                .map(input => input.value.trim())
                .filter(value => value !== '');

            // Create hidden inputs for the concatenated values
            if (features.length > 0) {
                const hiddenFeatures = document.createElement('input');
                hiddenFeatures.type = 'hidden';
                hiddenFeatures.name = 'Product.Features';
                hiddenFeatures.value = features.join(',');
                form.appendChild(hiddenFeatures);
            }

            if (models.length > 0) {
                const hiddenModels = document.createElement('input');
                hiddenModels.type = 'hidden';
                hiddenModels.name = 'Product.Models';
                hiddenModels.value = models.join(',');
                form.appendChild(hiddenModels);
            }

            if (videos.length > 0) {
                const hiddenVideos = document.createElement('input');
                hiddenVideos.type = 'hidden';
                hiddenVideos.name = 'Product.Videos';
                hiddenVideos.value = videos.join(',');
                form.appendChild(hiddenVideos);
            }
        });
    }
});