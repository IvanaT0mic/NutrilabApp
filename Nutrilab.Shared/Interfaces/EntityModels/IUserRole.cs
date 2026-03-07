namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IUserRole
    {
        long UserId { get; }

        IUser User { get; }

        int RoleId { get; }

        IRole Role { get; }
    }
}
