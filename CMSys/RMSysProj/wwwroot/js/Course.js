document.querySelectorAll(".select").forEach(item => {
    item.addEventListener("change", e => {
        var nameParamener = e.target.getAttribute("name-parameter");
        var namePath = e.target.getAttribute("name-path");
        var Url = new URL(`${window.location.origin}${namePath}${window.location.search}`);

        if (e.target.value) {
            Url.searchParams.set(nameParamener, e.target.value);
        }
        else {
            Url.searchParams.delete(nameParamener);
        }

        location = Url;
    })
})

$('body').on('click', '.link', function (e) {
    document.location = `${e.target.getAttribute("href")}${ location.search}`;
    return false;
})