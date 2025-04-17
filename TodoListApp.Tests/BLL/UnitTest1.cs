

using BLL.Services;
using BLL.Interfaces;

using Microsoft.EntityFrameworkCore;
using Moq;
using Models.Enities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TodoListApp.Tests.BLL
{
    public class LabelServiceTests
    {
        private readonly Mock<ILabelRepository> _labelRepositoryMock;
        private readonly LabelService _labelService;
        public LabelServiceTests()
        {
            _labelRepositoryMock = new Mock<ILabelRepository>();
            _labelService = new LabelService(_labelRepositoryMock.Object);
        }
        [Fact]
        public async Task GetAllLabelsAsync_ShouldReturnAllLabels()
        {
            var labels = new List<Label>
            {
                new Label { LabelId = 1, LabelName = "Work", CreatedDate = DateTime.MinValue, DueDate =  },
                new Label { LabelId = 2, LabelName = "Personal" }
            };
            _labelRepositoryMock.Setup(repo => repo.GetAllLabelsAsync()).ReturnsAsync(labels);

            var result = await _labelService.GetAllLabelsAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal("Work", result[0].Name);
            Assert.Equal("Personal", result[1].Name);
        }
       
    }



}