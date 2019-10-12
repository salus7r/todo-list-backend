using System;
using System.Collections.Generic;
using TodoApp.Enums;

namespace TodoApp.Models {
    public class TodoItem {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public StatusType Status { get; set; }
        public List<TaskItem> SubTasks { get; set; }
    }
}
