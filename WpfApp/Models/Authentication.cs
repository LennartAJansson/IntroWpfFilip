using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace WpfApp.Models
{
    //https://social.technet.microsoft.com/wiki/contents/articles/25726.wpf-implementing-custom-authentication-and-authorization.aspx

    public interface IAuthenticationService
    {
        AuthUser AuthenticateUser(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private class InternalUserData
        {
            public InternalUserData(string username, string email, string hashedPassword, string[] roles)
            {
                Username = username;
                Email = email;
                HashedPassword = hashedPassword;
                Roles = roles;
            }
            public string Username
            {
                get;
                private set;
            }

            public string Email
            {
                get;
                private set;
            }

            public string HashedPassword
            {
                get;
                private set;
            }

            public string[] Roles
            {
                get;
                private set;
            }
        }

        private static readonly List<InternalUserData> users = new()
        {
            new InternalUserData("Mark", "mark@company.com", "MB5PYIsbI2YzCUe34Q5ZU2VferIoI4Ttd+ydolWV0OE=", new string[] { "Administrators" }),
            new InternalUserData("John", "john@company.com", "hMaLizwzOQ5LeOnMuj+C6W75Zl5CXXYbwDSHWW9ZOXc=", Array.Empty<string>())
        };

        public AuthUser AuthenticateUser(string username, string clearTextPassword)
        {
            InternalUserData userData = users.FirstOrDefault(u => u.Username.Equals(username)
                && u.HashedPassword.Equals(CalculateHash(clearTextPassword, u.Username)));

            if (userData == null)
                throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

            return new AuthUser(userData.Username, userData.Email, userData.Roles);
        }

        private static string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }
    }

    public class AuthUser
    {
        public AuthUser(string username, string email, string[] roles)
        {
            Username = username;
            Email = email;
            Roles = roles;
        }
        public string Username
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string[] Roles
        {
            get;
            set;
        }
    }
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string name, string email, string[] roles)
        {
            Name = name;
            Email = email;
            Roles = roles;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string[] Roles { get; private set; }

        #region IIdentity Members
        public string AuthenticationType { get { return "Custom authentication"; } }

        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
        #endregion
    }

    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity()
            : base(string.Empty, string.Empty, Array.Empty<string>())
        { }
    }

    public class CustomPrincipal : IPrincipal
    {
        private CustomIdentity identity;

        public CustomIdentity Identity
        {
            get { return identity ?? new AnonymousIdentity(); }
            set { identity = value; }
        }

        #region IPrincipal Members
        IIdentity IPrincipal.Identity
        {
            get { return this.Identity; }
        }

        public bool IsInRole(string role)
        {
            return identity.Roles.Contains(role);
        }
        #endregion
    }
}

