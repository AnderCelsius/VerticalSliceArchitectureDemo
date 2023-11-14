using FluentAssertions;
using VerticalSliceArchitectureDemo.Domain.Blogs;

namespace VerticalSliceArchitectureDemo.UnitTests.Blogs
{
    public class NameTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldThrow_ArgumentNullException_WhenValueIsInvalid(string? value)
        {
            Name Action() => new(value);

            // Assert
            FluentActions.Invoking(Action).Should().Throw<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");
        }
    }
}