var button = document.querySelector("#srcButton")
var input = document.querySelector("#srcInput")

button.addEventListener("click", e => {
    var url = new URL(`${location.origin}${location.pathname}`);

    if (input.value) {
        url.searchParams.set("search", input.value);
    }

    location = url;
})

$('body').on('click', '.link', function (e) {
    document.location = `${e.target.getAttribute("href")}${location.search}`;
    return false;
})
