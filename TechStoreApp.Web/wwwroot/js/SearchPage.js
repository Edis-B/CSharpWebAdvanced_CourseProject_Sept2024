﻿document.addEventListener('DOMContentLoaded', () => {
    appendPopupContainer();

    addToCart();
    addFavoriting();

    categoriesMenu();
    addImageStockBlocker();
});

async function categoriesMenu() {
    const categories = document.querySelectorAll('#category-filter-div ul li a');
    const model = getModel();

    categories.forEach(optionA => {
        const categoryModel = model.category;

        if (categoryModel && optionA.id == categoryModel) {
            optionA.style.backgroundColor = 'white';
        }

        optionA.addEventListener('click', async () => {
            const response = await fetch(`/Search/Search`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Category: optionA.id,
                    Query: model.query
                })
            });

            const data = await response.json();

            window.location.href = data.redirectUrl;
        });

    });
}