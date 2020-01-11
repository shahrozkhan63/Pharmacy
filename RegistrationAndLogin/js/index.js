var table = document.getElementById("myTable");

function myCreateFunction() {

  var i = table.rows.length;
  var row = table.insertRow(i);

  for (var j = 0; j < table.rows[0].cells.length - 1; j++) {
    var cell = row.insertCell(j);
    cell.innerHTML = "new name" + i + j;
    table.rows[i].cells[j].addEventListener("click", function() {
      editText(this);
    }, false);
  }

  var cell = row.insertCell(row.cells.length);
  cell.innerHTML = "x";
  cell.classList.add("delete_row");
  cell.addEventListener("click", function() {
    deleteRow(this);
  }, false);
}

function deleteRow(r) {
  var i = r.parentNode.rowIndex;
  document.getElementById("myTable").deleteRow(i);
}

if (table != null) {
  for (var i = 0; i < table.rows.length; i++) {
    for (var j = 0; j < table.rows[i].cells.length - 1; j++) {
      table.rows[i].cells[j].addEventListener("click", function() {
        editText(this);
      }, false);
    }
    var n = table.rows[i].cells.length - 1;
    table.rows[i].cells[n].addEventListener("click", function() {
        deleteRow(this);
      }, false);
  }
}

function editText(tableCell) {
  var txt = tableCell.innerText || tableCell.textContent;
  tableCell.innerText = tableCell.textContent = "";
  var input = document.createElement("input");
  input.type = "text";
  tableCell.appendChild(input);
  input.value = txt;
  input.focus();
  input.onblur = function() {
    tableCell.innerText = input.value;
    tableCell.textContent = input.value;
  }
}

function leaveCell(tableCell) {
  tableCell.innerText = input.value;
  tableCell.textContent = input.value;
}