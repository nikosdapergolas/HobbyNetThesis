//const postContainer = document.getElementById('postContainer');
//const postCardDeck = document.getElementById('postCardDeck');
//const loadingSpinner = document.getElementById('loadingSpinner');

//let currentPage = 1;
//let isLoading = false;

//async function loadMorePosts() {
//    if (isLoading) return;

//isLoading = true;
//loadingSpinner.style.display = 'block';

//try {
//        const response = await fetch(`/api/Posts?page=${currentPage}`);
//const newPosts = await response.json();

//        if (newPosts.length > 0) {
//    currentPage++;
//            newPosts.forEach(post => {
//                // Create and append the new post cards
//                const card = document.createElement('div');
//card.className = 'card mb-4';
//// Construct the card HTML content using post data
//// ...
//postCardDeck.appendChild(card);
//            });
//        }
//    } catch (error) {
//    console.error('Error loading more posts:', error);
//    }

//isLoading = false;
//loadingSpinner.style.display = 'none';
//}

//// Add an event listener to trigger loading more posts when scrolling
//postContainer.addEventListener('scroll', () => {
//    const scrollPosition = postContainer.scrollTop + postContainer.clientHeight;
//const contentHeight = postContainer.scrollHeight;

//    if (scrollPosition >= contentHeight - 100) {
//    loadMorePosts();
//    }
//});

//// Load initial set of posts on page load
//loadMorePosts();

// Add event listener for scroll
//document.getElementById("postContainer").addEventListener("scroll", (event) => {
//    DotNet.invokeMethodAsync("YourAssemblyName", "OnScroll", event);
//});
