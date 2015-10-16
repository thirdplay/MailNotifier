using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailNotifierPlugin.Models.Interface
{
    /// <summary>
    /// メール送信サーバ設定
    /// </summary>
    public interface ISendMailServerSettings
    {
        /// <summary>
        /// ホスト名
        /// </summary>
        string Host { get; }

        /// <summary>
        /// ポート
        /// </summary>
        int Port { get; }

        /// <summary>
        /// ユーザ名
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// パスワード
        /// </summary>
        string Password { get; }

        /// <summary>
        /// SSL使用フラグ
        /// </summary>
        bool EnableSsl { get; }
    }
}
