using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Enums;
using TodoApp.Models;

namespace TodoApp.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class TodoAppController : ControllerBase {
        private List<TodoItem> _data = DB.TodoItems;

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoList([FromQuery]FilterType filterType = FilterType.All) {
            var result = new List<TodoItem>();

            if (filterType == FilterType.Active) {
                result = _data.Where(a => a.Status == StatusType.Active).ToList();
            } else if (filterType == FilterType.Completed) {
                result = _data.Where(a => a.Status == StatusType.Completed).ToList();
            } else {
                result = _data;
            }

            return Ok(result.OrderByDescending(a => a.CreatedDate));
        }

        [HttpPost("AddTodoItem")]
        public ActionResult<TodoItem> AddTodoItem(AddTodoItemReq request) {
            try {
                var newItem = request.MapTodoItemToDbResponse();
                _data.Add(newItem);
                return Ok(newItem);
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPut("UpdateTodoItem")]
        public ActionResult<TodoItem> UpdateTodoItem(UpdateTodoItemReq request) {
            try {
                var findItemIndex = _data.FindIndex(ind => ind.Id == request.Id);
                if (findItemIndex > -1) {
                    var updateItem = request.MapTodoItemToDbResponse();
                    _data[findItemIndex] = updateItem;
                    return Ok(updateItem);
                } else {
                    return NotFound();
                }
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPatch("UpdateTodoItemStatus")]
        public ActionResult<bool> UpdateTodoItemStatus(UpdateTodoItemStatusReq request) {
            try {
                var findItemIndex = _data.FindIndex(ind => ind.Id == request.Id);
                if (findItemIndex > -1) {
                    _data[findItemIndex].Status = request.Status;
                    return Ok(true);
                } else {
                    return NotFound();
                }
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpDelete("DeleteTodoItem")]
        public ActionResult<bool> DeleteTodoItem(DeleteTodoItemReq request) {
            try {
                var findItem = _data.Find(ind => ind.Id == request.Id);
                if (findItem != null) {
                    _data.Remove(findItem);
                    return Ok(true);
                } else {
                    return NotFound();
                }
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPost("AddTaskItem")]
        public ActionResult<TaskItem> AddTaskItem(AddTaskItemReq request) {
            try {
                var todoItem = _data.FirstOrDefault(a => a.Id == request.TodoItemId);
                if (todoItem != null) {
                    var newItem = request.MapTaskItemToDbResponse();
                    todoItem.SubTasks.Add(newItem);
                    return Ok(newItem);
                } else {
                    return NotFound();
                }
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPut("UpdateTaskItem")]
        public ActionResult<TaskItem> UpdateTaskItem(UpdateTaskItemReq request) {
            try {
                var todoItem = _data.FirstOrDefault(a => a.Id == request.TodoItemId);
                if (todoItem != null) {
                    var findItemIndex = todoItem.SubTasks.FindIndex(ind => ind.Id == request.TaskItemId);
                    if (findItemIndex > -1) {
                        var updateItem = request.MapTaskItemToDbResponse();
                        todoItem.SubTasks[findItemIndex] = updateItem;
                        return Ok(updateItem);
                    } else {
                        return NotFound();
                    }
                } else {
                    return NotFound();
                }
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPatch("UpdateTaskItemStatus")]
        public ActionResult<bool> UpdateTaskItemStatus(UpdateTaskItemStatusReq request) {
            try {
                var todoItem = _data.FirstOrDefault(a => a.Id == request.TodoItemId);
                if (todoItem != null) {
                    var findItemIndex = todoItem.SubTasks.FindIndex(ind => ind.Id == request.TaskItemId);
                    if (findItemIndex > -1) {
                        todoItem.SubTasks[findItemIndex].Status = request.Status;
                        return Ok(true);
                    } else {
                        return NotFound();
                    }
                } else {
                    return NotFound();
                }
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpDelete("DeleteTaskItem")]
        public ActionResult<bool> DeleteTaskItem(DeleteTaskItemReq request) {
            try {
                var todoItem = _data.FirstOrDefault(a => a.Id == request.TodoItemId);
                if (todoItem != null && todoItem.SubTasks.Any()) {
                    var subItemFind = todoItem.SubTasks.Find(x => x.Id == request.TaskItemId);
                    if (subItemFind != null) {
                        todoItem.SubTasks.Remove(subItemFind);
                        return Ok(true);
                    } else {
                        return NotFound();
                    }
                } else {
                    return NotFound();
                }
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }
    }
}
