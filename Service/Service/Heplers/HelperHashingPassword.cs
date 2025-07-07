using System.Security.Cryptography;

namespace Service.Heplers
{
    public static class HelperHashingPassword
    {
        private const int SaltSize = 32;
        private const int HashSize = 32;
        private const int Iterations = 100000;
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }

            //Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            Console.WriteLine($"--> Salt: {Convert.ToBase64String(salt)}");

            //Hash the password with the salt

            // Use PBKDF2 to hash the password with the salt
            using var pbkdf2 = new Rfc2898DeriveBytes(password,salt,Iterations,HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);
            Console.WriteLine($"--> Hash: {Convert.ToBase64String(hash)}");

            //Combine the salt and hash 
            byte[] combined = new byte[SaltSize + HashSize];
            // Copy the salt and hash into the combined array
            Buffer.BlockCopy(salt, 0, combined, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, combined, SaltSize, HashSize);
            Console.WriteLine($"--> Combined: {Convert.ToBase64String(combined)}");
            return Convert.ToBase64String(combined);

            //
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentException("--> Password and hashed password cannot be null or empty.");
            }
            try
            {
                //decode the base64 string
                byte[] combined = Convert.FromBase64String(hashedPassword);
                // check if the combined length is valid
                if (combined.Length != SaltSize + HashSize)
                {
                    Console.WriteLine("--> Invalid hashed password format.");
                    return false;
                }
                //Extract the salt and hash
                byte[] salt = new byte[SaltSize];
                byte[] hash = new byte[HashSize];
                // Copy the salt and hash from the combined array
                Buffer.BlockCopy(combined, 0, salt, 0, SaltSize);
                Buffer.BlockCopy(combined, SaltSize, hash, 0, HashSize);

                //Hash the input password with the extracted salt
                using var pbkdf2  = new Rfc2898DeriveBytes(password,salt,Iterations, HashAlgorithmName.SHA256);
                byte[] inputHash = pbkdf2.GetBytes(HashSize);
                return CryptographicOperations.FixedTimeEquals(hash, inputHash);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"--> Error verifying password: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> An unexpected error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
