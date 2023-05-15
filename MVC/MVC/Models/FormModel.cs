namespace MVC.Models
{
    public class FormModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; } = string.Empty;
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
        public Guid? Guid { get; set; }
        
    }
    
}
