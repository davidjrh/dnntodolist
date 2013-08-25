using System;

namespace DavidRodriguez.Modules.TodoItems.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class TodoItem
    {
        public int ModuleId { get; set; }
        public int ItemId { get; set; }
        public string Content { get; set; }
        public bool Complete { get; set; }
        public int CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
    }

}