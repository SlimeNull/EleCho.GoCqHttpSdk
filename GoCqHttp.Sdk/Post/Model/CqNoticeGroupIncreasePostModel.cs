﻿using NullLib.GoCqHttpSdk.Enumeration;

namespace NullLib.GoCqHttpSdk.Post.Model
{
    internal class CqNoticeGroupIncreasePostModel : CqNoticePostModel
    {
        public override string notice_type => "group_increase";

        /// <summary>
        /// <see cref="CqNoticeGroupIncreaseType"/>
        /// </summary>
        public string sub_type { get; set; }
        public long group_id { get; set; }
        public long operator_id { get; set; }
        public long user_id { get; set; }
    }
}