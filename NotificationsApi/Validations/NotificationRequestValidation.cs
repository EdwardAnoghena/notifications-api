using FluentValidation;
using NotificationsApi.V1.Boundary.Request;
using System;

namespace NotificationsApi.Validations
{
    public class NotificationRequestValidation : AbstractValidator<NotificationRequest>
    {
        public NotificationRequestValidation()
        {
            RuleFor(p => p.TargetId).NotNull().NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.TargetType).NotNull().IsInEnum().WithMessage("{PropertyName} is required.");
        }
    }

}
