// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//var searchInp = document.getElementById("searchInp");

//searchInp.addEventListener("keyup", function () {

//    var searchValue = searchInp.value;

//    var xhr = new XMLHttpRequest();

//    xhr.open("Get", `https://localhost:7072/Employee?search=${searchValue}`);

//    xhr.send();

//    xhr.onreadystatechange = function () {
//        if (xhr.readyState == XMLHttpRequest.DONE) {
//            if (xhr.status == 200) {
//                //document.body.innerHTML = xhr.responseText;
//                document.getElementById("employeeList").innerHTML = xhr.responseText;
//            }
//            else {
//                alert('Error was happened');
//            }
//        }
//    }

//});

const SearchBar = $('#searchInp');
const table = $('table');

SearchBar.on('keyup', function (event) {
    var searchval = SearchBar.val();

    $.ajax({
        data: { search: searchval },
        url: '/Employee/Search',
        type: 'Get',
        success: function (result) {
            table.html(result)
        },
        error: function (xhr, status, error) {
            console.log(error)
        }
    })
})