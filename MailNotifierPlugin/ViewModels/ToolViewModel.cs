using Livet;
using MailNotifierPlugin.Models;
using MailNotifierPlugin.Models.Settings;
using MailNotifierPlugin.Properties;
using MetroTrilithon.Lifetime;
using MetroTrilithon.Mvvm;
using System.Net.Mail;

namespace MailNotifierPlugin.ViewModels
{
    /// <summary>
    /// ツールビューモデル
    /// </summary>
    public class ToolViewModel : ViewModel
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
                }
            }
        }
        #endregion

        #region NotifierMailAddress 変更通知プロパティ
        private string _NotifierMailAddress;
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

        #region SendServerUserName 変更通知プロパティ
        private string _SendServerUserName;
        public string SendServerUserName
        {
            get { return this._SendServerUserName; }
            set
            {
                if (this._SendServerUserName != value)
                {
                    this._SendServerUserName = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region SendServerPassword 変更通知プロパティ
        private string _SendServerPassword;
        public string SendServerPassword
        {
            get { return this._SendServerPassword; }
            set
            {
                if (this._SendServerPassword != value)
                {
                    this._SendServerPassword = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region SendServerIsEnableSsl 変更通知プロパティ
        private bool _SendServerIsEnableSsl;
        public bool SendServerIsEnableSsl
        {
            get { return this._SendServerIsEnableSsl; }
            set
            {
                if (this._SendServerIsEnableSsl != value)
                {
                    this._SendServerIsEnableSsl = value;
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
            MailNotifierSettings.IsEnabled.Subscribe(x => this.IsEnabled = x).AddTo(this);
            MailNotifierSettings.Notifier.MailAddress.Subscribe(x => this.NotifierMailAddress = x).AddTo(this);
            MailNotifierSettings.Notifier.DisplayName.Subscribe(x => this.NotifierDisplayName = x).AddTo(this);
            MailNotifierSettings.Sender.MailAddress.Subscribe(x => this.SenderMailAddress = x).AddTo(this);
            MailNotifierSettings.Sender.DisplayName.Subscribe(x => this.SenderDisplayName = x).AddTo(this);
            MailNotifierSettings.SendServer.Host.Subscribe(x => this.SendServerHost = x).AddTo(this);
            MailNotifierSettings.SendServer.Port.Subscribe(x => this.SendServerPort = x).AddTo(this);
            MailNotifierSettings.SendServer.UserName.Subscribe(x => this.SendServerUserName = x).AddTo(this);
            MailNotifierSettings.SendServer.Password.Subscribe(x => this.SendServerPassword = x).AddTo(this);
            MailNotifierSettings.SendServer.IsEnableSsl.Subscribe(x => this.SendServerIsEnableSsl = x).AddTo(this);
        }

        /// <summary>
        /// テスト送信
        /// </summary>
        public void SendTest()
        {
            MailSender ms = new MailSender()
            {
                Host = this.SendServerHost,
                Port = this.SendServerPort,
                UserName = this.SendServerUserName,
                Password = this.SendServerPassword,
                EnableSsl = this.SendServerIsEnableSsl
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

        /// <summary>
        /// 適用
        /// </summary>
        public void Apply()
        {
            MailNotifierSettings.IsEnabled.Value = this.IsEnabled;
            MailNotifierSettings.Notifier.MailAddress.Value = this.NotifierMailAddress;
            MailNotifierSettings.Notifier.DisplayName.Value = this.NotifierDisplayName;
            MailNotifierSettings.Sender.MailAddress.Value = this.SenderMailAddress;
            MailNotifierSettings.Sender.DisplayName.Value = this.SenderDisplayName;
            MailNotifierSettings.SendServer.Host.Value = this.SendServerHost;
            MailNotifierSettings.SendServer.Port.Value = this.SendServerPort;
            MailNotifierSettings.SendServer.UserName.Value = this.SendServerUserName;
            MailNotifierSettings.SendServer.Password.Value = this.SendServerPassword;
            MailNotifierSettings.SendServer.IsEnableSsl.Value = this.SendServerIsEnableSsl;
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
            this.SendServerUserName = MailNotifierSettings.SendServer.UserName;
            this.SendServerPassword = MailNotifierSettings.SendServer.Password;
            this.SendServerIsEnableSsl = MailNotifierSettings.SendServer.IsEnableSsl;
        }
    }
}
