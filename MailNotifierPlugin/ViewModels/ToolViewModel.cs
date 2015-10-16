using Livet;
using MailNotifierPlugin.Models;
using MailNotifierPlugin.Models.Settings;
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

        #region SendMailServerHost 変更通知プロパティ
        private string _SendMailServerHost;
        public string SendMailServerHost
        {
            get { return this._SendMailServerHost; }
            set
            {
                if (this._SendMailServerHost != value)
                {
                    this._SendMailServerHost = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region SendMailServerPort 変更通知プロパティ
        private int _SendMailServerPort;
        public int SendMailServerPort
        {
            get { return this._SendMailServerPort; }
            set
            {
                if (this._SendMailServerPort != value)
                {
                    this._SendMailServerPort = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region SendMailServerUserName 変更通知プロパティ
        private string _SendMailServerUserName;
        public string SendMailServerUserName
        {
            get { return this._SendMailServerUserName; }
            set
            {
                if (this._SendMailServerUserName != value)
                {
                    this._SendMailServerUserName = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region SendMailServerPassword 変更通知プロパティ
        private string _SendMailServerPassword;
        public string SendMailServerPassword
        {
            get { return this._SendMailServerPassword; }
            set
            {
                if (this._SendMailServerPassword != value)
                {
                    this._SendMailServerPassword = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region SendMailServerEnableSsl 変更通知プロパティ
        private bool _SendMailServerEnableSsl;
        public bool SendMailServerIsEnableSsl
        {
            get { return this._SendMailServerEnableSsl; }
            set
            {
                if (this._SendMailServerEnableSsl != value)
                {
                    this._SendMailServerEnableSsl = value;
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
            MailNotifierSettings.SendServer.Host.Subscribe(x => this.SendMailServerHost = x).AddTo(this);
            MailNotifierSettings.SendServer.Port.Subscribe(x => this.SendMailServerPort = x).AddTo(this);
            MailNotifierSettings.SendServer.UserName.Subscribe(x => this.SendMailServerUserName = x).AddTo(this);
            MailNotifierSettings.SendServer.Password.Subscribe(x => this.SendMailServerPassword = x).AddTo(this);
            MailNotifierSettings.SendServer.IsEnableSsl.Subscribe(x => this.SendMailServerIsEnableSsl = x).AddTo(this);
        }

        /// <summary>
        /// テスト送信
        /// </summary>
        public void SendTest()
        {
            MailSender ms = new MailSender()
            {
                Host = this.SendMailServerHost,
                Port = this.SendMailServerPort,
                UserName = this.SendMailServerUserName,
                Password = this.SendMailServerPassword,
                EnableSsl = this.SendMailServerIsEnableSsl
            };
            ms.Send(
                new MailAddress(this.SenderMailAddress, this.SenderDisplayName),
                new MailAddress(this.NotifierMailAddress, this.NotifierDisplayName),
                "テストメッセージ",
                "このメールはメール通知設定のテスト中に、自動送信されたものです。"
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
            MailNotifierSettings.SendServer.Host.Value = this.SendMailServerHost;
            MailNotifierSettings.SendServer.Port.Value = this.SendMailServerPort;
            MailNotifierSettings.SendServer.UserName.Value = this.SendMailServerUserName;
            MailNotifierSettings.SendServer.Password.Value = this.SendMailServerPassword;
            MailNotifierSettings.SendServer.IsEnableSsl.Value = this.SendMailServerIsEnableSsl;
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
            this.SendMailServerHost = MailNotifierSettings.SendServer.Host;
            this.SendMailServerPort = MailNotifierSettings.SendServer.Port;
            this.SendMailServerUserName = MailNotifierSettings.SendServer.UserName;
            this.SendMailServerPassword = MailNotifierSettings.SendServer.Password;
            this.SendMailServerIsEnableSsl = MailNotifierSettings.SendServer.IsEnableSsl;
        }
    }
}
