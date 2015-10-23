using MailNotifierPlugin.Models;
using MailNotifierPlugin.Models.Settings;
using MailNotifierPlugin.Properties;
using MetroTrilithon.Lifetime;
using MetroTrilithon.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security;

namespace MailNotifierPlugin.ViewModels
{
    /// <summary>
    /// ツールビューモデル
    /// </summary>
    public class ToolViewModel : ValidationViewModel
    {
        #region IsEnabled 変更通知プロパティ
        private bool _IsEnabled;
        public bool IsEnabled
        {
            get { return this._IsEnabled; }
            set
            {
                if (this._IsEnabled != value)
                {
                    this._IsEnabled = value;
                    this.RaisePropertyChanged();

                    // メール通知が無効の場合、検証エラーをクリアする
                    if (!this.IsEnabled)
                    {
                        this.ClearErrors();
                    }
                }
            }
        }
        #endregion

        #region NotifierMailAddress 変更通知プロパティ
        private string _NotifierMailAddress;
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Validation_Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Validation_EmailAddress")]
        public string NotifierMailAddress
        {
            get { return this._NotifierMailAddress; }
            set
            {
                if (this._NotifierMailAddress != value)
                {
                    this._NotifierMailAddress = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region NotifierDisplayName 変更通知プロパティ
        private string _NotifierDisplayName;
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Validation_Required")]
        public string NotifierDisplayName
        {
            get { return this._NotifierDisplayName; }
            set
            {
                if (this._NotifierDisplayName != value)
                {
                    this._NotifierDisplayName = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region SenderMailAddress 変更通知プロパティ
        private string _SenderMailAddress;
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Validation_Required")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Validation_EmailAddress")]
        public string SenderMailAddress
        {
            get { return this._SenderMailAddress; }
            set
            {
                if (this._SenderMailAddress != value)
                {
                    this._SenderMailAddress = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region SenderDisplayName 変更通知プロパティ
        private string _SenderDisplayName;
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Validation_Required")]
        public string SenderDisplayName
        {
            get { return this._SenderDisplayName; }
            set
            {
                if (this._SenderDisplayName != value)
                {
                    this._SenderDisplayName = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region SendServerHost 変更通知プロパティ
        private string _SendServerHost;
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Validation_Required")]
        public string SendServerHost
        {
            get { return this._SendServerHost; }
            set
            {
                if (this._SendServerHost != value)
                {
                    this._SendServerHost = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region SendServerPort 変更通知プロパティ
        private int _SendServerPort;
        [Range(0, 65535, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Validation_Range")]
        public int SendServerPort
        {
            get { return this._SendServerPort; }
            set
            {
                if (this._SendServerPort != value)
                {
                    this._SendServerPort = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region AuthUserName 変更通知プロパティ
        private string _AuthUserName;
        public string AuthUserName
        {
            get { return this._AuthUserName; }
            set
            {
                if (this._AuthUserName != value)
                {
                    this._AuthUserName = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region AuthPassword 変更通知プロパティ
        private String _AuthPassword;
        public String AuthPassword
        {
            get { return this._AuthPassword; }
            set
            {
                if (this._AuthPassword != value)
                {
                    this._AuthPassword = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region AuthIsEnableSsl 変更通知プロパティ
        private bool _AuthIsEnableSsl;
        public bool AuthIsEnableSsl
        {
            get { return this._AuthIsEnableSsl; }
            set
            {
                if (this._AuthIsEnableSsl != value)
                {
                    this._AuthIsEnableSsl = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ToolViewModel()
        {
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            this.Cancel();
        }

        /// <summary>
        /// テスト送信
        /// </summary>
        public void SendTest()
        {
            this.Validate();
            if (!this.HasErrors)
            {
                MailSender ms = new MailSender()
                {
                    Host = this.SendServerHost,
                    Port = this.SendServerPort,
                    UserName = this.AuthUserName,
                    Password = this.AuthPassword,
                    EnableSsl = this.AuthIsEnableSsl
                };
                ms.Send(
                    this.SenderMailAddress,
                    this.SenderDisplayName,
                    this.NotifierMailAddress,
                    this.NotifierDisplayName,
                    Resources.SendTest_Subject,
                    Resources.SendTest_Body
                );
            }
        }

        /// <summary>
        /// 適用
        /// </summary>
        public void Apply()
        {
            if (this.IsEnabled)
            {
                this.Validate();
            }
            if (!this.HasErrors)
            {
                MailNotifierSettings.IsEnabled.Value = this.IsEnabled;
                MailNotifierSettings.Notifier.MailAddress.Value = this.NotifierMailAddress;
                MailNotifierSettings.Notifier.DisplayName.Value = this.NotifierDisplayName;
                MailNotifierSettings.Sender.MailAddress.Value = this.SenderMailAddress;
                MailNotifierSettings.Sender.DisplayName.Value = this.SenderDisplayName;
                MailNotifierSettings.SendServer.Host.Value = this.SendServerHost;
                MailNotifierSettings.SendServer.Port.Value = this.SendServerPort;
                MailNotifierSettings.Auth.UserName.Value = this.AuthUserName;
                MailNotifierSettings.Auth.SourcePassword = this.AuthPassword;
                MailNotifierSettings.Auth.IsEnableSsl.Value = this.AuthIsEnableSsl;
            }
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        public void Cancel()
        {
            this.IsEnabled = MailNotifierSettings.IsEnabled;
            this.NotifierMailAddress = MailNotifierSettings.Notifier.MailAddress;
            this.NotifierDisplayName = MailNotifierSettings.Notifier.DisplayName;
            this.SenderMailAddress = MailNotifierSettings.Sender.MailAddress;
            this.SenderDisplayName = MailNotifierSettings.Sender.DisplayName;
            this.SendServerHost = MailNotifierSettings.SendServer.Host;
            this.SendServerPort = MailNotifierSettings.SendServer.Port;
            this.AuthUserName = MailNotifierSettings.Auth.UserName;
            this.AuthPassword = MailNotifierSettings.Auth.SourcePassword;
            this.AuthIsEnableSsl = MailNotifierSettings.Auth.IsEnableSsl;
        }
    }
}
