using BLL.Interfaces;
using Models.DTOs;
using static BLL.Utilities.Validators.ValidateDataType;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;

namespace BLL.Utilities.Validators
{
    public class ValidateSubTask
    {
        private readonly ITodoTaskRepository _todoTaskRepo;
        private readonly ISubTaskRepository _subTaskRepo;

        public ValidateSubTask(ITodoTaskRepository todoTaskRepo, ISubTaskRepository subTaskRepo)
        {
            _todoTaskRepo = todoTaskRepo;
            _subTaskRepo = subTaskRepo;
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
            var todoTask = await _todoTaskRepo.GetTodoTaskByIdAsync(todoTaskId);
            if (todoTask == null)
            {
                throw new Exception("Task không tồn tại");
            }
        }
       

        public async Task FindSubTask(int subTaskId)
        {
            var subTask = await _subTaskRepo.GetSubTaskByIdAsync(subTaskId);
            if (subTask == null)
            {
                throw new Exception("SubTask không tồn tại");
            }
        }
    }
}
