using MVC.Models;
using System.Globalization;

namespace MVC.Service
{
    public class HomeService
    {
        /// <summary>
        /// 建立表單
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<List<FormModel>> CreateForm(FormReq req)
        {
            FormModel res = new FormModel();
            List<FormModel> listRes = new List<FormModel>();

            //檢查
            if (!string.IsNullOrEmpty(req.Name))
            {
                res.Name = req.Name;
            }
            if(req.Age != 0)
            {
                res.Age = req.Age;
            }
           
            if(!string.IsNullOrEmpty(req.Birthday.ToString("yyyy-MM-dd")))
            {
                res.strBirthday = req.Birthday.ToString("yyyy-MM-dd");

            }


            res.Guid =  Guid.NewGuid();

            if(req.listSaveForm.Any())
            {
                foreach (var item in req.listSaveForm)
                {
                    FormModel model = new FormModel();
                    model.Name = item.Name;
                    model.Age = item.Age;
                    model.strBirthday = item.Birthday.ToString("yyyy-MM-dd");
                    model.Guid = item.Guid;
                    listRes.Add(model);
                }
            }

            listRes.Add(res);

            return  listRes;
        }
        /// <summary>
        /// 刪除表單
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<List<FormModel>> DeleteForm(FormReq req)
        {
           
            List<FormModel> listRes = new List<FormModel>();

            var deleteForm = req.listSaveForm.Where(x => x.Guid == req.Guid).FirstOrDefault();

            if(deleteForm!=null)
            {
                req.listSaveForm.Remove(deleteForm);
            }

            foreach (var item in req.listSaveForm)
            {
                FormModel res = new FormModel();
                res.Name = item.Name;
                res.Age = item.Age;
                res.strBirthday = item.Birthday.ToString("yyyy-MM-dd");
                res.Guid = item.Guid;
                listRes.Add(res);
            }

            return listRes;
        }
        /// <summary>
        /// 編輯表單
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<List<FormModel>> UpdateForm(FormReq req)
        {

            List<FormModel> listRes = new List<FormModel>();

            var updateForm = req.listSaveForm.Where(x => x.Guid == req.Guid).FirstOrDefault();

            if (updateForm != null)
            {
                updateForm.Birthday = req.Birthday;
                updateForm.Name = req.Name;
                updateForm.Age = req.Age;
            }

            foreach (var item in req.listSaveForm)
            {
                FormModel res = new FormModel();
                res.Name = item.Name;
                res.Age = item.Age;
                res.strBirthday = item.Birthday.ToString("yyyy-MM-dd");
                res.Guid = item.Guid;
                listRes.Add(res);
            }

            return listRes;
        }
    }
}
