using MetroTrilithon.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MailNotifierPlugin.Models.Settings
{
    /// <summary>
    /// 設定ホスト
    /// </summary>
    public abstract class SettingsHost
    {
        private static readonly Dictionary<Type, SettingsHost> instances = new Dictionary<Type, SettingsHost>();
        private readonly Dictionary<string, object> cachedProperties = new Dictionary<string, object>();

        protected virtual string CategoryName => this.GetType().Name;

        protected SettingsHost()
        {
            instances[this.GetType()] = this;
        }

        /// <summary>
        /// 現在のインスタンスにキャッシュされている <see cref="SerializableProperty{T}"/>
        /// を取得します。 キャッシュがない場合は <see cref="create"/> に従って生成します。
        /// </summary>
        /// <returns></returns>
        protected SerializableProperty<T> Cache<T>(Func<string, SerializableProperty<T>> create, [CallerMemberName] string propertyName = "")
        {
            var key = this.CategoryName + "." + propertyName;

            object obj;
            if (this.cachedProperties.TryGetValue(key, out obj) && obj is SerializableProperty<T>) return (SerializableProperty<T>)obj;

            var property = create(key);
            this.cachedProperties[key] = property;

            return property;
        }

        #region Load / Save

        /// <summary>
        /// ロード
        /// </summary>
        public static void Load()
        {
            try
            {
                Providers.Local.Load();
            }
            catch (Exception)
            {
                File.Delete(Providers.LocalFilePath);
                Providers.Local.Load();
            }
        }

        /// <summary>
        /// セーブ
        /// </summary>
        public static void Save()
        {
            #region const message

            const string message = @"設定ファイル ({0}) の保存に失敗しました。

エラーの詳細: {1}";

            #endregion

            try
            {
                Providers.Local.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(message, Providers.LocalFilePath, ex.Message), "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        #endregion

        /// <summary>
        /// <typeparamref name="T"/> 型の設定オブジェクトの唯一のインスタンスを取得します。
        /// </summary>
        public static T Instance<T>() where T : SettingsHost, new()
        {
            SettingsHost host;
            return instances.TryGetValue(typeof(T), out host) ? (T)host : new T();
        }
    }
}
