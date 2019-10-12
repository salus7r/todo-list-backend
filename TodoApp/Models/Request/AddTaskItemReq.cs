using System;
using TodoApp.Enums;

namespace TodoApp.Models {
    public class TaskItemReq {
        public string Title { get; set; }
        public StatusType Status { get; set; }
    }

    public class AddTaskItemReq {
        public Guid TodoItemId { get; set; }
        public string Title { get; set; }
        public StatusType Status { get; set; }

        /// <summary>
        /// Alternative to Extensions
        /// </summary>
        /// <returns></returns>
        public TaskItem MapTaskItemToDbResponse() {
            return new TaskItem {
                Id = Guid.NewGuid(),
                Title = Title,
                Status = Status,
                CreatedDate = DateTime.Now
            };
        }
    }
}
