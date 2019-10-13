using System;
using System.Collections.Generic;
using System.Linq;
using TodoApp.Enums;

namespace TodoApp.Models {
    public static class DB {
        public static List<TodoItem> TodoItems = Enumerable.Range(1, 5).Select(index => new TodoItem {
            Id = Guid.NewGuid(),
            Title = $"Todo Item {index}",
            Description = $"Todo Item {index} Description",
            DueDate = DateTime.Now.AddDays(index),
            Status = index % 2 == 0 ? StatusType.Active : StatusType.Completed,
            SubTasks = new List<TaskItem>(),
            CreatedDate = DateTime.Now
        }).ToList();
    }
}
