using AverageLatencyApplication.Models.Dtos;
using AverageLatencyApplication.Validators;
using FluentValidation.TestHelper;

namespace AverageLatencyApplicationTests.Validators
{
    public class DateValidatorTests
    {

        private readonly DateValidator _validator;

        public DateValidatorTests()
        {
            _validator = new DateValidator();
        }

        [Fact]
        public void WrongFormat_Should_NotBeAllowed()
        {
            var input = new DateDto("2021/01/01", "2021/01/03");

            var result = _validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(d => d.StartDate);
            result.ShouldHaveValidationErrorFor(d => d.EndDate);
        }
    }
}
