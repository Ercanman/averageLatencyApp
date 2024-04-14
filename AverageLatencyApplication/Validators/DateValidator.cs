using AverageLatencyApplication.Models.Dtos;
using FluentValidation;
using System.Globalization;

namespace AverageLatencyApplication.Validators
{
    public class DateValidator : AbstractValidator<DateDto>
    {
        public DateValidator()
        {
            RuleFor(dateObject => dateObject.StartDate)
                .NotEmpty()
                .Must(BeAValidDate)
                .WithName("StartDate");

            RuleFor(dateObject => dateObject.EndDate)
                .NotEmpty()
                .Must(BeAValidDate)
                .WithName("EndDate");

            RuleFor(dateObject => DateTime.Parse(dateObject.EndDate))
                .GreaterThanOrEqualTo(dateObject => DateTime.Parse(dateObject.StartDate))
                .WithName("EndDate");

            RuleFor(dateObject => DateTime.Parse(dateObject.StartDate))
                .GreaterThanOrEqualTo(new DateTime(2021, 01, 01))
                .WithName("StartDate");

            RuleFor(dateObject => DateTime.Parse(dateObject.EndDate))
                .LessThanOrEqualTo(new DateTime(2021, 12, 31))
                .WithName("EndDate");

        }

        private bool BeAValidDate(string input)
        {
            var isParseable = DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

            return isParseable;
        }
    }
}
