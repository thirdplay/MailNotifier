using System;
using MetroTrilithon.Serialization;
using System.IO;

namespace MailNotifierPlugin.Models.Settings
{
    /// <summary>
    /// 設定プロバイダー
    /// </summary>
    public static class Providers
    {
        public static string LocalFilePath { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "thirdplay", "KanColleViewer", "MailNotifierSettings.xaml");

        public static ISerializationProvider Local { get; } = new FileSettingsProvider(LocalFilePath);
    }
}
