using Grabacr07.KanColleViewer.Composition;
using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models;
using MailNotifierPlugin.Models.Settings;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace MailNotifierPlugin.Models
{
    /// <summary>
    /// メール送信モデル
    /// </summary>
    public class MailSender
    {
        #region プロパティ
        /// <summary>
        /// 送信メールサーバのホスト
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 送信メールサーバのポート
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// ユーザ名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// SSL使用フラグ
        /// </summary>
        public bool EnableSsl { get; set; }
        #endregion

        /// <summary>
        /// メール送信
        /// </summary>
        /// <param name="from">差出人メールアドレス</param>
        /// <param name="to">受信者メールアドレス</param>
        /// <param name="subject">件名</param>
        /// <param name="body">本文</param>
        /// <returns>true:成功,false:失敗</returns>
        public bool Send(MailAddress from, MailAddress to, string subject, string body)
        {
            try
            {
                using (MailMessage msg = new MailMessage())
                {
                    // メールメッセージ作成
                    msg.From = from;
                    msg.To.Add(to);
                    msg.Subject = subject;
                    msg.Body = body;

                    // SMTPサーバー設定
                    using (SmtpClient sc = new SmtpClient())
                    {
                        sc.Host = this.Host;
                        sc.Port = this.Port;
                        sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                        sc.EnableSsl = this.EnableSsl;
                        if (!String.IsNullOrEmpty(this.UserName))
                        {
                            sc.Credentials = new NetworkCredential(this.UserName, this.Password);
                        }

                        // メッセージを送信する
                        sc.Send(msg);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// メール本文取得
        /// </summary>
        /// <remarks>
        /// 現在の遠征状況を追記したメール本文を取得する。
        /// </remarks>
        /// <param name="body">メール本文</param>
        /// <returns>メール本文</returns>
        public static String GetMailBody(String body)
        {
            // 本文に遠征状況を追加
            body += Environment.NewLine;
            body += "------------------------------------" + Environment.NewLine;

            // 艦これが起動中の場合
            if (KanColleClient.Current != null && KanColleClient.Current.IsStarted)
            {
                foreach (Fleet fleet in KanColleClient.Current.Homeport.Organization.Fleets.Values)
                {
                    // 第一艦隊は対象外
                    if (fleet.Id == 1)
                    {
                        continue;
                    }

                    // 遠征情報の取得
                    Expedition expedition = fleet.Expedition;

                    // 遠征中の場合、残り時間を取得する
                    String remaining = "";
                    if (expedition.IsInExecution)
                    {
                        remaining = expedition.Remaining.Value.ToString(@"hh\:mm\:ss");
                    }

                    // 本文に遠征状況を追加
                    body += String.Format("/{0} - {1}", fleet.Id, remaining) + Environment.NewLine;
                }
            }
            return body;
        }
    }
}
