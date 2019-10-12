using System.ComponentModel;

namespace TodoApp.Enums {
    public enum FilterType {

        [Description("To see all tasks")]
        All = 1,

        [Description("To see only new / active tasks")]
        Active = 2,

        [Description("To see only completed tasks")]
        Completed = 3
    }
}
