using System;
using TodoApp.Enums;

namespace TodoApp.Models {
    public class UpdateTodoItemStatusReq {
        public Guid Id { get; set; }
        public StatusType Status { get; set; }
    }
}
