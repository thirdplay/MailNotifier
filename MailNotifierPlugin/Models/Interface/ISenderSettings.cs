using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailNotifierPlugin.Models.Interface
{
    /// <summary>
    /// 送信元設定
    /// </summary>
    public interface ISenderSettings
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
