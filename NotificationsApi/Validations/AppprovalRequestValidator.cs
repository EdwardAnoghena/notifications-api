using FluentValidation;
using NotificationsApi.V1.Boundary.Request;

namespace NotificationsApi.Validations
{
    public class ApprovalRequestValidator : AbstractValidator<ApprovalRequest>
    {
        public ApprovalRequestValidator()
        {
            RuleFor(p => p.ApprovalNote).NotEmpty();
            RuleFor(p => p.ApprovalStatus).NotNull().IsInEnum().WithMessage("{PropertyName} is required.");
        }
    }
}
