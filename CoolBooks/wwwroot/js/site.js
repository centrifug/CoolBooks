// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Sökfunktion för authors i create-book
$(document).ready(function () {
    $('#filterAuthor').keyup(function () {
        var query = this.value;

        $(".author").each(function () {

            $(this).hide();
            if ($(this).text().toLowerCase().indexOf(query.toLowerCase()) != -1) {
                $(this).show();
            }
        });

    });
});

// Sökfunktion för Genre i create-book
$(document).ready(function () {
    $('#filterGenre').keyup(function () {
        var query = this.value;

        $(".genre").each(function () {

            $(this).hide();
            if ($(this).text().toLowerCase().indexOf(query.toLowerCase()) != -1) {
                $(this).show();
            }
        });

    });
});

// Sortera tabel funktion 
//(saxad från W3Sschools och modifierad för att hantera både string och number sortering)
//type 1 = string, 0 = nummer
function sortTable(n, type)
{
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("sortableTable");
    switching = true;
    //Set the sorting direction to ascending:
    dir = "asc";
    /*Make a loop that will continue until
    no switching has been done:*/
    while (switching)
    {
        //start by saying: no switching is done:
        switching = false;
        rows = table.rows;
        /*Loop through all table rows (except the
        first, which contains table headers):*/
        for (i = 1; i < (rows.length - 1); i++) {
            //start by saying there should be no switching:
            shouldSwitch = false;
            /*Get the two elements you want to compare,
            one from current row and one from the next:*/
            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];
            /*check if the two rows should switch place,
            based on the direction, asc or desc:*/

            /*type 1 = Sort by STRING*/
            if(type == 1) {
                if (dir == "asc") {
                    if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
                        //if so, mark as a switch and break the loop:
                        shouldSwitch = true;
                    break;
                }
                } else if (dir == "desc") {
                    if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
                        //if so, mark as a switch and break the loop:
                        shouldSwitch = true;
                        break;
                    }
                }
            }

            /*type 0 = Sort by NUMBER*/
            else if(type == 0) {              
                if (dir == "asc") {                  
                    if (Number(x.innerHTML) > Number(y.innerHTML)) {
                        //if so, mark as a switch and break the loop:
                        shouldSwitch = true;
                        break;
                    }
                    } else if (dir == "desc") {
                        if (Number(x.innerHTML) < Number(y.innerHTML)) {
                            //if so, mark as a switch and break the loop:
                            shouldSwitch = true;
                        break;
                    }
                }
            }

        }

        if (shouldSwitch) {
            /*If a switch has been marked, make the switch
            and mark that a switch has been done:*/
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            //Each time a switch is done, increase this count by 1:
            switchcount ++;      
        } else
        {
            /*If no switching has been done AND the direction is "asc",
            set the direction to "desc" and run the while loop again.*/
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}
