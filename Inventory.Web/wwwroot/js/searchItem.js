const searchText = document.querySelector("#searchText");
const tableOutput = document.querySelector(".table-output");
const appTable = document.querySelector(".table");
const tableBody = document.querySelector(".table-body");
const notFound = document.querySelector(".not-found");


searchText.addEventListener('keyup', (e) => {

    const search = e.target.value;
    console.log("Value", search);
    appTable.style.display = "none";

    if (search.trim().length > 0) {
        tableBody.innerHTML = "";

        fetch("/itemapi?search="+search).then((res) => res.json())
            .then((data) => {
                console.log('Data', data);

                appTable.style.display = "none";
                tableOutput.style.display = "block";

                if (data.length === 0) {

                    console.log('length', 0);
                    notFound.style.display = "block";
                    tableOutput.style.display = "none";

                } else {

                    console.log('length', data.length);
                    notFound.style.display = "none";

                    data.forEach(item => {

                        tableBody.innerHTML += '<tr><td>' + item.name + '</td><td>' +
                            item.brand + '</td><td>' + item.description + '</td><td>' + item.price + '</td><td>' +
                            item.storedDate + '</td><td>' + item.borrowedDate + '</td><td>' + item.soldDate + '</td><td>' +
                            item.status + '</td><td>' + item.borrower + '</td><td>' +
                            item.buyer + '</td><td>' + item.category.name + '</td></tr>';

                    });

                }

            });

    } else {

        tableOutput.style.display = "none";
        notFound.style.display = "none";
        appTable.style.display = "block";
    }

});