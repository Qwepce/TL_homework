using WebAPI.Application.ResultPattern;
using WebAPI.Application.UseCases.Properties.BaseValidator;

namespace WebAPI.Application.UseCases.Properties.Commands.CreateProperty;

public class CreatePropertyCommandValidator : BasePropertyCommandsValidator<CreatePropertyCommand>
{
    public override Task<Result> Validate( CreatePropertyCommand command )
    {
        return base.Validate( command );
    }
}