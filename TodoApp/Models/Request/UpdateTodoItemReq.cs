using System;
using System.Collections.Generic;
using System.Linq;
using TodoApp.Enums;

namespace TodoApp.Models {
    public class UpdateTodoItemReq {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public StatusType Status { get; set; }
        public List<TaskItemReq> SubTasks { get; set; }

        /// <summary>
        /// Alternative to Extensions
        /// </summary>
        /// <returns></returns>
        public TodoItem MapTodoItemToDbResponse() {
            return new TodoItem {
                Id = Id,
                Title = Title,
                Description = Description,
                DueDate = DueDate,
                Status = Status,
                SubTasks = MapTasksListToDbResponse(SubTasks).ToList()
            };
        }

        /// <summary>
        /// Alternative to Extensions
        /// </summary>
        /// <returns></returns>
        private IEnumerable<TaskItem> MapTasksListToDbResponse(IEnumerable<TaskItemReq> subTasks) {
            foreach (var task in subTasks) {
                yield return new TaskItem {
                    Id = Guid.NewGuid(),
                    Title = task.Title,
                    Status = task.Status
                };
            }
        }
    }
}
