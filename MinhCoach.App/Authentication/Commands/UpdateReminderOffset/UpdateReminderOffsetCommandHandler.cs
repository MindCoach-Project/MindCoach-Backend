using MediatR;
using ErrorOr;
using MinhCoach.App.Common.Interfaces.Authentication;
using MinhCoach.App.Common.Interfaces.Persistence;
using MinhCoach.App.Common.Response;
using MinhCoach.Domain.Common.Errors;
using MinhCoach.Domain.User;
using AErrors = MinhCoach.App.Common.Errors.Errors;
using MinhCoach.Domain.User.ValueObjects;

namespace MinhCoach.App.Authentication.Commands.UpdateReminderOffset;

public class UpdateReminderOffsetCommandHandler :
    IRequestHandler<UpdateReminderOffsetCommand, ErrorOr<ObjectResponse<User>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    
    public UpdateReminderOffsetCommandHandler(
        IUnitOfWork unitOfWork,
        ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }
    
    public async Task<ErrorOr<ObjectResponse<User>>> Handle(
        UpdateReminderOffsetCommand command,
        CancellationToken cancellationToken)
    {   
        //get userid from token
        var userId = _tokenService.GetUserIdFromToken();
        if (userId == Guid.Empty)
        {
            return AErrors.Authentication.UserIdFromTokenNotFound;
        }
        
        if (await _unitOfWork.UserRepository.GetUserById(
                UserId.Create(userId)) is not User user)
            return Errors.User.NotFound;
        
        //create user
        user.UpdateReminderOffset(command.ReminderOffset);

        await _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
        
        
        return new ObjectResponse<User>(
            "Reminder Offset updated successful", 
                user
        );
    }

}