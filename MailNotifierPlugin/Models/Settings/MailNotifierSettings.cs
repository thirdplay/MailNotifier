using MetroTrilithon.Serialization;
using System.Runtime.CompilerServices;

namespace MailNotifierPlugin.Models.Settings
{
    /// <summary>
    /// メール通知設定
    /// </summary>
    public class MailNotifierSettings
    {
        /// <summary>
        /// 有効状態
        /// </summary>
        public static SerializablePropertyBase<bool> IsEnabled { get; } = new SerializableProperty<bool>(GetKey(), Providers.Local, false);

        private static string GetKey([CallerMemberName] string propertyName = "")
        {
            return nameof(MailNotifierSettings) + "." + propertyName;
        }

        /// <summary>
        /// 通知先
        /// </summary>
        public class Notifier
        {
            /// <summary>
            /// メールアドレス
            /// </summary>
            public static SerializablePropertyBase<string> MailAddress { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, null);

            /// <summary>
            /// 表示名
            /// </summary>
            public static SerializablePropertyBase<string> DisplayName { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, "提督");

            private static string GetKey([CallerMemberName] string propertyName = "")
            {
                return nameof(MailNotifierSettings) + "." + nameof(Notifier) + "." + propertyName;
            }
        }

        /// <summary>
        /// 送信元
        /// </summary>
        public class Sender
        {
            /// <summary>
            /// メールアドレス
            /// </summary>
            public static SerializablePropertyBase<string> MailAddress { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, null);

            /// <summary>
            /// 表示名
            /// </summary>
            public static SerializablePropertyBase<string> DisplayName { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, "艦これ");

            private static string GetKey([CallerMemberName] string propertyName = "")
            {
                return nameof(MailNotifierSettings) + "." + nameof(Sender) + "." + propertyName;
            }
        }

        /// <summary>
        /// 送信サーバ
        /// </summary>
        public class SendServer
        {
            /// <summary>
            /// ホスト名
            /// </summary>
            public static SerializablePropertyBase<string> Host { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, null);

            /// <summary>
            /// ポート
            /// </summary>
            public static SerializablePropertyBase<int> Port { get; } = new SerializableProperty<int>(GetKey(), Providers.Local, 25);

            /// <summary>
            /// ユーザ名
            /// </summary>
            public static SerializablePropertyBase<string> UserName { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, null);

            /// <summary>
            /// パスワード
            /// </summary>
            public static SerializablePropertyBase<string> Password { get; } = new SerializableProperty<string>(GetKey(), Providers.Local, null);

            /// <summary>
            /// SSL有効状態
            /// </summary>
            public static SerializablePropertyBase<bool> IsEnableSsl { get; } = new SerializableProperty<bool>(GetKey(), Providers.Local, false);

            private static string GetKey([CallerMemberName] string propertyName = "")
            {
                return nameof(MailNotifierSettings) + "." + nameof(SendServer) + "." + propertyName;
            }
        }
    }
}
