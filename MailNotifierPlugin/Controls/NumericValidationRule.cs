using MailNotifierPlugin.Properties;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace MailNotifierPlugin.Controls
{
    /// <summary>
    /// 数字の入力値検証
    /// </summary>
    public class NumericValidationRule : ValidationRule
    {
        /// <summary>
        /// 入力に空文字を許容するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool AllowsEmpty { get; set; }

        /// <summary>
        /// 入力値が半角数字かどうか検証する。
        /// </summary>
        /// <param name="value">入力値</param>
        /// <param name="cultureInfo">カルチャ</param>
        /// <returns>検証結果</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var numberAsString = value as string;
            if (string.IsNullOrEmpty(numberAsString))
            {
                return this.AllowsEmpty
                    ? new ValidationResult(true, null)
                    : new ValidationResult(false, Resources.Validation_Required);
            }
            try
            {
                int number = int.Parse(numberAsString);
            }
            catch (Exception)
            {
                return new ValidationResult(false, Resources.Validation_Numeric);
            }
            return ValidationResult.ValidResult;
        }
    }
}
