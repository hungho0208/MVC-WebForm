namespace MVC.Models
{
    public class FormReq
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
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 每筆資料PK
        /// </summary>
        public Guid Guid { get; set; }
        public List<SaveForm> listSaveForm { get; set; } = new List<SaveForm>();
    }
    /// <summary>
    /// 暫存表單
    /// </summary>
    public class SaveForm
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
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 每筆資料PK
        /// </summary>
        public Guid Guid { get; set; }
    }
}
