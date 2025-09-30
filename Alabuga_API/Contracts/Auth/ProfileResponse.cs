namespace Alabuga_API.Contracts.Auth;

public record ProfileResponse(
    string Image,
    string FirstName,
    string LastName,
    string Surname,
    int Exp,
    int Energy,
    string RankName,
    string Email,
    string Phone,
    string Direction
);