using Grabacr07.KanColleViewer.Composition;
using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models;
using MailNotifierPlugin.Models;
using MailNotifierPlugin.Models.Settings;
using MailNotifierPlugin.ViewModels;
using MailNotifierPlugin.Views;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using System.Windows;

namespace MailNotifierPlugin
{
    [Export(typeof(IPlugin))]
    [Export(typeof(ITool))]
    [Export(typeof(INotifier))]
    [ExportMetadata("Guid", "C2F3BD7F-1A3E-447F-A79C-D14760A52BA4")]
    [ExportMetadata("Title", "MailNotifier")]
    [ExportMetadata("Description", "メール通知機能を提供します。")]
    [ExportMetadata("Version", "1.0.0")]
    [ExportMetadata("Author", "@Thirdplay")]
    public class Plugin : IPlugin, ITool, INotifier, IDisposable
    {
        /// <summary>
        /// ビューモデル
        /// </summary>
        private ToolViewModel vm;

        /// <summary>
        /// メール送信モデル
        /// </summary>
        private MailSender mailSender;

        /// <summary>
        /// 設定ビュー
        /// </summary>
        public object View => new ToolView { DataContext = this.vm };

        /// <summary>
        /// タブ名
        /// </summary>
        public string Name => "MailNotifier";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Plugin()
        {
            this.vm = new ToolViewModel();
            this.mailSender = new MailSender();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            SettingsHost.Load();
            this.vm.Initialize();
        }

        /// <summary>
        /// リソース破棄
        /// </summary>
        public void Dispose()
        {
            SettingsHost.Save();
        }

        /// <summary>
        /// 通知イベント
        /// </summary>
        /// <param name="notification">ユーザへの通知を示すメンバー</param>
        public void Notify(INotification notification)
        {
            this.mailSender.Send(notification);
        }
    }
}
