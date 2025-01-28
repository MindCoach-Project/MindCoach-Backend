namespace MinhCoach.Contracts.Authentication;

public record AuthenticationResponse
(
    Guid Id,
    string Username,
    string Phone,
    string Address,
    string ImageUrl,
    string Email,
    string Token,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? DeletedAt
);