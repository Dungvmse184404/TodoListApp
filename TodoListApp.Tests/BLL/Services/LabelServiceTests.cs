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
                new Label { LabelId = 1, LabelName = "Công việc quan trọng" },
                new Label { LabelId = 2, LabelName = "Ý tưởng mới", CreatedDate = DateTime.Now.AddDays(-7) },
                new Label { LabelId = 3, LabelName = "Việc cá nhân" },
                new Label { LabelId = 4, LabelName = "Dự án A", CreatedDate = new DateTime(2024, 1, 15)},
                new Label { LabelId = 5, LabelName = "Thử nghiệm các trường null", CreatedDate = null }
            };
        }




        //--------------------------- Test GetAllLabelsAsync ------------------------
        [Theory]
        [InlineData(1, "Công việc quan trọng" )]
        [InlineData(2, "Ý tưởng mới")]
        [InlineData(3, "Việc cá nhân")]
        [InlineData(4, "Dự án A")]
        [InlineData(5, "Thử nghiệm các trường null")]
        public async Task GetAllLabelsAsync_ShouldReturnAllLabels(int labelId, string name)
        {
            _mockLabelRepository.Setup(repo => repo.GetAllLabelsAsync()).ReturnsAsync(_labels);
            var result = await _labelService.GetAllLabelsAsync();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count);

            var label = result.Find(l => l.LabelId == labelId);
            Assert.NotNull(label);
            Assert.Equal(name, label.LabelName);
        }

        //--------------------------- Test GetLabelByIdAsync ------------------------
        [Theory]
        [InlineData(1, "Công việc quan trọng")]
        [InlineData(2, "Ý tưởng mới")]
        [InlineData(5, "Thử nghiệm các trường null")]
        public async Task GetLabelByIdAsync_ShouldReturnLabel_WhenLabelExists(int labelId, string expectedLabelName)
        {
            var label = _labels.FirstOrDefault(l => l.LabelId == labelId);
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
            var label = _labels.FirstOrDefault(l => l.LabelId == labelId);
            _mockLabelRepository.Setup(repo => repo.GetLabelByIdAsync(labelId)).ReturnsAsync(label);

            var result = await _labelService.GetLabelByIdAsync(labelId);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public async Task GetLabelByIdAsync_ShouldReturnNull_WhenLabelDoesExist(int labelId)
        {
            var label = _labels.FirstOrDefault(l => l.LabelId == labelId);
            _mockLabelRepository.Setup(repo => repo.GetLabelByIdAsync(labelId)).ReturnsAsync(label);

            var result = await _labelService.GetLabelByIdAsync(labelId);

            Assert.NotNull(result);
        }
        //------------------------------ Test DeleteLabelAsync -------------------------

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task DeleteLabelAsync_ShouldReturnNull_AfterDeletion(int labelId)
        {
            var label = _labels.FirstOrDefault(l => l.LabelId == labelId);

            _mockLabelRepository.Setup(repo => repo.DeleteLabelAsync(labelId)).ReturnsAsync(label);
            var result = await _labelService.DeleteLabelAsync(labelId);

            _mockLabelRepository.Setup(repo => repo.GetLabelByIdAsync(labelId)).ReturnsAsync((Label)null);
            Assert.NotNull(result);

            var afterDelete = await _labelService.GetLabelByIdAsync(labelId);

            Assert.Null(afterDelete);
        }

    }
}
