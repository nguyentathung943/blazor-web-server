namespace Models;

public class RegistrationResponseDTO
{
    public bool IsRegisterationSuccessful { get; set; }
    public IEnumerable<string> Errors { get; set; }
}