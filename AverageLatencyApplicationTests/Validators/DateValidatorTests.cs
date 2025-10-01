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

        [Fact]
        public void ValidDates_Should_PassValidation()
        {
            var input = new DateDto("2021-01-01", "2021-01-10");

            var result = _validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void StartDateBeforeAllowedRange_Should_NotBeAllowed()
        {
            var input = new DateDto("2020-12-31", "2021-01-02");

            var result = _validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(d => d.StartDate);
        }

        [Fact]
        public void EndDateAfterAllowedRange_Should_NotBeAllowed()
        {
            var input = new DateDto("2021-12-30", "2022-01-01");

            var result = _validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(d => d.EndDate);
        }

        [Fact]
        public void EndDateBeforeStartDate_Should_NotBeAllowed()
        {
            var input = new DateDto("2021-01-10", "2021-01-05");

            var result = _validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(d => d.EndDate);
        }
    }
}
