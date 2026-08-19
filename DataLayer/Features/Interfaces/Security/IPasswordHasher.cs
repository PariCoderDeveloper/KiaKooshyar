namespace KiaKooshar.Application.Construct.Security
{
    public interface IPasswordHasher
    {
        string HashPassword ( string password );
        bool VerifyPassword ( string hashedPassword, string enteredPassword );
        bool NeedsRehash ( string hashedPassword );

    }
}
