using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailNotifierPlugin.Models.Interface
{
    /// <summary>
    /// 通知先設定
    /// </summary>
    public interface INotifierSettings
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        string MailAddress { get; }

        /// <summary>
        /// 表示名
        /// </summary>
        string DisplayName { get; }
    }
}
