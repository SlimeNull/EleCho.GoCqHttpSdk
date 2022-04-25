﻿using NullLib.GoCqHttpSdk.Util;
using System;

namespace NullLib.GoCqHttpSdk.Message
{
    public class CqShareMsg : CqMsg
    {
        public override string Type => Consts.MsgType.Share;

        internal CqShareMsg() { }
        public CqShareMsg(string url, string title)
        {
            Url = url;
            Title = title;
        }

        /// <summary>
        /// 说明: URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 说明: 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 说明: 发送时可选, 内容描述
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 说明: 发送时可选, 图片 URL
        /// </summary>
        public string? Image { get; set; }

        internal override CqMsgModel GetModel() => new CqMsgModel(Type, new CqShareDataModel(Url, Title, Content, Image));
        internal override void ReadDataModel(object model)
        {
            var m = model as CqShareDataModel;
            if (m == null)
                throw new ArgumentException();

            Url = m.url;
            Title = m.title;
            Content = m.content;
            Image = m.image;
        }
    }

    public class CqShareDataModel
    {
        internal CqShareDataModel() {         }
        public CqShareDataModel(string url, string title, string? content, string? image)
        {
            this.url = url;
            this.title = title;
            this.content = content;
            this.image = image;
        }

        public string url { get; set; }
        public string title { get; set; }
        public string? content { get; set; }
        public string? image { get; set; }
    }
}