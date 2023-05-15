
///建立表單
function createForm() {
    var form = document.getElementById("form");
    var name = form.elements["nameInput"].value;
    var age = form.elements["ageInput"].value;
    var birthday = form.elements["birthdayInput"].value;
    var guid = form.elements["guidInput"].value;
    var checkguid = guid || null;
   
    var saveForm = getTableArray();
    

    if (checkModalFormError(form)) {
        var value = {
            Name: name,
            Age: age,
            Birthday: birthday,
            listSaveForm: saveForm
        };
        var btn = document.getElementById("btn");
        if (btn.innerText === "建立帳號") {
            $.ajax({
                url: "/Home/CreateForm",
                type: "post",
                data: JSON.stringify(value),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    CreateTable(data);
                    clearInput();
                },
                error: function (e) {
                    console.error("資料訪問失敗")
                }

            });
        } else if (btn.innerText === "修改帳號") {
            var value = {
                Name: name,
                Age: age,
                Birthday: birthday,
                Guid: checkguid,
                listSaveForm: saveForm
            };
            $.ajax({
                url: "/Home/UpdateForm",
                type: "post",
                  data: JSON.stringify(value),
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    CreateTable(data);
                    clearInput();
                },
                error: function (e) {
                    console.error("資料訪問失敗")
                }

            });

        }

        
    }
}
///驗證表單
 function checkModalFormError(formData) {

    let name = form.elements["nameInput"].value;
    let age = form.elements["ageInput"].value;
    let birthday = form.elements["birthdayInput"].value;

    if (name === "") {
        alert("請輸入姓名");
        return false;
    } else if (age === "") {
        alert("請輸入年齡");
        return false;
    } else if (birthday === "") {
        alert("請輸入生日");
        return false;
    }

    if (isNaN(age)) {
        alert("年齡只能輸入數字");
        return false;
    } 

    var regex = /^\d{4}-\d{2}-\d{2}$/;
    if (!regex.test(birthday)) {
        alert("生日格式錯誤");
        return false;
    }
    return true;
}

//清除輸入框資料
function toggleClearButton() {
    var input = document.getElementById("birthdayInput");
    var clearButton = document.querySelector(".clear-btn");

    if (input.value.trim() !== "") {
        clearButton.style.display = "inline-block";
    } else {
        clearButton.style.display = "none";
    }

}
function clearInput() {
    var input = document.getElementById("birthdayInput");
    input.value = "";
    input.focus();
    toggleClearButton();
}
//建立動態表格
function createTrElement(data) {
    // 建立元素
    const $tr = document.createElement("tr");

    // 帶入 data
    $tr.dataset.age = data.age;
    $tr.dataset.name = data.name;
    $tr.dataset.birthday = data.strBirthday;
    $tr.dataset.guid = data.guid;

    $tr.innerHTML = `
                <td  scope="row">${data.name}</td>
                <td class='custom-td text-left' >${data.age}</td>
                <td >${data.strBirthday}</td>
                <td >
                    <div class="col-12">
                        <button
                            type="button"
                            name="table-btn--edit"
                            class="btn btn-secondary btn-sm"
                            data-toggle="modal"
                            data-target="#componentModal">編輯</button>
                        <button
                            type="button"
                            name="table-btn--delete"
                            class="btn btn-danger btn-sm">刪除</button>
                    </div>
                </td>
            `;

    return $tr;
}

function clearInput() {
    var nameInput = document.getElementById("nameInput");
    if (nameInput) {
        nameInput.value = '';
    }
    var ageInput = document.getElementById("ageInput");
    if (ageInput) {
        ageInput.value = '';
    }
    var birthdayInput = document.getElementById("birthdayInput");
    if (birthdayInput) {
        birthdayInput.value = '';
    }
    var guidInput = document.getElementById("guidInput");
    if (guidInput) {
        guidInput.value = '';
    }
    var btn = document.getElementById("btn");
    btn.innerText = "建立帳號";
}
//表單事件
 function tableBtnDelegation(event) {
    const { name } = event.target;

    // 如果沒有點到按鈕，就不做任何事情
    if (!name) return;

    const $closestTr = event.target.closest("tr");
    var saveForm = getTableArray();

    const trData = {
        Name: $closestTr.dataset["name"],
        Age: $closestTr.dataset["age"],
        Birthday: $closestTr.dataset["birthday"],
        Guid: $closestTr.dataset["guid"],
        listSaveForm: saveForm
    };

    switch (name) {
        case "table-btn--edit":

            var nameInput = document.getElementById("nameInput");
            if (nameInput) {
                nameInput.value = $closestTr.dataset["name"];
            }
            var ageInput = document.getElementById("ageInput");
            if (ageInput) {
                ageInput.value = $closestTr.dataset["age"];
            }
            var birthdayInput = document.getElementById("birthdayInput");
            if (birthdayInput) {
                birthdayInput.value = $closestTr.dataset["birthday"];
            }
            var guidInput = document.getElementById("guidInput");
            if (guidInput) {
                guidInput.value = $closestTr.dataset["guid"];
            }
            var btn = document.getElementById("btn");

            if (btn) {
                btn.innerHTML = "修改帳號";
            }


            break;

        case "table-btn--delete":
            $.ajax({
                url: "/Home/DeleteForm",
                type: "post",
                data: JSON.stringify(trData),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                   
                    CreateTable(data);
                },
                error: function (e) {
                    console.error("資料訪問失敗")
                }

            });

          
            break;

        default:
            break;
    }
}
//取得表單陣列
function getTableArray() {
    // 取得容器
    var $table = document.querySelector("#table-data");
    var $tbody = $table.querySelector("tbody");

    var $trArr = $tbody.querySelectorAll("tr");
    var saveForm = [];

    $trArr.forEach($tr => {
        var name = $tr.dataset.name;
        var age = $tr.dataset.age;
        var birthday = $tr.dataset.birthday;
        var guid = $tr.dataset.guid;
        saveForm.push({
            Name: name,
            Age: age,
            Birthday: birthday,
            Guid: guid
        }); // 將值加入陣列
    });

    return saveForm;
}
function CreateTable(data) {
    var table = document.getElementById("table-data");
    table.style.display = "block";
    $("#row-data").empty();


    data.forEach(model => {
        htmlobj = createTrElement(model);
        $("#row-data").append(htmlobj);
        htmlobj = "";
    });
}