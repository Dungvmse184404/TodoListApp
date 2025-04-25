using BLL.Interfaces;
using Models.DTOs;
using Models.Entities;
using static BLL.Utilities.Validators.ValidateDataType;


namespace BLL.Utilities.Validators
{
    public class ValidateTodoTask
    {
        private readonly ITodoTaskService _todoTaskSer;
        private readonly ILabelService _labelSer;
        public ValidateTodoTask(ITodoTaskService todoTaskSer, ILabelService labelSer)
        {
            _todoTaskSer = todoTaskSer;
            _labelSer = labelSer;
        }
        public async Task<TodoTaskDto> ValidateTaskDto(TodoTaskDto taskDto)
        {
            ValidateTitle(taskDto.Title);

            ValidateDate(ValidateDateFormat(taskDto.StartDate),
                         ValidateDateFormat(taskDto.DueDate),
                         (DateTime)taskDto.CreatedDate);

            ValidateStatus(taskDto.Status);

            if (taskDto.LabelId != null)
            {
                await ValidateLabel((int)taskDto.LabelId);
            }
            return taskDto;
        }

        public async Task<TodoTask> ValidateUpdateTodoTaskDto(TodoTaskDto todoTaskDto, int Id)
        {
            var todoTask = await _todoTaskSer.GetTodoTaskByIdAsync(Id);
            if (todoTask == null)
            {
                throw new Exception("Label không tồn tại");
            }
            await ValidateTaskDto(todoTaskDto);
            return todoTask;
        }

        //--------------------------------------------------------
        public async Task ValidateLabel(int LabelId)
        {
            var label = await _labelSer.GetLabelByIdAsync(LabelId);
            if (label == null)
            {
                throw new Exception("Label không tồn tại");
            }
        }

        
    }
}
