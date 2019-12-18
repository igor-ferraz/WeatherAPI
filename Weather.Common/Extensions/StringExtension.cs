using System.Globalization;
using System.Text;

namespace Weather.Common.Extensions
{
    public static class StringExtension
    {
        public static string RemoveDiacritics(this string text, bool toUpper)
        {
            StringBuilder textResult = new StringBuilder();
            var textArray = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in textArray)
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    textResult.Append(letter);

            return toUpper ? textResult.ToString().ToUpper() : textResult.ToString();
        }
    }
}