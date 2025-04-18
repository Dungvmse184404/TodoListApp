using BLL.Services;
using DAL.Interfaces;
using Models.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TodoListApp.Tests.BLL.Services
{
    public class LabelServiceTests
    {
        private readonly Mock<ILabelRepository> _mockLabelRepository;
        private readonly LabelService _labelService;
        private readonly List<Label> _labels;

        public LabelServiceTests()
        {
            _mockLabelRepository = new Mock<ILabelRepository>();
            _labelService = new LabelService(_mockLabelRepository.Object);
            _labels = new List<Label>
            {
                new Label { LabelId = 1, LabelName = "Công việc quan trọng", Status = "InProgress" },
                new Label { LabelId = 2, LabelName = "Ý tưởng mới", CreatedDate = DateTime.Now.AddDays(-7), Status = "Pending" },
                new Label { LabelId = 3, LabelName = "Việc cá nhân", StartDate = DateTime.Now, DueDate = DateTime.Now.AddDays(3), Status = "Pending" },
                new Label { LabelId = 4, LabelName = "Dự án A", CreatedDate = new DateTime(2024, 1, 15), StartDate = new DateTime(2024, 2, 1), DueDate = new DateTime(2024, 2, 28), Status = "Completed" },
                new Label { LabelId = 5, LabelName = "Thử nghiệm các trường null", CreatedDate = null, StartDate = null, DueDate = null, Status = "Cancelled" }
            };
        }

        //--------------------------- Test GetAllLabelsAsync ------------------------
        [Theory]
        [InlineData(1, "Công việc quan trọng", "InProgress")]
        [InlineData(2, "Ý tưởng mới", "Pending")]
        [InlineData(3, "Việc cá nhân", "Pending")]
        [InlineData(4, "Dự án A", "Completed")]
        [InlineData(5, "Thử nghiệm các trường null", "Cancelled")]
        public async Task GetAllLabelsAsync_ShouldReturnAllLabels(int labelId, string name, string status)
        {
            _mockLabelRepository.Setup(repo => repo.GetAllLabelsAsync()).ReturnsAsync(_labels);
            var result = await _labelService.GetAllLabelsAsync();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count); 

            var label = result.Find(l => l.LabelId == labelId);
            Assert.NotNull(label);
            Assert.Equal(name, label.LabelName); 
            Assert.Equal(status, label.Status);
        }

        //--------------------------- Test GetLabelByIdAsync ------------------------
        [Theory]
        [InlineData(1, "Công việc quan trọng")]
        [InlineData(2, "Ý tưởng mới")]
        [InlineData(5, "Thử nghiệm các trường null")]
        public async Task GetLabelByIdAsync_ShouldReturnLabel_WhenLabelExists(int labelId, string expectedLabelName)
        {
            var label = new Label { LabelId = labelId, LabelName = expectedLabelName };
            _mockLabelRepository.Setup(repo => repo.GetLabelByIdAsync(labelId)).ReturnsAsync(label);

            var result = await _labelService.GetLabelByIdAsync(labelId);

            Assert.NotNull(result);
            Assert.Equal(labelId, result.LabelId);
            Assert.Equal(expectedLabelName, result.LabelName);
        }

        [Theory]
        [InlineData(-1)] 
        [InlineData(1000)]
        public async Task GetLabelByIdAsync_ShouldReturnNull_WhenLabelDoesNotExist(int labelId)
        {
            _mockLabelRepository.Setup(repo => repo.GetLabelByIdAsync(labelId)).ReturnsAsync((Label)null);

            var result = await _labelService.GetLabelByIdAsync(labelId);

            Assert.Null(result);
        }
        //---------------------------------------------------------------------------
    }
}
