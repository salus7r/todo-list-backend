using System;
using TodoApp.Enums;

namespace TodoApp.Models {
    public class TaskItem {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public StatusType Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
