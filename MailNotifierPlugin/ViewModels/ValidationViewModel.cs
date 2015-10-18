using Livet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MailNotifierPlugin.ViewModels
{
    /// <summary>
    /// ViewModelの基底クラス
    /// *バリデーション機能付加版
    /// </summary>
    public class ValidationViewModel : ViewModel, INotifyDataErrorInfo
    {
        /// <summary>
        /// 各プロパティのエラー辞書
        /// </summary>
        private readonly Dictionary<string, List<string>> _currentErrors = new Dictionary<string, List<string>>();

        #region INotifyDataErrorInfoの実装
        /// <summary>
        /// 検証エラーイベント
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// 検証エラー変更イベントを発生させる。
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 指定したプロパティまたはエンティティ全体の検証エラーを取得します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>検証エラー</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (this.HasErrors)
            {
                if (string.IsNullOrEmpty(propertyName))
                {
                    var allErrors = new List<string>();
                    foreach (var errors in _currentErrors.Values)
                    {
                        allErrors.AddRange(errors);
                    }
                    return allErrors;
                }
                if (_currentErrors.ContainsKey(propertyName))
                {
                    return _currentErrors[propertyName];
                }
            }
            return null;
        }

        /// <summary>
        /// 検証エラーがあるかどうか取得します。
        /// </summary>
        public bool HasErrors => _currentErrors.Count > 0;
        #endregion

        /// <summary>
        /// プロパティの入力値を検証する
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void ValidateProperty([CallerMemberName]string propertyName = null)
        {
            object value = this.GetType().GetProperty(propertyName).GetValue(this);
            var context = new ValidationContext(this) { MemberName = propertyName };
            var validationErrors = new List<ValidationResult>();
            if (!Validator.TryValidateProperty(value, context, validationErrors))
            {
                var errors = validationErrors.Select(error => error.ErrorMessage);
                SetErrors(propertyName, errors);
            }
            else
            {
                ClearErrors(propertyName);
            }
        }

        /// <summary>
        /// 全てのプロパティの入力値を検証する
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void Validate()
        {
            {
                ClearErrors();
                object value = this.GetType().GetProperty("NotifierMailAddress").GetValue(this);
                var context = new ValidationContext(this) { MemberName = "NotifierMailAddress" };
                var validationErrors = new List<ValidationResult>();
                if (!Validator.TryValidateProperty(value, context, validationErrors))
                {
                }
            }

            {
                ClearErrors();
                var context = new ValidationContext(this);
                var validationErrors = new List<ValidationResult>();
                if (!Validator.TryValidateObject(this, context, validationErrors, true))
                {
                    var errors = validationErrors.Where(_ => _.MemberNames.Any()).GroupBy(_ => _.MemberNames.First());
                    foreach (var error in errors)
                    {
                        SetErrors(error.Key, error.Select(_ => _.ErrorMessage));
                    }
                }
            }
        }

        /// <summary>
        /// 引数で指定されたプロパティに、errorsで指定されたエラーをすべて登録します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        /// <param name="errors">エラーリスト</param>
        protected void SetErrors(string propertyName, IEnumerable<string> errors)
        {
            var hasCurrentError = _currentErrors.ContainsKey(propertyName);
            var hasNewError = errors != null && errors.Count() > 0;

            if (!hasCurrentError && !hasNewError)
            {
                return;
            }

            if (hasNewError)
            {
                _currentErrors[propertyName] = new List<string>(errors);
            }
            else
            {
                _currentErrors.Remove(propertyName);
            }
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        /// 引数で指定されたプロパティのまたはエンティティ全体のエラーをすべて解除します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void ClearErrors(string propertyName = "")
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (_currentErrors.ContainsKey(propertyName))
                {
                    _currentErrors.Remove(propertyName);
                    OnErrorsChanged(propertyName);
                }
            }
            else
            {
                while (_currentErrors.Count > 0)
                {
                    string key = _currentErrors.First().Key;
                    _currentErrors.Remove(key);
                    OnErrorsChanged(key);
                }
            }
        }
    }
}
