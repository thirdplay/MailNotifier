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
        public bool SendMailServerEnableSsl
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
            MailNotifierSettings.Notifier.MailAddress.Subscribe(x => this.NotifierMailAddress = x).AddTo(this);
            MailNotifierSettings.Notifier.DisplayName.Subscribe(x => this.NotifierDisplayName = x).AddTo(this);
            MailNotifierSettings.Sender.MailAddress.Subscribe(x => this.SenderMailAddress = x).AddTo(this);
            MailNotifierSettings.Sender.DisplayName.Subscribe(x => this.SenderDisplayName = x).AddTo(this);
            MailNotifierSettings.SendMailServer.Host.Subscribe(x => this.SendMailServerHost = x).AddTo(this);
            MailNotifierSettings.SendMailServer.Port.Subscribe(x => this.SendMailServerPort = x).AddTo(this);
            MailNotifierSettings.SendMailServer.UserName.Subscribe(x => this.SendMailServerUserName = x).AddTo(this);
            MailNotifierSettings.SendMailServer.Password.Subscribe(x => this.SendMailServerPassword = x).AddTo(this);
            MailNotifierSettings.SendMailServer.EnableSsl.Subscribe(x => this.SendMailServerEnableSsl = x).AddTo(this);
        }

        /// <summary>
        /// テスト送信
        /// </summary>
        public void SendTest()
        {
            MailSender ms = new MailSender();
            ms.Send(
                new MailAddress(this.SenderMailAddress, this.SenderDisplayName),
                new MailAddress(this.NotifierMailAddress, this.NotifierDisplayName),
                "テストメッセージ",
                "このメールはメール通知設定のテスト中に、自動送信されたものです。",
                this.SendMailServerHost,
                this.SendMailServerPort,
                this.SendMailServerUserName,
                this.SendMailServerPassword,
                this.SendMailServerEnableSsl
            );
        }

        /// <summary>
        /// 適用
        /// </summary>
        public void Apply()
        {
            MailNotifierSettings.Notifier.MailAddress.Value = this.NotifierMailAddress;
            MailNotifierSettings.Notifier.DisplayName.Value = this.NotifierDisplayName;
            MailNotifierSettings.Sender.MailAddress.Value = this.SenderMailAddress;
            MailNotifierSettings.Sender.DisplayName.Value = this.SenderDisplayName;
            MailNotifierSettings.SendMailServer.Host.Value = this.SendMailServerHost;
            MailNotifierSettings.SendMailServer.Port.Value = this.SendMailServerPort;
            MailNotifierSettings.SendMailServer.UserName.Value = this.SendMailServerUserName;
            MailNotifierSettings.SendMailServer.Password.Value = this.SendMailServerPassword;
            MailNotifierSettings.SendMailServer.EnableSsl.Value = this.SendMailServerEnableSsl;
        }

        /// <summary>
        /// キャンセル
        /// </summary>
        public void Cancel()
        {
            this.NotifierMailAddress = MailNotifierSettings.Notifier.MailAddress;
            this.NotifierDisplayName = MailNotifierSettings.Notifier.DisplayName;
            this.SenderMailAddress = MailNotifierSettings.Sender.MailAddress;
            this.SenderDisplayName = MailNotifierSettings.Sender.DisplayName;
            this.SendMailServerHost = MailNotifierSettings.SendMailServer.Host;
            this.SendMailServerPort = MailNotifierSettings.SendMailServer.Port;
            this.SendMailServerUserName = MailNotifierSettings.SendMailServer.UserName;
            this.SendMailServerPassword = MailNotifierSettings.SendMailServer.Password;
            this.SendMailServerEnableSsl = MailNotifierSettings.SendMailServer.EnableSsl;
        }
    }
}
