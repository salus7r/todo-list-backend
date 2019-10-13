using System;
using TodoApp.Enums;

namespace TodoApp.Models {
    public class UpdateTaskItemReq {
        public Guid TodoItemId { get; set; }
        public Guid TaskItemId { get; set; }
        public string Title { get; set; }
        public StatusType Status { get; set; }

        /// <summary>
        /// Alternative to Extensions
        /// </summary>
        /// <returns></returns>
        public TaskItem MapTaskItemToDbResponse() {
            return new TaskItem {
                Id = TaskItemId,
                Title = Title,
                Status = Status,
                CreatedDate = DateTime.Now
            };
        }
    }
}
