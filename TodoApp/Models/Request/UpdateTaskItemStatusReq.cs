using System;
using TodoApp.Enums;

namespace TodoApp.Models {
    public class UpdateTaskItemStatusReq {
        public Guid TodoItemId { get; set; }
        public Guid TaskItemId { get; set; }
        public StatusType Status { get; set; }
    }
}
