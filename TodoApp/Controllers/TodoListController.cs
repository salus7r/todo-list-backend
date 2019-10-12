using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class TodoAppController : ControllerBase {
        private List<TodoItem> Data = DB.TodoItems;

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoList() {
            return Ok(Data);
        }

        [HttpPost("AddTodoItem")]
        public ActionResult<bool> AddTodoItem(AddTodoItemReq request) {
            try {
                Data.Add(request.MapTodoItemToDbResponse());
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPut("UpdateTodoItem")]
        public ActionResult<bool> UpdateTodoItem(UpdateTodoItemReq request) {
            try {
                var findItemIndex = Data.FindIndex(ind => ind.Id == request.Id);
                if (findItemIndex > -1) {
                    Data[findItemIndex] = request.MapTodoItemToDbResponse();
                    return Ok();
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
                var findItemIndex = Data.FindIndex(ind => ind.Id == request.Id);
                if (findItemIndex > -1) {
                    Data[findItemIndex].Status = request.Status;
                    return Ok();
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
                var findItem = Data.Find(ind => ind.Id == request.Id);
                if (findItem != null) {
                    Data.Remove(findItem);
                    return Ok();
                } else {
                    return NotFound();
                }
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPost("AddTaskItem")]
        public ActionResult<bool> AddTaskItem(AddTaskItemReq request) {
            try {
                var todoItem = Data.FirstOrDefault(a => a.Id == request.TodoItemId);
                if (todoItem != null) {
                    todoItem.SubTasks.Add(request.MapTaskItemToDbResponse());
                }
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPut("UpdateTaskItem")]
        public ActionResult<bool> UpdateTaskItem(UpdateTaskItemReq request) {
            try {
                var todoItem = Data.FirstOrDefault(a => a.Id == request.TodoItemId);
                if (todoItem != null) {
                    var findItemIndex = todoItem.SubTasks.FindIndex(ind => ind.Id == request.TaskItemId);
                    if (findItemIndex > -1) {
                        todoItem.SubTasks[findItemIndex] = request.MapTaskItemToDbResponse();
                        return Ok();
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
                var todoItem = Data.FirstOrDefault(a => a.Id == request.TodoItemId);
                if (todoItem != null) {
                    var findItemIndex = todoItem.SubTasks.FindIndex(ind => ind.Id == request.TaskItemId);
                    if (findItemIndex > -1) {
                        todoItem.SubTasks[findItemIndex].Status = request.Status;
                        return Ok();
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
                var todoItem = Data.FirstOrDefault(a => a.Id == request.TodoItemId);
                if (todoItem != null && todoItem.SubTasks.Any()) {
                    var subItemFind = todoItem.SubTasks.Find(x => x.Id == request.TaskItemId);
                    if(subItemFind != null) {
                        todoItem.SubTasks.Remove(subItemFind);
                        return Ok();
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
