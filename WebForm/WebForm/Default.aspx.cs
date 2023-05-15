using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.Model;

namespace WebForm
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormModel model = new FormModel();
            model.listSaveForm = GetSaveValue();
            GenerateTable(model);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strName = txtName.Text;
            string strAge = txtAge.Text;
            string strBirthday = txtBirthday.Text;
            int number;
            FormModel model = new FormModel();
            litAlertMessage.Visible = false;
            //檢查
            if (string.IsNullOrEmpty(strName))
            {
                litAlertMessage.Text = "<script>alert('姓名為必填');</script>";
                litAlertMessage.Visible = true;
                return;
            }
            if (string.IsNullOrEmpty(strAge))
            {
                litAlertMessage.Text = "<script>alert('年齡為必填');</script>";
                litAlertMessage.Visible = true;
                return;
            }
            else
            {
                if (!int.TryParse(strAge, out number))
                {
                    litAlertMessage.Text = "<script>alert('年齡必須為數字');</script>";
                    litAlertMessage.Visible = true;
                    return;
                }
            }
            if (string.IsNullOrEmpty(strBirthday))
            {
                litAlertMessage.Text = "<script>alert('生日為必填');</script>";
                litAlertMessage.Visible = true;
                return;
            }
            else
            {
                //日期格式驗證
                if (Page.IsValid)
                {
                    model.strBirthday = strBirthday;
                }
            }


            if(btnSubmit.Text =="建立帳號")
            {

                model.Name = strName;
                model.Age = number;
                model.Guid = Guid.NewGuid();

                SaveForm saveForm = new SaveForm();
                saveForm.Name = model.Name;
                saveForm.Age = model.Age;
                saveForm.strBirthday = model.strBirthday;
                saveForm.Guid = model.Guid;

                //取得table暫存資料
                List<SaveForm> listSaveForm = GetSaveValue();


                //將資料塞入model
                listSaveForm.Add(saveForm);

                model.listSaveForm = listSaveForm;
                //將資料暫存在ViewState
                ViewState["listSaveForm"] = listSaveForm;
            }
            else if(btnSubmit.Text == "修改帳號")
            {
                model.Name = strName;
                model.Age = number;
                model.Guid = Guid.Parse(txtGuid.Text);

                //取得table暫存資料
                List<SaveForm> listSaveForm = GetSaveValue();

                var updateSaveForm = listSaveForm.Where(x => x.Guid == model.Guid).FirstOrDefault();

                if(updateSaveForm != null)
                {
                    updateSaveForm.Name = model.Name;
                    updateSaveForm.Age = model.Age;
                    updateSaveForm.strBirthday = model.strBirthday;
                    updateSaveForm.Guid = model.Guid;
                }

                model.listSaveForm = listSaveForm;
                //將資料暫存在ViewState
                ViewState["listSaveForm"] = listSaveForm;
            }


            GenerateTable(model);
            ClearText();
        }
        /// <summary>
        /// 產生動態table
        /// </summary>
        /// <param name="model"></param>
        private void GenerateTable(Model.FormModel model)
        {
            Table table = (Table)phTable.FindControl("dynamicTable");

            if (table != null && model.listSaveForm.Any())
            {
                table.Controls.Clear();
                // 創建表頭行
                TableRow headerRow = new TableRow();
                TableCell headerCell1 = new TableCell();
                headerCell1.Text = "姓名";
                headerRow.Cells.Add(headerCell1);
                TableCell headerCell2 = new TableCell();
                headerCell2.Text = "年齡";
                headerRow.Cells.Add(headerCell2);
                TableCell headerCell3 = new TableCell();
                headerCell3.Text = "生日";
                headerRow.Cells.Add(headerCell3);
                table.Rows.Add(headerRow);

                if (model.listSaveForm !=null)
                {
                    // 創建資料行
                    foreach (var item in model.listSaveForm)
                    {
                        TableRow dataRow = new TableRow();
                        TableCell dataCell1 = new TableCell();
                        dataCell1.Text = item.Name;

                        TableCell dataCell2 = new TableCell();
                        dataCell2.Text = item.Age.ToString();


                        TableCell dataCell3 = new TableCell();
                        dataCell3.Text = item.strBirthday;

                        TableCell dataCell4 = new TableCell();
                        Button btn = new Button();
                        btn.ID = "btnEdit_" + item.Guid;
                        btn.Text = "編輯";
                        btn.CommandArgument = item.Guid.ToString(); // 設定按鈕的 CommandArgument 屬性
                        btn.Click += btnEdit_Click; // 設定按鈕的事件處理函式
                        dataCell4.Controls.Add(btn);

                        Button btn2 = new Button();
                        btn2.ID = "btnDelete_" + item.Guid;
                        btn2.Text = "刪除";
                        btn2.CommandArgument = item.Guid.ToString(); // 設定按鈕的 CommandArgument 屬性
                        btn2.Click += btnDelete_Click; // 設定按鈕的事件處理函式
                        dataCell4.Controls.Add(btn2);

                        TableCell dataCell5 = new TableCell();
                        dataCell5.Text = item.Guid.ToString();
                        dataCell5.Visible = false;

                        dataRow.Cells.Add(dataCell1);
                        dataRow.Cells.Add(dataCell2);
                        dataRow.Cells.Add(dataCell3);
                        dataRow.Cells.Add(dataCell4);
                        dataRow.Cells.Add(dataCell5);
                        table.Rows.Add(dataRow);
                    }

                    // 將表格添加到 PlaceHolder 控件中
                    phTable.Controls.Add(table);
                }
            }
            
           
        }
        /// <summary>
        /// 編輯事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            // 獲取按下的按鈕
            Button btn = (Button)sender;

            // 獲取按鈕的 CommandArgument 屬性
            string rowId = btn.CommandArgument;
            

            //取得table暫存資料
            List<SaveForm> listSaveForm = GetSaveValue();

            var checkSaveForm = listSaveForm.Where(x => x.Guid == Guid.Parse(rowId)).FirstOrDefault();

            if(checkSaveForm != null)
            {
                txtBirthday.Text = checkSaveForm.strBirthday;
                txtAge.Text = checkSaveForm.Age.ToString();
                txtName.Text = checkSaveForm.Name;
                txtGuid.Text = checkSaveForm.Guid.ToString();

                btnSubmit.Text = "修改帳號";
            }
          


        }
        /// <summary>
        /// 刪除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // 獲取按下的按鈕
            Button btn = (Button)sender;

            // 獲取按鈕的 CommandArgument 屬性
            string rowId = btn.CommandArgument;


            //取得table暫存資料
            List<SaveForm> listSaveForm = GetSaveValue();

            var deleteSaveForm = listSaveForm.Where(x => x.Guid == Guid.Parse(rowId)).FirstOrDefault();

            if (deleteSaveForm != null)
            {
                listSaveForm.Remove(deleteSaveForm);
                ViewState["listSaveForm"] = listSaveForm;

                FormModel model = new FormModel();
                model.listSaveForm = GetSaveValue();

                GenerateTable(model);
                ClearText();
            }

        }
        /// <summary>
        /// 取得暫存table目前的資料
        /// </summary>
        /// <returns></returns>
        protected List<SaveForm> GetSaveValue()
        {
            List<SaveForm> listSaveForm = new List<SaveForm>();

            if (ViewState["listSaveForm"] != null)
            {
                listSaveForm = (List<SaveForm>)ViewState["listSaveForm"];
            }
            
            return listSaveForm;
        }
        /// <summary>
        /// 清除輸入框
        /// </summary>
        protected void ClearText()
        {
            txtAge.Text = "";
            txtBirthday.Text = "";
            txtGuid.Text = "";
            txtName.Text = "";
            btnSubmit.Text = "建立帳號";
        }

      
    }

}