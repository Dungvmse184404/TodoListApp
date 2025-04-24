using BLL.Services;
using BLL.Utilities.Validators;
using DAL.Interfaces;
using Models.DTOs;
using Models.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListApp.Tests.Utilities
{
    public class ValidateLabelTest
    {
        private readonly Mock<ILabelRepository> _mockLabelRepository;
        private readonly LabelService _labelService;
        private readonly List<LabelDto> _labelDtos;

        public ValidateLabelTest()
        {
            _mockLabelRepository = new Mock<ILabelRepository>();
            _labelDtos = new List<LabelDto>
            {
                new LabelDto { LabelName = "Công việc quan trọng", Status = "InProgress" },
                new LabelDto { LabelName = "Ý tưởng mới", CreateDate = DateTime.Now, StartDate = DateTime.Now.AddDays(-1), Status = "Pending" },
                new LabelDto { LabelName = "Ý tưởng mới", CreateDate = DateTime.Now, DueDate = DateTime.Now.AddDays(-1), Status = "Pending" },
                new LabelDto { LabelName = "Việc cá nhân", StartDate = DateTime.Now, DueDate = DateTime.Now.AddDays(3), Status = "Pending" },
            };
        }

        //[Theory]
        //[InlineData(_labelDtos[0], "")]
        //[InlineData(_labelDtos[1], "")]
        //[InlineData(_labelDtos[2], "")]
        //[InlineData(_labelDtos[3], "")]
        //public void ValidateLabel_ShouldReturnExceptionOrObject(LabelDto labelDto, Exception expected)
        //{
        //    var result = ValidateLabel.ValidatelabelDto(labelDto);
        //    Assert.Equal(expected, result);
        //}



    }
}
