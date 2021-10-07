
using System;
using System.Globalization;

namespace WpfApp.Models
{
    public class SocialSecurityNumber
    {
        public bool IsValid { get; private set; }

        public string SSN
        {
            get => ssn;
            set { SetSSN(value); }
        }

        private string ssn;

        public DateTime BirthDate { get; private set; }

        public string Identification { get; private set; }

        public SocialSecurityNumber(string ssn)
        {
            SetSSN(ssn);
        }

        private void SetSSN(string ssn)
        {
            this.ssn = string.Empty;
            BirthDate = DateTime.MinValue;
            Identification = string.Empty;

            if (ssn.Length != 12)
            {
                IsValid = false;
            }
            else
            {
                try
                {
                    string temp = ssn[0..^4];
                    BirthDate = DateTime.ParseExact(temp, "yyyyMMdd", CultureInfo.InvariantCulture);
                    Identification = ssn[^4..];
                    if (Verify(ssn))
                    {
                        this.ssn = ssn;
                        IsValid = true;
                    }
                }
                catch (Exception)
                {
                    IsValid = false;
                }
            }
        }

        private static bool Verify(string ssn)
        {
            string tempSSN = ssn[2..];
            int sum = 0;
            for (int i = 0; i < tempSSN.Length; i++)
            {
                int digit = int.Parse(tempSSN[i].ToString());
                if (i % 2 == 0)
                {
                    digit = digit * 2;
                }
                sum += digit / 10 + digit % 10;
            }
            return sum % 10 == 0;
        }

        public override string ToString() => ssn;
    }
}
