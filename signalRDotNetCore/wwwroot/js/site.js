// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(() => {
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();
    connection.start();
    connection.on("LoadProducts", function () {
        LoadProdData();
    })
    LoadProdData();

    function LoadProdData() {
        var tr = '';
        $.ajax({
            url: 'GetProducts',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, t) => {
                    tr += `<tr>
                            <td>${t.Name}</td>
                            <td>${t.price}</td>
                            <td>${t.dateAdded}</td>
                            <td>
                                <a href='../Products/Edit?id=${t.id}'>Edit</a>|
                                <a href='../Products/Details?id=${t.id}'>Details</a>|
                                <a href='../Products/Delete?id=${t.id}'>Delete</a>
                            </td>
                            </tr>`
                })
                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error);
            }
        });
    }
})