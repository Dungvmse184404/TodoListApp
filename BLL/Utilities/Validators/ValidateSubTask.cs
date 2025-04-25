using BLL.Interfaces;
using Models.DTOs;
using static BLL.Utilities.Validators.ValidateDataType;
using BLL.Services;

namespace BLL.Utilities.Validators
{
    public class ValidateSubTask
    {
        private readonly ITodoTaskService _todoTaskSer = new TodoTaskService();
        private readonly ISubTaskService _subTaskSer = new SubTaskService();
        public ValidateSubTask()
        {
        }

        /// <summary>
        /// kiểm tra các field AddSubTask
        /// </summary>
        /// <param name="subTaskDto"></param>
        /// <returns></returns>
        public async Task<SubTaskDto> ValidateSubTaskDto(SubTaskDto subTaskDto)
        {
            ValidateDescription(subTaskDto.Description, 255);

            if (subTaskDto.TodoTaskId != null)
            {
               await FindTodoTask((int)subTaskDto.TodoTaskId);
            }
            return subTaskDto;
        }

        /// <summary>
        /// kiểm tra các field UpdateSubTask
        /// </summary>
        /// <param name="upSubTaskDto"></param>
        /// <returns></returns>
        public async Task<UpdateSubTaskDto> ValidateUpdateSubTaskDto(UpdateSubTaskDto upSubTaskDto)
        {
            await FindSubTask(upSubTaskDto.SubTaskId);

            ValidateDescription(upSubTaskDto.Description, 255);

            await FindTodoTask((int)upSubTaskDto.TodoTaskId);

            return upSubTaskDto;
        }

        //-------------------------------------------------------------------

        public async Task FindTodoTask(int todoTaskId)
        {
            var todoTask = await _todoTaskSer.GetTodoTaskByIdAsync(todoTaskId);
            if (todoTask == null)
            {
                throw new Exception("Task không tồn tại");
            }
        }
       

        public async Task FindSubTask(int subTaskId)
        {
            var subTask = await _todoTaskSer.GetTodoTaskByIdAsync(subTaskId);
            if (subTask == null)
            {
                throw new Exception("SubTask không tồn tại");
            }
        }
    }
}
