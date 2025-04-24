using AutoMapper;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            {
                //Thêm cấu hình ánh xạ chỗ này ...
                CreateMap<Label, LabelDto>();
                CreateMap<LabelDto, Label>();
                CreateMap<DailyTaskDto, DailyTask >();
            }
        }
    }
}
