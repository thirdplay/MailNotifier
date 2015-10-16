using MailNotifierPlugin.Models.Interface;
using MetroTrilithon.Serialization;
using System.Runtime.CompilerServices;

namespace MailNotifierPlugin.Models.Settings
{
    /// <summary>
    /// メール通知設定
    /// </summary>
    public static class MailNotifierSettings
    {
        /// <summary>
        /// 通知先
        /// </summary>
        public class Notifier : INotifierSettings
        {
            public static SerializablePropertyBase<string> MailAddress { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, null);
            public static SerializablePropertyBase<string> DisplayName { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, "提督");

            #region INotifierSettings members
            /// <summary>
            /// メールアドレス
            /// </summary>
            string INotifierSettings.MailAddress => MailAddress.Value;

            /// <summary>
            /// 表示名
            /// </summary>
            string INotifierSettings.DisplayName => DisplayName.Value;
            #endregion

            private static string GetKey([CallerMemberName] string propertyName = "")
            {
                return nameof(MailNotifierSettings) + "." + nameof(Notifier) + "." + propertyName;
            }
        }

        /// <summary>
        /// 送信元
        /// </summary>
        public class Sender : ISenderSettings
        {
            public static SerializablePropertyBase<string> MailAddress { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, null);
            public static SerializablePropertyBase<string> DisplayName { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, "艦これ");

            #region ISenderSettings members
            /// <summary>
            /// メールアドレス
            /// </summary>
            string ISenderSettings.MailAddress => MailAddress.Value;

            /// <summary>
            /// 表示名
            /// </summary>
            string ISenderSettings.DisplayName => DisplayName.Value;
            #endregion

            private static string GetKey([CallerMemberName] string propertyName = "")
            {
                return nameof(MailNotifierSettings) + "." + nameof(Sender) + "." + propertyName;
            }
        }

        /// <summary>
        /// 送信メールサーバ
        /// </summary>
        public class SendMailServer : ISendMailServerSettings
        {
            public static SerializablePropertyBase<string> Host { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, null);
            public static SerializablePropertyBase<int> Port { get; } = new SerializableProperty<int>(GetKey(), Providers.Local, 25);
            public static SerializablePropertyBase<string> UserName { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, null);
            public static SerializablePropertyBase<string> Password { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, null);
            public static SerializablePropertyBase<bool> EnableSsl { get; } = new SerializableProperty<bool>(GetKey(), Providers.Local, false);

            #region ISendMailServerSettings members
            /// <summary>
            /// ホスト名
            /// </summary>
            string ISendMailServerSettings.Host => Host.Value;

            /// <summary>
            /// ポート
            /// </summary>
            int ISendMailServerSettings.Port => Port.Value;

            /// <summary>
            /// ユーザ名
            /// </summary>
            string ISendMailServerSettings.UserName => UserName.Value;

            /// <summary>
            /// パスワード
            /// </summary>
            string ISendMailServerSettings.Password => Password.Value;

            /// <summary>
            /// SSL使用フラグ
            /// </summary>
            bool ISendMailServerSettings.EnableSsl => EnableSsl.Value;
            #endregion

            private static string GetKey([CallerMemberName] string propertyName = "")
            {
                return nameof(MailNotifierSettings) + "." + nameof(SendMailServer) + "." + propertyName;
            }
        }
    }
}
