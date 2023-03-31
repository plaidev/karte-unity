namespace Io.Karte
{

    /// <summary>
    /// KarteAttributesクラスは、 KARTEが自動で処理するプッシュ通知のデータを取り扱うクラスです。
    /// </summary>
    public class KarteAttributes
    {
        internal KarteAttributes(string title, string body, bool sound, string channel, string link, string type, string fileUrl)
        {
            this.title = title;
            this.body = body;
            this.sound = sound;
            this.channel = channel;
            this.link = link;
            this.type = type;
            this.fileUrl = fileUrl;
        }
        private string title;
        private string body;
        private bool sound;
        private string channel;
        private string link;
        private string type;
        private string fileUrl;

        /// <summary>
        /// プッシュ通知の本文を返します。これはプッシュ通知アクションに指定した静的変数のbodyに対応しています。
        /// </summary>
        /// <returns>
        /// プッシュ通知の本文を返します。
        /// </returns>
        public string Body
        {
            get { return body; }
        }

        /// <summary>
        /// プッシュ通知の画像URLを返します。これはプッシュ通知アクションに指定した静的変数のattachment_urlに対応しています。
        /// </summary>
        /// <returns>
        /// プッシュ通知の画像URLを返します。
        /// </returns>
        public string BigImageUrl
        {
            get { return fileUrl; }
        }

        /// <summary>
        /// プッシュ通知のクリックリンクを返します。これはプッシュ通知アクションに指定した静的変数のurlに対応しています。
        /// </summary>
        /// <returns>
        /// プッシュ通知のクリックリンクを返します。
        /// </returns>
        public string Link
        {
            get { return link; }
        }

        /// <summary>
        /// プッシュ通知のチャンネルIDを返します。これはプッシュ通知アクションに指定した静的変数のandroid_channel_idに対応しています。
        /// </summary>
        /// <returns>
        /// プッシュ通知のチャンネルIDを返します。
        /// </returns>
        public string Channel
        {
            get { return channel; }
        }

        /// <summary>
        /// プッシュ通知の通知音可否を返します。これはプッシュ通知アクションに指定した静的変数のsound_for_androidに対応しています。
        /// </summary>
        /// <returns>
        /// プッシュ通知の通知音可否を返します。
        /// </returns>
        public bool Sound
        {
            get { return sound; }
        }

        /// <summary>
        /// プッシュ通知のタイトルを返します。これはプッシュ通知アクションに指定した静的変数のtitleに対応しています。
        /// </summary>
        /// <value>
        /// プッシュ通知のタイトルを返します。
        /// </value>
        public string Title
        {
            get { return title; }
        }
    }
}