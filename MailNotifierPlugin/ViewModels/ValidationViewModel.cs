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
        /// プロパティ変更通知イベントを発生させます
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.RaisePropertyChanged(propertyName);
            this.ValidateProperty(propertyName);
        }

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

        #region 発生中のエラーを保持する処理を実装
        readonly Dictionary<string, List<string>> _currentErrors = new Dictionary<string, List<string>>();

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
        /// 引数で指定されたプロパティのエラーをすべて解除します。
        /// </summary>
        /// <param name="propertyName">プロパティ名</param>
        protected void ClearErrors(string propertyName)
        {
            if (_currentErrors.ContainsKey(propertyName))
            {
                _currentErrors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
        #endregion

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
            if (string.IsNullOrEmpty(propertyName) ||
                !_currentErrors.ContainsKey(propertyName))
            {
                return null;
            }

            return _currentErrors[propertyName];
        }

        /// <summary>
        /// 検証エラーがあるかどうか取得します。
        /// </summary>
        public bool HasErrors
        {
            get { return _currentErrors.Count > 0; }
        }
        #endregion
    }
}
