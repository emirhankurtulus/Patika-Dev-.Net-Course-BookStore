namespace Project.Commands;

public class ChangeUserPasswordCommand
{
    public Guid Id { get; set; }

    public string PreviousPassword { get; set; }

    public string NewPassword { get; set; }
}