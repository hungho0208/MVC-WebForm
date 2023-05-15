using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Model
{
    public class FormModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 生日日期
        /// </summary>
        public string strBirthday { get; set; } 
        /// <summary>
        /// 每筆資料PK
        /// </summary>
        public Guid Guid { get; set; }
        public List<SaveForm> listSaveForm { get; set; } 
    }
     /// <summary>
    /// 暫存表單
    /// </summary>
    [Serializable]
    public class SaveForm
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } 
        /// <summary>
        /// 生日
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 生日日期
        /// </summary>
        public string strBirthday { get; set; }
        /// <summary>
        /// 每筆資料PK
        /// </summary>
        public Guid Guid { get; set; }
    }
    
}