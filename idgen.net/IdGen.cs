using System;
using System.Text;
using System.Text.RegularExpressions;

namespace idgen.net
{
    public static class IdGen
    {
        private const string NieStartLetters = "XYZ";

        #region Generators
        public static string GenerateDni()
        {
            return GenerateNif();
        }

        public static string GenerateNif()
        {
            var digits = GetRandomDigits(8);
            return digits + CalculateChecksum(digits);
        }

        public static string GenerateNie() {
            var index = new Random().Next(3);
            var digits = GetRandomDigits(7);

            return NieStartLetters[index] + digits + CalculateChecksum(index + digits);
        }
        #endregion

        #region Validators

        public static ValidationResult ValidateDni(string dni)
        {
            return ValidateNif(dni);
        }

        public static ValidationResult ValidateNif(string nif)
        {
            var nifRegex = new Regex("^[0-9]{8}[a-zA-Z]{1}$");
	        if (!nifRegex.IsMatch(nif))
            {
                return ValidationResult.PatternMismatch;
            }
            
            var letter = char.ToUpperInvariant(nif[8]);
            var numbers = nif.Substring(0, 8);
            var expected = CalculateChecksum(numbers);

            if (letter != expected)
            {
                return ValidationResult.ChecksumMismatch;
            } 

            return ValidationResult.Valid;
        }

        public static ValidationResult ValidateNie(string nie) {
	        var nieRegex = new Regex("^[xyzXYZ]{1}[0-9]{7}[a-zA-Z]{1}$");
	        if (!nieRegex.IsMatch(nie)) {
                return ValidationResult.PatternMismatch;
            }

            var startLetter = char.ToUpperInvariant(nie[0]);
            var numbers = nie.Substring(1, 7);
            var letter = char.ToUpperInvariant(nie[8]);

	        var expected = CalculateChecksum(NieStartLetters.IndexOf(startLetter) + numbers);

            if (letter != expected)
            {
                return ValidationResult.ChecksumMismatch;
            }

            return ValidationResult.Valid;
        }

        
        #endregion
        #region Helpers
        private static char CalculateChecksum(string digits)
        {
            const string checkSumLetters = "TRWAGMYFPDXBNJZSQVHLCKE";
            var index = (int)(long.Parse(digits) % 23);
            return checkSumLetters[index];
        }

        private static string GetRandomDigits(int count)
        {
            var sb = new StringBuilder(count);
            var rd = new Random();
            for (var i = 0; i < count; i++)
            {
                sb.Append(rd.Next(10));
            }
            return sb.ToString();
        }

        #endregion
    }

    public enum ValidationResult
    {
        PatternMismatch,
        ChecksumMismatch,
        Valid
    }
}
