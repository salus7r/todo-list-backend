using System;

namespace TodoApp.Models {
    public class DeleteTaskItemReq {
        public Guid TaskItemId { get; set; }
        public Guid TodoItemId { get; set; }
    }
}
