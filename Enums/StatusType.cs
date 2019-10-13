using System.ComponentModel;

namespace TodoApp.Enums {
    public enum StatusType {
        [Description("When Task is new or not completed")]
        Active = 1,

        [Description("When Task is completed")]
        Completed = 2
    }
}
