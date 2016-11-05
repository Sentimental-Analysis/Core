using Core.Models;
using FluentAssertions;
using Xunit;

namespace Core.Tests.Models
{
    public class Result
    {
        [Fact]
        public void Test_Wrap_When_Value_Is_Null()
        {
            string value = null;
            var testResult = Result<string>.Wrap(value);
            testResult.Should().NotBeNull();
            testResult.IsSuccess.Should().BeFalse();
            testResult.Value.Should().BeNull();
        }

        [Fact]
        public void Test_Wrap_Value_With_Func_When_Value_Is_Empty_String()
        {
            string value = string.Empty;
            var testResult = Result<int>.Wrap(value, x => !string.IsNullOrEmpty(x));
            testResult.Should().NotBeNull();
            testResult.IsSuccess.Should().BeFalse();
            testResult.Value.Should().Be(value);
        }

        [Fact]
        public void Test_Wrap_Value_With_Func()
        {
            int value = 22;
            var testResult = Result<int>.Wrap(value, x => x == 22);
            testResult.Should().NotBeNull();
            testResult.IsSuccess.Should().BeTrue();
            testResult.Value.Should().Be(value);
        }

        [Fact]
        public void Test_Wrap_When_Value_Is_Not_Null()
        {
            string value = "test";
            var testResult = Result<string>.Wrap(value);
            testResult.Should().NotBeNull();
            testResult.IsSuccess.Should().BeTrue();
            testResult.Value.Should().NotBeNull();
        }

        [Fact]
        public void Test_Wrap_Value()
        {
            int value = 2;
            var testResult = Result<int>.WrapValue(value);
            testResult.Should().NotBeNull();
            testResult.IsSuccess.Should().BeTrue();
            testResult.Value.Should().Be(value);
        }

        [Fact]
        public void Test_Error_Wrap_Value_Type()
        {
            var testResult = Result<int>.Error("Error");
            testResult.Should().NotBeNull();
            testResult.IsSuccess.Should().BeFalse();
            testResult.Value.Should().Be(default(int));
        }

        [Fact]
        public void Test_Error_Wrap_Reference_Type()
        {
            var testResult = Result<string>.Error("Error");
            testResult.Should().NotBeNull();
            testResult.IsSuccess.Should().BeFalse();
            testResult.Value.Should().Be(default(string));
        }
    }
}
}