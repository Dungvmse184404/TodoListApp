using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BLL.Utilities.Validators;

namespace TodoListApp.Tests.Utilities
{
    public class ValidatorsTest
    {
        [Theory]
        [InlineData("Pending", "Pending")]
        [InlineData("inprogress", "InProgress")]
        [InlineData("  Completed  ", "Completed")]
        [InlineData(" CANCELLED ", "Cancelled")]
        [InlineData("some rand0m words", null)] 
        [InlineData("", null)] 
        [InlineData(" ", null)]
        public void ValidateStatus_ShouldReturnValidEnumOrNull(string status, string expected)
        {
            var result = ValidateDataType.ValidateStatus(status);
            Assert.Equal(expected, result);
        }
    }
}
